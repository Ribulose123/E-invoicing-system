using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_invocing.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ValidationForInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "quantity",
                table: "InvoicesItems");

            migrationBuilder.DropColumn(
                name: "unitPrice",
                table: "InvoicesItems");

            migrationBuilder.AlterColumn<decimal>(
                name: "baseAmount",
                table: "Invoices",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<decimal>(
                name: "convertedTotalAmount",
                table: "Invoices",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "exchangeRate",
                table: "Invoices",
                type: "decimal(18,8)",
                precision: 18,
                scale: 8,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "settlementCurreny",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "taxAmount",
                table: "Invoices",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "totalAmount",
                table: "Invoices",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "convertedTotalAmount",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "exchangeRate",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "settlementCurreny",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "taxAmount",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "totalAmount",
                table: "Invoices");

            migrationBuilder.AddColumn<int>(
                name: "quantity",
                table: "InvoicesItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "unitPrice",
                table: "InvoicesItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "baseAmount",
                table: "Invoices",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);
        }
    }
}
