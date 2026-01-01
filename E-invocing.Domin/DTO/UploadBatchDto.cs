namespace E_invocing.Domin.DTO
{
    public class UploadBatchRequestDto
    {
        public string UploadBy { get; set; } = string.Empty;
        public List<InvoiceUploadDto> Invoices { get; set; } = new();
    }
}
