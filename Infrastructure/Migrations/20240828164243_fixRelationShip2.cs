using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixRelationShip2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Marketings_MarketingStatus_MarketingStatusId",
                schema: "Campaign",
                table: "Marketings");

            migrationBuilder.DropForeignKey(
                name: "FK_Marketings_MetaConfigurations_MetaConfigurationId",
                schema: "Campaign",
                table: "Marketings");

            migrationBuilder.AlterColumn<int>(
                name: "MetaConfigurationId",
                schema: "Campaign",
                table: "Marketings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "MarketingStatusId",
                schema: "Campaign",
                table: "Marketings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Marketings_MarketingStatus_MarketingStatusId",
                schema: "Campaign",
                table: "Marketings",
                column: "MarketingStatusId",
                principalSchema: "Campaign",
                principalTable: "MarketingStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Marketings_MetaConfigurations_MetaConfigurationId",
                schema: "Campaign",
                table: "Marketings",
                column: "MetaConfigurationId",
                principalSchema: "Campaign",
                principalTable: "MetaConfigurations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Marketings_MarketingStatus_MarketingStatusId",
                schema: "Campaign",
                table: "Marketings");

            migrationBuilder.DropForeignKey(
                name: "FK_Marketings_MetaConfigurations_MetaConfigurationId",
                schema: "Campaign",
                table: "Marketings");

            migrationBuilder.AlterColumn<int>(
                name: "MetaConfigurationId",
                schema: "Campaign",
                table: "Marketings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MarketingStatusId",
                schema: "Campaign",
                table: "Marketings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Marketings_MarketingStatus_MarketingStatusId",
                schema: "Campaign",
                table: "Marketings",
                column: "MarketingStatusId",
                principalSchema: "Campaign",
                principalTable: "MarketingStatus",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Marketings_MetaConfigurations_MetaConfigurationId",
                schema: "Campaign",
                table: "Marketings",
                column: "MetaConfigurationId",
                principalSchema: "Campaign",
                principalTable: "MetaConfigurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
