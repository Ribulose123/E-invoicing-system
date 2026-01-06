using e_invocie.IServices;
using E_invocing.Domin.DTO;
using E_invocing.Domin.Entities;
using E_invocing.Domin.InterFaces;
using E_invocing.Persistence;
using E_invoicing.Infrastructure.Logic.Excel;
using Microsoft.EntityFrameworkCore;

namespace e_invocie.Services
{
    public class UploadBatchServices : IUploadbatch
    {
        private readonly E_invocingDbContext _context;
        private readonly ITaxService _taxService;
        private readonly IFxServices _fxService;

        public UploadBatchServices(
            E_invocingDbContext context,
            ITaxService taxService,
            IFxServices fxServices)
        {
            _context = context;
            _taxService = taxService;
            _fxService = fxServices;
        }

        public async Task<(string message, bool success)> UploadInvoiceAsync(UploadRequestDto dto)
        {
            if (dto.FileStream == null)
                return ("No invoices uploaded", false);

            // 1️⃣ Create upload batch
            var uploadBatch = new UploadBatch(dto.UploadBy);
            await _context.UploadBatches.AddAsync(uploadBatch);
            await _context.SaveChangesAsync();

            // 2️⃣ Parse invoices
            IExcelParser parser = new ExcelParser();
            var invoices = parser.ParseInvoice(dto.FileStream);

            int totalRecords = invoices.Count;
            int successfulRecords = 0;
            int failedRecords = 0;

            var uploadedInvoiceNumbers = new HashSet<string>();

            string settlementCurrency = "USD";

            

            foreach (var invoice in invoices)
            {
                try
                {
                    // Normalize inputs
                    var invoiceNumber = invoice.InvoiceNumber?.Trim();
                    var customerEmail = invoice.CustomerEmail?.Trim();
                    var baseCurrency = invoice.Currency?.Trim();
                    var amountText = invoice.Amount;

                    // 3️⃣ Structural validation
                    if (string.IsNullOrWhiteSpace(invoiceNumber) ||
                        string.IsNullOrWhiteSpace(customerEmail) ||
                        string.IsNullOrWhiteSpace(baseCurrency) ||
                        !decimal.TryParse(amountText, out decimal baseAmount) ||
                        baseAmount <= 0)
                    {
                        await _context.ValidationErrors.AddAsync(new ValidationError(
                            uploadBatch.Id,
                            "Invoice",
                            "Required fields missing or invalid."
                        ));
                        failedRecords++;
                        continue;
                    }

                    // Make compiler-safe
                    string safeInvoiceNumber = invoiceNumber!;
                    string safeBaseCurrency = baseCurrency!;

                    // 4️⃣ Check duplicates in upload
                    if (!uploadedInvoiceNumbers.Add(safeInvoiceNumber))
                    {
                        await _context.ValidationErrors.AddAsync(new ValidationError(
                            uploadBatch.Id,
                            "InvoiceNumber",
                            "Duplicate invoice number in this upload batch."
                        ));
                        failedRecords++;
                        continue;
                    }

                    // 5️⃣ Check duplicates in database
                    bool existsInDb = await _context.Invoices
                        .AnyAsync(i => i.InvoiceNumber == safeInvoiceNumber);

                    if (existsInDb)
                    {
                        await _context.ValidationErrors.AddAsync(new ValidationError(
                            uploadBatch.Id,
                            "InvoiceNumber",
                            "Invoice number already exists in the system."
                        ));
                        failedRecords++;
                        continue;
                    }

                    // 6️⃣ Tax calculation
                    decimal taxRate = await _taxService
                        .GetTaxRateAsync(invoice.CustomerCountry);

                    // 7️⃣ FX calculation
                    decimal fxRate = safeBaseCurrency == settlementCurrency
                        ? 1m
                        : await _fxService.GetExchangeRateAsync(
                            safeBaseCurrency,
                            settlementCurrency
                        );

                    // 8️⃣ Customer lookup / creation
                    var customer = await _context.Customers
                        .FirstOrDefaultAsync(c => c.Email == customerEmail);

                    if (customer == null)
                    {
                        customer = new Customer(
                            customerEmail!,
                            customerEmail!,
                            invoice.CustomerCountry
                        );

                        await _context.Customers.AddAsync(customer);
                        await _context.SaveChangesAsync();
                    }

                    // 9️⃣ Create invoice
                    var newInvoice = new Invoice(
                        customer.Id,
                        uploadBatch.Id,
                        safeInvoiceNumber,
                        safeBaseCurrency,
                        baseAmount
                    );

                    newInvoice.ApplyTax(taxRate);
                    newInvoice.ApplyFx(fxRate, settlementCurrency);

                    await _context.Invoices.AddAsync(newInvoice);

                    successfulRecords++;
                }
                catch (Exception ex)
                {
                    await _context.ValidationErrors.AddAsync(new ValidationError(
                        uploadBatch.Id,
                        "InvoiceProcessing",
                        $"Error processing invoice {invoice.InvoiceNumber}: {ex.Message}"
                    ));

                    failedRecords++;
                }
            }

            // 🔟 Update upload batch summary
            uploadBatch.SetTotalRecords(totalRecords);

            for (int i = 0; i < successfulRecords; i++)
                uploadBatch.RecordSuccess();

            for (int i = 0; i < failedRecords; i++)
                uploadBatch.RecordFailure();

            if (successfulRecords > 0)
                uploadBatch.MarkAsCompleted();

            await _context.SaveChangesAsync();

            string message =
                $"Upload completed. Total: {totalRecords}, Success: {successfulRecords}, Failed: {failedRecords}";

            return (message, successfulRecords > 0);
        }
    }
}
