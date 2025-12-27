

using E_invocing.Domin.Enum;

namespace E_invocing.Domin.Entities
{
    public class UploadBatch
    {
        public int Id { get; set; }
        public string? UploadBy { get; set; }
        public UploadStatus Status { get; set; } = UploadStatus.Processing;
        public DateTime UploadedAt { get; set; }
        public int TotaleRecord { get; set; }
        public int SuccessfulRecords { get; set; }
        public int FailedRecords { get; set; }

    }
}
