using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ReviseUnitAndTitledeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TitledeedDetail_District_HouseDistrictID",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_TitledeedDetail_Province_HouseProvinceID",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_TitledeedDetail_SubDistrict_HouseSubDistrictID",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_TitledeedDetail_LandOffice_LandOfficeID",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_TitledeedDetailHistory_District_HouseDistrictID",
                schema: "PRJ",
                table: "TitledeedDetailHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_TitledeedDetailHistory_Province_HouseProvinceID",
                schema: "PRJ",
                table: "TitledeedDetailHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_TitledeedDetailHistory_SubDistrict_HouseSubDistrictID",
                schema: "PRJ",
                table: "TitledeedDetailHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_TitledeedDetailHistory_LandOffice_LandOfficeID",
                schema: "PRJ",
                table: "TitledeedDetailHistory");

            migrationBuilder.DropIndex(
                name: "IX_TitledeedDetailHistory_HouseDistrictID",
                schema: "PRJ",
                table: "TitledeedDetailHistory");

            migrationBuilder.DropIndex(
                name: "IX_TitledeedDetailHistory_HouseProvinceID",
                schema: "PRJ",
                table: "TitledeedDetailHistory");

            migrationBuilder.DropIndex(
                name: "IX_TitledeedDetailHistory_HouseSubDistrictID",
                schema: "PRJ",
                table: "TitledeedDetailHistory");

            migrationBuilder.DropIndex(
                name: "IX_TitledeedDetailHistory_LandOfficeID",
                schema: "PRJ",
                table: "TitledeedDetailHistory");

            migrationBuilder.DropIndex(
                name: "IX_TitledeedDetail_HouseDistrictID",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropIndex(
                name: "IX_TitledeedDetail_HouseProvinceID",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropIndex(
                name: "IX_TitledeedDetail_HouseSubDistrictID",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropIndex(
                name: "IX_TitledeedDetail_LandOfficeID",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropColumn(
                name: "AirArea",
                schema: "PRJ",
                table: "TitledeedDetailHistory");

            migrationBuilder.DropColumn(
                name: "BalconyArea",
                schema: "PRJ",
                table: "TitledeedDetailHistory");

            migrationBuilder.DropColumn(
                name: "FenceArea",
                schema: "PRJ",
                table: "TitledeedDetailHistory");

            migrationBuilder.DropColumn(
                name: "FenceIronArea",
                schema: "PRJ",
                table: "TitledeedDetailHistory");

            migrationBuilder.DropColumn(
                name: "HouseDistrictID",
                schema: "PRJ",
                table: "TitledeedDetailHistory");

            migrationBuilder.DropColumn(
                name: "HouseMoo",
                schema: "PRJ",
                table: "TitledeedDetailHistory");

            migrationBuilder.DropColumn(
                name: "HouseNo",
                schema: "PRJ",
                table: "TitledeedDetailHistory");

            migrationBuilder.DropColumn(
                name: "HousePostalCode",
                schema: "PRJ",
                table: "TitledeedDetailHistory");

            migrationBuilder.DropColumn(
                name: "HouseProvinceID",
                schema: "PRJ",
                table: "TitledeedDetailHistory");

            migrationBuilder.DropColumn(
                name: "HouseRoadEN",
                schema: "PRJ",
                table: "TitledeedDetailHistory");

            migrationBuilder.DropColumn(
                name: "HouseRoadTH",
                schema: "PRJ",
                table: "TitledeedDetailHistory");

            migrationBuilder.DropColumn(
                name: "HouseSoiEN",
                schema: "PRJ",
                table: "TitledeedDetailHistory");

            migrationBuilder.DropColumn(
                name: "HouseSoiTH",
                schema: "PRJ",
                table: "TitledeedDetailHistory");

            migrationBuilder.DropColumn(
                name: "HouseSubDistrictID",
                schema: "PRJ",
                table: "TitledeedDetailHistory");

            migrationBuilder.DropColumn(
                name: "IsSameAddressAsTitledeed",
                schema: "PRJ",
                table: "TitledeedDetailHistory");

            migrationBuilder.DropColumn(
                name: "LandOfficeID",
                schema: "PRJ",
                table: "TitledeedDetailHistory");

            migrationBuilder.DropColumn(
                name: "ParkingArea",
                schema: "PRJ",
                table: "TitledeedDetailHistory");

            migrationBuilder.DropColumn(
                name: "TitleDeedAddress",
                schema: "PRJ",
                table: "TitledeedDetailHistory");

            migrationBuilder.DropColumn(
                name: "UsedArea",
                schema: "PRJ",
                table: "TitledeedDetailHistory");

            migrationBuilder.DropColumn(
                name: "YearGotHouseNo",
                schema: "PRJ",
                table: "TitledeedDetailHistory");

            migrationBuilder.DropColumn(
                name: "AirArea",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropColumn(
                name: "BalconyArea",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropColumn(
                name: "FenceArea",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropColumn(
                name: "FenceIronArea",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropColumn(
                name: "HouseDistrictID",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropColumn(
                name: "HouseMoo",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropColumn(
                name: "HouseNo",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropColumn(
                name: "HousePostalCode",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropColumn(
                name: "HouseProvinceID",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropColumn(
                name: "HouseRoadEN",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropColumn(
                name: "HouseRoadTH",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropColumn(
                name: "HouseSoiEN",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropColumn(
                name: "HouseSoiTH",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropColumn(
                name: "HouseSubDistrictID",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropColumn(
                name: "IsSameAddressAsTitledeed",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropColumn(
                name: "LandOfficeID",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropColumn(
                name: "ParkingArea",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropColumn(
                name: "TitleDeedAddress",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropColumn(
                name: "UsedArea",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropColumn(
                name: "YearGotHouseNo",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.RenameColumn(
                name: "YearGotHouseNo",
                schema: "PRJ",
                table: "Unit",
                newName: "HouseNoReceivedYear");

            migrationBuilder.AddColumn<double>(
                name: "AirArea",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "BalconyArea",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CensusHouseNo",
                schema: "PRJ",
                table: "Unit",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FenceArea",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FenceIronArea",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "HouseDistrictID",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HouseMoo",
                schema: "PRJ",
                table: "Unit",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HousePostalCode",
                schema: "PRJ",
                table: "Unit",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "HouseProvinceID",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HouseRoadEN",
                schema: "PRJ",
                table: "Unit",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HouseRoadTH",
                schema: "PRJ",
                table: "Unit",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HouseSoiEN",
                schema: "PRJ",
                table: "Unit",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HouseSoiTH",
                schema: "PRJ",
                table: "Unit",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "HouseSubDistrictID",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSameAddressAsTitledeed",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LandOfficeID",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ParkingArea",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "UsedArea",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Unit_HouseDistrictID",
                schema: "PRJ",
                table: "Unit",
                column: "HouseDistrictID");

            migrationBuilder.CreateIndex(
                name: "IX_Unit_HouseProvinceID",
                schema: "PRJ",
                table: "Unit",
                column: "HouseProvinceID");

            migrationBuilder.CreateIndex(
                name: "IX_Unit_HouseSubDistrictID",
                schema: "PRJ",
                table: "Unit",
                column: "HouseSubDistrictID");

            migrationBuilder.CreateIndex(
                name: "IX_Unit_LandOfficeID",
                schema: "PRJ",
                table: "Unit",
                column: "LandOfficeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Unit_District_HouseDistrictID",
                schema: "PRJ",
                table: "Unit",
                column: "HouseDistrictID",
                principalSchema: "MST",
                principalTable: "District",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Unit_Province_HouseProvinceID",
                schema: "PRJ",
                table: "Unit",
                column: "HouseProvinceID",
                principalSchema: "MST",
                principalTable: "Province",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Unit_SubDistrict_HouseSubDistrictID",
                schema: "PRJ",
                table: "Unit",
                column: "HouseSubDistrictID",
                principalSchema: "MST",
                principalTable: "SubDistrict",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Unit_LandOffice_LandOfficeID",
                schema: "PRJ",
                table: "Unit",
                column: "LandOfficeID",
                principalSchema: "MST",
                principalTable: "LandOffice",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Unit_District_HouseDistrictID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropForeignKey(
                name: "FK_Unit_Province_HouseProvinceID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropForeignKey(
                name: "FK_Unit_SubDistrict_HouseSubDistrictID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropForeignKey(
                name: "FK_Unit_LandOffice_LandOfficeID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropIndex(
                name: "IX_Unit_HouseDistrictID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropIndex(
                name: "IX_Unit_HouseProvinceID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropIndex(
                name: "IX_Unit_HouseSubDistrictID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropIndex(
                name: "IX_Unit_LandOfficeID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "AirArea",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "BalconyArea",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "CensusHouseNo",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "FenceArea",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "FenceIronArea",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "HouseDistrictID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "HouseMoo",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "HousePostalCode",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "HouseProvinceID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "HouseRoadEN",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "HouseRoadTH",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "HouseSoiEN",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "HouseSoiTH",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "HouseSubDistrictID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "IsSameAddressAsTitledeed",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "LandOfficeID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "ParkingArea",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "UsedArea",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.RenameColumn(
                name: "HouseNoReceivedYear",
                schema: "PRJ",
                table: "Unit",
                newName: "YearGotHouseNo");

            migrationBuilder.AddColumn<double>(
                name: "AirArea",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "BalconyArea",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FenceArea",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FenceIronArea",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "HouseDistrictID",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HouseMoo",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HouseNo",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HousePostalCode",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "HouseProvinceID",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HouseRoadEN",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HouseRoadTH",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HouseSoiEN",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HouseSoiTH",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "HouseSubDistrictID",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSameAddressAsTitledeed",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LandOfficeID",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ParkingArea",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleDeedAddress",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "UsedArea",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "YearGotHouseNo",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "AirArea",
                schema: "PRJ",
                table: "TitledeedDetail",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "BalconyArea",
                schema: "PRJ",
                table: "TitledeedDetail",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FenceArea",
                schema: "PRJ",
                table: "TitledeedDetail",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FenceIronArea",
                schema: "PRJ",
                table: "TitledeedDetail",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "HouseDistrictID",
                schema: "PRJ",
                table: "TitledeedDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HouseMoo",
                schema: "PRJ",
                table: "TitledeedDetail",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HouseNo",
                schema: "PRJ",
                table: "TitledeedDetail",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HousePostalCode",
                schema: "PRJ",
                table: "TitledeedDetail",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "HouseProvinceID",
                schema: "PRJ",
                table: "TitledeedDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HouseRoadEN",
                schema: "PRJ",
                table: "TitledeedDetail",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HouseRoadTH",
                schema: "PRJ",
                table: "TitledeedDetail",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HouseSoiEN",
                schema: "PRJ",
                table: "TitledeedDetail",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HouseSoiTH",
                schema: "PRJ",
                table: "TitledeedDetail",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "HouseSubDistrictID",
                schema: "PRJ",
                table: "TitledeedDetail",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSameAddressAsTitledeed",
                schema: "PRJ",
                table: "TitledeedDetail",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LandOfficeID",
                schema: "PRJ",
                table: "TitledeedDetail",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ParkingArea",
                schema: "PRJ",
                table: "TitledeedDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleDeedAddress",
                schema: "PRJ",
                table: "TitledeedDetail",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "UsedArea",
                schema: "PRJ",
                table: "TitledeedDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "YearGotHouseNo",
                schema: "PRJ",
                table: "TitledeedDetail",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TitledeedDetailHistory_HouseDistrictID",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                column: "HouseDistrictID");

            migrationBuilder.CreateIndex(
                name: "IX_TitledeedDetailHistory_HouseProvinceID",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                column: "HouseProvinceID");

            migrationBuilder.CreateIndex(
                name: "IX_TitledeedDetailHistory_HouseSubDistrictID",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                column: "HouseSubDistrictID");

            migrationBuilder.CreateIndex(
                name: "IX_TitledeedDetailHistory_LandOfficeID",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                column: "LandOfficeID");

            migrationBuilder.CreateIndex(
                name: "IX_TitledeedDetail_HouseDistrictID",
                schema: "PRJ",
                table: "TitledeedDetail",
                column: "HouseDistrictID");

            migrationBuilder.CreateIndex(
                name: "IX_TitledeedDetail_HouseProvinceID",
                schema: "PRJ",
                table: "TitledeedDetail",
                column: "HouseProvinceID");

            migrationBuilder.CreateIndex(
                name: "IX_TitledeedDetail_HouseSubDistrictID",
                schema: "PRJ",
                table: "TitledeedDetail",
                column: "HouseSubDistrictID");

            migrationBuilder.CreateIndex(
                name: "IX_TitledeedDetail_LandOfficeID",
                schema: "PRJ",
                table: "TitledeedDetail",
                column: "LandOfficeID");

            migrationBuilder.AddForeignKey(
                name: "FK_TitledeedDetail_District_HouseDistrictID",
                schema: "PRJ",
                table: "TitledeedDetail",
                column: "HouseDistrictID",
                principalSchema: "MST",
                principalTable: "District",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TitledeedDetail_Province_HouseProvinceID",
                schema: "PRJ",
                table: "TitledeedDetail",
                column: "HouseProvinceID",
                principalSchema: "MST",
                principalTable: "Province",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TitledeedDetail_SubDistrict_HouseSubDistrictID",
                schema: "PRJ",
                table: "TitledeedDetail",
                column: "HouseSubDistrictID",
                principalSchema: "MST",
                principalTable: "SubDistrict",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TitledeedDetail_LandOffice_LandOfficeID",
                schema: "PRJ",
                table: "TitledeedDetail",
                column: "LandOfficeID",
                principalSchema: "MST",
                principalTable: "LandOffice",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TitledeedDetailHistory_District_HouseDistrictID",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                column: "HouseDistrictID",
                principalSchema: "MST",
                principalTable: "District",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TitledeedDetailHistory_Province_HouseProvinceID",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                column: "HouseProvinceID",
                principalSchema: "MST",
                principalTable: "Province",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TitledeedDetailHistory_SubDistrict_HouseSubDistrictID",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                column: "HouseSubDistrictID",
                principalSchema: "MST",
                principalTable: "SubDistrict",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TitledeedDetailHistory_LandOffice_LandOfficeID",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                column: "LandOfficeID",
                principalSchema: "MST",
                principalTable: "LandOffice",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
