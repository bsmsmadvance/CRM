using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddField_Referent_in_FET_kim : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FET_PaymentCreditCard_PaymentCreditCardID",
                schema: "FIN",
                table: "FET");

            migrationBuilder.DropForeignKey(
                name: "FK_FET_PaymentForeignBankTransfer_PaymentForeignBankTransferID",
                schema: "FIN",
                table: "FET");

            migrationBuilder.DropIndex(
                name: "IX_FET_PaymentCreditCardID",
                schema: "FIN",
                table: "FET");

            migrationBuilder.DropIndex(
                name: "IX_FET_PaymentForeignBankTransferID",
                schema: "FIN",
                table: "FET");

            migrationBuilder.DropColumn(
                name: "PaymentCreditCardID",
                schema: "FIN",
                table: "FET");

            migrationBuilder.RenameColumn(
                name: "PaymentForeignBankTransferID",
                schema: "FIN",
                table: "FET",
                newName: "ReferentGUID");

            migrationBuilder.AddColumn<string>(
                name: "ReferentType",
                schema: "FIN",
                table: "FET",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReferentType",
                schema: "FIN",
                table: "FET");

            migrationBuilder.RenameColumn(
                name: "ReferentGUID",
                schema: "FIN",
                table: "FET",
                newName: "PaymentForeignBankTransferID");

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentCreditCardID",
                schema: "FIN",
                table: "FET",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FET_PaymentCreditCardID",
                schema: "FIN",
                table: "FET",
                column: "PaymentCreditCardID");

            migrationBuilder.CreateIndex(
                name: "IX_FET_PaymentForeignBankTransferID",
                schema: "FIN",
                table: "FET",
                column: "PaymentForeignBankTransferID");

            migrationBuilder.AddForeignKey(
                name: "FK_FET_PaymentCreditCard_PaymentCreditCardID",
                schema: "FIN",
                table: "FET",
                column: "PaymentCreditCardID",
                principalSchema: "FIN",
                principalTable: "PaymentCreditCard",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FET_PaymentForeignBankTransfer_PaymentForeignBankTransferID",
                schema: "FIN",
                table: "FET",
                column: "PaymentForeignBankTransferID",
                principalSchema: "FIN",
                principalTable: "PaymentForeignBankTransfer",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
