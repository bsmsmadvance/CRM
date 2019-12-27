using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddAmountToQTExpense : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
