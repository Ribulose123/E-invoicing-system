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

            if (fileStream.CanSeek && fileStream.Position > 0)
                fileStream.Position = 0;

            using var workbook = new XLWorkbook(fileStream);
            var worksheet = workbook.Worksheet(1);

            var headers = worksheet.Row(1)
                .CellsUsed()
                .Select(c => c.GetString().Trim())
                .ToList();

            var rows = worksheet.RowsUsed().Skip(1);

            foreach (var row in rows)
            {
                var invoice = new InvoiceUploadDto();

                for (int i = 0; i < headers.Count; i++)
                {
                    string rawHeader = headers[i];
                    string header = Normalize(rawHeader);

                    string value = row.Cell(i + 1).GetString().Trim();

                    // 🔎 LOG EXACT MAPPING
                    Console.WriteLine($"Header {i + 1}: {rawHeader} = {value}");

                    switch (header)
                    {
                        case "invoicenumber":
                            invoice.InvoiceNumber = value;
                            break;

                        case "currency":
                            invoice.Currency = value;
                            break;

                        case "amount":
                            invoice.Amount = value;
                            break;

                        case "customeremail":
                            invoice.CustomerEmail = value;
                            break;

                        case "customercountry":
                        case "country":
                        case "county":
                            invoice.CustomerCountry = value;
                            break;

                        default:
                            invoice.OtherFields[rawHeader] = value;
                            break;
                    }
                }

                invoices.Add(invoice);
            }

            return invoices;
        }

        private string Normalize(string header)
        {
            return header
                .Replace(" ", "")
                .Replace("_", "")
                .Trim()
                .ToLower();
        }
    }
}
