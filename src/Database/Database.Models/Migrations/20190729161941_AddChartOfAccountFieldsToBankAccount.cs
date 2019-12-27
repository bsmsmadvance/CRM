using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddChartOfAccountFieldsToBankAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GLAccountTypeMasterCenterID",
                schema: "MST",
                table: "BankAccount",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasVat",
                schema: "MST",
                table: "BankAccount",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "MST",
                table: "BankAccount",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                schema: "MST",
                table: "BankAccount",
                maxLength: 5000,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BankAccount_GLAccountTypeMasterCenterID",
                schema: "MST",
                table: "BankAccount",
                column: "GLAccountTypeMasterCenterID");

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccount_MasterCenter_GLAccountTypeMasterCenterID",
                schema: "MST",
                table: "BankAccount",
                column: "GLAccountTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccount_MasterCenter_GLAccountTypeMasterCenterID",
                schema: "MST",
                table: "BankAccount");

            migrationBuilder.DropIndex(
                name: "IX_BankAccount_GLAccountTypeMasterCenterID",
                schema: "MST",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "GLAccountTypeMasterCenterID",
                schema: "MST",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "HasVat",
                schema: "MST",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "MST",
                table: "BankAccount");

            migrationBuilder.DropColumn(
                name: "Remark",
                schema: "MST",
                table: "BankAccount");
        }
    }
}
