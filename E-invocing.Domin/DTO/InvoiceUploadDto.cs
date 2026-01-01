namespace E_invocing.Domin.DTO
{
    public class InvoiceUploadDto
    {
        public string InvoiceNumber { get; set; } = string.Empty;
        public string Currency { get; set; } = string.Empty;
        public string Amount { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string CustomerCountry { get; set; } = string.Empty;

        public Dictionary<string, string> OtherFields { get; set; } = new();
    }
}
