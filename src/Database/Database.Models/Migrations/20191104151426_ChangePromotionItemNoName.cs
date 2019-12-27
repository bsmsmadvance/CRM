using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ChangePromotionItemNoName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ItemNo",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem",
                newName: "PromotionItemNo");

            migrationBuilder.RenameColumn(
                name: "ItemNo",
                schema: "PRM",
                table: "MasterTransferCreditCardItem",
                newName: "PromotionItemNo");

            migrationBuilder.RenameColumn(
                name: "ItemNo",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem",
                newName: "PromotionItemNo");

            migrationBuilder.RenameColumn(
                name: "ItemNo",
                schema: "PRM",
                table: "MasterBookingCreditCardItem",
                newName: "PromotionItemNo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PromotionItemNo",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem",
                newName: "ItemNo");

            migrationBuilder.RenameColumn(
                name: "PromotionItemNo",
                schema: "PRM",
                table: "MasterTransferCreditCardItem",
                newName: "ItemNo");

            migrationBuilder.RenameColumn(
                name: "PromotionItemNo",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem",
                newName: "ItemNo");

            migrationBuilder.RenameColumn(
                name: "PromotionItemNo",
                schema: "PRM",
                table: "MasterBookingCreditCardItem",
                newName: "ItemNo");
        }
    }
}
