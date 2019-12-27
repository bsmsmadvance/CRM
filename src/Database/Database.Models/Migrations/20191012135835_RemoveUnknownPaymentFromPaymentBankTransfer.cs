using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class RemoveUnknownPaymentFromPaymentBankTransfer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentBankTransfer_UnknownPayment_UnknownPaymentID",
                schema: "FIN",
                table: "PaymentBankTransfer");

            migrationBuilder.DropIndex(
                name: "IX_PaymentBankTransfer_UnknownPaymentID",
                schema: "FIN",
                table: "PaymentBankTransfer");

            migrationBuilder.DropColumn(
                name: "UnknownPaymentID",
                schema: "FIN",
                table: "PaymentBankTransfer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UnknownPaymentID",
                schema: "FIN",
                table: "PaymentBankTransfer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentBankTransfer_UnknownPaymentID",
                schema: "FIN",
                table: "PaymentBankTransfer",
                column: "UnknownPaymentID");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentBankTransfer_UnknownPayment_UnknownPaymentID",
                schema: "FIN",
                table: "PaymentBankTransfer",
                column: "UnknownPaymentID",
                principalSchema: "FIN",
                principalTable: "UnknownPayment",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
