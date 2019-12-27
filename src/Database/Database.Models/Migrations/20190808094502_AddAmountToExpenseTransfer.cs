using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddAmountToExpenseTransfer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                schema: "PRM",
                table: "QuotationPromotionExpense");

            migrationBuilder.DropColumn(
                name: "BuyerAmount",
                schema: "PRM",
                table: "QuotationPromotionExpense");

            migrationBuilder.DropColumn(
                name: "SellerAmount",
                schema: "PRM",
                table: "QuotationPromotionExpense");

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                schema: "PRM",
                table: "TransferPromotionExpense",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "BuyerAmount",
                schema: "PRM",
                table: "TransferPromotionExpense",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SellerAmount",
                schema: "PRM",
                table: "TransferPromotionExpense",
                type: "Money",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                schema: "PRM",
                table: "TransferPromotionExpense");

            migrationBuilder.DropColumn(
                name: "BuyerAmount",
                schema: "PRM",
                table: "TransferPromotionExpense");

            migrationBuilder.DropColumn(
                name: "SellerAmount",
                schema: "PRM",
                table: "TransferPromotionExpense");

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                schema: "PRM",
                table: "QuotationPromotionExpense",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "BuyerAmount",
                schema: "PRM",
                table: "QuotationPromotionExpense",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SellerAmount",
                schema: "PRM",
                table: "QuotationPromotionExpense",
                type: "Money",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
