using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class FixWaiveQC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitNo",
                schema: "PRJ",
                table: "WaiveQC");

            migrationBuilder.DropColumn(
                name: "UnitStatus",
                schema: "PRJ",
                table: "WaiveQC");

            migrationBuilder.AddColumn<Guid>(
                name: "UnitID",
                schema: "PRJ",
                table: "WaiveQC",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UnitStatusMasterCenterID",
                schema: "PRJ",
                table: "WaiveQC",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WaiveQC_UnitID",
                schema: "PRJ",
                table: "WaiveQC",
                column: "UnitID");

            migrationBuilder.CreateIndex(
                name: "IX_WaiveQC_UnitStatusMasterCenterID",
                schema: "PRJ",
                table: "WaiveQC",
                column: "UnitStatusMasterCenterID");

            migrationBuilder.AddForeignKey(
                name: "FK_WaiveQC_Unit_UnitID",
                schema: "PRJ",
                table: "WaiveQC",
                column: "UnitID",
                principalSchema: "PRJ",
                principalTable: "Unit",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WaiveQC_MasterCenter_UnitStatusMasterCenterID",
                schema: "PRJ",
                table: "WaiveQC",
                column: "UnitStatusMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WaiveQC_Unit_UnitID",
                schema: "PRJ",
                table: "WaiveQC");

            migrationBuilder.DropForeignKey(
                name: "FK_WaiveQC_MasterCenter_UnitStatusMasterCenterID",
                schema: "PRJ",
                table: "WaiveQC");

            migrationBuilder.DropIndex(
                name: "IX_WaiveQC_UnitID",
                schema: "PRJ",
                table: "WaiveQC");

            migrationBuilder.DropIndex(
                name: "IX_WaiveQC_UnitStatusMasterCenterID",
                schema: "PRJ",
                table: "WaiveQC");

            migrationBuilder.DropColumn(
                name: "UnitID",
                schema: "PRJ",
                table: "WaiveQC");

            migrationBuilder.DropColumn(
                name: "UnitStatusMasterCenterID",
                schema: "PRJ",
                table: "WaiveQC");

            migrationBuilder.AddColumn<string>(
                name: "UnitNo",
                schema: "PRJ",
                table: "WaiveQC",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitStatus",
                schema: "PRJ",
                table: "WaiveQC",
                nullable: true);
        }
    }
}
