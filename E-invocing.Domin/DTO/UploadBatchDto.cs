namespace E_invocing.Domin.DTO
{
    public class UploadRequestDto
    {
        public string UploadBy { get; set; } = string.Empty;
        public Stream FileStream { get; set; } = Stream.Null;
    }
}
