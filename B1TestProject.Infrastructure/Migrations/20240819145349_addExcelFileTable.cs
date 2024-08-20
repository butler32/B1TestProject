using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace B1TestProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addExcelFileTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExcelFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ExcelFileId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExcelFiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TextLines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Latin = table.Column<string>(type: "text", nullable: false),
                    Russian = table.Column<string>(type: "text", nullable: false),
                    IntegerNum = table.Column<int>(type: "integer", nullable: false),
                    DoubleNum = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextLines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BalanceSheetEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Class = table.Column<int>(type: "integer", nullable: false),
                    AccountNumber = table.Column<string>(type: "text", nullable: false),
                    IncomingBalanceActive = table.Column<decimal>(type: "numeric", nullable: false),
                    IncomingBalancePassive = table.Column<decimal>(type: "numeric", nullable: false),
                    TurnoverDebit = table.Column<decimal>(type: "numeric", nullable: false),
                    TurnoverCredit = table.Column<decimal>(type: "numeric", nullable: false),
                    ExcelFilesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BalanceSheetEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BalanceSheetEntries_ExcelFiles_ExcelFilesId",
                        column: x => x.ExcelFilesId,
                        principalTable: "ExcelFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BalanceSheetEntries_ExcelFilesId",
                table: "BalanceSheetEntries",
                column: "ExcelFilesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BalanceSheetEntries");

            migrationBuilder.DropTable(
                name: "TextLines");

            migrationBuilder.DropTable(
                name: "ExcelFiles");
        }
    }
}
