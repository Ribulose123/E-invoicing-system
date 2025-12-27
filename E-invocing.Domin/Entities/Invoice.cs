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
        public int Id {get; set;}
        public string? InvoiceNumber  {get; set; }
        public string? BaseCurrency {get; set; }
        public string? BaseAmount {get; set; }
        public Status Status { get; set; } = Status.Pending;
        public int CustomerId { get; set; }
        public int UploadBatchId { get; set; }
        public DateTime UplodadDate { get; set; }
    }
}
