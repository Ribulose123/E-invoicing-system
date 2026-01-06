using E_invocing.Domin.DTO;
using System.Threading.Tasks;

namespace e_invocie.IServices
{
    public interface IUploadbatch
    {
        Task<(string message, bool success)> UploadInvoiceAsync(UploadRequestDto dto);
    }
}
