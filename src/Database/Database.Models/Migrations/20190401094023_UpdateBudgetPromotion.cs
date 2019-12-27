using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class UpdateBudgetPromotion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HouseNo",
                schema: "PRJ",
                table: "BudgetPromotion");

            migrationBuilder.DropColumn(
                name: "HouseType",
                schema: "PRJ",
                table: "BudgetPromotion");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                schema: "PRJ",
                table: "BudgetPromotion");

            migrationBuilder.DropColumn(
                name: "UnitNo",
                schema: "PRJ",
                table: "BudgetPromotion");

            migrationBuilder.DropColumn(
                name: "WBSCRM",
                schema: "PRJ",
                table: "BudgetPromotion");

            migrationBuilder.DropColumn(
                name: "WBSSAP",
                schema: "PRJ",
                table: "BudgetPromotion");

            migrationBuilder.AlterColumn<decimal>(
                name: "PromotionTransferPrice",
                schema: "PRJ",
                table: "BudgetPromotion",
                type: "Money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Money");

            migrationBuilder.AlterColumn<decimal>(
                name: "PromotionPrice",
                schema: "PRJ",
                table: "BudgetPromotion",
                type: "Money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Money");

            migrationBuilder.AddColumn<Guid>(
                name: "UnitID",
                schema: "PRJ",
                table: "BudgetPromotion",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_BudgetPromotion_UnitID",
                schema: "PRJ",
                table: "BudgetPromotion",
                column: "UnitID");

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetPromotion_Unit_UnitID",
                schema: "PRJ",
                table: "BudgetPromotion",
                column: "UnitID",
                principalSchema: "PRJ",
                principalTable: "Unit",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BudgetPromotion_Unit_UnitID",
                schema: "PRJ",
                table: "BudgetPromotion");

            migrationBuilder.DropIndex(
                name: "IX_BudgetPromotion_UnitID",
                schema: "PRJ",
                table: "BudgetPromotion");

            migrationBuilder.DropColumn(
                name: "UnitID",
                schema: "PRJ",
                table: "BudgetPromotion");

            migrationBuilder.AlterColumn<decimal>(
                name: "PromotionTransferPrice",
                schema: "PRJ",
                table: "BudgetPromotion",
                type: "Money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Money",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PromotionPrice",
                schema: "PRJ",
                table: "BudgetPromotion",
                type: "Money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Money",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HouseNo",
                schema: "PRJ",
                table: "BudgetPromotion",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HouseType",
                schema: "PRJ",
                table: "BudgetPromotion",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                schema: "PRJ",
                table: "BudgetPromotion",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "UnitNo",
                schema: "PRJ",
                table: "BudgetPromotion",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WBSCRM",
                schema: "PRJ",
                table: "BudgetPromotion",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WBSSAP",
                schema: "PRJ",
                table: "BudgetPromotion",
                nullable: true);
        }
    }
}
