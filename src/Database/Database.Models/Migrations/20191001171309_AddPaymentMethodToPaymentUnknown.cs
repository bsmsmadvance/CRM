using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddPaymentMethodToPaymentUnknown : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PaymentMethodID",
                schema: "FIN",
                table: "PaymentUnknownPayment",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_PaymentUnknownPayment_PaymentMethodID",
                schema: "FIN",
                table: "PaymentUnknownPayment",
                column: "PaymentMethodID");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentUnknownPayment_PaymentMethod_PaymentMethodID",
                schema: "FIN",
                table: "PaymentUnknownPayment",
                column: "PaymentMethodID",
                principalSchema: "FIN",
                principalTable: "PaymentMethod",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentUnknownPayment_PaymentMethod_PaymentMethodID",
                schema: "FIN",
                table: "PaymentUnknownPayment");

            migrationBuilder.DropIndex(
                name: "IX_PaymentUnknownPayment_PaymentMethodID",
                schema: "FIN",
                table: "PaymentUnknownPayment");

            migrationBuilder.DropColumn(
                name: "PaymentMethodID",
                schema: "FIN",
                table: "PaymentUnknownPayment");
        }
    }
}
