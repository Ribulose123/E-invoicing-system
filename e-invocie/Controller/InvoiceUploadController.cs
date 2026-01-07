using e_invocie.IServices;
using e_invocie.DTOs;
using E_invocing.Domin.DTO;
using Microsoft.AspNetCore.Mvc;

namespace e_invocie.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceUploadController : ControllerBase
    {
        private readonly IUploadbatch _uploadbatch;

        public InvoiceUploadController(IUploadbatch uploadbatch)
        {
            _uploadbatch = uploadbatch;
        }

        [HttpPost("upload-invoices")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadInvoices([FromForm] UploadInvoiceFormDto form)
        {
            if (form.File == null || form.File.Length == 0)
                return BadRequest("File is empty.");

            using var stream = form.File.OpenReadStream();

            var request = new UploadRequestDto
            {
                UploadBy = form.UploadBy,
                FileStream = stream
            };

            var (message, success) = await _uploadbatch.UploadInvoiceAsync(request);

            return success
                ? Ok(new { success = true, message })
                : BadRequest(new { success = false, message });
        }
    }
}
