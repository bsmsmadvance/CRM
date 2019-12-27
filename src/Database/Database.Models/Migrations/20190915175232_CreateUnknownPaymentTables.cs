using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class CreateUnknownPaymentTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillPayment_Bank_BankID",
                schema: "FIN",
                table: "BillPayment");

            migrationBuilder.DropForeignKey(
                name: "FK_BillPaymentTransaction_BillPayment_BillPaymentID",
                schema: "FIN",
                table: "BillPaymentTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectCreditDebitApprovalForm_Booking_BookingID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentDirectCreditDebit_DirectCreditDebitTransaction_DirectCreditDebitTransactionID",
                schema: "FIN",
                table: "PaymentDirectCreditDebit");

            migrationBuilder.DropForeignKey(
                name: "FK_UnknownPayment_Bank_AttachFileFromBankID",
                schema: "FIN",
                table: "UnknownPayment");

            migrationBuilder.DropTable(
                name: "DirectCreditDebitExport",
                schema: "FIN");

            migrationBuilder.DropTable(
                name: "DirectCreditDebitTransaction",
                schema: "FIN");

            migrationBuilder.DropTable(
                name: "DirectCreditDetail",
                schema: "FIN");

            migrationBuilder.DropTable(
                name: "DirectDebitDetail",
                schema: "FIN");

            migrationBuilder.DropTable(
                name: "DirectCreditDebitUnitPriceItem",
                schema: "FIN");

            migrationBuilder.DropIndex(
                name: "IX_PaymentDirectCreditDebit_DirectCreditDebitTransactionID",
                schema: "FIN",
                table: "PaymentDirectCreditDebit");

            migrationBuilder.DropColumn(
                name: "AttachFile",
                schema: "FIN",
                table: "UnknownPayment");

            migrationBuilder.DropColumn(
                name: "BankDepositNo",
                schema: "FIN",
                table: "UnknownPayment");

            migrationBuilder.DropColumn(
                name: "CancelMemo",
                schema: "FIN",
                table: "UnknownPayment");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "FIN",
                table: "UnknownPayment");

            migrationBuilder.DropColumn(
                name: "TransferDate",
                schema: "FIN",
                table: "UnknownPayment");

            migrationBuilder.DropColumn(
                name: "DirectCreditDebitTransactionID",
                schema: "FIN",
                table: "PaymentDirectCreditDebit");

            migrationBuilder.DropColumn(
                name: "BillPaymentType",
                schema: "FIN",
                table: "BillPaymentTransaction");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "FIN",
                table: "BillPaymentTransaction");

            migrationBuilder.DropColumn(
                name: "ImportTime",
                schema: "FIN",
                table: "BillPayment");

            migrationBuilder.DropColumn(
                name: "PayDate",
                schema: "FIN",
                table: "BillPayment");

            migrationBuilder.RenameColumn(
                name: "Memo",
                schema: "LET",
                table: "DownPaymentLetter",
                newName: "Remark");

            migrationBuilder.RenameColumn(
                name: "AttachFileFromBankID",
                schema: "FIN",
                table: "UnknownPayment",
                newName: "BookingID");

            migrationBuilder.RenameIndex(
                name: "IX_UnknownPayment_AttachFileFromBankID",
                schema: "FIN",
                table: "UnknownPayment",
                newName: "IX_UnknownPayment_BookingID");

            migrationBuilder.RenameColumn(
                name: "Memo",
                schema: "FIN",
                table: "Payment",
                newName: "Remark");

            migrationBuilder.RenameColumn(
                name: "ApprovalStatus",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                newName: "OwnerName");

            migrationBuilder.RenameColumn(
                name: "UnitPriceItemKey",
                schema: "FIN",
                table: "BillPaymentTransaction",
                newName: "DeleteReason");

            migrationBuilder.RenameColumn(
                name: "PayDate",
                schema: "FIN",
                table: "BillPaymentTransaction",
                newName: "PaymentDate");

            migrationBuilder.RenameColumn(
                name: "BillPaymentID",
                schema: "FIN",
                table: "BillPaymentTransaction",
                newName: "BillPaymentHeaderID");

            migrationBuilder.RenameIndex(
                name: "IX_BillPaymentTransaction_BillPaymentID",
                schema: "FIN",
                table: "BillPaymentTransaction",
                newName: "IX_BillPaymentTransaction_BillPaymentHeaderID");

            migrationBuilder.RenameColumn(
                name: "BankID",
                schema: "FIN",
                table: "BillPayment",
                newName: "BankAccountID");

            migrationBuilder.RenameIndex(
                name: "IX_BillPayment_BankID",
                schema: "FIN",
                table: "BillPayment",
                newName: "IX_BillPayment_BankAccountID");

            migrationBuilder.AddColumn<string>(
                name: "CancelRemark",
                schema: "FIN",
                table: "UnknownPayment",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                schema: "FIN",
                table: "UnknownPayment",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnknowPaymentStatus",
                schema: "FIN",
                table: "UnknownPayment",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "DirectPeriod",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "BookingID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<string>(
                name: "AccountNO",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ApproveDate",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BankBranchID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BankID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CancelDate",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CitizenIdentityNo",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreditCardExpireMonth",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreditCardExpireYear",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FormStatus",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "ProvinceID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RejectDate",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PayType",
                schema: "FIN",
                table: "BillPaymentTransaction",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BankRef3",
                schema: "FIN",
                table: "BillPaymentTransaction",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BankRef2",
                schema: "FIN",
                table: "BillPaymentTransaction",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BankRef1",
                schema: "FIN",
                table: "BillPaymentTransaction",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BillPaymentStatus",
                schema: "FIN",
                table: "BillPaymentTransaction",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "BookingID",
                schema: "FIN",
                table: "BillPaymentTransaction",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReconcileDate",
                schema: "FIN",
                table: "BillPaymentTransaction",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                schema: "FIN",
                table: "BillPaymentTransaction",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BatchID",
                schema: "FIN",
                table: "BillPayment",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ImportDate",
                schema: "FIN",
                table: "BillPayment",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImportFileName",
                schema: "FIN",
                table: "BillPayment",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalRecord",
                schema: "FIN",
                table: "BillPayment",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DirectCreditDebitExportHeader",
                schema: "FIN",
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
                    DirectFormType = table.Column<int>(nullable: false),
                    BankAccountID = table.Column<Guid>(nullable: true),
                    BankID = table.Column<Guid>(nullable: true),
                    DirectPeriod = table.Column<int>(nullable: false),
                    PeriodMonth = table.Column<int>(nullable: false),
                    PeriodYear = table.Column<int>(nullable: false),
                    DirectPayDate = table.Column<DateTime>(nullable: false),
                    ImportDate = table.Column<DateTime>(nullable: true),
                    ImportFileName = table.Column<string>(maxLength: 100, nullable: true),
                    TotalRecord = table.Column<int>(nullable: false),
                    TotalAmount = table.Column<decimal>(type: "Money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectCreditDebitExportHeader", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DirectCreditDebitExportHeader_BankAccount_BankAccountID",
                        column: x => x.BankAccountID,
                        principalSchema: "MST",
                        principalTable: "BankAccount",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DirectCreditDebitExportHeader_Bank_BankID",
                        column: x => x.BankID,
                        principalSchema: "MST",
                        principalTable: "Bank",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DirectCreditDebitExportHeader_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DirectCreditDebitExportHeader_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UnknownPaymentReverse",
                schema: "FIN",
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
                    UnknownPaymentID = table.Column<Guid>(nullable: true),
                    BookingID = table.Column<Guid>(nullable: true),
                    ReverseDate = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(type: "Money", nullable: false),
                    CancelRemark = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnknownPaymentReverse", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UnknownPaymentReverse_Booking_BookingID",
                        column: x => x.BookingID,
                        principalSchema: "SAL",
                        principalTable: "Booking",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UnknownPaymentReverse_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UnknownPaymentReverse_UnknownPayment_UnknownPaymentID",
                        column: x => x.UnknownPaymentID,
                        principalSchema: "FIN",
                        principalTable: "UnknownPayment",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UnknownPaymentReverse_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DirectCreditDebitExportDetail",
                schema: "FIN",
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
                    DirectCreditDebitExportHeaderID = table.Column<Guid>(nullable: false),
                    DirectCreditDebitApprovalFormID = table.Column<Guid>(nullable: false),
                    BatchID = table.Column<string>(maxLength: 50, nullable: true),
                    Seq = table.Column<int>(nullable: false),
                    UnitPriceInstallmentID = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(type: "Money", nullable: false),
                    ReceiveAmount = table.Column<decimal>(type: "Money", nullable: false),
                    TransCode = table.Column<string>(maxLength: 50, nullable: true),
                    IsComplete = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectCreditDebitExportDetail", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DirectCreditDebitExportDetail_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DirectCreditDebitExportDetail_DirectCreditDebitApprovalForm_DirectCreditDebitApprovalFormID",
                        column: x => x.DirectCreditDebitApprovalFormID,
                        principalSchema: "FIN",
                        principalTable: "DirectCreditDebitApprovalForm",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DirectCreditDebitExportDetail_DirectCreditDebitExportHeader_DirectCreditDebitExportHeaderID",
                        column: x => x.DirectCreditDebitExportHeaderID,
                        principalSchema: "FIN",
                        principalTable: "DirectCreditDebitExportHeader",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DirectCreditDebitExportDetail_UnitPriceInstallment_UnitPriceInstallmentID",
                        column: x => x.UnitPriceInstallmentID,
                        principalSchema: "SAL",
                        principalTable: "UnitPriceInstallment",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DirectCreditDebitExportDetail_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UnknownPaymentReverseDetail",
                schema: "FIN",
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
                    UnknownPaymentReverseID = table.Column<Guid>(nullable: true),
                    Amount = table.Column<decimal>(type: "Money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnknownPaymentReverseDetail", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UnknownPaymentReverseDetail_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UnknownPaymentReverseDetail_UnknownPaymentReverse_UnknownPaymentReverseID",
                        column: x => x.UnknownPaymentReverseID,
                        principalSchema: "FIN",
                        principalTable: "UnknownPaymentReverse",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UnknownPaymentReverseDetail_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DirectCreditDebitApprovalForm_BankBranchID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                column: "BankBranchID");

            migrationBuilder.CreateIndex(
                name: "IX_DirectCreditDebitApprovalForm_BankID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                column: "BankID");

            migrationBuilder.CreateIndex(
                name: "IX_DirectCreditDebitApprovalForm_ProvinceID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                column: "ProvinceID");

            migrationBuilder.CreateIndex(
                name: "IX_BillPaymentTransaction_BookingID",
                schema: "FIN",
                table: "BillPaymentTransaction",
                column: "BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_DirectCreditDebitExportDetail_CreatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitExportDetail",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_DirectCreditDebitExportDetail_DirectCreditDebitApprovalFormID",
                schema: "FIN",
                table: "DirectCreditDebitExportDetail",
                column: "DirectCreditDebitApprovalFormID");

            migrationBuilder.CreateIndex(
                name: "IX_DirectCreditDebitExportDetail_DirectCreditDebitExportHeaderID",
                schema: "FIN",
                table: "DirectCreditDebitExportDetail",
                column: "DirectCreditDebitExportHeaderID");

            migrationBuilder.CreateIndex(
                name: "IX_DirectCreditDebitExportDetail_UnitPriceInstallmentID",
                schema: "FIN",
                table: "DirectCreditDebitExportDetail",
                column: "UnitPriceInstallmentID");

            migrationBuilder.CreateIndex(
                name: "IX_DirectCreditDebitExportDetail_UpdatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitExportDetail",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_DirectCreditDebitExportHeader_BankAccountID",
                schema: "FIN",
                table: "DirectCreditDebitExportHeader",
                column: "BankAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_DirectCreditDebitExportHeader_BankID",
                schema: "FIN",
                table: "DirectCreditDebitExportHeader",
                column: "BankID");

            migrationBuilder.CreateIndex(
                name: "IX_DirectCreditDebitExportHeader_CreatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitExportHeader",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_DirectCreditDebitExportHeader_UpdatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitExportHeader",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UnknownPaymentReverse_BookingID",
                schema: "FIN",
                table: "UnknownPaymentReverse",
                column: "BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_UnknownPaymentReverse_CreatedByUserID",
                schema: "FIN",
                table: "UnknownPaymentReverse",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UnknownPaymentReverse_UnknownPaymentID",
                schema: "FIN",
                table: "UnknownPaymentReverse",
                column: "UnknownPaymentID");

            migrationBuilder.CreateIndex(
                name: "IX_UnknownPaymentReverse_UpdatedByUserID",
                schema: "FIN",
                table: "UnknownPaymentReverse",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UnknownPaymentReverseDetail_CreatedByUserID",
                schema: "FIN",
                table: "UnknownPaymentReverseDetail",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UnknownPaymentReverseDetail_UnknownPaymentReverseID",
                schema: "FIN",
                table: "UnknownPaymentReverseDetail",
                column: "UnknownPaymentReverseID");

            migrationBuilder.CreateIndex(
                name: "IX_UnknownPaymentReverseDetail_UpdatedByUserID",
                schema: "FIN",
                table: "UnknownPaymentReverseDetail",
                column: "UpdatedByUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_BillPayment_BankAccount_BankAccountID",
                schema: "FIN",
                table: "BillPayment",
                column: "BankAccountID",
                principalSchema: "MST",
                principalTable: "BankAccount",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BillPaymentTransaction_BillPayment_BillPaymentHeaderID",
                schema: "FIN",
                table: "BillPaymentTransaction",
                column: "BillPaymentHeaderID",
                principalSchema: "FIN",
                principalTable: "BillPayment",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BillPaymentTransaction_Booking_BookingID",
                schema: "FIN",
                table: "BillPaymentTransaction",
                column: "BookingID",
                principalSchema: "SAL",
                principalTable: "Booking",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectCreditDebitApprovalForm_BankBranch_BankBranchID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                column: "BankBranchID",
                principalSchema: "MST",
                principalTable: "BankBranch",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectCreditDebitApprovalForm_Bank_BankID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                column: "BankID",
                principalSchema: "MST",
                principalTable: "Bank",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectCreditDebitApprovalForm_Booking_BookingID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                column: "BookingID",
                principalSchema: "SAL",
                principalTable: "Booking",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectCreditDebitApprovalForm_Province_ProvinceID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                column: "ProvinceID",
                principalSchema: "MST",
                principalTable: "Province",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnknownPayment_Booking_BookingID",
                schema: "FIN",
                table: "UnknownPayment",
                column: "BookingID",
                principalSchema: "SAL",
                principalTable: "Booking",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillPayment_BankAccount_BankAccountID",
                schema: "FIN",
                table: "BillPayment");

            migrationBuilder.DropForeignKey(
                name: "FK_BillPaymentTransaction_BillPayment_BillPaymentHeaderID",
                schema: "FIN",
                table: "BillPaymentTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_BillPaymentTransaction_Booking_BookingID",
                schema: "FIN",
                table: "BillPaymentTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectCreditDebitApprovalForm_BankBranch_BankBranchID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectCreditDebitApprovalForm_Bank_BankID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectCreditDebitApprovalForm_Booking_BookingID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectCreditDebitApprovalForm_Province_ProvinceID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm");

            migrationBuilder.DropForeignKey(
                name: "FK_UnknownPayment_Booking_BookingID",
                schema: "FIN",
                table: "UnknownPayment");

            migrationBuilder.DropTable(
                name: "DirectCreditDebitExportDetail",
                schema: "FIN");

            migrationBuilder.DropTable(
                name: "UnknownPaymentReverseDetail",
                schema: "FIN");

            migrationBuilder.DropTable(
                name: "DirectCreditDebitExportHeader",
                schema: "FIN");

            migrationBuilder.DropTable(
                name: "UnknownPaymentReverse",
                schema: "FIN");

            migrationBuilder.DropIndex(
                name: "IX_DirectCreditDebitApprovalForm_BankBranchID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm");

            migrationBuilder.DropIndex(
                name: "IX_DirectCreditDebitApprovalForm_BankID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm");

            migrationBuilder.DropIndex(
                name: "IX_DirectCreditDebitApprovalForm_ProvinceID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm");

            migrationBuilder.DropIndex(
                name: "IX_BillPaymentTransaction_BookingID",
                schema: "FIN",
                table: "BillPaymentTransaction");

            migrationBuilder.DropColumn(
                name: "CancelRemark",
                schema: "FIN",
                table: "UnknownPayment");

            migrationBuilder.DropColumn(
                name: "Remark",
                schema: "FIN",
                table: "UnknownPayment");

            migrationBuilder.DropColumn(
                name: "UnknowPaymentStatus",
                schema: "FIN",
                table: "UnknownPayment");

            migrationBuilder.DropColumn(
                name: "AccountNO",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm");

            migrationBuilder.DropColumn(
                name: "ApproveDate",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm");

            migrationBuilder.DropColumn(
                name: "BankBranchID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm");

            migrationBuilder.DropColumn(
                name: "BankID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm");

            migrationBuilder.DropColumn(
                name: "CancelDate",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm");

            migrationBuilder.DropColumn(
                name: "CitizenIdentityNo",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm");

            migrationBuilder.DropColumn(
                name: "CreditCardExpireMonth",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm");

            migrationBuilder.DropColumn(
                name: "CreditCardExpireYear",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm");

            migrationBuilder.DropColumn(
                name: "FormStatus",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm");

            migrationBuilder.DropColumn(
                name: "ProvinceID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm");

            migrationBuilder.DropColumn(
                name: "RejectDate",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm");

            migrationBuilder.DropColumn(
                name: "Remark",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm");

            migrationBuilder.DropColumn(
                name: "StartDate",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm");

            migrationBuilder.DropColumn(
                name: "BillPaymentStatus",
                schema: "FIN",
                table: "BillPaymentTransaction");

            migrationBuilder.DropColumn(
                name: "BookingID",
                schema: "FIN",
                table: "BillPaymentTransaction");

            migrationBuilder.DropColumn(
                name: "ReconcileDate",
                schema: "FIN",
                table: "BillPaymentTransaction");

            migrationBuilder.DropColumn(
                name: "Remark",
                schema: "FIN",
                table: "BillPaymentTransaction");

            migrationBuilder.DropColumn(
                name: "ImportDate",
                schema: "FIN",
                table: "BillPayment");

            migrationBuilder.DropColumn(
                name: "ImportFileName",
                schema: "FIN",
                table: "BillPayment");

            migrationBuilder.DropColumn(
                name: "TotalRecord",
                schema: "FIN",
                table: "BillPayment");

            migrationBuilder.RenameColumn(
                name: "Remark",
                schema: "LET",
                table: "DownPaymentLetter",
                newName: "Memo");

            migrationBuilder.RenameColumn(
                name: "BookingID",
                schema: "FIN",
                table: "UnknownPayment",
                newName: "AttachFileFromBankID");

            migrationBuilder.RenameIndex(
                name: "IX_UnknownPayment_BookingID",
                schema: "FIN",
                table: "UnknownPayment",
                newName: "IX_UnknownPayment_AttachFileFromBankID");

            migrationBuilder.RenameColumn(
                name: "Remark",
                schema: "FIN",
                table: "Payment",
                newName: "Memo");

            migrationBuilder.RenameColumn(
                name: "OwnerName",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                newName: "ApprovalStatus");

            migrationBuilder.RenameColumn(
                name: "PaymentDate",
                schema: "FIN",
                table: "BillPaymentTransaction",
                newName: "PayDate");

            migrationBuilder.RenameColumn(
                name: "DeleteReason",
                schema: "FIN",
                table: "BillPaymentTransaction",
                newName: "UnitPriceItemKey");

            migrationBuilder.RenameColumn(
                name: "BillPaymentHeaderID",
                schema: "FIN",
                table: "BillPaymentTransaction",
                newName: "BillPaymentID");

            migrationBuilder.RenameIndex(
                name: "IX_BillPaymentTransaction_BillPaymentHeaderID",
                schema: "FIN",
                table: "BillPaymentTransaction",
                newName: "IX_BillPaymentTransaction_BillPaymentID");

            migrationBuilder.RenameColumn(
                name: "BankAccountID",
                schema: "FIN",
                table: "BillPayment",
                newName: "BankID");

            migrationBuilder.RenameIndex(
                name: "IX_BillPayment_BankAccountID",
                schema: "FIN",
                table: "BillPayment",
                newName: "IX_BillPayment_BankID");

            migrationBuilder.AddColumn<string>(
                name: "AttachFile",
                schema: "FIN",
                table: "UnknownPayment",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankDepositNo",
                schema: "FIN",
                table: "UnknownPayment",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CancelMemo",
                schema: "FIN",
                table: "UnknownPayment",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "FIN",
                table: "UnknownPayment",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TransferDate",
                schema: "FIN",
                table: "UnknownPayment",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "DirectCreditDebitTransactionID",
                schema: "FIN",
                table: "PaymentDirectCreditDebit",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DirectPeriod",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<Guid>(
                name: "BookingID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PayType",
                schema: "FIN",
                table: "BillPaymentTransaction",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BankRef3",
                schema: "FIN",
                table: "BillPaymentTransaction",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BankRef2",
                schema: "FIN",
                table: "BillPaymentTransaction",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BankRef1",
                schema: "FIN",
                table: "BillPaymentTransaction",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillPaymentType",
                schema: "FIN",
                table: "BillPaymentTransaction",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "FIN",
                table: "BillPaymentTransaction",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BatchID",
                schema: "FIN",
                table: "BillPayment",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ImportTime",
                schema: "FIN",
                table: "BillPayment",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PayDate",
                schema: "FIN",
                table: "BillPayment",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "DirectCreditDebitExport",
                schema: "FIN",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    BankID = table.Column<Guid>(nullable: true),
                    BatchID = table.Column<string>(nullable: true),
                    CompanyID = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    DirectFormType = table.Column<int>(nullable: false),
                    DirectPayDate = table.Column<DateTime>(nullable: false),
                    DirectPeriod = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsFromMigration = table.Column<bool>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectCreditDebitExport", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DirectCreditDebitExport_Bank_BankID",
                        column: x => x.BankID,
                        principalSchema: "MST",
                        principalTable: "Bank",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DirectCreditDebitExport_Company_CompanyID",
                        column: x => x.CompanyID,
                        principalSchema: "MST",
                        principalTable: "Company",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DirectCreditDebitExport_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DirectCreditDebitExport_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DirectCreditDebitUnitPriceItem",
                schema: "FIN",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    DirectCreditDebitFormID = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsFromMigration = table.Column<bool>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false),
                    UnitPriceItemKey = table.Column<string>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectCreditDebitUnitPriceItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DirectCreditDebitUnitPriceItem_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DirectCreditDebitUnitPriceItem_DirectCreditDebitApprovalForm_DirectCreditDebitFormID",
                        column: x => x.DirectCreditDebitFormID,
                        principalSchema: "FIN",
                        principalTable: "DirectCreditDebitApprovalForm",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DirectCreditDebitUnitPriceItem_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DirectCreditDetail",
                schema: "FIN",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    BankID = table.Column<Guid>(nullable: true),
                    CitizenIdentityNo = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    CreditCardExpireMonth = table.Column<int>(nullable: false),
                    CreditCardExpireYear = table.Column<int>(nullable: false),
                    CreditCardNo = table.Column<string>(nullable: true),
                    CreditCardOwner = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsFromMigration = table.Column<bool>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false),
                    Memo = table.Column<string>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectCreditDetail", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DirectCreditDetail_Bank_BankID",
                        column: x => x.BankID,
                        principalSchema: "MST",
                        principalTable: "Bank",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DirectCreditDetail_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DirectCreditDetail_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DirectDebitDetail",
                schema: "FIN",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    BankBranchID = table.Column<Guid>(nullable: true),
                    BankID = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsFromMigration = table.Column<bool>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false),
                    Memo = table.Column<string>(nullable: true),
                    ProvinceID = table.Column<Guid>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectDebitDetail", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DirectDebitDetail_BankBranch_BankBranchID",
                        column: x => x.BankBranchID,
                        principalSchema: "MST",
                        principalTable: "BankBranch",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DirectDebitDetail_Bank_BankID",
                        column: x => x.BankID,
                        principalSchema: "MST",
                        principalTable: "Bank",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DirectDebitDetail_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DirectDebitDetail_Province_ProvinceID",
                        column: x => x.ProvinceID,
                        principalSchema: "MST",
                        principalTable: "Province",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DirectDebitDetail_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DirectCreditDebitTransaction",
                schema: "FIN",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(type: "Money", nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    DirectCreditDebitUnitPriceItemID = table.Column<Guid>(nullable: false),
                    DueDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsFromMigration = table.Column<bool>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false),
                    Memo = table.Column<string>(nullable: true),
                    PayAmount = table.Column<decimal>(type: "Money", nullable: false),
                    PayDate = table.Column<DateTime>(nullable: false),
                    Result = table.Column<bool>(nullable: false),
                    TransactionNo = table.Column<string>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectCreditDebitTransaction", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DirectCreditDebitTransaction_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DirectCreditDebitTransaction_DirectCreditDebitUnitPriceItem_DirectCreditDebitUnitPriceItemID",
                        column: x => x.DirectCreditDebitUnitPriceItemID,
                        principalSchema: "FIN",
                        principalTable: "DirectCreditDebitUnitPriceItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DirectCreditDebitTransaction_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentDirectCreditDebit_DirectCreditDebitTransactionID",
                schema: "FIN",
                table: "PaymentDirectCreditDebit",
                column: "DirectCreditDebitTransactionID");

            migrationBuilder.CreateIndex(
                name: "IX_DirectCreditDebitExport_BankID",
                schema: "FIN",
                table: "DirectCreditDebitExport",
                column: "BankID");

            migrationBuilder.CreateIndex(
                name: "IX_DirectCreditDebitExport_CompanyID",
                schema: "FIN",
                table: "DirectCreditDebitExport",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_DirectCreditDebitExport_CreatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitExport",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_DirectCreditDebitExport_UpdatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitExport",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_DirectCreditDebitTransaction_CreatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitTransaction",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_DirectCreditDebitTransaction_DirectCreditDebitUnitPriceItemID",
                schema: "FIN",
                table: "DirectCreditDebitTransaction",
                column: "DirectCreditDebitUnitPriceItemID");

            migrationBuilder.CreateIndex(
                name: "IX_DirectCreditDebitTransaction_UpdatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitTransaction",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_DirectCreditDebitUnitPriceItem_CreatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitUnitPriceItem",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_DirectCreditDebitUnitPriceItem_DirectCreditDebitFormID",
                schema: "FIN",
                table: "DirectCreditDebitUnitPriceItem",
                column: "DirectCreditDebitFormID");

            migrationBuilder.CreateIndex(
                name: "IX_DirectCreditDebitUnitPriceItem_UpdatedByUserID",
                schema: "FIN",
                table: "DirectCreditDebitUnitPriceItem",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_DirectCreditDetail_BankID",
                schema: "FIN",
                table: "DirectCreditDetail",
                column: "BankID");

            migrationBuilder.CreateIndex(
                name: "IX_DirectCreditDetail_CreatedByUserID",
                schema: "FIN",
                table: "DirectCreditDetail",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_DirectCreditDetail_UpdatedByUserID",
                schema: "FIN",
                table: "DirectCreditDetail",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_DirectDebitDetail_BankBranchID",
                schema: "FIN",
                table: "DirectDebitDetail",
                column: "BankBranchID");

            migrationBuilder.CreateIndex(
                name: "IX_DirectDebitDetail_BankID",
                schema: "FIN",
                table: "DirectDebitDetail",
                column: "BankID");

            migrationBuilder.CreateIndex(
                name: "IX_DirectDebitDetail_CreatedByUserID",
                schema: "FIN",
                table: "DirectDebitDetail",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_DirectDebitDetail_ProvinceID",
                schema: "FIN",
                table: "DirectDebitDetail",
                column: "ProvinceID");

            migrationBuilder.CreateIndex(
                name: "IX_DirectDebitDetail_UpdatedByUserID",
                schema: "FIN",
                table: "DirectDebitDetail",
                column: "UpdatedByUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_BillPayment_Bank_BankID",
                schema: "FIN",
                table: "BillPayment",
                column: "BankID",
                principalSchema: "MST",
                principalTable: "Bank",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BillPaymentTransaction_BillPayment_BillPaymentID",
                schema: "FIN",
                table: "BillPaymentTransaction",
                column: "BillPaymentID",
                principalSchema: "FIN",
                principalTable: "BillPayment",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectCreditDebitApprovalForm_Booking_BookingID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                column: "BookingID",
                principalSchema: "SAL",
                principalTable: "Booking",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentDirectCreditDebit_DirectCreditDebitTransaction_DirectCreditDebitTransactionID",
                schema: "FIN",
                table: "PaymentDirectCreditDebit",
                column: "DirectCreditDebitTransactionID",
                principalSchema: "FIN",
                principalTable: "DirectCreditDebitTransaction",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnknownPayment_Bank_AttachFileFromBankID",
                schema: "FIN",
                table: "UnknownPayment",
                column: "AttachFileFromBankID",
                principalSchema: "MST",
                principalTable: "Bank",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
