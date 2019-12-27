using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class RequestPreSalePromotionChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PreSalePromotionRequestUnit_MasterPreSalePromotion_MasterPreSalePromotionID",
                schema: "PRM",
                table: "PreSalePromotionRequestUnit");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationUnitPriceItem_PriceListItem_FromPriceListItemID",
                schema: "SAL",
                table: "QuotationUnitPriceItem");

            migrationBuilder.DropIndex(
                name: "IX_QuotationUnitPriceItem_FromPriceListItemID",
                schema: "SAL",
                table: "QuotationUnitPriceItem");

            migrationBuilder.DropIndex(
                name: "IX_PreSalePromotionRequestUnit_MasterPreSalePromotionID",
                schema: "PRM",
                table: "PreSalePromotionRequestUnit");

            migrationBuilder.DropColumn(
                name: "FromPriceListItemID",
                schema: "SAL",
                table: "QuotationUnitPriceItem");

            migrationBuilder.DropColumn(
                name: "MasterPreSalePromotionID",
                schema: "PRM",
                table: "PreSalePromotionRequestUnit");

            migrationBuilder.AddColumn<Guid>(
                name: "MasterPreSalePromotionID",
                schema: "PRM",
                table: "PreSalePromotionRequest",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PreSalePromotionRequest_MasterPreSalePromotionID",
                schema: "PRM",
                table: "PreSalePromotionRequest",
                column: "MasterPreSalePromotionID");

            migrationBuilder.AddForeignKey(
                name: "FK_PreSalePromotionRequest_MasterPreSalePromotion_MasterPreSalePromotionID",
                schema: "PRM",
                table: "PreSalePromotionRequest",
                column: "MasterPreSalePromotionID",
                principalSchema: "PRM",
                principalTable: "MasterPreSalePromotion",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PreSalePromotionRequest_MasterPreSalePromotion_MasterPreSalePromotionID",
                schema: "PRM",
                table: "PreSalePromotionRequest");

            migrationBuilder.DropIndex(
                name: "IX_PreSalePromotionRequest_MasterPreSalePromotionID",
                schema: "PRM",
                table: "PreSalePromotionRequest");

            migrationBuilder.DropColumn(
                name: "MasterPreSalePromotionID",
                schema: "PRM",
                table: "PreSalePromotionRequest");

            migrationBuilder.AddColumn<Guid>(
                name: "FromPriceListItemID",
                schema: "SAL",
                table: "QuotationUnitPriceItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MasterPreSalePromotionID",
                schema: "PRM",
                table: "PreSalePromotionRequestUnit",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_QuotationUnitPriceItem_FromPriceListItemID",
                schema: "SAL",
                table: "QuotationUnitPriceItem",
                column: "FromPriceListItemID");

            migrationBuilder.CreateIndex(
                name: "IX_PreSalePromotionRequestUnit_MasterPreSalePromotionID",
                schema: "PRM",
                table: "PreSalePromotionRequestUnit",
                column: "MasterPreSalePromotionID");

            migrationBuilder.AddForeignKey(
                name: "FK_PreSalePromotionRequestUnit_MasterPreSalePromotion_MasterPreSalePromotionID",
                schema: "PRM",
                table: "PreSalePromotionRequestUnit",
                column: "MasterPreSalePromotionID",
                principalSchema: "PRM",
                principalTable: "MasterPreSalePromotion",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationUnitPriceItem_PriceListItem_FromPriceListItemID",
                schema: "SAL",
                table: "QuotationUnitPriceItem",
                column: "FromPriceListItemID",
                principalSchema: "PRJ",
                principalTable: "PriceListItem",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
