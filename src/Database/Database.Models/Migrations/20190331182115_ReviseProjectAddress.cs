using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ReviseProjectAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMainAddress",
                schema: "PRJ",
                table: "Address");

            migrationBuilder.RenameColumn(
                name: "VillageNo",
                schema: "PRJ",
                table: "Address",
                newName: "TitledeedSoiTH");

            migrationBuilder.RenameColumn(
                name: "SubDistrict",
                schema: "PRJ",
                table: "Address",
                newName: "TitledeedSoiEN");

            migrationBuilder.RenameColumn(
                name: "Province",
                schema: "PRJ",
                table: "Address",
                newName: "TitledeedRoadTH");

            migrationBuilder.RenameColumn(
                name: "LaneTH",
                schema: "PRJ",
                table: "Address",
                newName: "TitledeedRoadEN");

            migrationBuilder.RenameColumn(
                name: "LaneEN",
                schema: "PRJ",
                table: "Address",
                newName: "TitledeedMoo");

            migrationBuilder.RenameColumn(
                name: "District",
                schema: "PRJ",
                table: "Address",
                newName: "SoiTH");

            migrationBuilder.RenameColumn(
                name: "CategoryType",
                schema: "PRJ",
                table: "Address",
                newName: "SoiEN");

            migrationBuilder.AddColumn<Guid>(
                name: "DistrictID",
                schema: "PRJ",
                table: "Address",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HouseMoo",
                schema: "PRJ",
                table: "Address",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HouseRoadEN",
                schema: "PRJ",
                table: "Address",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HouseRoadTH",
                schema: "PRJ",
                table: "Address",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HouseSoiEN",
                schema: "PRJ",
                table: "Address",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HouseSoiTH",
                schema: "PRJ",
                table: "Address",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "HouseSubDistrictID",
                schema: "PRJ",
                table: "Address",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InspectionNo",
                schema: "PRJ",
                table: "Address",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LandNo",
                schema: "PRJ",
                table: "Address",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LandOfficeID",
                schema: "PRJ",
                table: "Address",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Moo",
                schema: "PRJ",
                table: "Address",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectAddressTypeMasterCenterID",
                schema: "PRJ",
                table: "Address",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProvinceID",
                schema: "PRJ",
                table: "Address",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SubDistrictID",
                schema: "PRJ",
                table: "Address",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TitledeedSubDistrictID",
                schema: "PRJ",
                table: "Address",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Address_DistrictID",
                schema: "PRJ",
                table: "Address",
                column: "DistrictID");

            migrationBuilder.CreateIndex(
                name: "IX_Address_HouseSubDistrictID",
                schema: "PRJ",
                table: "Address",
                column: "HouseSubDistrictID");

            migrationBuilder.CreateIndex(
                name: "IX_Address_LandOfficeID",
                schema: "PRJ",
                table: "Address",
                column: "LandOfficeID");

            migrationBuilder.CreateIndex(
                name: "IX_Address_ProjectAddressTypeMasterCenterID",
                schema: "PRJ",
                table: "Address",
                column: "ProjectAddressTypeMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_Address_ProvinceID",
                schema: "PRJ",
                table: "Address",
                column: "ProvinceID");

            migrationBuilder.CreateIndex(
                name: "IX_Address_SubDistrictID",
                schema: "PRJ",
                table: "Address",
                column: "SubDistrictID");

            migrationBuilder.CreateIndex(
                name: "IX_Address_TitledeedSubDistrictID",
                schema: "PRJ",
                table: "Address",
                column: "TitledeedSubDistrictID");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_District_DistrictID",
                schema: "PRJ",
                table: "Address",
                column: "DistrictID",
                principalSchema: "MST",
                principalTable: "District",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Address_SubDistrict_HouseSubDistrictID",
                schema: "PRJ",
                table: "Address",
                column: "HouseSubDistrictID",
                principalSchema: "MST",
                principalTable: "SubDistrict",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Address_LandOffice_LandOfficeID",
                schema: "PRJ",
                table: "Address",
                column: "LandOfficeID",
                principalSchema: "MST",
                principalTable: "LandOffice",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Address_MasterCenter_ProjectAddressTypeMasterCenterID",
                schema: "PRJ",
                table: "Address",
                column: "ProjectAddressTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Province_ProvinceID",
                schema: "PRJ",
                table: "Address",
                column: "ProvinceID",
                principalSchema: "MST",
                principalTable: "Province",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Address_SubDistrict_SubDistrictID",
                schema: "PRJ",
                table: "Address",
                column: "SubDistrictID",
                principalSchema: "MST",
                principalTable: "SubDistrict",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Address_SubDistrict_TitledeedSubDistrictID",
                schema: "PRJ",
                table: "Address",
                column: "TitledeedSubDistrictID",
                principalSchema: "MST",
                principalTable: "SubDistrict",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_District_DistrictID",
                schema: "PRJ",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Address_SubDistrict_HouseSubDistrictID",
                schema: "PRJ",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Address_LandOffice_LandOfficeID",
                schema: "PRJ",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Address_MasterCenter_ProjectAddressTypeMasterCenterID",
                schema: "PRJ",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Address_Province_ProvinceID",
                schema: "PRJ",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Address_SubDistrict_SubDistrictID",
                schema: "PRJ",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Address_SubDistrict_TitledeedSubDistrictID",
                schema: "PRJ",
                table: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Address_DistrictID",
                schema: "PRJ",
                table: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Address_HouseSubDistrictID",
                schema: "PRJ",
                table: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Address_LandOfficeID",
                schema: "PRJ",
                table: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Address_ProjectAddressTypeMasterCenterID",
                schema: "PRJ",
                table: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Address_ProvinceID",
                schema: "PRJ",
                table: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Address_SubDistrictID",
                schema: "PRJ",
                table: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Address_TitledeedSubDistrictID",
                schema: "PRJ",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "DistrictID",
                schema: "PRJ",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "HouseMoo",
                schema: "PRJ",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "HouseRoadEN",
                schema: "PRJ",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "HouseRoadTH",
                schema: "PRJ",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "HouseSoiEN",
                schema: "PRJ",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "HouseSoiTH",
                schema: "PRJ",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "HouseSubDistrictID",
                schema: "PRJ",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "InspectionNo",
                schema: "PRJ",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "LandNo",
                schema: "PRJ",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "LandOfficeID",
                schema: "PRJ",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "Moo",
                schema: "PRJ",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "ProjectAddressTypeMasterCenterID",
                schema: "PRJ",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "ProvinceID",
                schema: "PRJ",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "SubDistrictID",
                schema: "PRJ",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "TitledeedSubDistrictID",
                schema: "PRJ",
                table: "Address");

            migrationBuilder.RenameColumn(
                name: "TitledeedSoiTH",
                schema: "PRJ",
                table: "Address",
                newName: "VillageNo");

            migrationBuilder.RenameColumn(
                name: "TitledeedSoiEN",
                schema: "PRJ",
                table: "Address",
                newName: "SubDistrict");

            migrationBuilder.RenameColumn(
                name: "TitledeedRoadTH",
                schema: "PRJ",
                table: "Address",
                newName: "Province");

            migrationBuilder.RenameColumn(
                name: "TitledeedRoadEN",
                schema: "PRJ",
                table: "Address",
                newName: "LaneTH");

            migrationBuilder.RenameColumn(
                name: "TitledeedMoo",
                schema: "PRJ",
                table: "Address",
                newName: "LaneEN");

            migrationBuilder.RenameColumn(
                name: "SoiTH",
                schema: "PRJ",
                table: "Address",
                newName: "District");

            migrationBuilder.RenameColumn(
                name: "SoiEN",
                schema: "PRJ",
                table: "Address",
                newName: "CategoryType");

            migrationBuilder.AddColumn<bool>(
                name: "IsMainAddress",
                schema: "PRJ",
                table: "Address",
                nullable: false,
                defaultValue: false);
        }
    }
}
