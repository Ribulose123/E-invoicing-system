namespace E_invocing.Domin.Entities
{
    public class InvoiceItem
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public string? Description { get; set; }
        public int UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
