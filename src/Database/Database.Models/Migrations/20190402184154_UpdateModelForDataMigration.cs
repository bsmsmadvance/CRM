using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class UpdateModelForDataMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Model_MasterCenter_CategoryMasterCenterID",
                schema: "PRJ",
                table: "Model");

            migrationBuilder.DropForeignKey(
                name: "FK_Model_MasterCenter_StyleMasterCenterID",
                schema: "PRJ",
                table: "Model");

            migrationBuilder.DropForeignKey(
                name: "FK_Model_MasterCenter_TypeMasterCenterID",
                schema: "PRJ",
                table: "Model");

            migrationBuilder.DropForeignKey(
                name: "FK_WaterElectricMeterPrice_Project_ProjectID",
                schema: "PRJ",
                table: "WaterElectricMeterPrice");

            migrationBuilder.DropIndex(
                name: "IX_WaterElectricMeterPrice_ProjectID",
                schema: "PRJ",
                table: "WaterElectricMeterPrice");

            migrationBuilder.DropColumn(
                name: "ProjectID",
                schema: "PRJ",
                table: "WaterElectricMeterPrice");

            migrationBuilder.DropColumn(
                name: "ModelDesc",
                schema: "PRJ",
                table: "Model");

            migrationBuilder.DropColumn(
                name: "PowerMeterPrice",
                schema: "PRJ",
                table: "Model");

            migrationBuilder.DropColumn(
                name: "WaterPowerMeterPrice",
                schema: "PRJ",
                table: "Model");

            migrationBuilder.RenameColumn(
                name: "WaterPowerMeter",
                schema: "PRJ",
                table: "Model",
                newName: "PreferUnitMinimum");

            migrationBuilder.RenameColumn(
                name: "TypeMasterCenterID",
                schema: "PRJ",
                table: "Model",
                newName: "UnitTypeMasterCenterID");

            migrationBuilder.RenameColumn(
                name: "StyleMasterCenterID",
                schema: "PRJ",
                table: "Model",
                newName: "TypeOfRealEstateID");

            migrationBuilder.RenameColumn(
                name: "ShortName",
                schema: "PRJ",
                table: "Model",
                newName: "NameTH");

            migrationBuilder.RenameColumn(
                name: "PowerMeter",
                schema: "PRJ",
                table: "Model",
                newName: "PreferUnit");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "PRJ",
                table: "Model",
                newName: "NameEN");

            migrationBuilder.RenameColumn(
                name: "Frontage",
                schema: "PRJ",
                table: "Model",
                newName: "PreferHouse");

            migrationBuilder.RenameColumn(
                name: "CategoryMasterCenterID",
                schema: "PRJ",
                table: "Model",
                newName: "ModelTypeMasterCenterID");

            migrationBuilder.RenameIndex(
                name: "IX_Model_TypeMasterCenterID",
                schema: "PRJ",
                table: "Model",
                newName: "IX_Model_UnitTypeMasterCenterID");

            migrationBuilder.RenameIndex(
                name: "IX_Model_StyleMasterCenterID",
                schema: "PRJ",
                table: "Model",
                newName: "IX_Model_TypeOfRealEstateID");

            migrationBuilder.RenameIndex(
                name: "IX_Model_CategoryMasterCenterID",
                schema: "PRJ",
                table: "Model",
                newName: "IX_Model_ModelTypeMasterCenterID");

            migrationBuilder.AddColumn<double>(
                name: "ElectricMeterSize",
                schema: "PRJ",
                table: "WaterElectricMeterPrice",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                schema: "PRJ",
                table: "WaterElectricMeterPrice",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "WaterMeterSize",
                schema: "PRJ",
                table: "WaterElectricMeterPrice",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FrontWidth",
                schema: "PRJ",
                table: "Model",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModelShortNameMasterCenterID",
                schema: "PRJ",
                table: "Model",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TypeOfRealEstate",
                schema: "MST",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    RealEstateCategoryMasterCenterID = table.Column<Guid>(nullable: true),
                    StandardCost = table.Column<decimal>(type: "Money", nullable: false),
                    StandardPrice = table.Column<decimal>(type: "Money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfRealEstate", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TypeOfRealEstate_MasterCenter_RealEstateCategoryMasterCenterID",
                        column: x => x.RealEstateCategoryMasterCenterID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Model_ModelShortNameMasterCenterID",
                schema: "PRJ",
                table: "Model",
                column: "ModelShortNameMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_TypeOfRealEstate_RealEstateCategoryMasterCenterID",
                schema: "MST",
                table: "TypeOfRealEstate",
                column: "RealEstateCategoryMasterCenterID");

            migrationBuilder.AddForeignKey(
                name: "FK_Model_MasterCenter_ModelShortNameMasterCenterID",
                schema: "PRJ",
                table: "Model",
                column: "ModelShortNameMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Model_MasterCenter_ModelTypeMasterCenterID",
                schema: "PRJ",
                table: "Model",
                column: "ModelTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Model_TypeOfRealEstate_TypeOfRealEstateID",
                schema: "PRJ",
                table: "Model",
                column: "TypeOfRealEstateID",
                principalSchema: "MST",
                principalTable: "TypeOfRealEstate",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Model_MasterCenter_UnitTypeMasterCenterID",
                schema: "PRJ",
                table: "Model",
                column: "UnitTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Model_MasterCenter_ModelShortNameMasterCenterID",
                schema: "PRJ",
                table: "Model");

            migrationBuilder.DropForeignKey(
                name: "FK_Model_MasterCenter_ModelTypeMasterCenterID",
                schema: "PRJ",
                table: "Model");

            migrationBuilder.DropForeignKey(
                name: "FK_Model_TypeOfRealEstate_TypeOfRealEstateID",
                schema: "PRJ",
                table: "Model");

            migrationBuilder.DropForeignKey(
                name: "FK_Model_MasterCenter_UnitTypeMasterCenterID",
                schema: "PRJ",
                table: "Model");

            migrationBuilder.DropTable(
                name: "TypeOfRealEstate",
                schema: "MST");

            migrationBuilder.DropIndex(
                name: "IX_Model_ModelShortNameMasterCenterID",
                schema: "PRJ",
                table: "Model");

            migrationBuilder.DropColumn(
                name: "ElectricMeterSize",
                schema: "PRJ",
                table: "WaterElectricMeterPrice");

            migrationBuilder.DropColumn(
                name: "Version",
                schema: "PRJ",
                table: "WaterElectricMeterPrice");

            migrationBuilder.DropColumn(
                name: "WaterMeterSize",
                schema: "PRJ",
                table: "WaterElectricMeterPrice");

            migrationBuilder.DropColumn(
                name: "FrontWidth",
                schema: "PRJ",
                table: "Model");

            migrationBuilder.DropColumn(
                name: "ModelShortNameMasterCenterID",
                schema: "PRJ",
                table: "Model");

            migrationBuilder.RenameColumn(
                name: "UnitTypeMasterCenterID",
                schema: "PRJ",
                table: "Model",
                newName: "TypeMasterCenterID");

            migrationBuilder.RenameColumn(
                name: "TypeOfRealEstateID",
                schema: "PRJ",
                table: "Model",
                newName: "StyleMasterCenterID");

            migrationBuilder.RenameColumn(
                name: "PreferUnitMinimum",
                schema: "PRJ",
                table: "Model",
                newName: "WaterPowerMeter");

            migrationBuilder.RenameColumn(
                name: "PreferUnit",
                schema: "PRJ",
                table: "Model",
                newName: "PowerMeter");

            migrationBuilder.RenameColumn(
                name: "PreferHouse",
                schema: "PRJ",
                table: "Model",
                newName: "Frontage");

            migrationBuilder.RenameColumn(
                name: "NameTH",
                schema: "PRJ",
                table: "Model",
                newName: "ShortName");

            migrationBuilder.RenameColumn(
                name: "NameEN",
                schema: "PRJ",
                table: "Model",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "ModelTypeMasterCenterID",
                schema: "PRJ",
                table: "Model",
                newName: "CategoryMasterCenterID");

            migrationBuilder.RenameIndex(
                name: "IX_Model_UnitTypeMasterCenterID",
                schema: "PRJ",
                table: "Model",
                newName: "IX_Model_TypeMasterCenterID");

            migrationBuilder.RenameIndex(
                name: "IX_Model_TypeOfRealEstateID",
                schema: "PRJ",
                table: "Model",
                newName: "IX_Model_StyleMasterCenterID");

            migrationBuilder.RenameIndex(
                name: "IX_Model_ModelTypeMasterCenterID",
                schema: "PRJ",
                table: "Model",
                newName: "IX_Model_CategoryMasterCenterID");

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectID",
                schema: "PRJ",
                table: "WaterElectricMeterPrice",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModelDesc",
                schema: "PRJ",
                table: "Model",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PowerMeterPrice",
                schema: "PRJ",
                table: "Model",
                type: "Money",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "WaterPowerMeterPrice",
                schema: "PRJ",
                table: "Model",
                type: "Money",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WaterElectricMeterPrice_ProjectID",
                schema: "PRJ",
                table: "WaterElectricMeterPrice",
                column: "ProjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_Model_MasterCenter_CategoryMasterCenterID",
                schema: "PRJ",
                table: "Model",
                column: "CategoryMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Model_MasterCenter_StyleMasterCenterID",
                schema: "PRJ",
                table: "Model",
                column: "StyleMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Model_MasterCenter_TypeMasterCenterID",
                schema: "PRJ",
                table: "Model",
                column: "TypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WaterElectricMeterPrice_Project_ProjectID",
                schema: "PRJ",
                table: "WaterElectricMeterPrice",
                column: "ProjectID",
                principalSchema: "PRJ",
                principalTable: "Project",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
