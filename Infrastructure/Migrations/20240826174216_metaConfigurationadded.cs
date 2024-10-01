using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class metaConfigurationadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OAuthId",
                schema: "Campaign",
                table: "Marketings",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "MetaConfigurationId",
                schema: "Campaign",
                table: "Marketings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MetaConfigurations",
                schema: "Campaign",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    OAuthId = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    MetaTypeServiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetaConfigurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MetaConfigurations_MetaTypeServices_MetaTypeServiceId",
                        column: x => x.MetaTypeServiceId,
                        principalSchema: "Campaign",
                        principalTable: "MetaTypeServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Marketings_MetaConfigurationId",
                schema: "Campaign",
                table: "Marketings",
                column: "MetaConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_MetaConfigurations_MetaTypeServiceId",
                schema: "Campaign",
                table: "MetaConfigurations",
                column: "MetaTypeServiceId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Marketings_MetaConfigurations_MetaConfigurationId",
                schema: "Campaign",
                table: "Marketings");

            migrationBuilder.DropTable(
                name: "MetaConfigurations",
                schema: "Campaign");

            migrationBuilder.DropIndex(
                name: "IX_Marketings_MetaConfigurationId",
                schema: "Campaign",
                table: "Marketings");

            migrationBuilder.DropColumn(
                name: "MetaConfigurationId",
                schema: "Campaign",
                table: "Marketings");

            migrationBuilder.AlterColumn<string>(
                name: "OAuthId",
                schema: "Campaign",
                table: "Marketings",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);
        }
    }
}
