using E_invocing.Domin.Entities;
using E_invocing.Domin.Enum;
using Microsoft.EntityFrameworkCore;

namespace E_invocing.Persistence
{
    public class E_invocingDbContext : DbContext
    {
        public E_invocingDbContext(DbContextOptions<E_invocingDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ======================
            // Invoice
            // ======================
            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasKey(i => i.Id);

                entity.Property(i => i.InvoiceNumber)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(i => i.BaseCurrency)
                      .IsRequired()
                      .HasMaxLength(10);

                entity.Property(i => i.BaseAmount)
                      .HasPrecision(18, 4);

                entity.Property(i => i.TaxAmount)
                      .HasPrecision(18, 4);

                entity.Property(i => i.FxRate)
                      .HasPrecision(18, 8);

                entity.Property(i => i.SettlementCurrency)
                      .HasMaxLength(10);

                entity.Property(i => i.SettlementAmount)
                      .HasPrecision(18, 4);

                entity.Property(i => i.Status)
                      .HasConversion<int>()
                      .HasDefaultValue(0)
                      .IsRequired();

                entity.Property(i => i.CreatedAt)
                      .HasDefaultValueSql("GETUTCDATE()");
            });

            // ======================
            // UploadBatch
            // ======================
            modelBuilder.Entity<UploadBatch>(entity =>
            {
                entity.HasKey(u => u.Id);
            });

            // ======================
            // ValidationError
            // ======================
            modelBuilder.Entity<ValidationError>(entity =>
            {
                entity.HasKey(v => v.Id);

                entity.Property(v => v.CreatedAt)
                      .HasDefaultValueSql("GETUTCDATE()");
            });

            // ======================
            // InvoiceItem
            // ======================
            modelBuilder.Entity<InvoiceItem>(entity =>
            {
                entity.HasKey(ii => ii.Id);

                entity.Property(ii => ii.Description)
                      .HasMaxLength(250);

                entity.Property(ii => ii.UnitPrice)
                      .HasPrecision(18, 4);
            });
        }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<InvoiceItem> InvoicesItems { get; set; }
        public DbSet<UploadBatch> UploadBatches { get; set; }
        public DbSet<ValidationError> ValidationErrors { get; set; }
    }
}
