using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddBankAccountTypeToLegalEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BankAccountTypeMasterCenterID",
                schema: "MST",
                table: "LegalEntity",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LegalEntity_BankAccountTypeMasterCenterID",
                schema: "MST",
                table: "LegalEntity",
                column: "BankAccountTypeMasterCenterID");

            migrationBuilder.AddForeignKey(
                name: "FK_LegalEntity_MasterCenter_BankAccountTypeMasterCenterID",
                schema: "MST",
                table: "LegalEntity",
                column: "BankAccountTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LegalEntity_MasterCenter_BankAccountTypeMasterCenterID",
                schema: "MST",
                table: "LegalEntity");

            migrationBuilder.DropIndex(
                name: "IX_LegalEntity_BankAccountTypeMasterCenterID",
                schema: "MST",
                table: "LegalEntity");

            migrationBuilder.DropColumn(
                name: "BankAccountTypeMasterCenterID",
                schema: "MST",
                table: "LegalEntity");
        }
    }
}
