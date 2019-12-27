using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ChangeMasterPromotionItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgreementNo",
                schema: "PRM",
                table: "MasterTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "ItemNo",
                schema: "PRM",
                table: "MasterTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "SAPName",
                schema: "PRM",
                table: "MasterTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "AgreementNo",
                schema: "PRM",
                table: "MasterBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "ItemNo",
                schema: "PRM",
                table: "MasterBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "SAPName",
                schema: "PRM",
                table: "MasterBookingPromotionItem");

            migrationBuilder.AddColumn<Guid>(
                name: "MainPromotionItemID",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PromotionMaterialID",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MasterPreSalePromotionItem_PromotionMaterialID",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                column: "PromotionMaterialID");

            migrationBuilder.AddForeignKey(
                name: "FK_MasterPreSalePromotionItem_PromotionMaterial_PromotionMaterialID",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                column: "PromotionMaterialID",
                principalSchema: "PRM",
                principalTable: "PromotionMaterial",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MasterPreSalePromotionItem_PromotionMaterial_PromotionMaterialID",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.DropIndex(
                name: "IX_MasterPreSalePromotionItem_PromotionMaterialID",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.DropColumn(
                name: "MainPromotionItemID",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.DropColumn(
                name: "PromotionMaterialID",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.AddColumn<string>(
                name: "AgreementNo",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ItemNo",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SAPName",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AgreementNo",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ItemNo",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SAPName",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                maxLength: 1000,
                nullable: true);
        }
    }
}
