using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using E_invocing.Domin.DTO;
using E_invocing.Domin.InterFaces;

namespace E_invoicing.Infrastructure.Logic.Excel
{
    public class ExcelParser : IExcelParser
    {
        public List<InvoiceUploadDto> ParseInvoice(Stream fileStream)
        {
            var invoices = new List<InvoiceUploadDto>();

            if(fileStream.CanSeek && fileStream.Position > 0)
            {
                fileStream.Position = 0;
            }

            using (var workBook = new XLWorkbook(fileStream))
            {
                var workSheet = workBook.Worksheet(1);
                var headerRow = workSheet.Row(1);
                var headers = headerRow.CellsUsed().Select(c => c.GetString().Trim()).ToList();

                var dataRows = workSheet.RowsUsed().Skip(1);

                foreach (var row in dataRows)
                {
                    var invoice = new InvoiceUploadDto();

                    for(var i = 0; i < headers.Count; i++)
                    {
                        var header = headers[i];
                        var value = row.Cell(1).GetString().Trim();

                        switch (header)
                        {
                            case "InvoiceNumber": invoice.InvoiceNumber = value; break;
                            case "Currency": invoice.Currency = value; break;
                            case "Amount": invoice.Amount = value; break;
                            case "CustomerEmail": invoice.CustomerEmail = value; break;
                            case "CustomerCountry": invoice.CustomerCountry = value; break;
                            default:
                                invoice.OtherFields[header] = value;
                                break;
                        }
                    }
                    invoices.Add(invoice);
                }

                return invoices;
            }
        }
    }
}
