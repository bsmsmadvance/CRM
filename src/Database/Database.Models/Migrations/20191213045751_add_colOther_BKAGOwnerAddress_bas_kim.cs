using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class add_colOther_BKAGOwnerAddress_bas_kim : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OtherCountry",
                schema: "SAL",
                table: "BookingOwnerAddress",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherDistrict",
                schema: "SAL",
                table: "BookingOwnerAddress",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherProvince",
                schema: "SAL",
                table: "BookingOwnerAddress",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherSubDistrict",
                schema: "SAL",
                table: "BookingOwnerAddress",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherCountry",
                schema: "SAL",
                table: "AgreementOwnerAddress",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherDistrict",
                schema: "SAL",
                table: "AgreementOwnerAddress",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherProvince",
                schema: "SAL",
                table: "AgreementOwnerAddress",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherSubDistrict",
                schema: "SAL",
                table: "AgreementOwnerAddress",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OtherCountry",
                schema: "SAL",
                table: "BookingOwnerAddress");

            migrationBuilder.DropColumn(
                name: "OtherDistrict",
                schema: "SAL",
                table: "BookingOwnerAddress");

            migrationBuilder.DropColumn(
                name: "OtherProvince",
                schema: "SAL",
                table: "BookingOwnerAddress");

            migrationBuilder.DropColumn(
                name: "OtherSubDistrict",
                schema: "SAL",
                table: "BookingOwnerAddress");

            migrationBuilder.DropColumn(
                name: "OtherCountry",
                schema: "SAL",
                table: "AgreementOwnerAddress");

            migrationBuilder.DropColumn(
                name: "OtherDistrict",
                schema: "SAL",
                table: "AgreementOwnerAddress");

            migrationBuilder.DropColumn(
                name: "OtherProvince",
                schema: "SAL",
                table: "AgreementOwnerAddress");

            migrationBuilder.DropColumn(
                name: "OtherSubDistrict",
                schema: "SAL",
                table: "AgreementOwnerAddress");
        }
    }
}
