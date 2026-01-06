namespace E_invocing.Domin.DTO
{
    public class UploadBatchRequestDto
    {
        public List<InvoiceUploadDto> Invoices { get; set; } = new();
    }
}
