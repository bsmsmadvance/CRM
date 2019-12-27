using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class RemoveStatusFromFreeItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MasterBookingPromotionFreeItem_MasterCenter_PromotionItemStatusMasterCenterID",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterTransferPromotionFreeItem_MasterCenter_PromotionItemStatusMasterCenterID",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem");

            migrationBuilder.DropIndex(
                name: "IX_MasterTransferPromotionFreeItem_PromotionItemStatusMasterCenterID",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem");

            migrationBuilder.DropIndex(
                name: "IX_MasterBookingPromotionFreeItem_PromotionItemStatusMasterCenterID",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "PromotionItemStatusMasterCenterID",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "PromotionItemStatusMasterCenterID",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PromotionItemStatusMasterCenterID",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PromotionItemStatusMasterCenterID",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MasterTransferPromotionFreeItem_PromotionItemStatusMasterCenterID",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem",
                column: "PromotionItemStatusMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterBookingPromotionFreeItem_PromotionItemStatusMasterCenterID",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem",
                column: "PromotionItemStatusMasterCenterID");

            migrationBuilder.AddForeignKey(
                name: "FK_MasterBookingPromotionFreeItem_MasterCenter_PromotionItemStatusMasterCenterID",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem",
                column: "PromotionItemStatusMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterTransferPromotionFreeItem_MasterCenter_PromotionItemStatusMasterCenterID",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem",
                column: "PromotionItemStatusMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
