using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ChangeEnumToMasterCenter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResponsibleBy",
                schema: "PRM",
                table: "TransferPromotionExpense");

            migrationBuilder.DropColumn(
                name: "ResponsibleBy",
                schema: "PRM",
                table: "QuotationPromotionExpense");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "PRM",
                table: "MasterTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "WhenReceive",
                schema: "PRM",
                table: "MasterTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "WhenReceive",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "PRM",
                table: "MasterTransferPromotion");

            migrationBuilder.DropColumn(
                name: "UseStatus",
                schema: "PRM",
                table: "MasterTransferPromotion");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "PRM",
                table: "MasterTransferCreditCardItem");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.DropColumn(
                name: "WhenReceive",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "PRM",
                table: "MasterPreSalePromotion");

            migrationBuilder.DropColumn(
                name: "UseStatus",
                schema: "PRM",
                table: "MasterPreSalePromotion");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "PRM",
                table: "MasterBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "WhenReceive",
                schema: "PRM",
                table: "MasterBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "WhenReceive",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "PRM",
                table: "MasterBookingPromotion");

            migrationBuilder.DropColumn(
                name: "UseStatus",
                schema: "PRM",
                table: "MasterBookingPromotion");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "PRM",
                table: "MasterBookingCreditCardItem");

            migrationBuilder.DropColumn(
                name: "ResponsibleBy",
                schema: "PRM",
                table: "BookingPromotionExpense");

            migrationBuilder.DropColumn(
                name: "BudgetPromotionType",
                schema: "PRJ",
                table: "BudgetPromotion");

            migrationBuilder.DropColumn(
                name: "StatusType",
                schema: "CTM",
                table: "OpportunityActivityStatus");

            migrationBuilder.DropColumn(
                name: "FollowUpType",
                schema: "CTM",
                table: "LeadActivityStatus");

            migrationBuilder.DropColumn(
                name: "StatusType",
                schema: "CTM",
                table: "LeadActivityStatus");

            migrationBuilder.DropColumn(
                name: "LeadStatus",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.AddColumn<Guid>(
                name: "ExpenseReponsibleByMasterCenterID",
                schema: "PRM",
                table: "TransferPromotionExpense",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ExpenseReponsibleByMasterCenterID",
                schema: "PRM",
                table: "QuotationPromotionExpense",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PromotionItemStatusMasterCenterID",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WhenPromotionReceiveMasterCenterID",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PromotionItemStatusMasterCenterID",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WhenPromotionReceiveMasterCenterID",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "MasterTransferPromotion",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "PromotionStatusMasterCenterID",
                schema: "PRM",
                table: "MasterTransferPromotion",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PromotionItemStatusMasterCenterID",
                schema: "PRM",
                table: "MasterTransferCreditCardItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PromotionItemStatusMasterCenterID",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WhenPromotionReceiveMasterCenterID",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "MasterPreSalePromotion",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "PromotionStatusMasterCenterID",
                schema: "PRM",
                table: "MasterPreSalePromotion",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PromotionItemStatusMasterCenterID",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WhenPromotionReceiveMasterCenterID",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PromotionItemStatusMasterCenterID",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WhenPromotionReceiveMasterCenterID",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "MasterBookingPromotion",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "PromotionStatusMasterCenterID",
                schema: "PRM",
                table: "MasterBookingPromotion",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PromotionItemStatusMasterCenterID",
                schema: "PRM",
                table: "MasterBookingCreditCardItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ExpenseReponsibleByMasterCenterID",
                schema: "PRM",
                table: "BookingPromotionExpense",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BudgetPromotionTypeMasterCenterID",
                schema: "PRJ",
                table: "BudgetPromotion",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WalkActivityStatusTypeMasterCenterId",
                schema: "CTM",
                table: "OpportunityActivityStatus",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LeadActivityFollowUpTypeMasterCenterID",
                schema: "CTM",
                table: "LeadActivityStatus",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LeadActivityStatusTypeMasterCenterID",
                schema: "CTM",
                table: "LeadActivityStatus",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LeadStatusMasterCenterID",
                schema: "CTM",
                table: "Lead",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransferPromotionExpense_ExpenseReponsibleByMasterCenterID",
                schema: "PRM",
                table: "TransferPromotionExpense",
                column: "ExpenseReponsibleByMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationPromotionExpense_ExpenseReponsibleByMasterCenterID",
                schema: "PRM",
                table: "QuotationPromotionExpense",
                column: "ExpenseReponsibleByMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterTransferPromotionItem_PromotionItemStatusMasterCenterID",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                column: "PromotionItemStatusMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterTransferPromotionItem_WhenPromotionReceiveMasterCenterID",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                column: "WhenPromotionReceiveMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterTransferPromotionFreeItem_PromotionItemStatusMasterCenterID",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem",
                column: "PromotionItemStatusMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterTransferPromotionFreeItem_WhenPromotionReceiveMasterCenterID",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem",
                column: "WhenPromotionReceiveMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterTransferPromotion_PromotionStatusMasterCenterID",
                schema: "PRM",
                table: "MasterTransferPromotion",
                column: "PromotionStatusMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterTransferCreditCardItem_PromotionItemStatusMasterCenterID",
                schema: "PRM",
                table: "MasterTransferCreditCardItem",
                column: "PromotionItemStatusMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterPreSalePromotionItem_PromotionItemStatusMasterCenterID",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                column: "PromotionItemStatusMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterPreSalePromotionItem_WhenPromotionReceiveMasterCenterID",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                column: "WhenPromotionReceiveMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterPreSalePromotion_PromotionStatusMasterCenterID",
                schema: "PRM",
                table: "MasterPreSalePromotion",
                column: "PromotionStatusMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterBookingPromotionItem_PromotionItemStatusMasterCenterID",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                column: "PromotionItemStatusMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterBookingPromotionItem_WhenPromotionReceiveMasterCenterID",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                column: "WhenPromotionReceiveMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterBookingPromotionFreeItem_PromotionItemStatusMasterCenterID",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem",
                column: "PromotionItemStatusMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterBookingPromotionFreeItem_WhenPromotionReceiveMasterCenterID",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem",
                column: "WhenPromotionReceiveMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterBookingPromotion_PromotionStatusMasterCenterID",
                schema: "PRM",
                table: "MasterBookingPromotion",
                column: "PromotionStatusMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterBookingCreditCardItem_PromotionItemStatusMasterCenterID",
                schema: "PRM",
                table: "MasterBookingCreditCardItem",
                column: "PromotionItemStatusMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingPromotionExpense_ExpenseReponsibleByMasterCenterID",
                schema: "PRM",
                table: "BookingPromotionExpense",
                column: "ExpenseReponsibleByMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetPromotion_BudgetPromotionTypeMasterCenterID",
                schema: "PRJ",
                table: "BudgetPromotion",
                column: "BudgetPromotionTypeMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_OpportunityActivityStatus_WalkActivityStatusTypeMasterCenterId",
                schema: "CTM",
                table: "OpportunityActivityStatus",
                column: "WalkActivityStatusTypeMasterCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_LeadActivityStatus_LeadActivityFollowUpTypeMasterCenterID",
                schema: "CTM",
                table: "LeadActivityStatus",
                column: "LeadActivityFollowUpTypeMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_LeadActivityStatus_LeadActivityStatusTypeMasterCenterID",
                schema: "CTM",
                table: "LeadActivityStatus",
                column: "LeadActivityStatusTypeMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_Lead_LeadStatusMasterCenterID",
                schema: "CTM",
                table: "Lead",
                column: "LeadStatusMasterCenterID");

            migrationBuilder.AddForeignKey(
                name: "FK_Lead_MasterCenter_LeadStatusMasterCenterID",
                schema: "CTM",
                table: "Lead",
                column: "LeadStatusMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LeadActivityStatus_MasterCenter_LeadActivityFollowUpTypeMasterCenterID",
                schema: "CTM",
                table: "LeadActivityStatus",
                column: "LeadActivityFollowUpTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LeadActivityStatus_MasterCenter_LeadActivityStatusTypeMasterCenterID",
                schema: "CTM",
                table: "LeadActivityStatus",
                column: "LeadActivityStatusTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OpportunityActivityStatus_MasterCenter_WalkActivityStatusTypeMasterCenterId",
                schema: "CTM",
                table: "OpportunityActivityStatus",
                column: "WalkActivityStatusTypeMasterCenterId",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetPromotion_MasterCenter_BudgetPromotionTypeMasterCenterID",
                schema: "PRJ",
                table: "BudgetPromotion",
                column: "BudgetPromotionTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingPromotionExpense_MasterCenter_ExpenseReponsibleByMasterCenterID",
                schema: "PRM",
                table: "BookingPromotionExpense",
                column: "ExpenseReponsibleByMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterBookingCreditCardItem_MasterCenter_PromotionItemStatusMasterCenterID",
                schema: "PRM",
                table: "MasterBookingCreditCardItem",
                column: "PromotionItemStatusMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterBookingPromotion_MasterCenter_PromotionStatusMasterCenterID",
                schema: "PRM",
                table: "MasterBookingPromotion",
                column: "PromotionStatusMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_MasterBookingPromotionFreeItem_MasterCenter_WhenPromotionReceiveMasterCenterID",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem",
                column: "WhenPromotionReceiveMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterBookingPromotionItem_MasterCenter_PromotionItemStatusMasterCenterID",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                column: "PromotionItemStatusMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterBookingPromotionItem_MasterCenter_WhenPromotionReceiveMasterCenterID",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                column: "WhenPromotionReceiveMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterPreSalePromotion_MasterCenter_PromotionStatusMasterCenterID",
                schema: "PRM",
                table: "MasterPreSalePromotion",
                column: "PromotionStatusMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterPreSalePromotionItem_MasterCenter_PromotionItemStatusMasterCenterID",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                column: "PromotionItemStatusMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterPreSalePromotionItem_MasterCenter_WhenPromotionReceiveMasterCenterID",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                column: "WhenPromotionReceiveMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterTransferCreditCardItem_MasterCenter_PromotionItemStatusMasterCenterID",
                schema: "PRM",
                table: "MasterTransferCreditCardItem",
                column: "PromotionItemStatusMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterTransferPromotion_MasterCenter_PromotionStatusMasterCenterID",
                schema: "PRM",
                table: "MasterTransferPromotion",
                column: "PromotionStatusMasterCenterID",
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

            migrationBuilder.AddForeignKey(
                name: "FK_MasterTransferPromotionFreeItem_MasterCenter_WhenPromotionReceiveMasterCenterID",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem",
                column: "WhenPromotionReceiveMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterTransferPromotionItem_MasterCenter_PromotionItemStatusMasterCenterID",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                column: "PromotionItemStatusMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterTransferPromotionItem_MasterCenter_WhenPromotionReceiveMasterCenterID",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                column: "WhenPromotionReceiveMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationPromotionExpense_MasterCenter_ExpenseReponsibleByMasterCenterID",
                schema: "PRM",
                table: "QuotationPromotionExpense",
                column: "ExpenseReponsibleByMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferPromotionExpense_MasterCenter_ExpenseReponsibleByMasterCenterID",
                schema: "PRM",
                table: "TransferPromotionExpense",
                column: "ExpenseReponsibleByMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lead_MasterCenter_LeadStatusMasterCenterID",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropForeignKey(
                name: "FK_LeadActivityStatus_MasterCenter_LeadActivityFollowUpTypeMasterCenterID",
                schema: "CTM",
                table: "LeadActivityStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_LeadActivityStatus_MasterCenter_LeadActivityStatusTypeMasterCenterID",
                schema: "CTM",
                table: "LeadActivityStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_OpportunityActivityStatus_MasterCenter_WalkActivityStatusTypeMasterCenterId",
                schema: "CTM",
                table: "OpportunityActivityStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_BudgetPromotion_MasterCenter_BudgetPromotionTypeMasterCenterID",
                schema: "PRJ",
                table: "BudgetPromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingPromotionExpense_MasterCenter_ExpenseReponsibleByMasterCenterID",
                schema: "PRM",
                table: "BookingPromotionExpense");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterBookingCreditCardItem_MasterCenter_PromotionItemStatusMasterCenterID",
                schema: "PRM",
                table: "MasterBookingCreditCardItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterBookingPromotion_MasterCenter_PromotionStatusMasterCenterID",
                schema: "PRM",
                table: "MasterBookingPromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterBookingPromotionFreeItem_MasterCenter_PromotionItemStatusMasterCenterID",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterBookingPromotionFreeItem_MasterCenter_WhenPromotionReceiveMasterCenterID",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterBookingPromotionItem_MasterCenter_PromotionItemStatusMasterCenterID",
                schema: "PRM",
                table: "MasterBookingPromotionItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterBookingPromotionItem_MasterCenter_WhenPromotionReceiveMasterCenterID",
                schema: "PRM",
                table: "MasterBookingPromotionItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterPreSalePromotion_MasterCenter_PromotionStatusMasterCenterID",
                schema: "PRM",
                table: "MasterPreSalePromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterPreSalePromotionItem_MasterCenter_PromotionItemStatusMasterCenterID",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterPreSalePromotionItem_MasterCenter_WhenPromotionReceiveMasterCenterID",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterTransferCreditCardItem_MasterCenter_PromotionItemStatusMasterCenterID",
                schema: "PRM",
                table: "MasterTransferCreditCardItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterTransferPromotion_MasterCenter_PromotionStatusMasterCenterID",
                schema: "PRM",
                table: "MasterTransferPromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterTransferPromotionFreeItem_MasterCenter_PromotionItemStatusMasterCenterID",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterTransferPromotionFreeItem_MasterCenter_WhenPromotionReceiveMasterCenterID",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterTransferPromotionItem_MasterCenter_PromotionItemStatusMasterCenterID",
                schema: "PRM",
                table: "MasterTransferPromotionItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterTransferPromotionItem_MasterCenter_WhenPromotionReceiveMasterCenterID",
                schema: "PRM",
                table: "MasterTransferPromotionItem");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationPromotionExpense_MasterCenter_ExpenseReponsibleByMasterCenterID",
                schema: "PRM",
                table: "QuotationPromotionExpense");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferPromotionExpense_MasterCenter_ExpenseReponsibleByMasterCenterID",
                schema: "PRM",
                table: "TransferPromotionExpense");

            migrationBuilder.DropIndex(
                name: "IX_TransferPromotionExpense_ExpenseReponsibleByMasterCenterID",
                schema: "PRM",
                table: "TransferPromotionExpense");

            migrationBuilder.DropIndex(
                name: "IX_QuotationPromotionExpense_ExpenseReponsibleByMasterCenterID",
                schema: "PRM",
                table: "QuotationPromotionExpense");

            migrationBuilder.DropIndex(
                name: "IX_MasterTransferPromotionItem_PromotionItemStatusMasterCenterID",
                schema: "PRM",
                table: "MasterTransferPromotionItem");

            migrationBuilder.DropIndex(
                name: "IX_MasterTransferPromotionItem_WhenPromotionReceiveMasterCenterID",
                schema: "PRM",
                table: "MasterTransferPromotionItem");

            migrationBuilder.DropIndex(
                name: "IX_MasterTransferPromotionFreeItem_PromotionItemStatusMasterCenterID",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem");

            migrationBuilder.DropIndex(
                name: "IX_MasterTransferPromotionFreeItem_WhenPromotionReceiveMasterCenterID",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem");

            migrationBuilder.DropIndex(
                name: "IX_MasterTransferPromotion_PromotionStatusMasterCenterID",
                schema: "PRM",
                table: "MasterTransferPromotion");

            migrationBuilder.DropIndex(
                name: "IX_MasterTransferCreditCardItem_PromotionItemStatusMasterCenterID",
                schema: "PRM",
                table: "MasterTransferCreditCardItem");

            migrationBuilder.DropIndex(
                name: "IX_MasterPreSalePromotionItem_PromotionItemStatusMasterCenterID",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.DropIndex(
                name: "IX_MasterPreSalePromotionItem_WhenPromotionReceiveMasterCenterID",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.DropIndex(
                name: "IX_MasterPreSalePromotion_PromotionStatusMasterCenterID",
                schema: "PRM",
                table: "MasterPreSalePromotion");

            migrationBuilder.DropIndex(
                name: "IX_MasterBookingPromotionItem_PromotionItemStatusMasterCenterID",
                schema: "PRM",
                table: "MasterBookingPromotionItem");

            migrationBuilder.DropIndex(
                name: "IX_MasterBookingPromotionItem_WhenPromotionReceiveMasterCenterID",
                schema: "PRM",
                table: "MasterBookingPromotionItem");

            migrationBuilder.DropIndex(
                name: "IX_MasterBookingPromotionFreeItem_PromotionItemStatusMasterCenterID",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem");

            migrationBuilder.DropIndex(
                name: "IX_MasterBookingPromotionFreeItem_WhenPromotionReceiveMasterCenterID",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem");

            migrationBuilder.DropIndex(
                name: "IX_MasterBookingPromotion_PromotionStatusMasterCenterID",
                schema: "PRM",
                table: "MasterBookingPromotion");

            migrationBuilder.DropIndex(
                name: "IX_MasterBookingCreditCardItem_PromotionItemStatusMasterCenterID",
                schema: "PRM",
                table: "MasterBookingCreditCardItem");

            migrationBuilder.DropIndex(
                name: "IX_BookingPromotionExpense_ExpenseReponsibleByMasterCenterID",
                schema: "PRM",
                table: "BookingPromotionExpense");

            migrationBuilder.DropIndex(
                name: "IX_BudgetPromotion_BudgetPromotionTypeMasterCenterID",
                schema: "PRJ",
                table: "BudgetPromotion");

            migrationBuilder.DropIndex(
                name: "IX_OpportunityActivityStatus_WalkActivityStatusTypeMasterCenterId",
                schema: "CTM",
                table: "OpportunityActivityStatus");

            migrationBuilder.DropIndex(
                name: "IX_LeadActivityStatus_LeadActivityFollowUpTypeMasterCenterID",
                schema: "CTM",
                table: "LeadActivityStatus");

            migrationBuilder.DropIndex(
                name: "IX_LeadActivityStatus_LeadActivityStatusTypeMasterCenterID",
                schema: "CTM",
                table: "LeadActivityStatus");

            migrationBuilder.DropIndex(
                name: "IX_Lead_LeadStatusMasterCenterID",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "ExpenseReponsibleByMasterCenterID",
                schema: "PRM",
                table: "TransferPromotionExpense");

            migrationBuilder.DropColumn(
                name: "ExpenseReponsibleByMasterCenterID",
                schema: "PRM",
                table: "QuotationPromotionExpense");

            migrationBuilder.DropColumn(
                name: "PromotionItemStatusMasterCenterID",
                schema: "PRM",
                table: "MasterTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "WhenPromotionReceiveMasterCenterID",
                schema: "PRM",
                table: "MasterTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "PromotionItemStatusMasterCenterID",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "WhenPromotionReceiveMasterCenterID",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "MasterTransferPromotion");

            migrationBuilder.DropColumn(
                name: "PromotionStatusMasterCenterID",
                schema: "PRM",
                table: "MasterTransferPromotion");

            migrationBuilder.DropColumn(
                name: "PromotionItemStatusMasterCenterID",
                schema: "PRM",
                table: "MasterTransferCreditCardItem");

            migrationBuilder.DropColumn(
                name: "PromotionItemStatusMasterCenterID",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.DropColumn(
                name: "WhenPromotionReceiveMasterCenterID",
                schema: "PRM",
                table: "MasterPreSalePromotionItem");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "MasterPreSalePromotion");

            migrationBuilder.DropColumn(
                name: "PromotionStatusMasterCenterID",
                schema: "PRM",
                table: "MasterPreSalePromotion");

            migrationBuilder.DropColumn(
                name: "PromotionItemStatusMasterCenterID",
                schema: "PRM",
                table: "MasterBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "WhenPromotionReceiveMasterCenterID",
                schema: "PRM",
                table: "MasterBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "PromotionItemStatusMasterCenterID",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "WhenPromotionReceiveMasterCenterID",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "MasterBookingPromotion");

            migrationBuilder.DropColumn(
                name: "PromotionStatusMasterCenterID",
                schema: "PRM",
                table: "MasterBookingPromotion");

            migrationBuilder.DropColumn(
                name: "PromotionItemStatusMasterCenterID",
                schema: "PRM",
                table: "MasterBookingCreditCardItem");

            migrationBuilder.DropColumn(
                name: "ExpenseReponsibleByMasterCenterID",
                schema: "PRM",
                table: "BookingPromotionExpense");

            migrationBuilder.DropColumn(
                name: "BudgetPromotionTypeMasterCenterID",
                schema: "PRJ",
                table: "BudgetPromotion");

            migrationBuilder.DropColumn(
                name: "WalkActivityStatusTypeMasterCenterId",
                schema: "CTM",
                table: "OpportunityActivityStatus");

            migrationBuilder.DropColumn(
                name: "LeadActivityFollowUpTypeMasterCenterID",
                schema: "CTM",
                table: "LeadActivityStatus");

            migrationBuilder.DropColumn(
                name: "LeadActivityStatusTypeMasterCenterID",
                schema: "CTM",
                table: "LeadActivityStatus");

            migrationBuilder.DropColumn(
                name: "LeadStatusMasterCenterID",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.AddColumn<int>(
                name: "ResponsibleBy",
                schema: "PRM",
                table: "TransferPromotionExpense",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ResponsibleBy",
                schema: "PRM",
                table: "QuotationPromotionExpense",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WhenReceive",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WhenReceive",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "PRM",
                table: "MasterTransferPromotion",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UseStatus",
                schema: "PRM",
                table: "MasterTransferPromotion",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "PRM",
                table: "MasterTransferCreditCardItem",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WhenReceive",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "PRM",
                table: "MasterPreSalePromotion",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UseStatus",
                schema: "PRM",
                table: "MasterPreSalePromotion",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WhenReceive",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WhenReceive",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "PRM",
                table: "MasterBookingPromotion",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UseStatus",
                schema: "PRM",
                table: "MasterBookingPromotion",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "PRM",
                table: "MasterBookingCreditCardItem",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ResponsibleBy",
                schema: "PRM",
                table: "BookingPromotionExpense",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BudgetPromotionType",
                schema: "PRJ",
                table: "BudgetPromotion",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StatusType",
                schema: "CTM",
                table: "OpportunityActivityStatus",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FollowUpType",
                schema: "CTM",
                table: "LeadActivityStatus",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StatusType",
                schema: "CTM",
                table: "LeadActivityStatus",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LeadStatus",
                schema: "CTM",
                table: "Lead",
                nullable: false,
                defaultValue: 0);
        }
    }
}
