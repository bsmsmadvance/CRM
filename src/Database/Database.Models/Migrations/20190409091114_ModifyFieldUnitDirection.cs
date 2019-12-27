using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ModifyFieldUnitDirection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HouseCode",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "HouseName",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.AddColumn<Guid>(
                name: "DirectionTypeMasterCenterID",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Unit_DirectionTypeMasterCenterID",
                schema: "PRJ",
                table: "Unit",
                column: "DirectionTypeMasterCenterID");

            migrationBuilder.AddForeignKey(
                name: "FK_Unit_MasterCenter_DirectionTypeMasterCenterID",
                schema: "PRJ",
                table: "Unit",
                column: "DirectionTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Unit_MasterCenter_DirectionTypeMasterCenterID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropIndex(
                name: "IX_Unit_DirectionTypeMasterCenterID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "DirectionTypeMasterCenterID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.AddColumn<string>(
                name: "HouseCode",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HouseName",
                schema: "PRJ",
                table: "Unit",
                nullable: true);
        }
    }
}
