using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_invocing.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InvocieitemEncapsulation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "InvoicesItems",
                newName: "unitPrice");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "InvoicesItems",
                newName: "quantity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "unitPrice",
                table: "InvoicesItems",
                newName: "UnitPrice");

            migrationBuilder.RenameColumn(
                name: "quantity",
                table: "InvoicesItems",
                newName: "Quantity");
        }
    }
}
