using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class EditWaiveQCTypoFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WaiveQC_MasterCenter_UnitStatusMasterCenterID",
                schema: "PRJ",
                table: "WaiveQC");

            migrationBuilder.DropIndex(
                name: "IX_WaiveQC_UnitStatusMasterCenterID",
                schema: "PRJ",
                table: "WaiveQC");

            migrationBuilder.DropColumn(
                name: "UnitStatusMasterCenterID",
                schema: "PRJ",
                table: "WaiveQC");

            migrationBuilder.RenameColumn(
                name: "WaiveSigneDate",
                schema: "PRJ",
                table: "WaiveQC",
                newName: "WaiveSignDate");

            migrationBuilder.RenameColumn(
                name: "WaiveQCeDate",
                schema: "PRJ",
                table: "WaiveQC",
                newName: "WaiveQCDate");

            migrationBuilder.RenameColumn(
                name: "EndMajoreDate",
                schema: "PRJ",
                table: "WaiveQC",
                newName: "EndMajorDate");

            migrationBuilder.RenameColumn(
                name: "EndFulleDate",
                schema: "PRJ",
                table: "WaiveQC",
                newName: "EndFullDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WaiveSignDate",
                schema: "PRJ",
                table: "WaiveQC",
                newName: "WaiveSigneDate");

            migrationBuilder.RenameColumn(
                name: "WaiveQCDate",
                schema: "PRJ",
                table: "WaiveQC",
                newName: "WaiveQCeDate");

            migrationBuilder.RenameColumn(
                name: "EndMajorDate",
                schema: "PRJ",
                table: "WaiveQC",
                newName: "EndMajoreDate");

            migrationBuilder.RenameColumn(
                name: "EndFullDate",
                schema: "PRJ",
                table: "WaiveQC",
                newName: "EndFulleDate");

            migrationBuilder.AddColumn<Guid>(
                name: "UnitStatusMasterCenterID",
                schema: "PRJ",
                table: "WaiveQC",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WaiveQC_UnitStatusMasterCenterID",
                schema: "PRJ",
                table: "WaiveQC",
                column: "UnitStatusMasterCenterID");

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
    }
}
