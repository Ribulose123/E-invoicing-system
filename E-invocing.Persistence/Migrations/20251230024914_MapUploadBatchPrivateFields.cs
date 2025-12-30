using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_invocing.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MapUploadBatchPrivateFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "failedRecords",
                table: "UploadBatches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "UploadBatches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "successfulRecords",
                table: "UploadBatches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "totalRecords",
                table: "UploadBatches",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "failedRecords",
                table: "UploadBatches");

            migrationBuilder.DropColumn(
                name: "status",
                table: "UploadBatches");

            migrationBuilder.DropColumn(
                name: "successfulRecords",
                table: "UploadBatches");

            migrationBuilder.DropColumn(
                name: "totalRecords",
                table: "UploadBatches");
        }
    }
}
