using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class UpdateTaxFeePriceAndAddressForDataMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LowRiseFenceFee_MasterCenter_ModelCategoryMasterCenterID",
                schema: "PRJ",
                table: "LowRiseFenceFee");

            migrationBuilder.DropForeignKey(
                name: "FK_RoundFee_MasterCenter_MasterCenterRoundBusinessFeeID",
                schema: "PRJ",
                table: "RoundFee");

            migrationBuilder.DropForeignKey(
                name: "FK_RoundFee_MasterCenter_MasterCenterRoundIncomeFeeID",
                schema: "PRJ",
                table: "RoundFee");

            migrationBuilder.DropForeignKey(
                name: "FK_RoundFee_MasterCenter_MasterCenterRoundLocalFeeID",
                schema: "PRJ",
                table: "RoundFee");

            migrationBuilder.DropForeignKey(
                name: "FK_RoundFee_MasterCenter_MasterCenterRoundTransferFeeID",
                schema: "PRJ",
                table: "RoundFee");

            migrationBuilder.DropColumn(
                name: "ConstructionModelID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "FirstFlag",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "IsUpdate",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "DocType",
                schema: "PRJ",
                table: "MinPrice");

            migrationBuilder.DropColumn(
                name: "UnitNo",
                schema: "PRJ",
                table: "MinPrice");

            migrationBuilder.DropColumn(
                name: "CalculateFence",
                schema: "PRJ",
                table: "LowRiseFenceFee");

            migrationBuilder.DropColumn(
                name: "BudgetType",
                schema: "PRJ",
                table: "BudgetMinPrice");

            migrationBuilder.RenameColumn(
                name: "MasterCenterRoundTransferFeeID",
                schema: "PRJ",
                table: "RoundFee",
                newName: "TransferFeeRoundFormulaMasterCenterID");

            migrationBuilder.RenameColumn(
                name: "MasterCenterRoundLocalFeeID",
                schema: "PRJ",
                table: "RoundFee",
                newName: "LocalTaxRoundFormulaMasterCenterID");

            migrationBuilder.RenameColumn(
                name: "MasterCenterRoundIncomeFeeID",
                schema: "PRJ",
                table: "RoundFee",
                newName: "IncomeTaxRoundFormulaMasterCenterID");

            migrationBuilder.RenameColumn(
                name: "MasterCenterRoundBusinessFeeID",
                schema: "PRJ",
                table: "RoundFee",
                newName: "BusinessTaxRoundFormulaMasterCenterID");

            migrationBuilder.RenameIndex(
                name: "IX_RoundFee_MasterCenterRoundTransferFeeID",
                schema: "PRJ",
                table: "RoundFee",
                newName: "IX_RoundFee_TransferFeeRoundFormulaMasterCenterID");

            migrationBuilder.RenameIndex(
                name: "IX_RoundFee_MasterCenterRoundLocalFeeID",
                schema: "PRJ",
                table: "RoundFee",
                newName: "IX_RoundFee_LocalTaxRoundFormulaMasterCenterID");

            migrationBuilder.RenameIndex(
                name: "IX_RoundFee_MasterCenterRoundIncomeFeeID",
                schema: "PRJ",
                table: "RoundFee",
                newName: "IX_RoundFee_IncomeTaxRoundFormulaMasterCenterID");

            migrationBuilder.RenameIndex(
                name: "IX_RoundFee_MasterCenterRoundBusinessFeeID",
                schema: "PRJ",
                table: "RoundFee",
                newName: "IX_RoundFee_BusinessTaxRoundFormulaMasterCenterID");

            migrationBuilder.RenameColumn(
                name: "ModelCategoryMasterCenterID",
                schema: "PRJ",
                table: "LowRiseFenceFee",
                newName: "TypeOfRealEstateID");

            migrationBuilder.RenameColumn(
                name: "IronRate",
                schema: "PRJ",
                table: "LowRiseFenceFee",
                newName: "IronPrice");

            migrationBuilder.RenameIndex(
                name: "IX_LowRiseFenceFee_ModelCategoryMasterCenterID",
                schema: "PRJ",
                table: "LowRiseFenceFee",
                newName: "IX_LowRiseFenceFee_TypeOfRealEstateID");

            migrationBuilder.AlterColumn<decimal>(
                name: "ROIMinprice",
                schema: "PRJ",
                table: "MinPrice",
                type: "Money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Money");

            migrationBuilder.AlterColumn<decimal>(
                name: "Cost",
                schema: "PRJ",
                table: "MinPrice",
                type: "Money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Money");

            migrationBuilder.AlterColumn<decimal>(
                name: "ApprovedMinPrice",
                schema: "PRJ",
                table: "MinPrice",
                type: "Money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Money");

            migrationBuilder.AddColumn<DateTime>(
                name: "ActiveDate",
                schema: "PRJ",
                table: "MinPrice",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MinPriceTypeMasterCenterID",
                schema: "PRJ",
                table: "MinPrice",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UnitID",
                schema: "PRJ",
                table: "MinPrice",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "ConcreteRate",
                schema: "PRJ",
                table: "LowRiseFenceFee",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Money",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ConcretePrice",
                schema: "PRJ",
                table: "LowRiseFenceFee",
                type: "Money",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCalculateDepreciation",
                schema: "PRJ",
                table: "LowRiseFenceFee",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ActiveDate",
                schema: "PRJ",
                table: "BudgetPromotion",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BudgetMinPriceTypeMasterCenterID",
                schema: "PRJ",
                table: "BudgetMinPrice",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "UnitAmount",
                schema: "PRJ",
                table: "BudgetMinPrice",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                schema: "MST",
                table: "MasterCenter",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_MinPrice_MinPriceTypeMasterCenterID",
                schema: "PRJ",
                table: "MinPrice",
                column: "MinPriceTypeMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_MinPrice_UnitID",
                schema: "PRJ",
                table: "MinPrice",
                column: "UnitID");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetMinPrice_BudgetMinPriceTypeMasterCenterID",
                schema: "PRJ",
                table: "BudgetMinPrice",
                column: "BudgetMinPriceTypeMasterCenterID");

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetMinPrice_MasterCenter_BudgetMinPriceTypeMasterCenterID",
                schema: "PRJ",
                table: "BudgetMinPrice",
                column: "BudgetMinPriceTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LowRiseFenceFee_TypeOfRealEstate_TypeOfRealEstateID",
                schema: "PRJ",
                table: "LowRiseFenceFee",
                column: "TypeOfRealEstateID",
                principalSchema: "MST",
                principalTable: "TypeOfRealEstate",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MinPrice_MasterCenter_MinPriceTypeMasterCenterID",
                schema: "PRJ",
                table: "MinPrice",
                column: "MinPriceTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MinPrice_Unit_UnitID",
                schema: "PRJ",
                table: "MinPrice",
                column: "UnitID",
                principalSchema: "PRJ",
                principalTable: "Unit",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoundFee_MasterCenter_BusinessTaxRoundFormulaMasterCenterID",
                schema: "PRJ",
                table: "RoundFee",
                column: "BusinessTaxRoundFormulaMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoundFee_MasterCenter_IncomeTaxRoundFormulaMasterCenterID",
                schema: "PRJ",
                table: "RoundFee",
                column: "IncomeTaxRoundFormulaMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoundFee_MasterCenter_LocalTaxRoundFormulaMasterCenterID",
                schema: "PRJ",
                table: "RoundFee",
                column: "LocalTaxRoundFormulaMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoundFee_MasterCenter_TransferFeeRoundFormulaMasterCenterID",
                schema: "PRJ",
                table: "RoundFee",
                column: "TransferFeeRoundFormulaMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BudgetMinPrice_MasterCenter_BudgetMinPriceTypeMasterCenterID",
                schema: "PRJ",
                table: "BudgetMinPrice");

            migrationBuilder.DropForeignKey(
                name: "FK_LowRiseFenceFee_TypeOfRealEstate_TypeOfRealEstateID",
                schema: "PRJ",
                table: "LowRiseFenceFee");

            migrationBuilder.DropForeignKey(
                name: "FK_MinPrice_MasterCenter_MinPriceTypeMasterCenterID",
                schema: "PRJ",
                table: "MinPrice");

            migrationBuilder.DropForeignKey(
                name: "FK_MinPrice_Unit_UnitID",
                schema: "PRJ",
                table: "MinPrice");

            migrationBuilder.DropForeignKey(
                name: "FK_RoundFee_MasterCenter_BusinessTaxRoundFormulaMasterCenterID",
                schema: "PRJ",
                table: "RoundFee");

            migrationBuilder.DropForeignKey(
                name: "FK_RoundFee_MasterCenter_IncomeTaxRoundFormulaMasterCenterID",
                schema: "PRJ",
                table: "RoundFee");

            migrationBuilder.DropForeignKey(
                name: "FK_RoundFee_MasterCenter_LocalTaxRoundFormulaMasterCenterID",
                schema: "PRJ",
                table: "RoundFee");

            migrationBuilder.DropForeignKey(
                name: "FK_RoundFee_MasterCenter_TransferFeeRoundFormulaMasterCenterID",
                schema: "PRJ",
                table: "RoundFee");

            migrationBuilder.DropIndex(
                name: "IX_MinPrice_MinPriceTypeMasterCenterID",
                schema: "PRJ",
                table: "MinPrice");

            migrationBuilder.DropIndex(
                name: "IX_MinPrice_UnitID",
                schema: "PRJ",
                table: "MinPrice");

            migrationBuilder.DropIndex(
                name: "IX_BudgetMinPrice_BudgetMinPriceTypeMasterCenterID",
                schema: "PRJ",
                table: "BudgetMinPrice");

            migrationBuilder.DropColumn(
                name: "ActiveDate",
                schema: "PRJ",
                table: "MinPrice");

            migrationBuilder.DropColumn(
                name: "MinPriceTypeMasterCenterID",
                schema: "PRJ",
                table: "MinPrice");

            migrationBuilder.DropColumn(
                name: "UnitID",
                schema: "PRJ",
                table: "MinPrice");

            migrationBuilder.DropColumn(
                name: "ConcretePrice",
                schema: "PRJ",
                table: "LowRiseFenceFee");

            migrationBuilder.DropColumn(
                name: "IsCalculateDepreciation",
                schema: "PRJ",
                table: "LowRiseFenceFee");

            migrationBuilder.DropColumn(
                name: "ActiveDate",
                schema: "PRJ",
                table: "BudgetPromotion");

            migrationBuilder.DropColumn(
                name: "BudgetMinPriceTypeMasterCenterID",
                schema: "PRJ",
                table: "BudgetMinPrice");

            migrationBuilder.DropColumn(
                name: "UnitAmount",
                schema: "PRJ",
                table: "BudgetMinPrice");

            migrationBuilder.RenameColumn(
                name: "TransferFeeRoundFormulaMasterCenterID",
                schema: "PRJ",
                table: "RoundFee",
                newName: "MasterCenterRoundTransferFeeID");

            migrationBuilder.RenameColumn(
                name: "LocalTaxRoundFormulaMasterCenterID",
                schema: "PRJ",
                table: "RoundFee",
                newName: "MasterCenterRoundLocalFeeID");

            migrationBuilder.RenameColumn(
                name: "IncomeTaxRoundFormulaMasterCenterID",
                schema: "PRJ",
                table: "RoundFee",
                newName: "MasterCenterRoundIncomeFeeID");

            migrationBuilder.RenameColumn(
                name: "BusinessTaxRoundFormulaMasterCenterID",
                schema: "PRJ",
                table: "RoundFee",
                newName: "MasterCenterRoundBusinessFeeID");

            migrationBuilder.RenameIndex(
                name: "IX_RoundFee_TransferFeeRoundFormulaMasterCenterID",
                schema: "PRJ",
                table: "RoundFee",
                newName: "IX_RoundFee_MasterCenterRoundTransferFeeID");

            migrationBuilder.RenameIndex(
                name: "IX_RoundFee_LocalTaxRoundFormulaMasterCenterID",
                schema: "PRJ",
                table: "RoundFee",
                newName: "IX_RoundFee_MasterCenterRoundLocalFeeID");

            migrationBuilder.RenameIndex(
                name: "IX_RoundFee_IncomeTaxRoundFormulaMasterCenterID",
                schema: "PRJ",
                table: "RoundFee",
                newName: "IX_RoundFee_MasterCenterRoundIncomeFeeID");

            migrationBuilder.RenameIndex(
                name: "IX_RoundFee_BusinessTaxRoundFormulaMasterCenterID",
                schema: "PRJ",
                table: "RoundFee",
                newName: "IX_RoundFee_MasterCenterRoundBusinessFeeID");

            migrationBuilder.RenameColumn(
                name: "TypeOfRealEstateID",
                schema: "PRJ",
                table: "LowRiseFenceFee",
                newName: "ModelCategoryMasterCenterID");

            migrationBuilder.RenameColumn(
                name: "IronPrice",
                schema: "PRJ",
                table: "LowRiseFenceFee",
                newName: "IronRate");

            migrationBuilder.RenameIndex(
                name: "IX_LowRiseFenceFee_TypeOfRealEstateID",
                schema: "PRJ",
                table: "LowRiseFenceFee",
                newName: "IX_LowRiseFenceFee_ModelCategoryMasterCenterID");

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

            migrationBuilder.AddColumn<bool>(
                name: "IsUpdate",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ROIMinprice",
                schema: "PRJ",
                table: "MinPrice",
                type: "Money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Money",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Cost",
                schema: "PRJ",
                table: "MinPrice",
                type: "Money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Money",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ApprovedMinPrice",
                schema: "PRJ",
                table: "MinPrice",
                type: "Money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Money",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DocType",
                schema: "PRJ",
                table: "MinPrice",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitNo",
                schema: "PRJ",
                table: "MinPrice",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ConcreteRate",
                schema: "PRJ",
                table: "LowRiseFenceFee",
                type: "Money",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CalculateFence",
                schema: "PRJ",
                table: "LowRiseFenceFee",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BudgetType",
                schema: "PRJ",
                table: "BudgetMinPrice",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Key",
                schema: "MST",
                table: "MasterCenter",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LowRiseFenceFee_MasterCenter_ModelCategoryMasterCenterID",
                schema: "PRJ",
                table: "LowRiseFenceFee",
                column: "ModelCategoryMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoundFee_MasterCenter_MasterCenterRoundBusinessFeeID",
                schema: "PRJ",
                table: "RoundFee",
                column: "MasterCenterRoundBusinessFeeID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoundFee_MasterCenter_MasterCenterRoundIncomeFeeID",
                schema: "PRJ",
                table: "RoundFee",
                column: "MasterCenterRoundIncomeFeeID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoundFee_MasterCenter_MasterCenterRoundLocalFeeID",
                schema: "PRJ",
                table: "RoundFee",
                column: "MasterCenterRoundLocalFeeID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoundFee_MasterCenter_MasterCenterRoundTransferFeeID",
                schema: "PRJ",
                table: "RoundFee",
                column: "MasterCenterRoundTransferFeeID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
