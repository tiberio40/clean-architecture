using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MarkegintAndStatusTableAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MarketingStatus",
                schema: "Campaign",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketingStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Marketing",
                schema: "Campaign",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Cover = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OAuthId = table.Column<int>(type: "int", nullable: false),
                    MarketingStatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marketing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Marketing_MarketingStatus_MarketingStatusId",
                        column: x => x.MarketingStatusId,
                        principalSchema: "Campaign",
                        principalTable: "MarketingStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Marketing_MarketingStatusId",
                schema: "Campaign",
                table: "Marketing",
                column: "MarketingStatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Marketing",
                schema: "Campaign");

            migrationBuilder.DropTable(
                name: "MarketingStatus",
                schema: "Campaign");
        }
    }
}
