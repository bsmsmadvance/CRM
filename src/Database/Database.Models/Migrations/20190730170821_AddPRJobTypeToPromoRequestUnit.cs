using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddPRJobTypeToPromoRequestUnit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PromotionRequestPRJobTypeMasterCenterID",
                schema: "PRM",
                table: "PreSalePromotionRequestUnit",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PreSalePromotionRequestUnit_PromotionRequestPRJobTypeMasterCenterID",
                schema: "PRM",
                table: "PreSalePromotionRequestUnit",
                column: "PromotionRequestPRJobTypeMasterCenterID");

            migrationBuilder.AddForeignKey(
                name: "FK_PreSalePromotionRequestUnit_MasterCenter_PromotionRequestPRJobTypeMasterCenterID",
                schema: "PRM",
                table: "PreSalePromotionRequestUnit",
                column: "PromotionRequestPRJobTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PreSalePromotionRequestUnit_MasterCenter_PromotionRequestPRJobTypeMasterCenterID",
                schema: "PRM",
                table: "PreSalePromotionRequestUnit");

            migrationBuilder.DropIndex(
                name: "IX_PreSalePromotionRequestUnit_PromotionRequestPRJobTypeMasterCenterID",
                schema: "PRM",
                table: "PreSalePromotionRequestUnit");

            migrationBuilder.DropColumn(
                name: "PromotionRequestPRJobTypeMasterCenterID",
                schema: "PRM",
                table: "PreSalePromotionRequestUnit");
        }
    }
}
