using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_invocing.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UploadStatusMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "UploadBatches",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "UploadBatches");
        }
    }
}
