
using E_invocing.Domin.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace E_invocing.Persistence
{
    public class E_invocingDbContext : DbContext
    {
        public E_invocingDbContext(DbContextOptions<E_invocingDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet <InvoiceItem> InvoicesItems { get; set; }
        public DbSet <UploadBatch> UploadBatches { get; set; }
        public DbSet<ValidationError> validationErrors { get; set; }
    }
}
