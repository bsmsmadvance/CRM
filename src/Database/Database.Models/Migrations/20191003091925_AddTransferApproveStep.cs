using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddTransferApproveStep : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AccountApprovedDate",
                schema: "SAL",
                table: "Transfer",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AccountApprovedUserID",
                schema: "SAL",
                table: "Transfer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAccountApproved",
                schema: "SAL",
                table: "Transfer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPaymentConfirmed",
                schema: "SAL",
                table: "Transfer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsReadyToTransfer",
                schema: "SAL",
                table: "Transfer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSentToFinance",
                schema: "SAL",
                table: "Transfer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTransferConfirmed",
                schema: "SAL",
                table: "Transfer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentConfirmedDate",
                schema: "SAL",
                table: "Transfer",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentConfirmedUserID",
                schema: "SAL",
                table: "Transfer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReadyToTransferDate",
                schema: "SAL",
                table: "Transfer",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ReadyToTransferUserID",
                schema: "SAL",
                table: "Transfer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SentToFinanceDate",
                schema: "SAL",
                table: "Transfer",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SentToFinanceUserID",
                schema: "SAL",
                table: "Transfer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TransferConfirmedDate",
                schema: "SAL",
                table: "Transfer",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TransferConfirmedUserID",
                schema: "SAL",
                table: "Transfer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountApprovedDate",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "AccountApprovedUserID",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "IsAccountApproved",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "IsPaymentConfirmed",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "IsReadyToTransfer",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "IsSentToFinance",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "IsTransferConfirmed",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "PaymentConfirmedDate",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "PaymentConfirmedUserID",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "ReadyToTransferDate",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "ReadyToTransferUserID",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "SentToFinanceDate",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "SentToFinanceUserID",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "TransferConfirmedDate",
                schema: "SAL",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "TransferConfirmedUserID",
                schema: "SAL",
                table: "Transfer");
        }
    }
}
