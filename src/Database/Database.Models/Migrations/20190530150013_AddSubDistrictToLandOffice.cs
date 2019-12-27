using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddSubDistrictToLandOffice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubDistrict_LandOffice_LandOfficeID",
                schema: "MST",
                table: "SubDistrict");

            migrationBuilder.DropIndex(
                name: "IX_SubDistrict_LandOfficeID",
                schema: "MST",
                table: "SubDistrict");

            migrationBuilder.DropColumn(
                name: "LandOfficeID",
                schema: "MST",
                table: "SubDistrict");

            migrationBuilder.AddColumn<Guid>(
                name: "SubDistrictID",
                schema: "MST",
                table: "LandOffice",
                nullable: true,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_LandOffice_SubDistrictID",
                schema: "MST",
                table: "LandOffice",
                column: "SubDistrictID");

            migrationBuilder.AddForeignKey(
                name: "FK_LandOffice_SubDistrict_SubDistrictID",
                schema: "MST",
                table: "LandOffice",
                column: "SubDistrictID",
                principalSchema: "MST",
                principalTable: "SubDistrict",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LandOffice_SubDistrict_SubDistrictID",
                schema: "MST",
                table: "LandOffice");

            migrationBuilder.DropIndex(
                name: "IX_LandOffice_SubDistrictID",
                schema: "MST",
                table: "LandOffice");

            migrationBuilder.DropColumn(
                name: "SubDistrictID",
                schema: "MST",
                table: "LandOffice");

            migrationBuilder.AddColumn<Guid>(
                name: "LandOfficeID",
                schema: "MST",
                table: "SubDistrict",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubDistrict_LandOfficeID",
                schema: "MST",
                table: "SubDistrict",
                column: "LandOfficeID");

            migrationBuilder.AddForeignKey(
                name: "FK_SubDistrict_LandOffice_LandOfficeID",
                schema: "MST",
                table: "SubDistrict",
                column: "LandOfficeID",
                principalSchema: "MST",
                principalTable: "LandOffice",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
