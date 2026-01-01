using E_invocing.Domin.DTO;

namespace E_invocing.Domin.InterFaces
{
    public interface IExcelParser
    {
        List<InvoiceUploadDto> ParseInvoice(Stream fileStream);
    }
}
