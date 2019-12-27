using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class CreateEDCAndEDCFee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardPaymentType",
                schema: "FIN",
                table: "EDCFee");

            migrationBuilder.DropColumn(
                name: "CardProvider",
                schema: "FIN",
                table: "EDCFee");

            migrationBuilder.DropColumn(
                name: "CardType",
                schema: "FIN",
                table: "EDCFee");

            migrationBuilder.DropColumn(
                name: "CustomerCardFrom",
                schema: "FIN",
                table: "EDCFee");

            migrationBuilder.DropColumn(
                name: "EDCType",
                schema: "FIN",
                table: "EDC");

            migrationBuilder.RenameTable(
                name: "EDCFee",
                schema: "FIN",
                newName: "EDCFee",
                newSchema: "MST");

            migrationBuilder.RenameTable(
                name: "EDC",
                schema: "FIN",
                newName: "EDC",
                newSchema: "MST");

            migrationBuilder.AddColumn<Guid>(
                name: "BankID",
                schema: "MST",
                table: "EDCFee",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreditCardPaymentTypeMasterCenterID",
                schema: "MST",
                table: "EDCFee",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreditCardTypeMasterCenterID",
                schema: "MST",
                table: "EDCFee",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsEDCBankCreditCard",
                schema: "MST",
                table: "EDCFee",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentCardTypeMasterCenterID",
                schema: "MST",
                table: "EDCFee",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TelNo",
                schema: "MST",
                table: "EDC",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "MST",
                table: "EDC",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BankAccountID",
                schema: "MST",
                table: "EDC",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CardMachineStatusMasterCenterID",
                schema: "MST",
                table: "EDC",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CardMachineTypeMasterCenterID",
                schema: "MST",
                table: "EDC",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyID",
                schema: "MST",
                table: "EDC",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceiveBy",
                schema: "MST",
                table: "EDC",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReceiveDate",
                schema: "MST",
                table: "EDC",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                schema: "MST",
                table: "EDC",
                maxLength: 5000,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EDCFee_BankID",
                schema: "MST",
                table: "EDCFee",
                column: "BankID");

            migrationBuilder.CreateIndex(
                name: "IX_EDCFee_CreditCardPaymentTypeMasterCenterID",
                schema: "MST",
                table: "EDCFee",
                column: "CreditCardPaymentTypeMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_EDCFee_CreditCardTypeMasterCenterID",
                schema: "MST",
                table: "EDCFee",
                column: "CreditCardTypeMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_EDCFee_PaymentCardTypeMasterCenterID",
                schema: "MST",
                table: "EDCFee",
                column: "PaymentCardTypeMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_EDC_BankAccountID",
                schema: "MST",
                table: "EDC",
                column: "BankAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_EDC_CardMachineStatusMasterCenterID",
                schema: "MST",
                table: "EDC",
                column: "CardMachineStatusMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_EDC_CardMachineTypeMasterCenterID",
                schema: "MST",
                table: "EDC",
                column: "CardMachineTypeMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_EDC_CompanyID",
                schema: "MST",
                table: "EDC",
                column: "CompanyID");

            migrationBuilder.AddForeignKey(
                name: "FK_EDC_BankAccount_BankAccountID",
                schema: "MST",
                table: "EDC",
                column: "BankAccountID",
                principalSchema: "MST",
                principalTable: "BankAccount",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EDC_MasterCenter_CardMachineStatusMasterCenterID",
                schema: "MST",
                table: "EDC",
                column: "CardMachineStatusMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EDC_MasterCenter_CardMachineTypeMasterCenterID",
                schema: "MST",
                table: "EDC",
                column: "CardMachineTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EDC_Company_CompanyID",
                schema: "MST",
                table: "EDC",
                column: "CompanyID",
                principalSchema: "MST",
                principalTable: "Company",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EDCFee_Bank_BankID",
                schema: "MST",
                table: "EDCFee",
                column: "BankID",
                principalSchema: "MST",
                principalTable: "Bank",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EDCFee_MasterCenter_CreditCardPaymentTypeMasterCenterID",
                schema: "MST",
                table: "EDCFee",
                column: "CreditCardPaymentTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EDCFee_MasterCenter_CreditCardTypeMasterCenterID",
                schema: "MST",
                table: "EDCFee",
                column: "CreditCardTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EDCFee_MasterCenter_PaymentCardTypeMasterCenterID",
                schema: "MST",
                table: "EDCFee",
                column: "PaymentCardTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EDC_BankAccount_BankAccountID",
                schema: "MST",
                table: "EDC");

            migrationBuilder.DropForeignKey(
                name: "FK_EDC_MasterCenter_CardMachineStatusMasterCenterID",
                schema: "MST",
                table: "EDC");

            migrationBuilder.DropForeignKey(
                name: "FK_EDC_MasterCenter_CardMachineTypeMasterCenterID",
                schema: "MST",
                table: "EDC");

            migrationBuilder.DropForeignKey(
                name: "FK_EDC_Company_CompanyID",
                schema: "MST",
                table: "EDC");

            migrationBuilder.DropForeignKey(
                name: "FK_EDCFee_Bank_BankID",
                schema: "MST",
                table: "EDCFee");

            migrationBuilder.DropForeignKey(
                name: "FK_EDCFee_MasterCenter_CreditCardPaymentTypeMasterCenterID",
                schema: "MST",
                table: "EDCFee");

            migrationBuilder.DropForeignKey(
                name: "FK_EDCFee_MasterCenter_CreditCardTypeMasterCenterID",
                schema: "MST",
                table: "EDCFee");

            migrationBuilder.DropForeignKey(
                name: "FK_EDCFee_MasterCenter_PaymentCardTypeMasterCenterID",
                schema: "MST",
                table: "EDCFee");

            migrationBuilder.DropIndex(
                name: "IX_EDCFee_BankID",
                schema: "MST",
                table: "EDCFee");

            migrationBuilder.DropIndex(
                name: "IX_EDCFee_CreditCardPaymentTypeMasterCenterID",
                schema: "MST",
                table: "EDCFee");

            migrationBuilder.DropIndex(
                name: "IX_EDCFee_CreditCardTypeMasterCenterID",
                schema: "MST",
                table: "EDCFee");

            migrationBuilder.DropIndex(
                name: "IX_EDCFee_PaymentCardTypeMasterCenterID",
                schema: "MST",
                table: "EDCFee");

            migrationBuilder.DropIndex(
                name: "IX_EDC_BankAccountID",
                schema: "MST",
                table: "EDC");

            migrationBuilder.DropIndex(
                name: "IX_EDC_CardMachineStatusMasterCenterID",
                schema: "MST",
                table: "EDC");

            migrationBuilder.DropIndex(
                name: "IX_EDC_CardMachineTypeMasterCenterID",
                schema: "MST",
                table: "EDC");

            migrationBuilder.DropIndex(
                name: "IX_EDC_CompanyID",
                schema: "MST",
                table: "EDC");

            migrationBuilder.DropColumn(
                name: "BankID",
                schema: "MST",
                table: "EDCFee");

            migrationBuilder.DropColumn(
                name: "CreditCardPaymentTypeMasterCenterID",
                schema: "MST",
                table: "EDCFee");

            migrationBuilder.DropColumn(
                name: "CreditCardTypeMasterCenterID",
                schema: "MST",
                table: "EDCFee");

            migrationBuilder.DropColumn(
                name: "IsEDCBankCreditCard",
                schema: "MST",
                table: "EDCFee");

            migrationBuilder.DropColumn(
                name: "PaymentCardTypeMasterCenterID",
                schema: "MST",
                table: "EDCFee");

            migrationBuilder.DropColumn(
                name: "BankAccountID",
                schema: "MST",
                table: "EDC");

            migrationBuilder.DropColumn(
                name: "CardMachineStatusMasterCenterID",
                schema: "MST",
                table: "EDC");

            migrationBuilder.DropColumn(
                name: "CardMachineTypeMasterCenterID",
                schema: "MST",
                table: "EDC");

            migrationBuilder.DropColumn(
                name: "CompanyID",
                schema: "MST",
                table: "EDC");

            migrationBuilder.DropColumn(
                name: "ReceiveBy",
                schema: "MST",
                table: "EDC");

            migrationBuilder.DropColumn(
                name: "ReceiveDate",
                schema: "MST",
                table: "EDC");

            migrationBuilder.DropColumn(
                name: "Remark",
                schema: "MST",
                table: "EDC");

            migrationBuilder.RenameTable(
                name: "EDCFee",
                schema: "MST",
                newName: "EDCFee",
                newSchema: "FIN");

            migrationBuilder.RenameTable(
                name: "EDC",
                schema: "MST",
                newName: "EDC",
                newSchema: "FIN");

            migrationBuilder.AddColumn<string>(
                name: "CardPaymentType",
                schema: "FIN",
                table: "EDCFee",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CardProvider",
                schema: "FIN",
                table: "EDCFee",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CardType",
                schema: "FIN",
                table: "EDCFee",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerCardFrom",
                schema: "FIN",
                table: "EDCFee",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TelNo",
                schema: "FIN",
                table: "EDC",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                schema: "FIN",
                table: "EDC",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EDCType",
                schema: "FIN",
                table: "EDC",
                nullable: true);
        }
    }
}
