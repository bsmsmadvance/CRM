using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class add_alter_col_BKOwnerBKOwAddrTROwner_bas_serm_kim : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OtherSubDistrict",
                schema: "SAL",
                table: "BookingOwnerAddress",
                newName: "OtherSubDistrictTH");

            migrationBuilder.RenameColumn(
                name: "OtherProvince",
                schema: "SAL",
                table: "BookingOwnerAddress",
                newName: "OtherSubDistrictEN");

            migrationBuilder.RenameColumn(
                name: "OtherDistrict",
                schema: "SAL",
                table: "BookingOwnerAddress",
                newName: "OtherProvinceTH");

            migrationBuilder.RenameColumn(
                name: "OtherCountry",
                schema: "SAL",
                table: "BookingOwnerAddress",
                newName: "OtherProvinceEN");

            migrationBuilder.AlterColumn<string>(
                name: "MobileNumber",
                schema: "SAL",
                table: "TransferOwner",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MarriageStatusMasterCenterID",
                schema: "SAL",
                table: "TransferOwner",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MarriageTitleTHMasterCenterID",
                schema: "SAL",
                table: "TransferOwner",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherCountryEN",
                schema: "SAL",
                table: "BookingOwnerAddress",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherCountryTH",
                schema: "SAL",
                table: "BookingOwnerAddress",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherDistrictEN",
                schema: "SAL",
                table: "BookingOwnerAddress",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherDistrictTH",
                schema: "SAL",
                table: "BookingOwnerAddress",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherNational",
                schema: "SAL",
                table: "BookingOwner",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransferOwner_MarriageStatusMasterCenterID",
                schema: "SAL",
                table: "TransferOwner",
                column: "MarriageStatusMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferOwner_MarriageTitleTHMasterCenterID",
                schema: "SAL",
                table: "TransferOwner",
                column: "MarriageTitleTHMasterCenterID");

            migrationBuilder.AddForeignKey(
                name: "FK_TransferOwner_MasterCenter_MarriageStatusMasterCenterID",
                schema: "SAL",
                table: "TransferOwner",
                column: "MarriageStatusMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferOwner_MasterCenter_MarriageTitleTHMasterCenterID",
                schema: "SAL",
                table: "TransferOwner",
                column: "MarriageTitleTHMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransferOwner_MasterCenter_MarriageStatusMasterCenterID",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferOwner_MasterCenter_MarriageTitleTHMasterCenterID",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropIndex(
                name: "IX_TransferOwner_MarriageStatusMasterCenterID",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropIndex(
                name: "IX_TransferOwner_MarriageTitleTHMasterCenterID",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "MarriageStatusMasterCenterID",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "MarriageTitleTHMasterCenterID",
                schema: "SAL",
                table: "TransferOwner");

            migrationBuilder.DropColumn(
                name: "OtherCountryEN",
                schema: "SAL",
                table: "BookingOwnerAddress");

            migrationBuilder.DropColumn(
                name: "OtherCountryTH",
                schema: "SAL",
                table: "BookingOwnerAddress");

            migrationBuilder.DropColumn(
                name: "OtherDistrictEN",
                schema: "SAL",
                table: "BookingOwnerAddress");

            migrationBuilder.DropColumn(
                name: "OtherDistrictTH",
                schema: "SAL",
                table: "BookingOwnerAddress");

            migrationBuilder.DropColumn(
                name: "OtherNational",
                schema: "SAL",
                table: "BookingOwner");

            migrationBuilder.RenameColumn(
                name: "OtherSubDistrictTH",
                schema: "SAL",
                table: "BookingOwnerAddress",
                newName: "OtherSubDistrict");

            migrationBuilder.RenameColumn(
                name: "OtherSubDistrictEN",
                schema: "SAL",
                table: "BookingOwnerAddress",
                newName: "OtherProvince");

            migrationBuilder.RenameColumn(
                name: "OtherProvinceTH",
                schema: "SAL",
                table: "BookingOwnerAddress",
                newName: "OtherDistrict");

            migrationBuilder.RenameColumn(
                name: "OtherProvinceEN",
                schema: "SAL",
                table: "BookingOwnerAddress",
                newName: "OtherCountry");

            migrationBuilder.AlterColumn<string>(
                name: "MobileNumber",
                schema: "SAL",
                table: "TransferOwner",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
