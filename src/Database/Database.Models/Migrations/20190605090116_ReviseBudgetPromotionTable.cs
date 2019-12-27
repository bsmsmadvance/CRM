using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ReviseBudgetPromotionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PromotionPrice",
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

            migrationBuilder.RenameColumn(
                name: "PromotionTransferPrice",
                schema: "PRJ",
                table: "BudgetPromotion",
                newName: "Budget");

            migrationBuilder.AddColumn<int>(
                name: "BudgetPromotionType",
                schema: "PRJ",
                table: "BudgetPromotion",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BudgetPromotionType",
                schema: "PRJ",
                table: "BudgetPromotion");

            migrationBuilder.RenameColumn(
                name: "Budget",
                schema: "PRJ",
                table: "BudgetPromotion",
                newName: "PromotionTransferPrice");

            migrationBuilder.AddColumn<decimal>(
                name: "PromotionPrice",
                schema: "PRJ",
                table: "BudgetPromotion",
                type: "Money",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WBSCRM",
                schema: "PRJ",
                table: "BudgetPromotion",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WBSSAP",
                schema: "PRJ",
                table: "BudgetPromotion",
                maxLength: 100,
                nullable: true);
        }
    }
}
