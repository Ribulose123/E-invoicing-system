using E_invocing.Domin.DTO;
using System.Threading.Tasks;

namespace e_invocie.IServices
{
    public interface IUploadbatch
    {
        Task<(string message, bool success, List<ValidationError> errors)>
            UploadInvoiceAsync(UploadRequestDto dto);
    }

}
