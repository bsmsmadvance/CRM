using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class UpdateRateSettingTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "CMS",
                table: "RateSettingTransfer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "CMS",
                table: "RateSettingSale",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "CMS",
                table: "RateSettingFixTransferModel",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "CMS",
                table: "RateSettingFixTransfer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "CMS",
                table: "RateSettingFixSaleModel",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "CMS",
                table: "RateSettingFixSale",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "CMS",
                table: "RateSettingAgent",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "CMS",
                table: "GeneralSetting",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "CMS",
                table: "RateSettingTransfer");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "CMS",
                table: "RateSettingSale");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "CMS",
                table: "RateSettingFixTransferModel");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "CMS",
                table: "RateSettingFixTransfer");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "CMS",
                table: "RateSettingFixSaleModel");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "CMS",
                table: "RateSettingFixSale");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "CMS",
                table: "RateSettingAgent");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "CMS",
                table: "GeneralSetting");
        }
    }
}
