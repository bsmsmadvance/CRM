using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddChangeUnitPaymentMethod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PaymentMethodID",
                schema: "FIN",
                table: "PaymentChangeUnit",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentChangeUnit_PaymentMethodID",
                schema: "FIN",
                table: "PaymentChangeUnit",
                column: "PaymentMethodID");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentChangeUnit_PaymentMethod_PaymentMethodID",
                schema: "FIN",
                table: "PaymentChangeUnit",
                column: "PaymentMethodID",
                principalSchema: "FIN",
                principalTable: "PaymentMethod",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentChangeUnit_PaymentMethod_PaymentMethodID",
                schema: "FIN",
                table: "PaymentChangeUnit");

            migrationBuilder.DropIndex(
                name: "IX_PaymentChangeUnit_PaymentMethodID",
                schema: "FIN",
                table: "PaymentChangeUnit");

            migrationBuilder.DropColumn(
                name: "PaymentMethodID",
                schema: "FIN",
                table: "PaymentChangeUnit");
        }
    }
}
