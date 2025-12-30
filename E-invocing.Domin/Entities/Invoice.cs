using E_invocing.Domin.Enum;
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
        private string? baseAmount;
        private Status status { get; set; } = Status.Pending;
        public int CustomerId { get; private set; }
        public int UploadBatchId { get;private set; }
        public DateTime UplodadDate { get; private set; } = DateTime.UtcNow;

        

        public string ? InvoiceNumber => invoiceNumber;
        public string ? BaseCurrency => baseCurrency;
        public string ? BaseAmount => baseAmount;
        public Status Status => status;

        protected Invoice() { }
        public Invoice( int customerId, int uploadBatchId)
        {
            CustomerId = customerId;
            UploadBatchId = uploadBatchId;
            UplodadDate = DateTime.UtcNow;
        }
        public void MarkAsProcessed() => status = Status.Success;
        public void MarkAsFailed() => status = Status.Failed;
    }
}
