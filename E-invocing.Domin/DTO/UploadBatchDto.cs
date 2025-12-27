using E_invocing.Domin.Enum;

namespace E_invocing.Domin.DTO
{
   public class UploadBatchDto
    {
        public string? UploadBy { get; set; }
        public UploadStatus Status { get; set; } = UploadStatus.Processing;
        public DateTime UploadedAt { get; set; }
    }
}
