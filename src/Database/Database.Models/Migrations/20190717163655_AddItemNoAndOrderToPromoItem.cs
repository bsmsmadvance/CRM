using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddItemNoAndOrderToPromoItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ItemNo",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ItemNo",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ItemNo",
                schema: "PRM",
                table: "MasterTransferCreditCardItem",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                schema: "PRM",
                table: "MasterTransferCreditCardItem",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ItemNo",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UsedDate",
                schema: "PRM",
                table: "MasterPreSalePromotion",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ItemNo",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ItemNo",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ItemNo",
                schema: "PRM",
                table: "MasterBookingCreditCardItem",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                schema: "PRM",
                table: "MasterBookingCreditCardItem",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemNo",
                schema: "PRM",
                table: "MasterTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "Order",
                schema: "PRM",
                table: "MasterTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "ItemNo",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "Order",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "ItemNo",
                schema: "PRM",
                table: "MasterTransferCreditCardItem");

            migrationBuilder.DropColumn(
                name: "Order",
                schema: "PRM",
                table: "MasterTransferCreditCardItem");

            migrationBuilder.DropColumn(
                name: "ItemNo",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.DropColumn(
                name: "Order",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.DropColumn(
                name: "UsedDate",
                schema: "PRM",
                table: "MasterPreSalePromotion");

            migrationBuilder.DropColumn(
                name: "ItemNo",
                schema: "PRM",
                table: "MasterBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "Order",
                schema: "PRM",
                table: "MasterBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "ItemNo",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "Order",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "ItemNo",
                schema: "PRM",
                table: "MasterBookingCreditCardItem");

            migrationBuilder.DropColumn(
                name: "Order",
                schema: "PRM",
                table: "MasterBookingCreditCardItem");
        }
    }
}
