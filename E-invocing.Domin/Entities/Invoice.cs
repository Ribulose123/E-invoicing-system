using E_invocing.Domin.Enum;

namespace E_invocing.Domin.Entities
{
    public class Invoice
    {
        public int Id { get; private set; }

        public int CustomerId { get; private set; }
        public int UploadBatchId { get; private set; }

        public string InvoiceNumber { get; private set; } = string.Empty;
        public string BaseCurrency { get; private set; } = string.Empty;

        public decimal BaseAmount { get; private set; }
        public decimal TaxAmount { get; private set; }
        public decimal FxRate { get; private set; }

        public string SettlementCurrency { get; private set; } = "USD";
        public decimal SettlementAmount { get; private set; }

        public InvoiceStatus Status { get; private set; }

        public DateTime CreatedAt { get; private set; }

        protected Invoice() { }

        public Invoice(
            int customerId,
            int uploadBatchId,
            string invoiceNumber,
            string baseCurrency,
            decimal baseAmount)
        {
            CustomerId = customerId;
            UploadBatchId = uploadBatchId;
            InvoiceNumber = invoiceNumber;
            BaseCurrency = baseCurrency;
            BaseAmount = baseAmount;

            Status = InvoiceStatus.Draft;
        }

        public void ApplyTax(decimal taxRate)
        {
            TaxAmount = BaseAmount * taxRate;
            Status = InvoiceStatus.Validated;
        }

        public void ApplyFx(decimal fxRate, string settlementCurrency)
        {
            FxRate = fxRate;
            SettlementCurrency = settlementCurrency;
            SettlementAmount = (BaseAmount + TaxAmount) * fxRate;
        }

        public void Approve()
        {
            if (Status != InvoiceStatus.Validated)
                throw new InvalidOperationException("Only validated invoices can be approved.");

            Status = InvoiceStatus.Approved;
        }

        public void Reject()
        {
            if (Status != InvoiceStatus.Validated)
                throw new InvalidOperationException("Only validated invoices can be rejected.");

            Status = InvoiceStatus.Rejected;
        }

        public void MarkSent()
        {
            if (Status != InvoiceStatus.Approved)
                throw new InvalidOperationException("Only approved invoices can be sent.");

            Status = InvoiceStatus.Sent;
        }

        public void MarkPaid()
        {
            if (Status != InvoiceStatus.Sent)
                throw new InvalidOperationException("Only sent invoices can be marked as paid.");

            Status = InvoiceStatus.Paid;
        }
    }
}
