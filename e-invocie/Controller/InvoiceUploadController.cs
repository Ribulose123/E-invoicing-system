using e_invocie.IServices;
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
        public async Task<IActionResult> UploadInvoices([FromForm] IFormFile file, [FromForm] string uploadBy)
        {
            
            if (file == null || file.Length == 0)
                return BadRequest("File is empty.");

            if (string.IsNullOrEmpty(uploadBy))
                return BadRequest("Uploader name (uploadBy) is required.");

            using (var stream = file.OpenReadStream())
            {
          
                var dto = new UploadRequestDto
                {
                    UploadBy = uploadBy,
                    FileStream = stream
                };


                var (message, success) = await _uploadbatch.UploadInvoiceAsync(dto);

                return success
                    ? Ok(new { success = true, message })
                    : BadRequest(new { success = false, message });
            }
        }


    }
}
