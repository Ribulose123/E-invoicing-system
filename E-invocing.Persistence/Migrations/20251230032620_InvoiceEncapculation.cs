using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_invocing.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InvoiceEncapculation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Invoices",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "InvoiceNumber",
                table: "Invoices",
                newName: "invoiceNumber");

            migrationBuilder.RenameColumn(
                name: "BaseCurrency",
                table: "Invoices",
                newName: "baseCurrency");

            migrationBuilder.RenameColumn(
                name: "BaseAmount",
                table: "Invoices",
                newName: "baseAmount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "status",
                table: "Invoices",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "invoiceNumber",
                table: "Invoices",
                newName: "InvoiceNumber");

            migrationBuilder.RenameColumn(
                name: "baseCurrency",
                table: "Invoices",
                newName: "BaseCurrency");

            migrationBuilder.RenameColumn(
                name: "baseAmount",
                table: "Invoices",
                newName: "BaseAmount");
        }
    }
}
