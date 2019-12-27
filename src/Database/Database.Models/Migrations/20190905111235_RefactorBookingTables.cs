using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class RefactorBookingTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_MasterCenter_BookingCancelReturnMasterCenterID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_MasterCenter_BookingReasonReturnMasterCenterID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_User_CancelByUserID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_MinPriceBudgetWorkflow_MasterCenter_PromotionTypeMasterCenterID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow");

            migrationBuilder.DropIndex(
                name: "IX_Booking_BookingCancelReturnMasterCenterID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_CancelByUserID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "BookingCancelReturnMasterCenterID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "CancelRemark",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.RenameColumn(
                name: "TotalBudgetMinPrice",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                newName: "TransferDiscount");

            migrationBuilder.RenameColumn(
                name: "PromotionTypeMasterCenterID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                newName: "BudgetPromotionTypeMasterCenterID");

            migrationBuilder.RenameIndex(
                name: "IX_MinPriceBudgetWorkflow_PromotionTypeMasterCenterID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                newName: "IX_MinPriceBudgetWorkflow_BudgetPromotionTypeMasterCenterID");

            migrationBuilder.RenameColumn(
                name: "CancelByUserID",
                schema: "SAL",
                table: "Booking",
                newName: "CreateBookingFromMasterCenterID");

            migrationBuilder.RenameColumn(
                name: "BookingReasonReturnMasterCenterID",
                schema: "SAL",
                table: "Booking",
                newName: "CreateBookingFromID");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_BookingReasonReturnMasterCenterID",
                schema: "SAL",
                table: "Booking",
                newName: "IX_Booking_CreateBookingFromID");

            migrationBuilder.AddColumn<decimal>(
                name: "CashDiscount",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsRequestBudgetPromotion",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRequestMinPrice",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                schema: "SAL",
                table: "MinPriceBudgetApproval",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsAgreementOwner",
                schema: "SAL",
                table: "BookingOwner",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCancelled",
                schema: "SAL",
                table: "Booking",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsReadyToPayment",
                schema: "SAL",
                table: "Booking",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "PRM",
                table: "TransferPromotion",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "BookingPromotionStageMasterCenterID",
                schema: "PRM",
                table: "BookingPromotion",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "PRM",
                table: "BookingPromotion",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "CancelMemo",
                schema: "SAL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsFromMigration = table.Column<bool>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false),
                    BookingID = table.Column<Guid>(nullable: true),
                    AgreementID = table.Column<Guid>(nullable: true),
                    HasAgreemnt = table.Column<bool>(nullable: false),
                    CancelReturnMasterCenterID = table.Column<Guid>(nullable: true),
                    CancelReasonID = table.Column<Guid>(nullable: true),
                    BankRejectDocument = table.Column<string>(maxLength: 100, nullable: true),
                    CancelRemark = table.Column<string>(maxLength: 5000, nullable: true),
                    CancelByUserID = table.Column<Guid>(nullable: true),
                    TotalReceivedAmount = table.Column<decimal>(type: "Money", nullable: true),
                    TotalPromotionDeliverAmount = table.Column<decimal>(type: "Money", nullable: true),
                    PenaltyAmount = table.Column<decimal>(type: "Money", nullable: true),
                    ReturnAmount = table.Column<decimal>(type: "Money", nullable: true),
                    ReturnBankID = table.Column<Guid>(nullable: true),
                    BankID = table.Column<Guid>(nullable: true),
                    ReturnBankAccount = table.Column<string>(maxLength: 50, nullable: true),
                    ReturnBankBranchID = table.Column<Guid>(nullable: true),
                    ReturnBankAccountName = table.Column<string>(maxLength: 100, nullable: true),
                    ReturnCitizenIdentityNo = table.Column<string>(maxLength: 50, nullable: true),
                    ReturnBookBankFile = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CancelMemo", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CancelMemo_Agreement_AgreementID",
                        column: x => x.AgreementID,
                        principalSchema: "SAL",
                        principalTable: "Agreement",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CancelMemo_Bank_BankID",
                        column: x => x.BankID,
                        principalSchema: "MST",
                        principalTable: "Bank",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CancelMemo_Booking_BookingID",
                        column: x => x.BookingID,
                        principalSchema: "SAL",
                        principalTable: "Booking",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CancelMemo_User_CancelByUserID",
                        column: x => x.CancelByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CancelMemo_CancelReason_CancelReasonID",
                        column: x => x.CancelReasonID,
                        principalSchema: "MST",
                        principalTable: "CancelReason",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CancelMemo_MasterCenter_CancelReturnMasterCenterID",
                        column: x => x.CancelReturnMasterCenterID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CancelMemo_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CancelMemo_BankBranch_ReturnBankBranchID",
                        column: x => x.ReturnBankBranchID,
                        principalSchema: "MST",
                        principalTable: "BankBranch",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CancelMemo_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingPromotion_BookingPromotionStageMasterCenterID",
                schema: "PRM",
                table: "BookingPromotion",
                column: "BookingPromotionStageMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_CancelMemo_AgreementID",
                schema: "SAL",
                table: "CancelMemo",
                column: "AgreementID");

            migrationBuilder.CreateIndex(
                name: "IX_CancelMemo_BankID",
                schema: "SAL",
                table: "CancelMemo",
                column: "BankID");

            migrationBuilder.CreateIndex(
                name: "IX_CancelMemo_BookingID",
                schema: "SAL",
                table: "CancelMemo",
                column: "BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_CancelMemo_CancelByUserID",
                schema: "SAL",
                table: "CancelMemo",
                column: "CancelByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CancelMemo_CancelReasonID",
                schema: "SAL",
                table: "CancelMemo",
                column: "CancelReasonID");

            migrationBuilder.CreateIndex(
                name: "IX_CancelMemo_CancelReturnMasterCenterID",
                schema: "SAL",
                table: "CancelMemo",
                column: "CancelReturnMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_CancelMemo_CreatedByUserID",
                schema: "SAL",
                table: "CancelMemo",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CancelMemo_ReturnBankBranchID",
                schema: "SAL",
                table: "CancelMemo",
                column: "ReturnBankBranchID");

            migrationBuilder.CreateIndex(
                name: "IX_CancelMemo_UpdatedByUserID",
                schema: "SAL",
                table: "CancelMemo",
                column: "UpdatedByUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingPromotion_MasterCenter_BookingPromotionStageMasterCenterID",
                schema: "PRM",
                table: "BookingPromotion",
                column: "BookingPromotionStageMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_MasterCenter_CreateBookingFromID",
                schema: "SAL",
                table: "Booking",
                column: "CreateBookingFromID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MinPriceBudgetWorkflow_MasterCenter_BudgetPromotionTypeMasterCenterID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                column: "BudgetPromotionTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingPromotion_MasterCenter_BookingPromotionStageMasterCenterID",
                schema: "PRM",
                table: "BookingPromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_MasterCenter_CreateBookingFromID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_MinPriceBudgetWorkflow_MasterCenter_BudgetPromotionTypeMasterCenterID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow");

            migrationBuilder.DropTable(
                name: "CancelMemo",
                schema: "SAL");

            migrationBuilder.DropIndex(
                name: "IX_BookingPromotion_BookingPromotionStageMasterCenterID",
                schema: "PRM",
                table: "BookingPromotion");

            migrationBuilder.DropColumn(
                name: "CashDiscount",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow");

            migrationBuilder.DropColumn(
                name: "IsRequestBudgetPromotion",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow");

            migrationBuilder.DropColumn(
                name: "IsRequestMinPrice",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow");

            migrationBuilder.DropColumn(
                name: "Order",
                schema: "SAL",
                table: "MinPriceBudgetApproval");

            migrationBuilder.DropColumn(
                name: "IsAgreementOwner",
                schema: "SAL",
                table: "BookingOwner");

            migrationBuilder.DropColumn(
                name: "IsCancelled",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "IsReadyToPayment",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropColumn(
                name: "BookingPromotionStageMasterCenterID",
                schema: "PRM",
                table: "BookingPromotion");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "PRM",
                table: "BookingPromotion");

            migrationBuilder.RenameColumn(
                name: "TransferDiscount",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                newName: "TotalBudgetMinPrice");

            migrationBuilder.RenameColumn(
                name: "BudgetPromotionTypeMasterCenterID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                newName: "PromotionTypeMasterCenterID");

            migrationBuilder.RenameIndex(
                name: "IX_MinPriceBudgetWorkflow_BudgetPromotionTypeMasterCenterID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                newName: "IX_MinPriceBudgetWorkflow_PromotionTypeMasterCenterID");

            migrationBuilder.RenameColumn(
                name: "CreateBookingFromMasterCenterID",
                schema: "SAL",
                table: "Booking",
                newName: "CancelByUserID");

            migrationBuilder.RenameColumn(
                name: "CreateBookingFromID",
                schema: "SAL",
                table: "Booking",
                newName: "BookingReasonReturnMasterCenterID");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_CreateBookingFromID",
                schema: "SAL",
                table: "Booking",
                newName: "IX_Booking_BookingReasonReturnMasterCenterID");

            migrationBuilder.AddColumn<Guid>(
                name: "BookingCancelReturnMasterCenterID",
                schema: "SAL",
                table: "Booking",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CancelRemark",
                schema: "SAL",
                table: "Booking",
                maxLength: 5000,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Booking_BookingCancelReturnMasterCenterID",
                schema: "SAL",
                table: "Booking",
                column: "BookingCancelReturnMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_CancelByUserID",
                schema: "SAL",
                table: "Booking",
                column: "CancelByUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_MasterCenter_BookingCancelReturnMasterCenterID",
                schema: "SAL",
                table: "Booking",
                column: "BookingCancelReturnMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_MasterCenter_BookingReasonReturnMasterCenterID",
                schema: "SAL",
                table: "Booking",
                column: "BookingReasonReturnMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_User_CancelByUserID",
                schema: "SAL",
                table: "Booking",
                column: "CancelByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MinPriceBudgetWorkflow_MasterCenter_PromotionTypeMasterCenterID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                column: "PromotionTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
