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
        private readonly ILogger<UploadBatchServices> _logger;

        public UploadBatchServices(
            E_invocingDbContext context,
            ITaxService taxService,
            IFxServices fxServices,
            ILogger<UploadBatchServices> logger)
        {
            _context = context;
            _taxService = taxService;
            _fxService = fxServices;
            _logger = logger;
        }

        public async Task<(string message, bool success, List<ValidationError> errors)>
    UploadInvoiceAsync(UploadRequestDto dto)
        {
            var errors = new List<ValidationError>();

            if (dto.FileStream == null)
                return ("No file uploaded", false, errors);

            var uploadBatch = new UploadBatch(dto.UploadBy);
            await _context.UploadBatches.AddAsync(uploadBatch);
            await _context.SaveChangesAsync();

            IExcelParser parser = new ExcelParser();
            var invoices = parser.ParseInvoice(dto.FileStream);

            int total = invoices.Count;
            int success = 0;
            int failed = 0;

            var batchDuplicates = new HashSet<string>();
            string settlementCurrency = "USD";

            foreach (var invoice in invoices)
            {
                try
                {
                    string? invoiceNumber = invoice.InvoiceNumber?.Trim();
                    string? email = invoice.CustomerEmail?.Trim();
                    string? currency = invoice.Currency?.Trim();
                    string? amountText = invoice.Amount;

                    if (string.IsNullOrWhiteSpace(invoiceNumber))
                    {
                        errors.Add(new ValidationError(uploadBatch.Id, "InvoiceNumber", "Invoice number is required"));
                        failed++; continue;
                    }

                    if (string.IsNullOrWhiteSpace(email))
                    {
                        errors.Add(new ValidationError(uploadBatch.Id, "CustomerEmail", "Customer email is required"));
                        failed++; continue;
                    }

                    if (string.IsNullOrWhiteSpace(currency))
                    {
                        errors.Add(new ValidationError(uploadBatch.Id, "Currency", "Currency is required"));
                        failed++; continue;
                    }

                    if (!decimal.TryParse(amountText, out decimal baseAmount))
                    {
                        errors.Add(new ValidationError(uploadBatch.Id, "Amount", "Amount must be numeric"));
                        failed++; continue;
                    }

                    if (baseAmount <= 0)
                    {
                        errors.Add(new ValidationError(uploadBatch.Id, "Amount", "Amount must be greater than zero"));
                        failed++; continue;
                    }

                    if (!batchDuplicates.Add(invoiceNumber))
                    {
                        errors.Add(new ValidationError(uploadBatch.Id, "InvoiceNumber", "Duplicate invoice in upload file"));
                        failed++; continue;
                    }

                    bool exists = await _context.Invoices
                        .AnyAsync(i => i.InvoiceNumber == invoiceNumber);

                    if (exists)
                    {
                        errors.Add(new ValidationError(uploadBatch.Id, "InvoiceNumber", "Invoice already exists in system"));
                        failed++; continue;
                    }

                    decimal taxRate = await _taxService.GetTaxRateAsync(invoice.CustomerCountry);

                    decimal fxRate = currency == settlementCurrency
                        ? 1m
                        : await _fxService.GetExchangeRateAsync(currency, settlementCurrency);

                    var customer = await _context.Customers
                        .FirstOrDefaultAsync(c => c.Email == email);

                    if (customer == null)
                    {
                        customer = new Customer(email, email, invoice.CustomerCountry);
                        await _context.Customers.AddAsync(customer);
                        await _context.SaveChangesAsync();
                    }

                    var newInvoice = new Invoice(
                        customer.Id,
                        uploadBatch.Id,
                        invoiceNumber,
                        currency,
                        baseAmount
                    );

                    newInvoice.ApplyTax(taxRate);
                    newInvoice.ApplyFx(fxRate, settlementCurrency);

                    await _context.Invoices.AddAsync(newInvoice);

                    success++;
                }
                catch (Exception ex)
                {
                    errors.Add(new ValidationError(uploadBatch.Id, "System", ex.Message));
                    failed++;
                    _logger.LogError(ex, "Invoice failed");
                }
            }

            uploadBatch.SetTotalRecords(total);
            for (int i = 0; i < success; i++) uploadBatch.RecordSuccess();
            for (int i = 0; i < failed; i++) uploadBatch.RecordFailure();

            if (success > 0) uploadBatch.MarkAsCompleted();

            await _context.ValidationErrors.AddRangeAsync(errors);
            await _context.SaveChangesAsync();

            string message =
                $"Upload completed. Total: {total}, Success: {success}, Failed: {failed}";

            return (message, success > 0, errors);
        }

    }
}
