

namespace E_invocing.Domin.DTO
{
    public class UploadResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<UploadErrorDto> Errors { get; set; } = new();
    }
}
