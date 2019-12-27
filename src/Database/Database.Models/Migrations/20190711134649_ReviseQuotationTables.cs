using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ReviseQuotationTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuotationBookingPromotion_MasterBookingPromotion_PromotionID",
                schema: "PRM",
                table: "QuotationBookingPromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationUnitPrice_Booking_BookingID",
                schema: "SAL",
                table: "QuotationUnitPrice");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationUnitPriceItem_Quotation_QuotationID",
                schema: "SAL",
                table: "QuotationUnitPriceItem");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "SAL",
                table: "Quotation");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "SAL",
                table: "Quotation");

            migrationBuilder.RenameColumn(
                name: "QuotationID",
                schema: "SAL",
                table: "QuotationUnitPriceItem",
                newName: "QuotationUnitPriceID");

            migrationBuilder.RenameIndex(
                name: "IX_QuotationUnitPriceItem_QuotationID",
                schema: "SAL",
                table: "QuotationUnitPriceItem",
                newName: "IX_QuotationUnitPriceItem_QuotationUnitPriceID");

            migrationBuilder.RenameColumn(
                name: "BookingID",
                schema: "SAL",
                table: "QuotationUnitPrice",
                newName: "QuotationID");

            migrationBuilder.RenameIndex(
                name: "IX_QuotationUnitPrice_BookingID",
                schema: "SAL",
                table: "QuotationUnitPrice",
                newName: "IX_QuotationUnitPrice_QuotationID");

            migrationBuilder.RenameColumn(
                name: "PromotionID",
                schema: "PRM",
                table: "QuotationBookingPromotion",
                newName: "MasterBookingPromotionID");

            migrationBuilder.RenameIndex(
                name: "IX_QuotationBookingPromotion_PromotionID",
                schema: "PRM",
                table: "QuotationBookingPromotion",
                newName: "IX_QuotationBookingPromotion_MasterBookingPromotionID");

            migrationBuilder.AddColumn<Guid>(
                name: "FromPriceListItemID",
                schema: "SAL",
                table: "QuotationUnitPriceItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FromPriceListID",
                schema: "SAL",
                table: "QuotationUnitPrice",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "SAL",
                table: "QuotationUnitPrice",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectID",
                schema: "SAL",
                table: "Quotation",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuotationNo",
                schema: "SAL",
                table: "Quotation",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "QuotationStatusMasterCenterID",
                schema: "SAL",
                table: "Quotation",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "QuotationID",
                schema: "SAL",
                table: "Booking",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFreeItem",
                schema: "PRM",
                table: "QuotationTransferPromotionItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "MainQuotationTransferPromotionID",
                schema: "PRM",
                table: "QuotationTransferPromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MasterTransferPromotionFreeItemID",
                schema: "PRM",
                table: "QuotationTransferPromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFreeItem",
                schema: "PRM",
                table: "QuotationBookingPromotionItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "MainQuotationBookingPromotionID",
                schema: "PRM",
                table: "QuotationBookingPromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MasterBookingPromotionFreeItemID",
                schema: "PRM",
                table: "QuotationBookingPromotionItem",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuotationUnitPriceItem_FromPriceListItemID",
                schema: "SAL",
                table: "QuotationUnitPriceItem",
                column: "FromPriceListItemID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationUnitPrice_FromPriceListID",
                schema: "SAL",
                table: "QuotationUnitPrice",
                column: "FromPriceListID");

            migrationBuilder.CreateIndex(
                name: "IX_Quotation_ProjectID",
                schema: "SAL",
                table: "Quotation",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Quotation_QuotationStatusMasterCenterID",
                schema: "SAL",
                table: "Quotation",
                column: "QuotationStatusMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_QuotationID",
                schema: "SAL",
                table: "Booking",
                column: "QuotationID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationTransferPromotionItem_MasterTransferPromotionFreeItemID",
                schema: "PRM",
                table: "QuotationTransferPromotionItem",
                column: "MasterTransferPromotionFreeItemID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationBookingPromotionItem_MasterBookingPromotionFreeItemID",
                schema: "PRM",
                table: "QuotationBookingPromotionItem",
                column: "MasterBookingPromotionFreeItemID");

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationBookingPromotion_MasterBookingPromotion_MasterBookingPromotionID",
                schema: "PRM",
                table: "QuotationBookingPromotion",
                column: "MasterBookingPromotionID",
                principalSchema: "PRM",
                principalTable: "MasterBookingPromotion",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationBookingPromotionItem_MasterBookingPromotionFreeItem_MasterBookingPromotionFreeItemID",
                schema: "PRM",
                table: "QuotationBookingPromotionItem",
                column: "MasterBookingPromotionFreeItemID",
                principalSchema: "PRM",
                principalTable: "MasterBookingPromotionFreeItem",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationTransferPromotionItem_MasterTransferPromotionFreeItem_MasterTransferPromotionFreeItemID",
                schema: "PRM",
                table: "QuotationTransferPromotionItem",
                column: "MasterTransferPromotionFreeItemID",
                principalSchema: "PRM",
                principalTable: "MasterTransferPromotionFreeItem",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Quotation_QuotationID",
                schema: "SAL",
                table: "Booking",
                column: "QuotationID",
                principalSchema: "SAL",
                principalTable: "Quotation",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Quotation_Project_ProjectID",
                schema: "SAL",
                table: "Quotation",
                column: "ProjectID",
                principalSchema: "PRJ",
                principalTable: "Project",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Quotation_MasterCenter_QuotationStatusMasterCenterID",
                schema: "SAL",
                table: "Quotation",
                column: "QuotationStatusMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationUnitPrice_PriceList_FromPriceListID",
                schema: "SAL",
                table: "QuotationUnitPrice",
                column: "FromPriceListID",
                principalSchema: "PRJ",
                principalTable: "PriceList",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationUnitPrice_Quotation_QuotationID",
                schema: "SAL",
                table: "QuotationUnitPrice",
                column: "QuotationID",
                principalSchema: "SAL",
                principalTable: "Quotation",
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

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationUnitPriceItem_QuotationUnitPrice_QuotationUnitPriceID",
                schema: "SAL",
                table: "QuotationUnitPriceItem",
                column: "QuotationUnitPriceID",
                principalSchema: "SAL",
                principalTable: "QuotationUnitPrice",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuotationBookingPromotion_MasterBookingPromotion_MasterBookingPromotionID",
                schema: "PRM",
                table: "QuotationBookingPromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationBookingPromotionItem_MasterBookingPromotionFreeItem_MasterBookingPromotionFreeItemID",
                schema: "PRM",
                table: "QuotationBookingPromotionItem");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationTransferPromotionItem_MasterTransferPromotionFreeItem_MasterTransferPromotionFreeItemID",
                schema: "PRM",
                table: "QuotationTransferPromotionItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Quotation_QuotationID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Quotation_Project_ProjectID",
                schema: "SAL",
                table: "Quotation");

            migrationBuilder.DropForeignKey(
                name: "FK_Quotation_MasterCenter_QuotationStatusMasterCenterID",
                schema: "SAL",
                table: "Quotation");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationUnitPrice_PriceList_FromPriceListID",
                schema: "SAL",
                table: "QuotationUnitPrice");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationUnitPrice_Quotation_QuotationID",
                schema: "SAL",
                table: "QuotationUnitPrice");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationUnitPriceItem_PriceListItem_FromPriceListItemID",
                schema: "SAL",
                table: "QuotationUnitPriceItem");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationUnitPriceItem_QuotationUnitPrice_QuotationUnitPriceID",
                schema: "SAL",
                table: "QuotationUnitPriceItem");

            migrationBuilder.DropIndex(
                name: "IX_QuotationUnitPriceItem_FromPriceListItemID",
                schema: "SAL",
                table: "QuotationUnitPriceItem");

            migrationBuilder.DropIndex(
                name: "IX_QuotationUnitPrice_FromPriceListID",
                schema: "SAL",
                table: "QuotationUnitPrice");

            migrationBuilder.DropIndex(
                name: "IX_Quotation_ProjectID",
                schema: "SAL",
                table: "Quotation");

            migrationBuilder.DropIndex(
                name: "IX_Quotation_QuotationStatusMasterCenterID",
                schema: "SAL",
                table: "Quotation");

            migrationBuilder.DropIndex(
                name: "IX_Booking_QuotationID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_QuotationTransferPromotionItem_MasterTransferPromotionFreeItemID",
                schema: "PRM",
                table: "QuotationTransferPromotionItem");

            migrationBuilder.DropIndex(
                name: "IX_QuotationBookingPromotionItem_MasterBookingPromotionFreeItemID",
                schema: "PRM",
                table: "QuotationBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "FromPriceListItemID",
                schema: "SAL",
                table: "QuotationUnitPriceItem");

            migrationBuilder.DropColumn(
                name: "FromPriceListID",
                schema: "SAL",
                table: "QuotationUnitPrice");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "SAL",
                table: "QuotationUnitPrice");

            migrationBuilder.DropColumn(
                name: "ProjectID",
                schema: "SAL",
                table: "Quotation");

            migrationBuilder.DropColumn(
                name: "QuotationNo",
                schema: "SAL",
                table: "Quotation");

            migrationBuilder.DropColumn(
                name: "QuotationStatusMasterCenterID",
                schema: "SAL",
                table: "Quotation");

            migrationBuilder.DropColumn(
                name: "QuotationID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "IsFreeItem",
                schema: "PRM",
                table: "QuotationTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "MainQuotationTransferPromotionID",
                schema: "PRM",
                table: "QuotationTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "MasterTransferPromotionFreeItemID",
                schema: "PRM",
                table: "QuotationTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "IsFreeItem",
                schema: "PRM",
                table: "QuotationBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "MainQuotationBookingPromotionID",
                schema: "PRM",
                table: "QuotationBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "MasterBookingPromotionFreeItemID",
                schema: "PRM",
                table: "QuotationBookingPromotionItem");

            migrationBuilder.RenameColumn(
                name: "QuotationUnitPriceID",
                schema: "SAL",
                table: "QuotationUnitPriceItem",
                newName: "QuotationID");

            migrationBuilder.RenameIndex(
                name: "IX_QuotationUnitPriceItem_QuotationUnitPriceID",
                schema: "SAL",
                table: "QuotationUnitPriceItem",
                newName: "IX_QuotationUnitPriceItem_QuotationID");

            migrationBuilder.RenameColumn(
                name: "QuotationID",
                schema: "SAL",
                table: "QuotationUnitPrice",
                newName: "BookingID");

            migrationBuilder.RenameIndex(
                name: "IX_QuotationUnitPrice_QuotationID",
                schema: "SAL",
                table: "QuotationUnitPrice",
                newName: "IX_QuotationUnitPrice_BookingID");

            migrationBuilder.RenameColumn(
                name: "MasterBookingPromotionID",
                schema: "PRM",
                table: "QuotationBookingPromotion",
                newName: "PromotionID");

            migrationBuilder.RenameIndex(
                name: "IX_QuotationBookingPromotion_MasterBookingPromotionID",
                schema: "PRM",
                table: "QuotationBookingPromotion",
                newName: "IX_QuotationBookingPromotion_PromotionID");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "SAL",
                table: "Quotation",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "SAL",
                table: "Quotation",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationBookingPromotion_MasterBookingPromotion_PromotionID",
                schema: "PRM",
                table: "QuotationBookingPromotion",
                column: "PromotionID",
                principalSchema: "PRM",
                principalTable: "MasterBookingPromotion",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationUnitPrice_Booking_BookingID",
                schema: "SAL",
                table: "QuotationUnitPrice",
                column: "BookingID",
                principalSchema: "SAL",
                principalTable: "Booking",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationUnitPriceItem_Quotation_QuotationID",
                schema: "SAL",
                table: "QuotationUnitPriceItem",
                column: "QuotationID",
                principalSchema: "SAL",
                principalTable: "Quotation",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
