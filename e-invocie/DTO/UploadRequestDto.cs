using Microsoft.AspNetCore.Http;

namespace e_invocie.DTOs
{
    public class UploadInvoiceFormDto
    {
        public string UploadBy { get; set; } = string.Empty;
        public IFormFile File { get; set; } = default!;
    }
}
