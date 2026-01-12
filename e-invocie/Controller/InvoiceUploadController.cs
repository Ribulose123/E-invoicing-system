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
        public async Task<IActionResult> UploadInvoices([FromForm] UploadInvoiceFormDto dto)
        {
            using var stream = dto.File.OpenReadStream();

            var request = new UploadRequestDto
            {
                UploadBy = dto.UploadBy,
                FileStream = stream
            };

            var (message, success, errors) = await _uploadbatch.UploadInvoiceAsync(request);

            if (!success)
            {
                return BadRequest(new
                {
                    success = false,
                    message,
                    errors = errors.Select(e => new
                    {
                        field = e.FieldName,
                        message = e.ErrorMessage
                    })
                });
            }

            return Ok(new
            {
                success = true,
                message
            });
        }

    }
}
