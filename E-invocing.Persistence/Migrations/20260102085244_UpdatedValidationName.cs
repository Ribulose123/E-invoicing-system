using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_invocing.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedValidationName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_validationErrors",
                table: "validationErrors");

            migrationBuilder.DropColumn(
                name: "rowNumber",
                table: "validationErrors");

            migrationBuilder.RenameTable(
                name: "validationErrors",
                newName: "ValidationErrors");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ValidationErrors",
                table: "ValidationErrors",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ValidationErrors",
                table: "ValidationErrors");

            migrationBuilder.RenameTable(
                name: "ValidationErrors",
                newName: "validationErrors");

            migrationBuilder.AddColumn<int>(
                name: "rowNumber",
                table: "validationErrors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_validationErrors",
                table: "validationErrors",
                column: "Id");
        }
    }
}
