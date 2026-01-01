using ClosedXML.Excel;
using E_invocing.Domin.DTO;
using E_invocing.Domin.InterFaces;

namespace E_invoicing.Infrastructure.Logic.Excel
{
    public class ExcelParser : IExcelParser
    {
        public List<InvoiceUploadDto> ParseInvoice(Stream fileStream)
        {
            var invoices = new List<InvoiceUploadDto>();

            using var workbook = new XLWorkbook(fileStream);
            var worksheet = workbook.Worksheet(1);

            var headerRow = worksheet.Row(1);
            var headers = headerRow.CellsUsed().Select(c => c.GetString().Trim()).ToList();

            var dataRows = worksheet.RowsUsed().Skip(1);

            foreach (var row in dataRows)
            {
                var rowDict = new Dictionary<string, string>();
                for (int i = 0; i < headers.Count; i++)
                {
                    rowDict[headers[i]] = row.Cell(i + 1).GetString().Trim();
                }

                var invoice = new InvoiceUploadDto();

                foreach (var kvp in rowDict)
                {
                    switch (kvp.Key)
                    {
                        case "InvoiceNumber":
                            invoice.InvoiceNumber = kvp.Value;
                            break;
                        case "Currency":
                            invoice.Currency = kvp.Value;
                            break;
                        case "Amount":
                            invoice.Amount = kvp.Value;
                            break;
                        case "CustomerEmail":
                            invoice.CustomerEmail = kvp.Value;
                            break;
                        case "CustomerCountry":
                            invoice.CustomerCountry = kvp.Value;
                            break;
                        default:
                            invoice.OtherFields[kvp.Key] = kvp.Value;
                            break;
                    }
                }

                invoices.Add(invoice);
            }

            return invoices;
        }
    }
}
