using E_invocing.Domin.Enum;
using E_invocing.Domin.InterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_invocing.Domin.Entities
{
    public class Invoice
    {
        public int Id {get; private set;}
        private string? invoiceNumber;
        private string? baseCurrency;
        private decimal baseAmount;

        private Status status { get; set; } = Status.Pending;
        public int CustomerId { get; private set; }
        public int UploadBatchId { get;private set; }
        public DateTime UplodedDate { get; private set; } = DateTime.UtcNow;

        //tax and fx

        private decimal taxAmount;
        private decimal totalAmount;
        private decimal exchangeRate;
        private decimal convertedTotalAmount;
        private string? settlementCurreny;

        

        public string ? InvoiceNumber => invoiceNumber;
        public string ? BaseCurrency => baseCurrency;
        public decimal BaseAmount => baseAmount;
        public Status Status => status;
        public decimal TaxAmount => taxAmount;
        public decimal TotalAmount => totalAmount;
        public decimal ExchangeRate => exchangeRate;
        public decimal ConvertedTotalAmount => convertedTotalAmount;
        public string ? SettlementCurreny => settlementCurreny;

        protected Invoice() { }
        public Invoice(int customerId, int uploadBatchId, string? invoiceNumber, string? baseCurrency, decimal baseAmount)
        {
            CustomerId = customerId;
            UploadBatchId = uploadBatchId;
            this.invoiceNumber = invoiceNumber;
            this.baseCurrency = baseCurrency;
            this.baseAmount = baseAmount;
            UplodedDate = DateTime.UtcNow;
        }
        public void MarkAsProcessed() => status = Status.Success;
        public void MarkAsFailed() => status = Status.Failed;


        public void ApplyTax(decimal taxRate)
        {
            if(status != Status.Pending) 
                throw new DomainExpectation("Only pending invoices can have tax applied.");

            if (taxRate < 0 || taxRate > 1)
                throw new DomainExpectation("Tax rate must be between 0 and 1.");

            if(baseAmount <=0)
                throw new DomainExpectation("Base amount must be greater than zero to apply tax.");

            taxAmount = taxRate * baseAmount;
            totalAmount = taxAmount + baseAmount;
        }

        public void ApplyFx(decimal rate, string targetCurrency)
        {
            if(rate <= 0)
                throw new DomainExpectation("Exchange rate must be greater than zero.");

            if(string.IsNullOrWhiteSpace(targetCurrency))   
                throw new DomainExpectation("Target currency must be provided.");

            if(totalAmount <=0)
                throw new DomainExpectation("Total amount must be greater than zero to apply FX.");


            settlementCurreny = targetCurrency;
            exchangeRate = rate;
            convertedTotalAmount = rate * totalAmount;
        }

        public class DomainExpectation : Exception
        {
            public DomainExpectation( string Message) :base(Message)
            {
                
            }
        } 
        
    }
}
