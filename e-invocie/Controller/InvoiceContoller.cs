using E_invocing.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using E_invocing.Domin.Entities;
using Microsoft.EntityFrameworkCore;

// alias to force resolution of the domain Invoice type
using DominInvoice = E_invocing.Domin.Entities.Invoice;

namespace e_invocie.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceContoller : ControllerBase
    {
        private readonly E_invocingDbContext _context;

        public InvoiceContoller(E_invocingDbContext context)
        {
            _context = context;
        }

        // Checking invoice number
        private async Task<DominInvoice?> GetInvoice(int id)
        {
            return await _context.Invoices.FindAsync(id);
        }

        //Invoice approved
        [HttpPost("approve-invoice/{id}")]
        [HttpPost("{id}/approve")]
        public async Task<IActionResult> Approve(int id)
        {
            var invoice = await GetInvoice(id);
            if (invoice == null) return NotFound();

            invoice.Approve();
            await _context.SaveChangesAsync();

            return Ok("Invoice approved.");
        }

        [HttpPost("{id}/reject")]
        public async Task<IActionResult> Reject(int id)
        {
            var invoice = await GetInvoice(id);
            if (invoice == null) return NotFound();

            invoice.Reject();
            await _context.SaveChangesAsync();

            return Ok("Invoice rejected.");
        }

        [HttpPost("{id}/send")]
        public async Task<IActionResult> Send(int id)
        {
            var invoice = await GetInvoice(id);
            if (invoice == null) return NotFound();

            invoice.MarkSent();
            await _context.SaveChangesAsync();

            return Ok("Invoice sent.");
        }

        [HttpPost("{id}/pay")]
        public async Task<IActionResult> Pay(int id)
        {
            var invoice = await GetInvoice(id);
            if (invoice == null) return NotFound();

            invoice.MarkPaid();         
            await _context.SaveChangesAsync();

            return Ok("Invoice marked as paid.");
        }
    }
}
