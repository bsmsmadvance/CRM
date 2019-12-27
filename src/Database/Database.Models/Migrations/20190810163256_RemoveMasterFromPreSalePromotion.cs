using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class RemoveMasterFromPreSalePromotion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PreSalePromotion_MasterPreSalePromotion_MasterPreSalePromotionID",
                schema: "PRM",
                table: "PreSalePromotion");

            migrationBuilder.DropIndex(
                name: "IX_PreSalePromotion_MasterPreSalePromotionID",
                schema: "PRM",
                table: "PreSalePromotion");

            migrationBuilder.DropColumn(
                name: "MasterPreSalePromotionID",
                schema: "PRM",
                table: "PreSalePromotion");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MasterPreSalePromotionID",
                schema: "PRM",
                table: "PreSalePromotion",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_PreSalePromotion_MasterPreSalePromotionID",
                schema: "PRM",
                table: "PreSalePromotion",
                column: "MasterPreSalePromotionID");

            migrationBuilder.AddForeignKey(
                name: "FK_PreSalePromotion_MasterPreSalePromotion_MasterPreSalePromotionID",
                schema: "PRM",
                table: "PreSalePromotion",
                column: "MasterPreSalePromotionID",
                principalSchema: "PRM",
                principalTable: "MasterPreSalePromotion",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
