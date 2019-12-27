using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class CreateMasterBankAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccount_District_DistrictID",
                schema: "ACC",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "GLAccountID",
                schema: "ACC",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "Type",
                schema: "ACC",
                table: "BankAccount");

            migrationBuilder.RenameTable(
                name: "BankAccount",
                schema: "ACC",
                newName: "BankAccount",
                newSchema: "MST");

            migrationBuilder.RenameColumn(
                name: "GLAccountID",
                schema: "ACC",
                table: "PostGLAccount",
                newName: "GLAccountNo");

            migrationBuilder.RenameColumn(
                name: "isDirectDebit",
                schema: "MST",
                table: "BankAccount",
                newName: "IsDirectDebit");

            migrationBuilder.RenameColumn(
                name: "isDirectCredit",
                schema: "MST",
                table: "BankAccount",
                newName: "IsDirectCredit");

            migrationBuilder.RenameColumn(
                name: "isDepositAccount",
                schema: "MST",
                table: "BankAccount",
                newName: "IsDepositAccount");

            migrationBuilder.RenameColumn(
                name: "isBankTransfer",
                schema: "MST",
                table: "BankAccount",
                newName: "IsTransferAccount");

            migrationBuilder.RenameColumn(
                name: "DistrictID",
                schema: "MST",
                table: "BankAccount",
                newName: "ProvinceID");

            migrationBuilder.RenameIndex(
                name: "IX_BankAccount_DistrictID",
                schema: "MST",
                table: "BankAccount",
                newName: "IX_BankAccount_ProvinceID");

            migrationBuilder.AlterColumn<string>(
                name: "BankAccountNo",
                schema: "MST",
                table: "BankAccount",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BankAccountTypeMasterCenterID",
                schema: "MST",
                table: "BankAccount",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GLAccountNo",
                schema: "MST",
                table: "BankAccount",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "MST",
                table: "BankAccount",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPCard",
                schema: "MST",
                table: "BankAccount",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MerchantID",
                schema: "MST",
                table: "BankAccount",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServiceCode",
                schema: "MST",
                table: "BankAccount",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BankAccount_BankAccountTypeMasterCenterID",
                schema: "MST",
                table: "BankAccount",
                column: "BankAccountTypeMasterCenterID");

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccount_MasterCenter_BankAccountTypeMasterCenterID",
                schema: "MST",
                table: "BankAccount",
                column: "BankAccountTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccount_Province_ProvinceID",
                schema: "MST",
                table: "BankAccount",
                column: "ProvinceID",
                principalSchema: "MST",
                principalTable: "Province",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccount_MasterCenter_BankAccountTypeMasterCenterID",
                schema: "MST",
                table: "BankAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_BankAccount_Province_ProvinceID",
                schema: "MST",
                table: "BankAccount");

            migrationBuilder.DropIndex(
                name: "IX_BankAccount_BankAccountTypeMasterCenterID",
                schema: "MST",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "BankAccountTypeMasterCenterID",
                schema: "MST",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "GLAccountNo",
                schema: "MST",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "MST",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "IsPCard",
                schema: "MST",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "MerchantID",
                schema: "MST",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "ServiceCode",
                schema: "MST",
                table: "BankAccount");

            migrationBuilder.RenameTable(
                name: "BankAccount",
                schema: "MST",
                newName: "BankAccount",
                newSchema: "ACC");

            migrationBuilder.RenameColumn(
                name: "GLAccountNo",
                schema: "ACC",
                table: "PostGLAccount",
                newName: "GLAccountID");

            migrationBuilder.RenameColumn(
                name: "IsDirectDebit",
                schema: "ACC",
                table: "BankAccount",
                newName: "isDirectDebit");

            migrationBuilder.RenameColumn(
                name: "IsDirectCredit",
                schema: "ACC",
                table: "BankAccount",
                newName: "isDirectCredit");

            migrationBuilder.RenameColumn(
                name: "IsDepositAccount",
                schema: "ACC",
                table: "BankAccount",
                newName: "isDepositAccount");

            migrationBuilder.RenameColumn(
                name: "ProvinceID",
                schema: "ACC",
                table: "BankAccount",
                newName: "DistrictID");

            migrationBuilder.RenameColumn(
                name: "IsTransferAccount",
                schema: "ACC",
                table: "BankAccount",
                newName: "isBankTransfer");

            migrationBuilder.RenameIndex(
                name: "IX_BankAccount_ProvinceID",
                schema: "ACC",
                table: "BankAccount",
                newName: "IX_BankAccount_DistrictID");

            migrationBuilder.AlterColumn<string>(
                name: "BankAccountNo",
                schema: "ACC",
                table: "BankAccount",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GLAccountID",
                schema: "ACC",
                table: "BankAccount",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                schema: "ACC",
                table: "BankAccount",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccount_District_DistrictID",
                schema: "ACC",
                table: "BankAccount",
                column: "DistrictID",
                principalSchema: "MST",
                principalTable: "District",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
