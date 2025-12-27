

namespace E_invocing.Domin.Entities
{
    public class ValidationError
    {
        public int Id { get; set; }
        public int UploadBatchId { get; set; }
        public int RowNumber { get; set; }
        public string? FieldName { get; set; }
        public string? ErrorMessage { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
