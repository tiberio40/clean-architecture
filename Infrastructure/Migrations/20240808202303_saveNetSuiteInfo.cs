using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class saveNetSuiteInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Marketing_MarketingStatus_MarketingStatusId",
                schema: "Campaign",
                table: "Marketing");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Marketing",
                schema: "Campaign",
                table: "Marketing");

            migrationBuilder.RenameTable(
                name: "Marketing",
                schema: "Campaign",
                newName: "Marketings",
                newSchema: "Campaign");

            migrationBuilder.RenameIndex(
                name: "IX_Marketing_MarketingStatusId",
                schema: "Campaign",
                table: "Marketings",
                newName: "IX_Marketings_MarketingStatusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Marketings",
                schema: "Campaign",
                table: "Marketings",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "MarketingCampaigns",
                schema: "Campaign",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OriginId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MarketingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketingCampaigns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarketingCampaigns_Marketings_MarketingId",
                        column: x => x.MarketingId,
                        principalSchema: "Campaign",
                        principalTable: "Marketings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MarketingUsers",
                schema: "Campaign",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MarketingCampaignId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketingUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MarketingUsers_MarketingCampaigns_MarketingCampaignId",
                        column: x => x.MarketingCampaignId,
                        principalSchema: "Campaign",
                        principalTable: "MarketingCampaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MarketingCampaigns_MarketingId",
                schema: "Campaign",
                table: "MarketingCampaigns",
                column: "MarketingId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketingUsers_MarketingCampaignId",
                schema: "Campaign",
                table: "MarketingUsers",
                column: "MarketingCampaignId");

            migrationBuilder.AddForeignKey(
                name: "FK_Marketings_MarketingStatus_MarketingStatusId",
                schema: "Campaign",
                table: "Marketings",
                column: "MarketingStatusId",
                principalSchema: "Campaign",
                principalTable: "MarketingStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Marketings_MarketingStatus_MarketingStatusId",
                schema: "Campaign",
                table: "Marketings");

            migrationBuilder.DropTable(
                name: "MarketingUsers",
                schema: "Campaign");

            migrationBuilder.DropTable(
                name: "MarketingCampaigns",
                schema: "Campaign");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Marketings",
                schema: "Campaign",
                table: "Marketings");

            migrationBuilder.RenameTable(
                name: "Marketings",
                schema: "Campaign",
                newName: "Marketing",
                newSchema: "Campaign");

            migrationBuilder.RenameIndex(
                name: "IX_Marketings_MarketingStatusId",
                schema: "Campaign",
                table: "Marketing",
                newName: "IX_Marketing_MarketingStatusId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Marketing",
                schema: "Campaign",
                table: "Marketing",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Marketing_MarketingStatus_MarketingStatusId",
                schema: "Campaign",
                table: "Marketing",
                column: "MarketingStatusId",
                principalSchema: "Campaign",
                principalTable: "MarketingStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
