using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ChangePreSalePromotionRequestUnitForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PreSalePromotionRequestItem_MasterCenter_PreSalePromotionRequestUnitID",
                schema: "PRM",
                table: "PreSalePromotionRequestItem");

            migrationBuilder.DropForeignKey(
                name: "FK_PreSalePromotionRequestItem_PreSalePromotionRequestUnit_PreSalePromotionRequestUnitID1",
                schema: "PRM",
                table: "PreSalePromotionRequestItem");

            migrationBuilder.DropIndex(
                name: "IX_PreSalePromotionRequestItem_PreSalePromotionRequestUnitID1",
                schema: "PRM",
                table: "PreSalePromotionRequestItem");

            migrationBuilder.DropColumn(
                name: "PreSalePromotionRequestUnitID1",
                schema: "PRM",
                table: "PreSalePromotionRequestItem");

            migrationBuilder.AddForeignKey(
                name: "FK_PreSalePromotionRequestItem_PreSalePromotionRequestUnit_PreSalePromotionRequestUnitID",
                schema: "PRM",
                table: "PreSalePromotionRequestItem",
                column: "PreSalePromotionRequestUnitID",
                principalSchema: "PRM",
                principalTable: "PreSalePromotionRequestUnit",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PreSalePromotionRequestItem_PreSalePromotionRequestUnit_PreSalePromotionRequestUnitID",
                schema: "PRM",
                table: "PreSalePromotionRequestItem");

            migrationBuilder.AddColumn<Guid>(
                name: "PreSalePromotionRequestUnitID1",
                schema: "PRM",
                table: "PreSalePromotionRequestItem",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PreSalePromotionRequestItem_PreSalePromotionRequestUnitID1",
                schema: "PRM",
                table: "PreSalePromotionRequestItem",
                column: "PreSalePromotionRequestUnitID1");

            migrationBuilder.AddForeignKey(
                name: "FK_PreSalePromotionRequestItem_MasterCenter_PreSalePromotionRequestUnitID",
                schema: "PRM",
                table: "PreSalePromotionRequestItem",
                column: "PreSalePromotionRequestUnitID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PreSalePromotionRequestItem_PreSalePromotionRequestUnit_PreSalePromotionRequestUnitID1",
                schema: "PRM",
                table: "PreSalePromotionRequestItem",
                column: "PreSalePromotionRequestUnitID1",
                principalSchema: "PRM",
                principalTable: "PreSalePromotionRequestUnit",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
