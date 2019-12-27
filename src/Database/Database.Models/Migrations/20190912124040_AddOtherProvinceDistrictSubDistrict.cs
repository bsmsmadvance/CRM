using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddOtherProvinceDistrictSubDistrict : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OtherDistrictEN",
                schema: "PRJ",
                table: "Address",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherDistrictTH",
                schema: "PRJ",
                table: "Address",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherProvinceEN",
                schema: "PRJ",
                table: "Address",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherProvinceTH",
                schema: "PRJ",
                table: "Address",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherSubDistrictEN",
                schema: "PRJ",
                table: "Address",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherSubDistrictTH",
                schema: "PRJ",
                table: "Address",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherDistrictEN",
                schema: "MST",
                table: "Company",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherDistrictTH",
                schema: "MST",
                table: "Company",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherProvinceEN",
                schema: "MST",
                table: "Company",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherProvinceTH",
                schema: "MST",
                table: "Company",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherSubDistrictEN",
                schema: "MST",
                table: "Company",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherSubDistrictTH",
                schema: "MST",
                table: "Company",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherDistrictTH",
                schema: "MST",
                table: "BankBranch",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherProvinceTH",
                schema: "MST",
                table: "BankBranch",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherSubDistrictTH",
                schema: "MST",
                table: "BankBranch",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherCountryEN",
                schema: "CTM",
                table: "ContactAddress",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherCountryTH",
                schema: "CTM",
                table: "ContactAddress",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherDistrictEN",
                schema: "CTM",
                table: "ContactAddress",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherDistrictTH",
                schema: "CTM",
                table: "ContactAddress",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherProvinceEN",
                schema: "CTM",
                table: "ContactAddress",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherProvinceTH",
                schema: "CTM",
                table: "ContactAddress",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherSubDistrictEN",
                schema: "CTM",
                table: "ContactAddress",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherSubDistrictTH",
                schema: "CTM",
                table: "ContactAddress",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherNational",
                schema: "CTM",
                table: "Contact",
                maxLength: 1000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OtherDistrictEN",
                schema: "PRJ",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "OtherDistrictTH",
                schema: "PRJ",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "OtherProvinceEN",
                schema: "PRJ",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "OtherProvinceTH",
                schema: "PRJ",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "OtherSubDistrictEN",
                schema: "PRJ",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "OtherSubDistrictTH",
                schema: "PRJ",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "OtherDistrictEN",
                schema: "MST",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "OtherDistrictTH",
                schema: "MST",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "OtherProvinceEN",
                schema: "MST",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "OtherProvinceTH",
                schema: "MST",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "OtherSubDistrictEN",
                schema: "MST",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "OtherSubDistrictTH",
                schema: "MST",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "OtherDistrictTH",
                schema: "MST",
                table: "BankBranch");

            migrationBuilder.DropColumn(
                name: "OtherProvinceTH",
                schema: "MST",
                table: "BankBranch");

            migrationBuilder.DropColumn(
                name: "OtherSubDistrictTH",
                schema: "MST",
                table: "BankBranch");

            migrationBuilder.DropColumn(
                name: "OtherCountryEN",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.DropColumn(
                name: "OtherCountryTH",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.DropColumn(
                name: "OtherDistrictEN",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.DropColumn(
                name: "OtherDistrictTH",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.DropColumn(
                name: "OtherProvinceEN",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.DropColumn(
                name: "OtherProvinceTH",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.DropColumn(
                name: "OtherSubDistrictEN",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.DropColumn(
                name: "OtherSubDistrictTH",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.DropColumn(
                name: "OtherNational",
                schema: "CTM",
                table: "Contact");
        }
    }
}
