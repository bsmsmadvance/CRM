using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class UpdateUnitAndTitledeedForDataMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Model_MasterCenter_UnitTypeMasterCenterID",
                schema: "PRJ",
                table: "Model");

            migrationBuilder.DropForeignKey(
                name: "FK_TitledeedDetail_Project_ProjectID",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.RenameColumn(
                name: "SapwbsObject",
                schema: "PRJ",
                table: "Unit",
                newName: "SAPWBSObject");

            migrationBuilder.RenameColumn(
                name: "SapwbsNo",
                schema: "PRJ",
                table: "Unit",
                newName: "SAPWBSNo");

            migrationBuilder.RenameColumn(
                name: "UnitNo",
                schema: "PRJ",
                table: "TitledeedDetail",
                newName: "LandPortionNo");

            migrationBuilder.RenameColumn(
                name: "UnitTypeMasterCenterID",
                schema: "PRJ",
                table: "Model",
                newName: "ModelUnitTypeMasterCenterID");

            migrationBuilder.RenameIndex(
                name: "IX_Model_UnitTypeMasterCenterID",
                schema: "PRJ",
                table: "Model",
                newName: "IX_Model_ModelUnitTypeMasterCenterID");

            migrationBuilder.RenameColumn(
                name: "FloorNameTH",
                schema: "PRJ",
                table: "Floor",
                newName: "NameTH");

            migrationBuilder.RenameColumn(
                name: "FloorNameEN",
                schema: "PRJ",
                table: "Floor",
                newName: "NameEN");

            migrationBuilder.RenameColumn(
                name: "FloorFilename",
                schema: "PRJ",
                table: "Floor",
                newName: "FileAttachment");

            migrationBuilder.AddColumn<double>(
                name: "AirArea",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "BuiltInArea",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "CurvedSteelArea",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FactorArea",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModelID",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ParkingArea",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SAPWBSStatus",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "UnitLoanAmount",
                schema: "PRJ",
                table: "Unit",
                type: "Money",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "VerandaArea",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectID",
                schema: "PRJ",
                table: "TitledeedDetail",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<double>(
                name: "LandSurveyArea",
                schema: "PRJ",
                table: "TitledeedDetail",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UnitID",
                schema: "PRJ",
                table: "TitledeedDetail",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Unit_ModelID",
                schema: "PRJ",
                table: "Unit",
                column: "ModelID");

            migrationBuilder.CreateIndex(
                name: "IX_TitledeedDetail_UnitID",
                schema: "PRJ",
                table: "TitledeedDetail",
                column: "UnitID");

            migrationBuilder.AddForeignKey(
                name: "FK_Model_MasterCenter_ModelUnitTypeMasterCenterID",
                schema: "PRJ",
                table: "Model",
                column: "ModelUnitTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TitledeedDetail_Project_ProjectID",
                schema: "PRJ",
                table: "TitledeedDetail",
                column: "ProjectID",
                principalSchema: "PRJ",
                principalTable: "Project",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TitledeedDetail_Unit_UnitID",
                schema: "PRJ",
                table: "TitledeedDetail",
                column: "UnitID",
                principalSchema: "PRJ",
                principalTable: "Unit",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Unit_Model_ModelID",
                schema: "PRJ",
                table: "Unit",
                column: "ModelID",
                principalSchema: "PRJ",
                principalTable: "Model",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Model_MasterCenter_ModelUnitTypeMasterCenterID",
                schema: "PRJ",
                table: "Model");

            migrationBuilder.DropForeignKey(
                name: "FK_TitledeedDetail_Project_ProjectID",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_TitledeedDetail_Unit_UnitID",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_Unit_Model_ModelID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropIndex(
                name: "IX_Unit_ModelID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropIndex(
                name: "IX_TitledeedDetail_UnitID",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropColumn(
                name: "AirArea",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "BuiltInArea",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "CurvedSteelArea",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "FactorArea",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "ModelID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "ParkingArea",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "SAPWBSStatus",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "UnitLoanAmount",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "VerandaArea",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "LandSurveyArea",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.DropColumn(
                name: "UnitID",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.RenameColumn(
                name: "SAPWBSObject",
                schema: "PRJ",
                table: "Unit",
                newName: "SapwbsObject");

            migrationBuilder.RenameColumn(
                name: "SAPWBSNo",
                schema: "PRJ",
                table: "Unit",
                newName: "SapwbsNo");

            migrationBuilder.RenameColumn(
                name: "LandPortionNo",
                schema: "PRJ",
                table: "TitledeedDetail",
                newName: "UnitNo");

            migrationBuilder.RenameColumn(
                name: "ModelUnitTypeMasterCenterID",
                schema: "PRJ",
                table: "Model",
                newName: "UnitTypeMasterCenterID");

            migrationBuilder.RenameIndex(
                name: "IX_Model_ModelUnitTypeMasterCenterID",
                schema: "PRJ",
                table: "Model",
                newName: "IX_Model_UnitTypeMasterCenterID");

            migrationBuilder.RenameColumn(
                name: "NameTH",
                schema: "PRJ",
                table: "Floor",
                newName: "FloorNameTH");

            migrationBuilder.RenameColumn(
                name: "NameEN",
                schema: "PRJ",
                table: "Floor",
                newName: "FloorNameEN");

            migrationBuilder.RenameColumn(
                name: "FileAttachment",
                schema: "PRJ",
                table: "Floor",
                newName: "FloorFilename");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectID",
                schema: "PRJ",
                table: "TitledeedDetail",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Model_MasterCenter_UnitTypeMasterCenterID",
                schema: "PRJ",
                table: "Model",
                column: "UnitTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TitledeedDetail_Project_ProjectID",
                schema: "PRJ",
                table: "TitledeedDetail",
                column: "ProjectID",
                principalSchema: "PRJ",
                principalTable: "Project",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
