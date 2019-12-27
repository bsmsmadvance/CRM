using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class UpdateLegalEntityAndBG : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BankAccountNo",
                schema: "MST",
                table: "LegalEntity",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BankID",
                schema: "MST",
                table: "LegalEntity",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductTypeMasterCenterID",
                schema: "MST",
                table: "BG",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LegalEntity_BankID",
                schema: "MST",
                table: "LegalEntity",
                column: "BankID");

            migrationBuilder.CreateIndex(
                name: "IX_BG_ProductTypeMasterCenterID",
                schema: "MST",
                table: "BG",
                column: "ProductTypeMasterCenterID");

            migrationBuilder.AddForeignKey(
                name: "FK_BG_MasterCenter_ProductTypeMasterCenterID",
                schema: "MST",
                table: "BG",
                column: "ProductTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LegalEntity_Bank_BankID",
                schema: "MST",
                table: "LegalEntity",
                column: "BankID",
                principalSchema: "MST",
                principalTable: "Bank",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BG_MasterCenter_ProductTypeMasterCenterID",
                schema: "MST",
                table: "BG");

            migrationBuilder.DropForeignKey(
                name: "FK_LegalEntity_Bank_BankID",
                schema: "MST",
                table: "LegalEntity");

            migrationBuilder.DropIndex(
                name: "IX_LegalEntity_BankID",
                schema: "MST",
                table: "LegalEntity");

            migrationBuilder.DropIndex(
                name: "IX_BG_ProductTypeMasterCenterID",
                schema: "MST",
                table: "BG");

            migrationBuilder.DropColumn(
                name: "BankAccountNo",
                schema: "MST",
                table: "LegalEntity");

            migrationBuilder.DropColumn(
                name: "BankID",
                schema: "MST",
                table: "LegalEntity");

            migrationBuilder.DropColumn(
                name: "ProductTypeMasterCenterID",
                schema: "MST",
                table: "BG");
        }
    }
}
