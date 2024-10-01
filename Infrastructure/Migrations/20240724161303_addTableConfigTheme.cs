using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addTableConfigTheme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConfigTheme",
                schema: "Scaffolding",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColorButtonsAction = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ColorButtonCancel = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ColorButtonCreate = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ColorButtonText = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ColorTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ColorSubtitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ColorText = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ColorTextMenu = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ColorBackgroundMenu = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ColorHeaderTable = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ColorTextColumn = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TypeTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TypeSubtitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TypeParagraph = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    StyleLetter = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigTheme", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfigTheme",
                schema: "Scaffolding");
        }
    }
}
