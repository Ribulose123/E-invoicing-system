using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_invocing.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UploadEncapsulation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FailedRecords",
                table: "UploadBatches");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "UploadBatches");

            migrationBuilder.DropColumn(
                name: "SuccessfulRecords",
                table: "UploadBatches");

            migrationBuilder.DropColumn(
                name: "TotaleRecord",
                table: "UploadBatches");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FailedRecords",
                table: "UploadBatches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "UploadBatches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SuccessfulRecords",
                table: "UploadBatches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotaleRecord",
                table: "UploadBatches",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
