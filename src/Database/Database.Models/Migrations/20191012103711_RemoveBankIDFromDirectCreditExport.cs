using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class RemoveBankIDFromDirectCreditExport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DirectCreditDebitExportHeader_Bank_BankID",
                schema: "FIN",
                table: "DirectCreditDebitExportHeader");

            migrationBuilder.DropIndex(
                name: "IX_DirectCreditDebitExportHeader_BankID",
                schema: "FIN",
                table: "DirectCreditDebitExportHeader");

            migrationBuilder.DropColumn(
                name: "BankID",
                schema: "FIN",
                table: "DirectCreditDebitExportHeader");

            migrationBuilder.DropColumn(
                name: "DirectPeriod",
                schema: "FIN",
                table: "DirectCreditDebitExportHeader");

            migrationBuilder.DropColumn(
                name: "PeriodMonth",
                schema: "FIN",
                table: "DirectCreditDebitExportHeader");

            migrationBuilder.DropColumn(
                name: "PeriodYear",
                schema: "FIN",
                table: "DirectCreditDebitExportHeader");

            migrationBuilder.DropColumn(
                name: "ReceiveAmount",
                schema: "FIN",
                table: "DirectCreditDebitExportDetail");

            migrationBuilder.RenameColumn(
                name: "DirectPayDate",
                schema: "FIN",
                table: "DirectCreditDebitExportHeader",
                newName: "PeriodDate");

            migrationBuilder.AddColumn<Guid>(
                name: "BookingID",
                schema: "FIN",
                table: "DirectCreditDebitExportDetail",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsImport",
                schema: "FIN",
                table: "DirectCreditDebitExportDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DetailBatchID",
                schema: "FIN",
                table: "BillPaymentTransaction",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DirectCreditDebitExportDetail_BookingID",
                schema: "FIN",
                table: "DirectCreditDebitExportDetail",
                column: "BookingID");

            migrationBuilder.AddForeignKey(
                name: "FK_DirectCreditDebitExportDetail_Booking_BookingID",
                schema: "FIN",
                table: "DirectCreditDebitExportDetail",
                column: "BookingID",
                principalSchema: "SAL",
                principalTable: "Booking",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DirectCreditDebitExportDetail_Booking_BookingID",
                schema: "FIN",
                table: "DirectCreditDebitExportDetail");

            migrationBuilder.DropIndex(
                name: "IX_DirectCreditDebitExportDetail_BookingID",
                schema: "FIN",
                table: "DirectCreditDebitExportDetail");

            migrationBuilder.DropColumn(
                name: "BookingID",
                schema: "FIN",
                table: "DirectCreditDebitExportDetail");

            migrationBuilder.DropColumn(
                name: "IsImport",
                schema: "FIN",
                table: "DirectCreditDebitExportDetail");

            migrationBuilder.DropColumn(
                name: "DetailBatchID",
                schema: "FIN",
                table: "BillPaymentTransaction");

            migrationBuilder.RenameColumn(
                name: "PeriodDate",
                schema: "FIN",
                table: "DirectCreditDebitExportHeader",
                newName: "DirectPayDate");

            migrationBuilder.AddColumn<Guid>(
                name: "BankID",
                schema: "FIN",
                table: "DirectCreditDebitExportHeader",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DirectPeriod",
                schema: "FIN",
                table: "DirectCreditDebitExportHeader",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PeriodMonth",
                schema: "FIN",
                table: "DirectCreditDebitExportHeader",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PeriodYear",
                schema: "FIN",
                table: "DirectCreditDebitExportHeader",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "ReceiveAmount",
                schema: "FIN",
                table: "DirectCreditDebitExportDetail",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_DirectCreditDebitExportHeader_BankID",
                schema: "FIN",
                table: "DirectCreditDebitExportHeader",
                column: "BankID");

            migrationBuilder.AddForeignKey(
                name: "FK_DirectCreditDebitExportHeader_Bank_BankID",
                schema: "FIN",
                table: "DirectCreditDebitExportHeader",
                column: "BankID",
                principalSchema: "MST",
                principalTable: "Bank",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
