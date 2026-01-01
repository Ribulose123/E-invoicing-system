using e_invocie.Interface;
using e_invocie.IServices;
using E_invocing.Domin.DTO;
using E_invocing.Domin.Entities;
using E_invocing.Persistence;

namespace e_invocie.Services
{
    public class UploadBatchServices:IUploadbatch
    {
        private readonly E_invocingDbContext _context;

        public UploadBatchServices( E_invocingDbContext context)
        {
            _context = context;
        }

        public async Task<(string message, bool success)> UploadInvoiceAsync(UploadBatchRequestDto dto)
        {
            if (dto.Invoices == null)
                return ("No file uploaded", false);

            //Create an invoice
            var uploadinvoice = new UploadBatch(dto.UploadBy);
            await _context.UploadBatches.AddAsync(uploadinvoice);
            _context.SaveChanges();

            var totalRecords = 0;
            var successfulRecords = 0;
            var failableRecords = 0;

            //Parse excel to json

           return ("yes", false);

        }

    }
}
