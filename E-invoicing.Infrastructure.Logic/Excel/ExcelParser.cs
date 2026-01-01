using ClosedXML.Excel;
using e_invocie.Interface;
using E_invocing.Domin.DTO;
using Microsoft.AspNetCore.Http;

namespace E_invoicing.Infrastructure.Logic.Excel
{
    public class ExcelParser : IExcelParser
    {
        public List<InvoiceUploadDto> ParseInvoice(IFormFile file)
        {
            var invoices = new List<InvoiceUploadDto>();

            using var stream = file.OpenReadStream();
            using var workbook = new XLWorkbook(stream);
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
                            invoice.Amount = decimal.TryParse(kvp.Value, out var val) ? val : 0;
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
