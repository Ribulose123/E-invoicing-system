public class ValidationError
{
    public int Id { get; private set; }
    public int UploadBatchId { get; private set; }
    private string? fieldName;
    private string? errorMessage;
    private DateTime createdAt;

    // Public getters
    public string? FieldName => fieldName;
    public string? ErrorMessage => errorMessage;
    public DateTime CreatedAt => createdAt;

    protected ValidationError() { } // For EF

    // Minimal constructor
    public ValidationError(int uploadBatchId)
    {
        UploadBatchId = uploadBatchId;
        createdAt = DateTime.UtcNow;
    }

    // Full constructor for validation errors
    public ValidationError(int uploadBatchId, string fieldName, string errorMessage)
    {
        UploadBatchId = uploadBatchId;
        this.fieldName = fieldName;
        this.errorMessage = errorMessage;
        this.createdAt = DateTime.UtcNow;
    }
}
