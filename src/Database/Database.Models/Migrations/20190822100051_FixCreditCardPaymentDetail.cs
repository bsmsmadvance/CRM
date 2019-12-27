using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class FixCreditCardPaymentDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardPaymentType",
                schema: "FIN",
                table: "PaymentCreditCard");

            migrationBuilder.DropColumn(
                name: "CardProvider",
                schema: "FIN",
                table: "PaymentCreditCard");

            migrationBuilder.RenameColumn(
                name: "ItemAmout",
                schema: "FIN",
                table: "PaymentItem",
                newName: "ItemAmount");

            migrationBuilder.AlterColumn<string>(
                name: "CardNo",
                schema: "FIN",
                table: "PaymentCreditCard",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreditCardPaymentTypeMasterCenterID",
                schema: "FIN",
                table: "PaymentCreditCard",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreditCardTypeMasterCenterID",
                schema: "FIN",
                table: "PaymentCreditCard",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsWrongAccount",
                schema: "FIN",
                table: "PaymentCreditCard",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "AttachFile",
                schema: "FIN",
                table: "Payment",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentCreditCard_CreditCardPaymentTypeMasterCenterID",
                schema: "FIN",
                table: "PaymentCreditCard",
                column: "CreditCardPaymentTypeMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentCreditCard_CreditCardTypeMasterCenterID",
                schema: "FIN",
                table: "PaymentCreditCard",
                column: "CreditCardTypeMasterCenterID");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentCreditCard_MasterCenter_CreditCardPaymentTypeMasterCenterID",
                schema: "FIN",
                table: "PaymentCreditCard",
                column: "CreditCardPaymentTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentCreditCard_MasterCenter_CreditCardTypeMasterCenterID",
                schema: "FIN",
                table: "PaymentCreditCard",
                column: "CreditCardTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentCreditCard_MasterCenter_CreditCardPaymentTypeMasterCenterID",
                schema: "FIN",
                table: "PaymentCreditCard");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentCreditCard_MasterCenter_CreditCardTypeMasterCenterID",
                schema: "FIN",
                table: "PaymentCreditCard");

            migrationBuilder.DropIndex(
                name: "IX_PaymentCreditCard_CreditCardPaymentTypeMasterCenterID",
                schema: "FIN",
                table: "PaymentCreditCard");

            migrationBuilder.DropIndex(
                name: "IX_PaymentCreditCard_CreditCardTypeMasterCenterID",
                schema: "FIN",
                table: "PaymentCreditCard");

            migrationBuilder.DropColumn(
                name: "CreditCardPaymentTypeMasterCenterID",
                schema: "FIN",
                table: "PaymentCreditCard");

            migrationBuilder.DropColumn(
                name: "CreditCardTypeMasterCenterID",
                schema: "FIN",
                table: "PaymentCreditCard");

            migrationBuilder.DropColumn(
                name: "IsWrongAccount",
                schema: "FIN",
                table: "PaymentCreditCard");

            migrationBuilder.RenameColumn(
                name: "ItemAmount",
                schema: "FIN",
                table: "PaymentItem",
                newName: "ItemAmout");

            migrationBuilder.AlterColumn<string>(
                name: "CardNo",
                schema: "FIN",
                table: "PaymentCreditCard",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CardPaymentType",
                schema: "FIN",
                table: "PaymentCreditCard",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CardProvider",
                schema: "FIN",
                table: "PaymentCreditCard",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AttachFile",
                schema: "FIN",
                table: "Payment",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true);
        }
    }
}
