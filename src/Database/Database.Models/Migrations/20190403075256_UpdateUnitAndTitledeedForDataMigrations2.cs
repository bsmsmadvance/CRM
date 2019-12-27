using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class UpdateUnitAndTitledeedForDataMigrations2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitArea",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "YearGotHouseNo",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "Address",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropColumn(
                name: "AddressDistrict",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropColumn(
                name: "AddressLaneEN",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropColumn(
                name: "AddressLaneTH",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropColumn(
                name: "Titledeed",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.RenameColumn(
                name: "UnitStatus",
                schema: "PRJ",
                table: "Unit",
                newName: "UnitLayoutType");

            migrationBuilder.RenameColumn(
                name: "PostalCode",
                schema: "PRJ",
                table: "TitledeedDetail",
                newName: "LandStatusNote");

            migrationBuilder.RenameColumn(
                name: "IsSameLocation",
                schema: "PRJ",
                table: "TitledeedDetail",
                newName: "IsSameAddressAsTitledeed");

            migrationBuilder.RenameColumn(
                name: "DepartmentOfLand",
                schema: "PRJ",
                table: "TitledeedDetail",
                newName: "HouseSoiTH");

            migrationBuilder.RenameColumn(
                name: "AddressVillageNo",
                schema: "PRJ",
                table: "TitledeedDetail",
                newName: "HouseSoiEN");

            migrationBuilder.RenameColumn(
                name: "AddressSubDistrict",
                schema: "PRJ",
                table: "TitledeedDetail",
                newName: "HouseRoadTH");

            migrationBuilder.RenameColumn(
                name: "AddressRoadTH",
                schema: "PRJ",
                table: "TitledeedDetail",
                newName: "HouseRoadEN");

            migrationBuilder.RenameColumn(
                name: "AddressRoadEN",
                schema: "PRJ",
                table: "TitledeedDetail",
                newName: "HousePostalCode");

            migrationBuilder.RenameColumn(
                name: "AddressProvince",
                schema: "PRJ",
                table: "TitledeedDetail",
                newName: "HouseMoo");

            migrationBuilder.AddColumn<Guid>(
                name: "AssetTypeMasterCenterID",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConstructionModelID",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "FirstFlag",
                schema: "PRJ",
                table: "Unit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "GLRaiseBatchID",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsUpdate",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UnitStatusMasterCenterID",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "YearGotHouseNo",
                schema: "PRJ",
                table: "TitledeedDetail",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<double>(
                name: "UsedArea",
                schema: "PRJ",
                table: "TitledeedDetail",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<double>(
                name: "ParkingArea",
                schema: "PRJ",
                table: "TitledeedDetail",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<double>(
                name: "FenceIronArea",
                schema: "PRJ",
                table: "TitledeedDetail",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<double>(
                name: "FenceArea",
                schema: "PRJ",
                table: "TitledeedDetail",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<double>(
                name: "BalconyArea",
                schema: "PRJ",
                table: "TitledeedDetail",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<double>(
                name: "AirArea",
                schema: "PRJ",
                table: "TitledeedDetail",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AddColumn<Guid>(
                name: "HouseDistrictID",
                schema: "PRJ",
                table: "TitledeedDetail",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "HouseProvinceID",
                schema: "PRJ",
                table: "TitledeedDetail",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "HouseSubDistrictID",
                schema: "PRJ",
                table: "TitledeedDetail",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LandOfficeID",
                schema: "PRJ",
                table: "TitledeedDetail",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LandStatusDate",
                schema: "PRJ",
                table: "TitledeedDetail",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LandStatusMasterCenterID",
                schema: "PRJ",
                table: "TitledeedDetail",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TitledeedArea",
                schema: "PRJ",
                table: "TitledeedDetail",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Unit_AssetTypeMasterCenterID",
                schema: "PRJ",
                table: "Unit",
                column: "AssetTypeMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_Unit_UnitStatusMasterCenterID",
                schema: "PRJ",
                table: "Unit",
                column: "UnitStatusMasterCenterID");

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

            migrationBuilder.CreateIndex(
                name: "IX_TitledeedDetail_LandStatusMasterCenterID",
                schema: "PRJ",
                table: "TitledeedDetail",
                column: "LandStatusMasterCenterID");

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
                name: "FK_TitledeedDetail_MasterCenter_LandStatusMasterCenterID",
                schema: "PRJ",
                table: "TitledeedDetail",
                column: "LandStatusMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Unit_MasterCenter_AssetTypeMasterCenterID",
                schema: "PRJ",
                table: "Unit",
                column: "AssetTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Unit_MasterCenter_UnitStatusMasterCenterID",
                schema: "PRJ",
                table: "Unit",
                column: "UnitStatusMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "FK_TitledeedDetail_MasterCenter_LandStatusMasterCenterID",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_Unit_MasterCenter_AssetTypeMasterCenterID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropForeignKey(
                name: "FK_Unit_MasterCenter_UnitStatusMasterCenterID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropIndex(
                name: "IX_Unit_AssetTypeMasterCenterID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropIndex(
                name: "IX_Unit_UnitStatusMasterCenterID",
                schema: "PRJ",
                table: "Unit");

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

            migrationBuilder.DropIndex(
                name: "IX_TitledeedDetail_LandStatusMasterCenterID",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropColumn(
                name: "AssetTypeMasterCenterID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "ConstructionModelID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "FirstFlag",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "GLRaiseBatchID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "IsUpdate",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "UnitStatusMasterCenterID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "HouseDistrictID",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropColumn(
                name: "HouseProvinceID",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropColumn(
                name: "HouseSubDistrictID",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropColumn(
                name: "LandOfficeID",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropColumn(
                name: "LandStatusDate",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropColumn(
                name: "LandStatusMasterCenterID",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropColumn(
                name: "TitledeedArea",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.RenameColumn(
                name: "UnitLayoutType",
                schema: "PRJ",
                table: "Unit",
                newName: "UnitStatus");

            migrationBuilder.RenameColumn(
                name: "LandStatusNote",
                schema: "PRJ",
                table: "TitledeedDetail",
                newName: "PostalCode");

            migrationBuilder.RenameColumn(
                name: "IsSameAddressAsTitledeed",
                schema: "PRJ",
                table: "TitledeedDetail",
                newName: "IsSameLocation");

            migrationBuilder.RenameColumn(
                name: "HouseSoiTH",
                schema: "PRJ",
                table: "TitledeedDetail",
                newName: "DepartmentOfLand");

            migrationBuilder.RenameColumn(
                name: "HouseSoiEN",
                schema: "PRJ",
                table: "TitledeedDetail",
                newName: "AddressVillageNo");

            migrationBuilder.RenameColumn(
                name: "HouseRoadTH",
                schema: "PRJ",
                table: "TitledeedDetail",
                newName: "AddressSubDistrict");

            migrationBuilder.RenameColumn(
                name: "HouseRoadEN",
                schema: "PRJ",
                table: "TitledeedDetail",
                newName: "AddressRoadTH");

            migrationBuilder.RenameColumn(
                name: "HousePostalCode",
                schema: "PRJ",
                table: "TitledeedDetail",
                newName: "AddressRoadEN");

            migrationBuilder.RenameColumn(
                name: "HouseMoo",
                schema: "PRJ",
                table: "TitledeedDetail",
                newName: "AddressProvince");

            migrationBuilder.AddColumn<double>(
                name: "UnitArea",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "YearGotHouseNo",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "YearGotHouseNo",
                schema: "PRJ",
                table: "TitledeedDetail",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "UsedArea",
                schema: "PRJ",
                table: "TitledeedDetail",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "ParkingArea",
                schema: "PRJ",
                table: "TitledeedDetail",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "FenceIronArea",
                schema: "PRJ",
                table: "TitledeedDetail",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "FenceArea",
                schema: "PRJ",
                table: "TitledeedDetail",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "BalconyArea",
                schema: "PRJ",
                table: "TitledeedDetail",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "AirArea",
                schema: "PRJ",
                table: "TitledeedDetail",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                schema: "PRJ",
                table: "TitledeedDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressDistrict",
                schema: "PRJ",
                table: "TitledeedDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressLaneEN",
                schema: "PRJ",
                table: "TitledeedDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressLaneTH",
                schema: "PRJ",
                table: "TitledeedDetail",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Titledeed",
                schema: "PRJ",
                table: "TitledeedDetail",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
