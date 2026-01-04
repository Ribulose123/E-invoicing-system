using E_invocing.Domin.Enum;
using System;

namespace E_invocing.Domin.Entities
{
    public class Invoice
    {
        public int Id { get; private set; }

        private string invoiceNumber = null!;
        private string baseCurrency = null!;
        private string settlementCurrency = null!;

        private decimal baseAmount;
        private decimal taxAmount;
        private decimal totalAmount;
        private decimal exchangeRate;
        private decimal convertedTotalAmount;

        private Status status = Status.Pending;

        public int CustomerId { get; private set; }
        public int UploadBatchId { get; private set; }
        public DateTime UploadedDate { get; private set; } = DateTime.UtcNow;

        public string InvoiceNumber => invoiceNumber;
        public string BaseCurrency => baseCurrency;
        public string SettlementCurrency => settlementCurrency;
        public decimal BaseAmount => baseAmount;
        public decimal TaxAmount => taxAmount;
        public decimal TotalAmount => totalAmount;
        public decimal ExchangeRate => exchangeRate;
        public decimal ConvertedTotalAmount => convertedTotalAmount;
        public Status Status => status;

        protected Invoice() { } // EF Core

        public Invoice(
            int customerId,
            int uploadBatchId,
            string invoiceNumber,
            string baseCurrency,
            decimal baseAmount)
        {
            CustomerId = customerId;
            UploadBatchId = uploadBatchId;

            this.invoiceNumber = invoiceNumber ?? throw new DomainException("Invoice number is required.");
            this.baseCurrency = baseCurrency ?? throw new DomainException("Base currency is required.");

            if (baseAmount <= 0)
                throw new DomainException("Base amount must be greater than zero.");

            this.baseAmount = baseAmount;
        }

        public void ApplyTax(decimal taxRate)
        {
            if (taxRate < 0 || taxRate > 1)
                throw new DomainException("Tax rate must be between 0 and 1.");

            taxAmount = baseAmount * taxRate;
            totalAmount = baseAmount + taxAmount;
        }

        public void ApplyFx(decimal rate, string targetCurrency)
        {
            if (rate <= 0)
                throw new DomainException("FX rate must be greater than zero.");

            if (string.IsNullOrWhiteSpace(targetCurrency))
                throw new DomainException("Target currency is required.");

            if (totalAmount <= 0)
                throw new DomainException("Apply tax before FX.");

            settlementCurrency = targetCurrency;
            exchangeRate = rate;
            convertedTotalAmount = totalAmount * rate;
        }

        public void MarkAsProcessed() => status = Status.Success;
        public void MarkAsFailed() => status = Status.Failed;

        public class DomainException : Exception
        {
            public DomainException(string message) : base(message) { }
        }
    }
}
