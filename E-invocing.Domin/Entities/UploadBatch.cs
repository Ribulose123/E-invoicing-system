using E_invocing.Domin.Enum;

namespace E_invocing.Domin.Entities
{
    public class UploadBatch
    {
        public int Id { get; private set; } 
        public string? UploadBy { get; private set; } 
        public DateTime UploadedAt { get; private set; } = DateTime.UtcNow;

        private int totalRecords;
        private int successfulRecords;
        private int failedRecords;

        private UploadStatus status = UploadStatus.Processing;

        // Public getters, no setters
        public int TotalRecords => totalRecords;
        public int SuccessfulRecords => successfulRecords;
        public int FailedRecords => failedRecords;
        public UploadStatus Status => status;

        // Constructor
        protected UploadBatch() { }
        public UploadBatch(string uploadedBy)
        {
            UploadBy = uploadedBy;
            UploadedAt = DateTime.UtcNow;
        }

        public void RecordSuccess() => successfulRecords++;
        public void RecordFailure() => failedRecords++;
        public void SetTotalRecords(int total) => totalRecords = total;

        public void MarkAsCompleted()
        {
            status = UploadStatus.Success;
        }
    }
}
