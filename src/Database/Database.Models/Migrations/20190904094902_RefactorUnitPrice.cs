using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class RefactorUnitPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaidAmount",
                schema: "SAL",
                table: "UnitPriceItem");

            migrationBuilder.DropColumn(
                name: "ActiveDate",
                schema: "SAL",
                table: "QuotationUnitPrice");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "SAL",
                table: "QuotationUnitPrice");

            migrationBuilder.AlterColumn<bool>(
                name: "IsPaid",
                schema: "SAL",
                table: "UnitPriceItem",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsPaid",
                schema: "SAL",
                table: "UnitPriceInstallment",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AddColumn<Guid>(
                name: "UnitPriceStageMasterCenterID",
                schema: "SAL",
                table: "UnitPrice",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                schema: "SAL",
                table: "Booking",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MasterPriceItemID",
                schema: "FIN",
                table: "PaymentItem",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UnitPrice_UnitPriceStageMasterCenterID",
                schema: "SAL",
                table: "UnitPrice",
                column: "UnitPriceStageMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentItem_MasterPriceItemID",
                schema: "FIN",
                table: "PaymentItem",
                column: "MasterPriceItemID");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentItem_MasterPriceItem_MasterPriceItemID",
                schema: "FIN",
                table: "PaymentItem",
                column: "MasterPriceItemID",
                principalSchema: "MST",
                principalTable: "MasterPriceItem",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnitPrice_MasterCenter_UnitPriceStageMasterCenterID",
                schema: "SAL",
                table: "UnitPrice",
                column: "UnitPriceStageMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentItem_MasterPriceItem_MasterPriceItemID",
                schema: "FIN",
                table: "PaymentItem");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitPrice_MasterCenter_UnitPriceStageMasterCenterID",
                schema: "SAL",
                table: "UnitPrice");

            migrationBuilder.DropIndex(
                name: "IX_UnitPrice_UnitPriceStageMasterCenterID",
                schema: "SAL",
                table: "UnitPrice");

            migrationBuilder.DropIndex(
                name: "IX_PaymentItem_MasterPriceItemID",
                schema: "FIN",
                table: "PaymentItem");

            migrationBuilder.DropColumn(
                name: "UnitPriceStageMasterCenterID",
                schema: "SAL",
                table: "UnitPrice");

            migrationBuilder.DropColumn(
                name: "IsPaid",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "MasterPriceItemID",
                schema: "FIN",
                table: "PaymentItem");

            migrationBuilder.AlterColumn<bool>(
                name: "IsPaid",
                schema: "SAL",
                table: "UnitPriceItem",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PaidAmount",
                schema: "SAL",
                table: "UnitPriceItem",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<bool>(
                name: "IsPaid",
                schema: "SAL",
                table: "UnitPriceInstallment",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ActiveDate",
                schema: "SAL",
                table: "QuotationUnitPrice",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "SAL",
                table: "QuotationUnitPrice",
                nullable: false,
                defaultValue: false);
        }
    }
}
