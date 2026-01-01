using E_invocing.Domin.DTO;

namespace e_invocie.Interface
{
    public interface IExcelParser
    {
        List<InvoiceUploadDto> ParseInvoice(IFormFile file);
    }
}
