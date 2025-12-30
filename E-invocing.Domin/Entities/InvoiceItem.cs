namespace E_invocing.Domin.Entities
{
    public class InvoiceItem
    {
        public int Id { get; private set; }
        public int InvoiceId { get; private set; }
        public string? Description { get; private set; }
        private int unitPrice;
        private int quantity;

        public int UnitPrice => unitPrice;
        public int Quantity => quantity;
        protected InvoiceItem() { }
        public InvoiceItem(int invoiceId, string? description)
        {
            InvoiceId = invoiceId;
            Description = description;
        }
    }
}
