using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class alter_column_on_BillPaymentDetailTemp_kim : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillPaymentTransactionTemp_BillPayment_BillPaymentHeaderID",
                schema: "FIN",
                table: "BillPaymentTransactionTemp");

            migrationBuilder.RenameColumn(
                name: "AttachFile",
                schema: "FIN",
                table: "FET",
                newName: "AttachFileUrl");

            migrationBuilder.AddColumn<Guid>(
                name: "FeeConfirmByUserID",
                schema: "FIN",
                table: "PaymentPersonalCheque",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FeeConfirmDate",
                schema: "FIN",
                table: "PaymentPersonalCheque",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFeeConfirm",
                schema: "FIN",
                table: "PaymentPersonalCheque",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "FeeConfirmByUserID",
                schema: "FIN",
                table: "PaymentDebitCard",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FeeConfirmDate",
                schema: "FIN",
                table: "PaymentDebitCard",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FeePercent",
                schema: "FIN",
                table: "PaymentDebitCard",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsFeeConfirm",
                schema: "FIN",
                table: "PaymentDebitCard",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "FeeConfirmByUserID",
                schema: "FIN",
                table: "PaymentCreditCard",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FeeConfirmDate",
                schema: "FIN",
                table: "PaymentCreditCard",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FeePercent",
                schema: "FIN",
                table: "PaymentCreditCard",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsFeeConfirm",
                schema: "FIN",
                table: "PaymentCreditCard",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "FeeConfirmByUserID",
                schema: "FIN",
                table: "PaymentCashierCheque",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FeeConfirmDate",
                schema: "FIN",
                table: "PaymentCashierCheque",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFeeConfirm",
                schema: "FIN",
                table: "PaymentCashierCheque",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "AttachFileName",
                schema: "FIN",
                table: "FET",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentPersonalCheque_FeeConfirmByUserID",
                schema: "FIN",
                table: "PaymentPersonalCheque",
                column: "FeeConfirmByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentDebitCard_FeeConfirmByUserID",
                schema: "FIN",
                table: "PaymentDebitCard",
                column: "FeeConfirmByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentCreditCard_FeeConfirmByUserID",
                schema: "FIN",
                table: "PaymentCreditCard",
                column: "FeeConfirmByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentCashierCheque_FeeConfirmByUserID",
                schema: "FIN",
                table: "PaymentCashierCheque",
                column: "FeeConfirmByUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_BillPaymentTransactionTemp_BillPaymentTemp_BillPaymentHeaderID",
                schema: "FIN",
                table: "BillPaymentTransactionTemp",
                column: "BillPaymentHeaderID",
                principalSchema: "FIN",
                principalTable: "BillPaymentTemp",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentCashierCheque_User_FeeConfirmByUserID",
                schema: "FIN",
                table: "PaymentCashierCheque",
                column: "FeeConfirmByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentCreditCard_User_FeeConfirmByUserID",
                schema: "FIN",
                table: "PaymentCreditCard",
                column: "FeeConfirmByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentDebitCard_User_FeeConfirmByUserID",
                schema: "FIN",
                table: "PaymentDebitCard",
                column: "FeeConfirmByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentPersonalCheque_User_FeeConfirmByUserID",
                schema: "FIN",
                table: "PaymentPersonalCheque",
                column: "FeeConfirmByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillPaymentTransactionTemp_BillPaymentTemp_BillPaymentHeaderID",
                schema: "FIN",
                table: "BillPaymentTransactionTemp");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentCashierCheque_User_FeeConfirmByUserID",
                schema: "FIN",
                table: "PaymentCashierCheque");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentCreditCard_User_FeeConfirmByUserID",
                schema: "FIN",
                table: "PaymentCreditCard");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentDebitCard_User_FeeConfirmByUserID",
                schema: "FIN",
                table: "PaymentDebitCard");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentPersonalCheque_User_FeeConfirmByUserID",
                schema: "FIN",
                table: "PaymentPersonalCheque");

            migrationBuilder.DropIndex(
                name: "IX_PaymentPersonalCheque_FeeConfirmByUserID",
                schema: "FIN",
                table: "PaymentPersonalCheque");

            migrationBuilder.DropIndex(
                name: "IX_PaymentDebitCard_FeeConfirmByUserID",
                schema: "FIN",
                table: "PaymentDebitCard");

            migrationBuilder.DropIndex(
                name: "IX_PaymentCreditCard_FeeConfirmByUserID",
                schema: "FIN",
                table: "PaymentCreditCard");

            migrationBuilder.DropIndex(
                name: "IX_PaymentCashierCheque_FeeConfirmByUserID",
                schema: "FIN",
                table: "PaymentCashierCheque");

            migrationBuilder.DropColumn(
                name: "FeeConfirmByUserID",
                schema: "FIN",
                table: "PaymentPersonalCheque");

            migrationBuilder.DropColumn(
                name: "FeeConfirmDate",
                schema: "FIN",
                table: "PaymentPersonalCheque");

            migrationBuilder.DropColumn(
                name: "IsFeeConfirm",
                schema: "FIN",
                table: "PaymentPersonalCheque");

            migrationBuilder.DropColumn(
                name: "FeeConfirmByUserID",
                schema: "FIN",
                table: "PaymentDebitCard");

            migrationBuilder.DropColumn(
                name: "FeeConfirmDate",
                schema: "FIN",
                table: "PaymentDebitCard");

            migrationBuilder.DropColumn(
                name: "FeePercent",
                schema: "FIN",
                table: "PaymentDebitCard");

            migrationBuilder.DropColumn(
                name: "IsFeeConfirm",
                schema: "FIN",
                table: "PaymentDebitCard");

            migrationBuilder.DropColumn(
                name: "FeeConfirmByUserID",
                schema: "FIN",
                table: "PaymentCreditCard");

            migrationBuilder.DropColumn(
                name: "FeeConfirmDate",
                schema: "FIN",
                table: "PaymentCreditCard");

            migrationBuilder.DropColumn(
                name: "FeePercent",
                schema: "FIN",
                table: "PaymentCreditCard");

            migrationBuilder.DropColumn(
                name: "IsFeeConfirm",
                schema: "FIN",
                table: "PaymentCreditCard");

            migrationBuilder.DropColumn(
                name: "FeeConfirmByUserID",
                schema: "FIN",
                table: "PaymentCashierCheque");

            migrationBuilder.DropColumn(
                name: "FeeConfirmDate",
                schema: "FIN",
                table: "PaymentCashierCheque");

            migrationBuilder.DropColumn(
                name: "IsFeeConfirm",
                schema: "FIN",
                table: "PaymentCashierCheque");

            migrationBuilder.DropColumn(
                name: "AttachFileName",
                schema: "FIN",
                table: "FET");

            migrationBuilder.RenameColumn(
                name: "AttachFileUrl",
                schema: "FIN",
                table: "FET",
                newName: "AttachFile");

            migrationBuilder.AddForeignKey(
                name: "FK_BillPaymentTransactionTemp_BillPayment_BillPaymentHeaderID",
                schema: "FIN",
                table: "BillPaymentTransactionTemp",
                column: "BillPaymentHeaderID",
                principalSchema: "FIN",
                principalTable: "BillPayment",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
