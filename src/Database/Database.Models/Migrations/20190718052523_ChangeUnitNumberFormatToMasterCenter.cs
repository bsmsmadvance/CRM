using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ChangeUnitNumberFormatToMasterCenter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitNumberFormat",
                schema: "MST",
                table: "Brand");

            migrationBuilder.AddColumn<Guid>(
                name: "UnitNumberFormatMasterCenterID",
                schema: "MST",
                table: "Brand",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Brand_UnitNumberFormatMasterCenterID",
                schema: "MST",
                table: "Brand",
                column: "UnitNumberFormatMasterCenterID");

            migrationBuilder.AddForeignKey(
                name: "FK_Brand_MasterCenter_UnitNumberFormatMasterCenterID",
                schema: "MST",
                table: "Brand",
                column: "UnitNumberFormatMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Brand_MasterCenter_UnitNumberFormatMasterCenterID",
                schema: "MST",
                table: "Brand");

            migrationBuilder.DropIndex(
                name: "IX_Brand_UnitNumberFormatMasterCenterID",
                schema: "MST",
                table: "Brand");

            migrationBuilder.DropColumn(
                name: "UnitNumberFormatMasterCenterID",
                schema: "MST",
                table: "Brand");

            migrationBuilder.AddColumn<string>(
                name: "UnitNumberFormat",
                schema: "MST",
                table: "Brand",
                maxLength: 100,
                nullable: true);
        }
    }
}
