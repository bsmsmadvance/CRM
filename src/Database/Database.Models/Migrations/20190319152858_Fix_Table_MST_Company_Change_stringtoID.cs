using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class Fix_Table_MST_Company_Change_stringtoID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DistrictEN",
                schema: "MST",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "DistrictTH",
                schema: "MST",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "ProvinceEN",
                schema: "MST",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "ProvinceTH",
                schema: "MST",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "SubDistrictEN",
                schema: "MST",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "SubDistrictTH",
                schema: "MST",
                table: "Company");

            migrationBuilder.AddColumn<Guid>(
                name: "DistrictID",
                schema: "MST",
                table: "Company",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProvinceID",
                schema: "MST",
                table: "Company",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SubDistrictID",
                schema: "MST",
                table: "Company",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Company_DistrictID",
                schema: "MST",
                table: "Company",
                column: "DistrictID");

            migrationBuilder.CreateIndex(
                name: "IX_Company_ProvinceID",
                schema: "MST",
                table: "Company",
                column: "ProvinceID");

            migrationBuilder.CreateIndex(
                name: "IX_Company_SubDistrictID",
                schema: "MST",
                table: "Company",
                column: "SubDistrictID");

            migrationBuilder.AddForeignKey(
                name: "FK_Company_District_DistrictID",
                schema: "MST",
                table: "Company",
                column: "DistrictID",
                principalSchema: "MST",
                principalTable: "District",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Company_Province_ProvinceID",
                schema: "MST",
                table: "Company",
                column: "ProvinceID",
                principalSchema: "MST",
                principalTable: "Province",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Company_SubDistrict_SubDistrictID",
                schema: "MST",
                table: "Company",
                column: "SubDistrictID",
                principalSchema: "MST",
                principalTable: "SubDistrict",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Company_District_DistrictID",
                schema: "MST",
                table: "Company");

            migrationBuilder.DropForeignKey(
                name: "FK_Company_Province_ProvinceID",
                schema: "MST",
                table: "Company");

            migrationBuilder.DropForeignKey(
                name: "FK_Company_SubDistrict_SubDistrictID",
                schema: "MST",
                table: "Company");

            migrationBuilder.DropIndex(
                name: "IX_Company_DistrictID",
                schema: "MST",
                table: "Company");

            migrationBuilder.DropIndex(
                name: "IX_Company_ProvinceID",
                schema: "MST",
                table: "Company");

            migrationBuilder.DropIndex(
                name: "IX_Company_SubDistrictID",
                schema: "MST",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "DistrictID",
                schema: "MST",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "ProvinceID",
                schema: "MST",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "SubDistrictID",
                schema: "MST",
                table: "Company");

            migrationBuilder.AddColumn<string>(
                name: "DistrictEN",
                schema: "MST",
                table: "Company",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DistrictTH",
                schema: "MST",
                table: "Company",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProvinceEN",
                schema: "MST",
                table: "Company",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProvinceTH",
                schema: "MST",
                table: "Company",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubDistrictEN",
                schema: "MST",
                table: "Company",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubDistrictTH",
                schema: "MST",
                table: "Company",
                nullable: true);
        }
    }
}
