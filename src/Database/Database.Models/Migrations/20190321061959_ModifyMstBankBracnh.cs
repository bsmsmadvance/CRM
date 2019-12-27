using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ModifyMstBankBracnh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "District",
                schema: "MST",
                table: "BankBranch");

            migrationBuilder.DropColumn(
                name: "Province",
                schema: "MST",
                table: "BankBranch");

            migrationBuilder.DropColumn(
                name: "SubDistrict",
                schema: "MST",
                table: "BankBranch");

            migrationBuilder.AddColumn<Guid>(
                name: "DistrictID",
                schema: "MST",
                table: "BankBranch",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProvinceID",
                schema: "MST",
                table: "BankBranch",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SubDistrictID",
                schema: "MST",
                table: "BankBranch",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BankBranch_DistrictID",
                schema: "MST",
                table: "BankBranch",
                column: "DistrictID");

            migrationBuilder.CreateIndex(
                name: "IX_BankBranch_ProvinceID",
                schema: "MST",
                table: "BankBranch",
                column: "ProvinceID");

            migrationBuilder.CreateIndex(
                name: "IX_BankBranch_SubDistrictID",
                schema: "MST",
                table: "BankBranch",
                column: "SubDistrictID");

            migrationBuilder.AddForeignKey(
                name: "FK_BankBranch_District_DistrictID",
                schema: "MST",
                table: "BankBranch",
                column: "DistrictID",
                principalSchema: "MST",
                principalTable: "District",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BankBranch_Province_ProvinceID",
                schema: "MST",
                table: "BankBranch",
                column: "ProvinceID",
                principalSchema: "MST",
                principalTable: "Province",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BankBranch_SubDistrict_SubDistrictID",
                schema: "MST",
                table: "BankBranch",
                column: "SubDistrictID",
                principalSchema: "MST",
                principalTable: "SubDistrict",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankBranch_District_DistrictID",
                schema: "MST",
                table: "BankBranch");

            migrationBuilder.DropForeignKey(
                name: "FK_BankBranch_Province_ProvinceID",
                schema: "MST",
                table: "BankBranch");

            migrationBuilder.DropForeignKey(
                name: "FK_BankBranch_SubDistrict_SubDistrictID",
                schema: "MST",
                table: "BankBranch");

            migrationBuilder.DropIndex(
                name: "IX_BankBranch_DistrictID",
                schema: "MST",
                table: "BankBranch");

            migrationBuilder.DropIndex(
                name: "IX_BankBranch_ProvinceID",
                schema: "MST",
                table: "BankBranch");

            migrationBuilder.DropIndex(
                name: "IX_BankBranch_SubDistrictID",
                schema: "MST",
                table: "BankBranch");

            migrationBuilder.DropColumn(
                name: "DistrictID",
                schema: "MST",
                table: "BankBranch");

            migrationBuilder.DropColumn(
                name: "ProvinceID",
                schema: "MST",
                table: "BankBranch");

            migrationBuilder.DropColumn(
                name: "SubDistrictID",
                schema: "MST",
                table: "BankBranch");

            migrationBuilder.AddColumn<string>(
                name: "District",
                schema: "MST",
                table: "BankBranch",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Province",
                schema: "MST",
                table: "BankBranch",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubDistrict",
                schema: "MST",
                table: "BankBranch",
                nullable: true);
        }
    }
}
