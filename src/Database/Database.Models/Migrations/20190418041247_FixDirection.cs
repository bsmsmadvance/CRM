using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class FixDirection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Unit_MasterCenter_DirectionTypeMasterCenterID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.RenameColumn(
                name: "DirectionTypeMasterCenterID",
                schema: "PRJ",
                table: "Unit",
                newName: "UnitDirectionMasterCenterID");

            migrationBuilder.RenameIndex(
                name: "IX_Unit_DirectionTypeMasterCenterID",
                schema: "PRJ",
                table: "Unit",
                newName: "IX_Unit_UnitDirectionMasterCenterID");

            migrationBuilder.AddForeignKey(
                name: "FK_Unit_MasterCenter_UnitDirectionMasterCenterID",
                schema: "PRJ",
                table: "Unit",
                column: "UnitDirectionMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Unit_MasterCenter_UnitDirectionMasterCenterID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.RenameColumn(
                name: "UnitDirectionMasterCenterID",
                schema: "PRJ",
                table: "Unit",
                newName: "DirectionTypeMasterCenterID");

            migrationBuilder.RenameIndex(
                name: "IX_Unit_UnitDirectionMasterCenterID",
                schema: "PRJ",
                table: "Unit",
                newName: "IX_Unit_DirectionTypeMasterCenterID");

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
    }
}
