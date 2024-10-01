using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixedNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StarDateRecurrencing",
                schema: "Campaign",
                table: "MarketingCampaigns",
                newName: "StarDateRecurring");

            migrationBuilder.RenameColumn(
                name: "EndDateRecurrencing",
                schema: "Campaign",
                table: "MarketingCampaigns",
                newName: "EndDateRecurring");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StarDateRecurring",
                schema: "Campaign",
                table: "MarketingCampaigns",
                newName: "StarDateRecurrencing");

            migrationBuilder.RenameColumn(
                name: "EndDateRecurring",
                schema: "Campaign",
                table: "MarketingCampaigns",
                newName: "EndDateRecurrencing");
        }
    }
}
