using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class CreatePromoForMigrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Unit",
                schema: "PRM",
                table: "PromotionMaterialItem",
                newName: "UnitTH");

            migrationBuilder.AlterColumn<string>(
                name: "Plant",
                schema: "PRM",
                table: "PromotionMaterialItem",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "BasePrice",
                schema: "PRM",
                table: "PromotionMaterialItem",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "BrandEN",
                schema: "PRM",
                table: "PromotionMaterialItem",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BrandTH",
                schema: "PRM",
                table: "PromotionMaterialItem",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DocType",
                schema: "PRM",
                table: "PromotionMaterialItem",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GLAccountNo",
                schema: "PRM",
                table: "PromotionMaterialItem",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "PRM",
                table: "PromotionMaterialItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPurchasing",
                schema: "PRM",
                table: "PromotionMaterialItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsShowInContract",
                schema: "PRM",
                table: "PromotionMaterialItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MaterialGroupKey",
                schema: "PRM",
                table: "PromotionMaterialItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MaterialItemStatusMasterCenterID",
                schema: "PRM",
                table: "PromotionMaterialItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PromotionMaterialID",
                schema: "PRM",
                table: "PromotionMaterialItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RemarkEN",
                schema: "PRM",
                table: "PromotionMaterialItem",
                maxLength: 5000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RemarkTH",
                schema: "PRM",
                table: "PromotionMaterialItem",
                maxLength: 5000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SAPBaseUnit",
                schema: "PRM",
                table: "PromotionMaterialItem",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SAPCompanyID",
                schema: "PRM",
                table: "PromotionMaterialItem",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SAPConditionRecordNo",
                schema: "PRM",
                table: "PromotionMaterialItem",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SAPCreatedTime",
                schema: "PRM",
                table: "PromotionMaterialItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SAPDeleteIndicator",
                schema: "PRM",
                table: "PromotionMaterialItem",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SAPPurchasingGroup",
                schema: "PRM",
                table: "PromotionMaterialItem",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SAPPurchasingOrg",
                schema: "PRM",
                table: "PromotionMaterialItem",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SAPSaleTaxCode",
                schema: "PRM",
                table: "PromotionMaterialItem",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SAPTermPaymentKey",
                schema: "PRM",
                table: "PromotionMaterialItem",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SAPVarKey",
                schema: "PRM",
                table: "PromotionMaterialItem",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SAPVendor",
                schema: "PRM",
                table: "PromotionMaterialItem",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpecEN",
                schema: "PRM",
                table: "PromotionMaterialItem",
                maxLength: 5000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpecTH",
                schema: "PRM",
                table: "PromotionMaterialItem",
                maxLength: 5000,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                schema: "PRM",
                table: "PromotionMaterialItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitEN",
                schema: "PRM",
                table: "PromotionMaterialItem",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Vat",
                schema: "PRM",
                table: "PromotionMaterialItem",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<Guid>(
                name: "WhenPromotionReceiveMasterCenterID",
                schema: "PRM",
                table: "PromotionMaterialItem",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "PRM",
                table: "PromotionMaterial",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MaterialGroupKey",
                schema: "PRM",
                table: "PromotionMaterial",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaterialGroupName",
                schema: "PRM",
                table: "PromotionMaterial",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PromotionMaterialItem_MaterialItemStatusMasterCenterID",
                schema: "PRM",
                table: "PromotionMaterialItem",
                column: "MaterialItemStatusMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionMaterialItem_PromotionMaterialID",
                schema: "PRM",
                table: "PromotionMaterialItem",
                column: "PromotionMaterialID");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionMaterialItem_WhenPromotionReceiveMasterCenterID",
                schema: "PRM",
                table: "PromotionMaterialItem",
                column: "WhenPromotionReceiveMasterCenterID");

            migrationBuilder.AddForeignKey(
                name: "FK_PromotionMaterialItem_MasterCenter_MaterialItemStatusMasterCenterID",
                schema: "PRM",
                table: "PromotionMaterialItem",
                column: "MaterialItemStatusMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PromotionMaterialItem_PromotionMaterial_PromotionMaterialID",
                schema: "PRM",
                table: "PromotionMaterialItem",
                column: "PromotionMaterialID",
                principalSchema: "PRM",
                principalTable: "PromotionMaterial",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PromotionMaterialItem_MasterCenter_WhenPromotionReceiveMasterCenterID",
                schema: "PRM",
                table: "PromotionMaterialItem",
                column: "WhenPromotionReceiveMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PromotionMaterialItem_MasterCenter_MaterialItemStatusMasterCenterID",
                schema: "PRM",
                table: "PromotionMaterialItem");

            migrationBuilder.DropForeignKey(
                name: "FK_PromotionMaterialItem_PromotionMaterial_PromotionMaterialID",
                schema: "PRM",
                table: "PromotionMaterialItem");

            migrationBuilder.DropForeignKey(
                name: "FK_PromotionMaterialItem_MasterCenter_WhenPromotionReceiveMasterCenterID",
                schema: "PRM",
                table: "PromotionMaterialItem");

            migrationBuilder.DropIndex(
                name: "IX_PromotionMaterialItem_MaterialItemStatusMasterCenterID",
                schema: "PRM",
                table: "PromotionMaterialItem");

            migrationBuilder.DropIndex(
                name: "IX_PromotionMaterialItem_PromotionMaterialID",
                schema: "PRM",
                table: "PromotionMaterialItem");

            migrationBuilder.DropIndex(
                name: "IX_PromotionMaterialItem_WhenPromotionReceiveMasterCenterID",
                schema: "PRM",
                table: "PromotionMaterialItem");

            migrationBuilder.DropColumn(
                name: "BasePrice",
                schema: "PRM",
                table: "PromotionMaterialItem");

            migrationBuilder.DropColumn(
                name: "BrandEN",
                schema: "PRM",
                table: "PromotionMaterialItem");

            migrationBuilder.DropColumn(
                name: "BrandTH",
                schema: "PRM",
                table: "PromotionMaterialItem");

            migrationBuilder.DropColumn(
                name: "DocType",
                schema: "PRM",
                table: "PromotionMaterialItem");

            migrationBuilder.DropColumn(
                name: "GLAccountNo",
                schema: "PRM",
                table: "PromotionMaterialItem");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "PRM",
                table: "PromotionMaterialItem");

            migrationBuilder.DropColumn(
                name: "IsPurchasing",
                schema: "PRM",
                table: "PromotionMaterialItem");

            migrationBuilder.DropColumn(
                name: "IsShowInContract",
                schema: "PRM",
                table: "PromotionMaterialItem");

            migrationBuilder.DropColumn(
                name: "MaterialGroupKey",
                schema: "PRM",
                table: "PromotionMaterialItem");

            migrationBuilder.DropColumn(
                name: "MaterialItemStatusMasterCenterID",
                schema: "PRM",
                table: "PromotionMaterialItem");

            migrationBuilder.DropColumn(
                name: "PromotionMaterialID",
                schema: "PRM",
                table: "PromotionMaterialItem");

            migrationBuilder.DropColumn(
                name: "RemarkEN",
                schema: "PRM",
                table: "PromotionMaterialItem");

            migrationBuilder.DropColumn(
                name: "RemarkTH",
                schema: "PRM",
                table: "PromotionMaterialItem");

            migrationBuilder.DropColumn(
                name: "SAPBaseUnit",
                schema: "PRM",
                table: "PromotionMaterialItem");

            migrationBuilder.DropColumn(
                name: "SAPCompanyID",
                schema: "PRM",
                table: "PromotionMaterialItem");

            migrationBuilder.DropColumn(
                name: "SAPConditionRecordNo",
                schema: "PRM",
                table: "PromotionMaterialItem");

            migrationBuilder.DropColumn(
                name: "SAPCreatedTime",
                schema: "PRM",
                table: "PromotionMaterialItem");

            migrationBuilder.DropColumn(
                name: "SAPDeleteIndicator",
                schema: "PRM",
                table: "PromotionMaterialItem");

            migrationBuilder.DropColumn(
                name: "SAPPurchasingGroup",
                schema: "PRM",
                table: "PromotionMaterialItem");

            migrationBuilder.DropColumn(
                name: "SAPPurchasingOrg",
                schema: "PRM",
                table: "PromotionMaterialItem");

            migrationBuilder.DropColumn(
                name: "SAPSaleTaxCode",
                schema: "PRM",
                table: "PromotionMaterialItem");

            migrationBuilder.DropColumn(
                name: "SAPTermPaymentKey",
                schema: "PRM",
                table: "PromotionMaterialItem");

            migrationBuilder.DropColumn(
                name: "SAPVarKey",
                schema: "PRM",
                table: "PromotionMaterialItem");

            migrationBuilder.DropColumn(
                name: "SAPVendor",
                schema: "PRM",
                table: "PromotionMaterialItem");

            migrationBuilder.DropColumn(
                name: "SpecEN",
                schema: "PRM",
                table: "PromotionMaterialItem");

            migrationBuilder.DropColumn(
                name: "SpecTH",
                schema: "PRM",
                table: "PromotionMaterialItem");

            migrationBuilder.DropColumn(
                name: "StartDate",
                schema: "PRM",
                table: "PromotionMaterialItem");

            migrationBuilder.DropColumn(
                name: "UnitEN",
                schema: "PRM",
                table: "PromotionMaterialItem");

            migrationBuilder.DropColumn(
                name: "Vat",
                schema: "PRM",
                table: "PromotionMaterialItem");

            migrationBuilder.DropColumn(
                name: "WhenPromotionReceiveMasterCenterID",
                schema: "PRM",
                table: "PromotionMaterialItem");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "PRM",
                table: "PromotionMaterial");

            migrationBuilder.DropColumn(
                name: "MaterialGroupKey",
                schema: "PRM",
                table: "PromotionMaterial");

            migrationBuilder.DropColumn(
                name: "MaterialGroupName",
                schema: "PRM",
                table: "PromotionMaterial");

            migrationBuilder.RenameColumn(
                name: "UnitTH",
                schema: "PRM",
                table: "PromotionMaterialItem",
                newName: "Unit");

            migrationBuilder.AlterColumn<string>(
                name: "Plant",
                schema: "PRM",
                table: "PromotionMaterialItem",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);
        }
    }
}
