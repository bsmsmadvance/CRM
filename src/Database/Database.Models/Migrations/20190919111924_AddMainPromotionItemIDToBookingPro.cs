using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddMainPromotionItemIDToBookingPro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MainTransferPromotionItemID",
                schema: "PRM",
                table: "TransferPromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MainBookingPromotionItemID",
                schema: "PRM",
                table: "BookingPromotionItem",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainTransferPromotionItemID",
                schema: "PRM",
                table: "TransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "MainBookingPromotionItemID",
                schema: "PRM",
                table: "BookingPromotionItem");
        }
    }
}
