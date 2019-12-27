using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ChangeReceiptTempRefToPayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptTempDetail_PaymentMethodToItem_PaymentMethodToItemID",
                schema: "FIN",
                table: "ReceiptTempDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptTempHeader_PaymentMethod_PaymentID",
                schema: "FIN",
                table: "ReceiptTempHeader");

            migrationBuilder.DropColumn(
                name: "PaymentMethodID",
                schema: "FIN",
                table: "ReceiptTempHeader");

            migrationBuilder.RenameColumn(
                name: "PaymentMethodToItemID",
                schema: "FIN",
                table: "ReceiptTempDetail",
                newName: "PaymentItemID");

            migrationBuilder.RenameIndex(
                name: "IX_ReceiptTempDetail_PaymentMethodToItemID",
                schema: "FIN",
                table: "ReceiptTempDetail",
                newName: "IX_ReceiptTempDetail_PaymentItemID");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptTempDetail_PaymentItem_PaymentItemID",
                schema: "FIN",
                table: "ReceiptTempDetail",
                column: "PaymentItemID",
                principalSchema: "FIN",
                principalTable: "PaymentItem",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptTempHeader_Payment_PaymentID",
                schema: "FIN",
                table: "ReceiptTempHeader",
                column: "PaymentID",
                principalSchema: "FIN",
                principalTable: "Payment",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptTempDetail_PaymentItem_PaymentItemID",
                schema: "FIN",
                table: "ReceiptTempDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptTempHeader_Payment_PaymentID",
                schema: "FIN",
                table: "ReceiptTempHeader");

            migrationBuilder.RenameColumn(
                name: "PaymentItemID",
                schema: "FIN",
                table: "ReceiptTempDetail",
                newName: "PaymentMethodToItemID");

            migrationBuilder.RenameIndex(
                name: "IX_ReceiptTempDetail_PaymentItemID",
                schema: "FIN",
                table: "ReceiptTempDetail",
                newName: "IX_ReceiptTempDetail_PaymentMethodToItemID");

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentMethodID",
                schema: "FIN",
                table: "ReceiptTempHeader",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptTempDetail_PaymentMethodToItem_PaymentMethodToItemID",
                schema: "FIN",
                table: "ReceiptTempDetail",
                column: "PaymentMethodToItemID",
                principalSchema: "FIN",
                principalTable: "PaymentMethodToItem",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptTempHeader_PaymentMethod_PaymentID",
                schema: "FIN",
                table: "ReceiptTempHeader",
                column: "PaymentID",
                principalSchema: "FIN",
                principalTable: "PaymentMethod",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
