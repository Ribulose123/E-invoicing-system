using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_invocing.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UploadMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UploadBatches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UploadBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UploadedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotaleRecord = table.Column<int>(type: "int", nullable: false),
                    SuccessfulRecords = table.Column<int>(type: "int", nullable: false),
                    FailedRecords = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadBatches", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UploadBatches");
        }
    }
}
