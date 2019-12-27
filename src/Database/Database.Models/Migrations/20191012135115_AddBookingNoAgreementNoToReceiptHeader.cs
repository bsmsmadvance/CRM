using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddBookingNoAgreementNoToReceiptHeader : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AgreementNo",
                schema: "FIN",
                table: "ReceiptTempHeader",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BookingNo",
                schema: "FIN",
                table: "ReceiptTempHeader",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyTaxID",
                schema: "FIN",
                table: "ReceiptTempHeader",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                schema: "FIN",
                table: "ReceiptTempHeader",
                maxLength: 5000,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                schema: "FIN",
                table: "ReceiptTempDetail",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "AgreementNo",
                schema: "FIN",
                table: "ReceiptHeader",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BookingNo",
                schema: "FIN",
                table: "ReceiptHeader",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyTaxID",
                schema: "FIN",
                table: "ReceiptHeader",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NextInstallmentDueDate",
                schema: "FIN",
                table: "ReceiptHeader",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                schema: "FIN",
                table: "ReceiptHeader",
                maxLength: 5000,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                schema: "FIN",
                table: "ReceiptDetail",
                type: "Money",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgreementNo",
                schema: "FIN",
                table: "ReceiptTempHeader");

            migrationBuilder.DropColumn(
                name: "BookingNo",
                schema: "FIN",
                table: "ReceiptTempHeader");

            migrationBuilder.DropColumn(
                name: "CompanyTaxID",
                schema: "FIN",
                table: "ReceiptTempHeader");

            migrationBuilder.DropColumn(
                name: "Remark",
                schema: "FIN",
                table: "ReceiptTempHeader");

            migrationBuilder.DropColumn(
                name: "Amount",
                schema: "FIN",
                table: "ReceiptTempDetail");

            migrationBuilder.DropColumn(
                name: "AgreementNo",
                schema: "FIN",
                table: "ReceiptHeader");

            migrationBuilder.DropColumn(
                name: "BookingNo",
                schema: "FIN",
                table: "ReceiptHeader");

            migrationBuilder.DropColumn(
                name: "CompanyTaxID",
                schema: "FIN",
                table: "ReceiptHeader");

            migrationBuilder.DropColumn(
                name: "NextInstallmentDueDate",
                schema: "FIN",
                table: "ReceiptHeader");

            migrationBuilder.DropColumn(
                name: "Remark",
                schema: "FIN",
                table: "ReceiptHeader");

            migrationBuilder.DropColumn(
                name: "Amount",
                schema: "FIN",
                table: "ReceiptDetail");
        }
    }
}
