using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_invocing.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddInvoiceStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UploadedDate",
                table: "Invoices",
                newName: "CreatedAt");

            migrationBuilder.AlterColumn<string>(
                name: "invoiceNumber",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "baseCurrency",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            
                

            migrationBuilder.AddColumn<decimal>(
                name: "FxRate",
                table: "Invoices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

           

            migrationBuilder.AddColumn<decimal>(
                name: "SettlementAmount",
                table: "Invoices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "SettlementCurrency",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

           

          
               
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaseAmount",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "BaseCurrency",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "FxRate",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "InvoiceNumber",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "SettlementAmount",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "SettlementCurrency",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "TaxAmount",
                table: "Invoices");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Invoices",
                newName: "UploadedDate");

            migrationBuilder.AlterColumn<string>(
                name: "invoiceNumber",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "baseCurrency",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
