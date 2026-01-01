using E_invocing.Domin.DTO;

namespace e_invocie.IServices
{
    public interface IUploadbatch
    {
        Task<(string message, bool success)> UploadInvoiceAsync(UploadBatchRequestDto dto);
    }
}
