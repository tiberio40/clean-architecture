using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MarketingTemplateEntitytableadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MarketingTemplates",
                schema: "Campaign",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TemplateMetaId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MarketingCampaignId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketingTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarketingTemplates_MarketingCampaigns_MarketingCampaignId",
                        column: x => x.MarketingCampaignId,
                        principalSchema: "Campaign",
                        principalTable: "MarketingCampaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MarketingTemplates_MarketingCampaignId",
                schema: "Campaign",
                table: "MarketingTemplates",
                column: "MarketingCampaignId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MarketingTemplates",
                schema: "Campaign");
        }
    }
}
