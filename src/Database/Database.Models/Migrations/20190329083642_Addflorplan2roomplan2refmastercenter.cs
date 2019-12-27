using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class Addflorplan2roomplan2refmastercenter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UnitType",
                schema: "PRJ",
                table: "Unit",
                newName: "RoomFilename2");

            migrationBuilder.RenameColumn(
                name: "Location",
                schema: "PRJ",
                table: "Unit",
                newName: "FloorFilename2");

            migrationBuilder.AddColumn<Guid>(
                name: "UnitTypeMasterCenterID",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Unit_UnitTypeMasterCenterID",
                schema: "PRJ",
                table: "Unit",
                column: "UnitTypeMasterCenterID");

            migrationBuilder.AddForeignKey(
                name: "FK_Unit_MasterCenter_UnitTypeMasterCenterID",
                schema: "PRJ",
                table: "Unit",
                column: "UnitTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Unit_MasterCenter_UnitTypeMasterCenterID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropIndex(
                name: "IX_Unit_UnitTypeMasterCenterID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "UnitTypeMasterCenterID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.RenameColumn(
                name: "RoomFilename2",
                schema: "PRJ",
                table: "Unit",
                newName: "UnitType");

            migrationBuilder.RenameColumn(
                name: "FloorFilename2",
                schema: "PRJ",
                table: "Unit",
                newName: "Location");
        }
    }
}
