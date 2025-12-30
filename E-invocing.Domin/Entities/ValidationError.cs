using System;

namespace E_invocing.Domin.Entities
{
    public class ValidationError
    {
        public int Id { get; private set; }
        public int UploadBatchId { get; private set; }
        private int rowNumber;
        private string? fieldName;
        private string? errorMessage;
        private DateTime createdAt;

        // Public getters
        public int RowNumber => rowNumber;
        public string? FieldName => fieldName;
        public string? ErrorMessage => errorMessage;
        public DateTime CreatedAt => createdAt;

        protected ValidationError() { }

        public ValidationError(int uploadBatchId)
        {
            UploadBatchId = uploadBatchId;
            createdAt = DateTime.UtcNow;
        }
    }
}
