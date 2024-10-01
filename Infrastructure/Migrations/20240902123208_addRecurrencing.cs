using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addRecurrencing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndDateRecurrencing",
                schema: "Campaign",
                table: "MarketingCampaigns",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IndefiniteEndDate",
                schema: "Campaign",
                table: "MarketingCampaigns",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "RecurringTypeId",
                schema: "Campaign",
                table: "MarketingCampaigns",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StarDateRecurrencing",
                schema: "Campaign",
                table: "MarketingCampaigns",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "RecurringTypes",
                schema: "Campaign",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecurringTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MarketingCampaigns_RecurringTypeId",
                schema: "Campaign",
                table: "MarketingCampaigns",
                column: "RecurringTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_MarketingCampaigns_RecurringTypes_RecurringTypeId",
                schema: "Campaign",
                table: "MarketingCampaigns",
                column: "RecurringTypeId",
                principalSchema: "Campaign",
                principalTable: "RecurringTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MarketingCampaigns_RecurringTypes_RecurringTypeId",
                schema: "Campaign",
                table: "MarketingCampaigns");

            migrationBuilder.DropTable(
                name: "RecurringTypes",
                schema: "Campaign");

            migrationBuilder.DropIndex(
                name: "IX_MarketingCampaigns_RecurringTypeId",
                schema: "Campaign",
                table: "MarketingCampaigns");

            migrationBuilder.DropColumn(
                name: "EndDateRecurrencing",
                schema: "Campaign",
                table: "MarketingCampaigns");

            migrationBuilder.DropColumn(
                name: "IndefiniteEndDate",
                schema: "Campaign",
                table: "MarketingCampaigns");

            migrationBuilder.DropColumn(
                name: "RecurringTypeId",
                schema: "Campaign",
                table: "MarketingCampaigns");

            migrationBuilder.DropColumn(
                name: "StarDateRecurrencing",
                schema: "Campaign",
                table: "MarketingCampaigns");
        }
    }
}
