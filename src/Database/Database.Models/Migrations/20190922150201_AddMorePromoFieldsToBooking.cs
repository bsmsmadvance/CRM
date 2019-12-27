using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddMorePromoFieldsToBooking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAuthorizeProject_User_UserID",
                schema: "USR",
                table: "UserAuthorizeProject");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_User_UserID",
                schema: "USR",
                table: "UserRole");

            //migrationBuilder.RenameColumn(
            //    name: "OtherNational",
            //    schema: "CTM",
            //    table: "Contact",
            //    newName: "OtherNationalTH");

            //migrationBuilder.AddColumn<string>(
            //    name: "ReferContactName",
            //    schema: "SAL",
            //    table: "Booking",
            //    maxLength: 1000,
            //    nullable: true);

            //migrationBuilder.AddColumn<Guid>(
            //    name: "MainTransferPromotionItemID",
            //    schema: "PRM",
            //    table: "TransferPromotionItem",
            //    nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentReceiverMasterCenterID",
                schema: "PRM",
                table: "TransferPromotionExpense",
                nullable: true);

            //migrationBuilder.AddColumn<bool>(
            //    name: "IsFreeMortgageCharge",
            //    schema: "PRM",
            //    table: "TransferPromotion",
            //    nullable: false,
            //    defaultValue: false);

            //migrationBuilder.AddColumn<bool>(
            //    name: "IsUnlocked3PercentTransferDiscount",
            //    schema: "PRM",
            //    table: "TransferPromotion",
            //    nullable: false,
            //    defaultValue: false);

            //migrationBuilder.AddColumn<bool>(
            //    name: "IsUnlockedTransferDiscount",
            //    schema: "PRM",
            //    table: "TransferPromotion",
            //    nullable: false,
            //    defaultValue: false);

            //migrationBuilder.AddColumn<Guid>(
            //    name: "Unlocked3PercentTransferDiscountByUserID",
            //    schema: "PRM",
            //    table: "TransferPromotion",
            //    nullable: true);

            //migrationBuilder.AddColumn<DateTime>(
            //    name: "Unlocked3PercentTransferDiscountDate",
            //    schema: "PRM",
            //    table: "TransferPromotion",
            //    nullable: true);

            //migrationBuilder.AddColumn<Guid>(
            //    name: "UnlockedTransferDiscountByUserID",
            //    schema: "PRM",
            //    table: "TransferPromotion",
            //    nullable: true);

            //migrationBuilder.AddColumn<DateTime>(
            //    name: "UnlockedTransferDiscountDate",
            //    schema: "PRM",
            //    table: "TransferPromotion",
            //    nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MasterPreSalePromotionItemID",
                schema: "PRM",
                table: "PreSalePromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MasterPreSalePromotionID",
                schema: "PRM",
                table: "PreSalePromotion",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreSalePromotionNo",
                schema: "PRM",
                table: "PreSalePromotion",
                maxLength: 100,
                nullable: true);

            //migrationBuilder.AddColumn<Guid>(
            //    name: "MainBookingPromotionItemID",
            //    schema: "PRM",
            //    table: "BookingPromotionItem",
            //    nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentReceiverMasterCenterID",
                schema: "PRM",
                table: "BookingPromotionExpense",
                nullable: true);

            //migrationBuilder.AddColumn<bool>(
            //    name: "IsPRAutoCost",
            //    schema: "PRJ",
            //    table: "Unit",
            //    nullable: false,
            //    defaultValue: true);

            //migrationBuilder.AddColumn<bool>(
            //    name: "IsPRAutoExpense",
            //    schema: "PRJ",
            //    table: "Unit",
            //    nullable: false,
            //    defaultValue: true);

            //migrationBuilder.AddColumn<bool>(
            //    name: "IsPRAutoFGF",
            //    schema: "PRJ",
            //    table: "Unit",
            //    nullable: false,
            //    defaultValue: true);

            //migrationBuilder.AddColumn<bool>(
            //    name: "IsPRAutoStand",
            //    schema: "PRJ",
            //    table: "Unit",
            //    nullable: false,
            //    defaultValue: true);

            //migrationBuilder.AddColumn<bool>(
            //    name: "IsPRAutoCost",
            //    schema: "PRJ",
            //    table: "Project",
            //    nullable: false,
            //    defaultValue: true);

            //migrationBuilder.AddColumn<bool>(
            //    name: "IsPRAutoExpense",
            //    schema: "PRJ",
            //    table: "Project",
            //    nullable: false,
            //    defaultValue: true);

            //migrationBuilder.AddColumn<bool>(
            //    name: "IsPRAutoFGF",
            //    schema: "PRJ",
            //    table: "Project",
            //    nullable: false,
            //    defaultValue: true);

            //migrationBuilder.AddColumn<bool>(
            //    name: "IsPRAutoStand",
            //    schema: "PRJ",
            //    table: "Project",
            //    nullable: false,
            //    defaultValue: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "OtherNationalEN",
            //    schema: "CTM",
            //    table: "Contact",
            //    maxLength: 1000,
            //    nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransferPromotionExpense_PaymentReceiverMasterCenterID",
                schema: "PRM",
                table: "TransferPromotionExpense",
                column: "PaymentReceiverMasterCenterID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TransferPromotion_Unlocked3PercentTransferDiscountByUserID",
            //    schema: "PRM",
            //    table: "TransferPromotion",
            //    column: "Unlocked3PercentTransferDiscountByUserID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TransferPromotion_UnlockedTransferDiscountByUserID",
            //    schema: "PRM",
            //    table: "TransferPromotion",
            //    column: "UnlockedTransferDiscountByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PreSalePromotionItem_MasterPreSalePromotionItemID",
                schema: "PRM",
                table: "PreSalePromotionItem",
                column: "MasterPreSalePromotionItemID");

            migrationBuilder.CreateIndex(
                name: "IX_PreSalePromotion_MasterPreSalePromotionID",
                schema: "PRM",
                table: "PreSalePromotion",
                column: "MasterPreSalePromotionID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingPromotionExpense_PaymentReceiverMasterCenterID",
                schema: "PRM",
                table: "BookingPromotionExpense",
                column: "PaymentReceiverMasterCenterID");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingPromotionExpense_MasterCenter_PaymentReceiverMasterCenterID",
                schema: "PRM",
                table: "BookingPromotionExpense",
                column: "PaymentReceiverMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PreSalePromotion_MasterPreSalePromotion_MasterPreSalePromotionID",
                schema: "PRM",
                table: "PreSalePromotion",
                column: "MasterPreSalePromotionID",
                principalSchema: "PRM",
                principalTable: "MasterPreSalePromotion",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PreSalePromotionItem_MasterPreSalePromotionItem_MasterPreSalePromotionItemID",
                schema: "PRM",
                table: "PreSalePromotionItem",
                column: "MasterPreSalePromotionItemID",
                principalSchema: "PRM",
                principalTable: "MasterPreSalePromotionItem",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_TransferPromotion_User_Unlocked3PercentTransferDiscountByUserID",
            //    schema: "PRM",
            //    table: "TransferPromotion",
            //    column: "Unlocked3PercentTransferDiscountByUserID",
            //    principalSchema: "USR",
            //    principalTable: "User",
            //    principalColumn: "ID",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_TransferPromotion_User_UnlockedTransferDiscountByUserID",
            //    schema: "PRM",
            //    table: "TransferPromotion",
            //    column: "UnlockedTransferDiscountByUserID",
            //    principalSchema: "USR",
            //    principalTable: "User",
            //    principalColumn: "ID",
            //    onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferPromotionExpense_MasterCenter_PaymentReceiverMasterCenterID",
                schema: "PRM",
                table: "TransferPromotionExpense",
                column: "PaymentReceiverMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAuthorizeProject_User_UserID",
                schema: "USR",
                table: "UserAuthorizeProject",
                column: "UserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_User_UserID",
                schema: "USR",
                table: "UserRole",
                column: "UserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingPromotionExpense_MasterCenter_PaymentReceiverMasterCenterID",
                schema: "PRM",
                table: "BookingPromotionExpense");

            migrationBuilder.DropForeignKey(
                name: "FK_PreSalePromotion_MasterPreSalePromotion_MasterPreSalePromotionID",
                schema: "PRM",
                table: "PreSalePromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_PreSalePromotionItem_MasterPreSalePromotionItem_MasterPreSalePromotionItemID",
                schema: "PRM",
                table: "PreSalePromotionItem");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferPromotion_User_Unlocked3PercentTransferDiscountByUserID",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferPromotion_User_UnlockedTransferDiscountByUserID",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferPromotionExpense_MasterCenter_PaymentReceiverMasterCenterID",
                schema: "PRM",
                table: "TransferPromotionExpense");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAuthorizeProject_User_UserID",
                schema: "USR",
                table: "UserAuthorizeProject");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_User_UserID",
                schema: "USR",
                table: "UserRole");

            migrationBuilder.DropIndex(
                name: "IX_TransferPromotionExpense_PaymentReceiverMasterCenterID",
                schema: "PRM",
                table: "TransferPromotionExpense");

            migrationBuilder.DropIndex(
                name: "IX_TransferPromotion_Unlocked3PercentTransferDiscountByUserID",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropIndex(
                name: "IX_TransferPromotion_UnlockedTransferDiscountByUserID",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropIndex(
                name: "IX_PreSalePromotionItem_MasterPreSalePromotionItemID",
                schema: "PRM",
                table: "PreSalePromotionItem");

            migrationBuilder.DropIndex(
                name: "IX_PreSalePromotion_MasterPreSalePromotionID",
                schema: "PRM",
                table: "PreSalePromotion");

            migrationBuilder.DropIndex(
                name: "IX_BookingPromotionExpense_PaymentReceiverMasterCenterID",
                schema: "PRM",
                table: "BookingPromotionExpense");

            migrationBuilder.DropColumn(
                name: "ReferContactName",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "MainTransferPromotionItemID",
                schema: "PRM",
                table: "TransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "PaymentReceiverMasterCenterID",
                schema: "PRM",
                table: "TransferPromotionExpense");

            migrationBuilder.DropColumn(
                name: "IsFreeMortgageCharge",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropColumn(
                name: "IsUnlocked3PercentTransferDiscount",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropColumn(
                name: "IsUnlockedTransferDiscount",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropColumn(
                name: "Unlocked3PercentTransferDiscountByUserID",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropColumn(
                name: "Unlocked3PercentTransferDiscountDate",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropColumn(
                name: "UnlockedTransferDiscountByUserID",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropColumn(
                name: "UnlockedTransferDiscountDate",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropColumn(
                name: "MasterPreSalePromotionItemID",
                schema: "PRM",
                table: "PreSalePromotionItem");

            migrationBuilder.DropColumn(
                name: "MasterPreSalePromotionID",
                schema: "PRM",
                table: "PreSalePromotion");

            migrationBuilder.DropColumn(
                name: "PreSalePromotionNo",
                schema: "PRM",
                table: "PreSalePromotion");

            migrationBuilder.DropColumn(
                name: "MainBookingPromotionItemID",
                schema: "PRM",
                table: "BookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "PaymentReceiverMasterCenterID",
                schema: "PRM",
                table: "BookingPromotionExpense");

            migrationBuilder.DropColumn(
                name: "IsPRAutoCost",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "IsPRAutoExpense",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "IsPRAutoFGF",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "IsPRAutoStand",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "IsPRAutoCost",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "IsPRAutoExpense",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "IsPRAutoFGF",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "IsPRAutoStand",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "OtherNationalEN",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.RenameColumn(
                name: "OtherNationalTH",
                schema: "CTM",
                table: "Contact",
                newName: "OtherNational");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAuthorizeProject_User_UserID",
                schema: "USR",
                table: "UserAuthorizeProject",
                column: "UserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_User_UserID",
                schema: "USR",
                table: "UserRole",
                column: "UserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
