using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_invocing.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ValidationErrorEncapsulation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RowNumber",
                table: "validationErrors",
                newName: "rowNumber");

            migrationBuilder.RenameColumn(
                name: "FieldName",
                table: "validationErrors",
                newName: "fieldName");

            migrationBuilder.RenameColumn(
                name: "ErrorMessage",
                table: "validationErrors",
                newName: "errorMessage");

            migrationBuilder.RenameColumn(
                name: "CreateAt",
                table: "validationErrors",
                newName: "createdAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "rowNumber",
                table: "validationErrors",
                newName: "RowNumber");

            migrationBuilder.RenameColumn(
                name: "fieldName",
                table: "validationErrors",
                newName: "FieldName");

            migrationBuilder.RenameColumn(
                name: "errorMessage",
                table: "validationErrors",
                newName: "ErrorMessage");

            migrationBuilder.RenameColumn(
                name: "createdAt",
                table: "validationErrors",
                newName: "CreateAt");
        }
    }
}
