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
        private  readonly ITaxService _taxService;
        private readonly IFxServices _fxService;

        public UploadBatchServices(E_invocingDbContext context, ITaxService taxService, IFxServices fxServices)
        {
            _context = context;
            _taxService = taxService;
            _fxService = fxServices;
        }

        public async Task<(string message, bool success)> UploadInvoiceAsync(UploadBatchRequestDto dto)
        {
            if (dto.Invoices == null || !dto.Invoices.Any())
                return ("No invoices uploaded", false);

            // 1️⃣ Create upload batch
            var uploadBatch = new UploadBatch(dto.UploadBy);
            await _context.UploadBatches.AddAsync(uploadBatch);
            await _context.SaveChangesAsync();

            int totalRecords = dto.Invoices.Count;
            int successfulRecords = 0;
            int failedRecords = 0;

            var uploadedInvoiceNumbers = new HashSet<string>();
            string targetCurrency = "USD";

            // 2️⃣ Parse invoices (from file if needed)
            IExcelParser parser = new ExcelParser();
            var invoices = parser.ParseInvoice(File.OpenRead("invoices.xlsx")); // Replace with dto.In

            foreach (var invoice in invoices)
            {
                try
                {
                    // Trim fields
                    var invoiceNumber = invoice.InvoiceNumber?.Trim();
                    var customerEmail = invoice.CustomerEmail?.Trim();
                    var amountText = invoice.Amount.ToString();

                    // 3️⃣ Structural validation
                    if (string.IsNullOrEmpty(invoiceNumber) ||
                        string.IsNullOrEmpty(customerEmail) ||
                        !decimal.TryParse(amountText, out decimal amount))
                    {
                        await _context.ValidationErrors.AddAsync(new ValidationError(
                            uploadBatch.Id,
                            "InvoiceNumber/CustomerEmail/Amount",
                            "Required fields missing or invalid."
                        ));
                        failedRecords++;
                        continue;
                    }

                    // 4️⃣ Check duplicates in current upload
                    if (!uploadedInvoiceNumbers.Add(invoiceNumber))
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
                        .AnyAsync(i => i.InvoiceNumber == invoiceNumber);

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

                    // tax calculation
                    decimal taxRate = await _taxService.GetTaxRateAsync(invoice.CustomerCountry);

                    //Fx calcualtion 
                    decimal fxRate = await _fxService.GetExchangeRateAsync(invoice.Currency, targetCurrency);

                    

                    // 6️⃣ Check if customer exists, otherwise create
                    var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Email == customerEmail);
                    if (customer == null)
                    {
                        customer = new Customer(customerEmail, customerEmail, invoice.CustomerCountry);
                        await _context.Customers.AddAsync(customer);
                        await _context.SaveChangesAsync();
                    }
                    
                    // 7️⃣ Save invoice
                    var newInvoice = new Invoice(
                     customer.Id,
                     uploadBatch.Id,
                     invoice.InvoiceNumber,
                     invoice.Currency,
                     amount
                     );

                    newInvoice.ApplyTax(taxRate);
                    newInvoice.ApplyFx(fxRate, targetCurrency);
                    await _context.Invoices.AddAsync(newInvoice);
                    successfulRecords++;
                }
                catch (Exception ex)
                {
                    // Catch any unexpected error for this invoice
                    await _context.ValidationErrors.AddAsync(new ValidationError(
                        uploadBatch.Id,
                        "InvoiceProcessing",
                        $"Error processing invoice {invoice.InvoiceNumber}: {ex.Message}"
                    ));
                    failedRecords++;
                    continue;
                }
            }

            // 8️⃣ Update upload batch stats
            uploadBatch.SetTotalRecords(totalRecords);
            for (int i = 0; i < successfulRecords; i++) uploadBatch.RecordSuccess();
            for (int i = 0; i < failedRecords; i++) uploadBatch.RecordFailure();
            if (successfulRecords > 0) uploadBatch.MarkAsCompleted();

            await _context.SaveChangesAsync();

            string message = $"Upload completed. Total: {totalRecords}, Success: {successfulRecords}, Failed: {failedRecords}";
            bool success = successfulRecords > 0;

            return (message, success);
        }
    }
}
