
using E_invocing.Domin.Entities;
using E_invocing.Domin.Enum;
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
            modelBuilder.Entity<UploadBatch>(entity =>
            {
                entity.Property<int>("totalRecords");
                entity.Property<int>("successfulRecords");
                entity.Property<int>("failedRecords");
                entity.Property<UploadStatus>("status");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.Property<string?>("invoiceNumber");
                entity.Property<string?>("baseCurrency");
                entity.Property<string?>("baseAmount");
                entity.Property<Status>("status");
            });

            modelBuilder.Entity<ValidationError>(entity =>
            {
                entity.Property<string?>("fieldName");
                entity.Property<string?>("errorMessage");
                entity.Property<DateTime>("createdAt");
            });

            modelBuilder.Entity<InvoiceItem>(entity =>
            {
                entity.Property<int>("quantity");
                entity.Property<int>("unitPrice");
            });
        }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet <InvoiceItem> InvoicesItems { get; set; }
        public DbSet <UploadBatch> UploadBatches { get; set; }
        public DbSet<ValidationError> ValidationErrors { get; set; }
    }
}
