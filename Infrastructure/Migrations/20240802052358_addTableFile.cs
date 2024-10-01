using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addTableFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                schema: "Scaffolding",
                table: "ConfigTheme",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdFile",
                schema: "Scaffolding",
                table: "ConfigTheme",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Files",
                schema: "Scaffolding",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UrlFile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeFile = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConfigTheme_IdFile",
                schema: "Scaffolding",
                table: "ConfigTheme",
                column: "IdFile");

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigTheme_Files_IdFile",
                schema: "Scaffolding",
                table: "ConfigTheme",
                column: "IdFile",
                principalSchema: "Scaffolding",
                principalTable: "Files",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConfigTheme_Files_IdFile",
                schema: "Scaffolding",
                table: "ConfigTheme");

            migrationBuilder.DropTable(
                name: "Files",
                schema: "Scaffolding");

            migrationBuilder.DropIndex(
                name: "IX_ConfigTheme_IdFile",
                schema: "Scaffolding",
                table: "ConfigTheme");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                schema: "Scaffolding",
                table: "ConfigTheme");

            migrationBuilder.DropColumn(
                name: "IdFile",
                schema: "Scaffolding",
                table: "ConfigTheme");
        }
    }
}
