using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ChangeFloorPlanRoomPlanInUnit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FloorFilename",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "FloorFilename2",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.RenameColumn(
                name: "RoomFilename2",
                schema: "PRJ",
                table: "Unit",
                newName: "RoomPlanFileName");

            migrationBuilder.RenameColumn(
                name: "RoomFilename",
                schema: "PRJ",
                table: "Unit",
                newName: "FloorPlanFileName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RoomPlanFileName",
                schema: "PRJ",
                table: "Unit",
                newName: "RoomFilename2");

            migrationBuilder.RenameColumn(
                name: "FloorPlanFileName",
                schema: "PRJ",
                table: "Unit",
                newName: "RoomFilename");

            migrationBuilder.AddColumn<string>(
                name: "FloorFilename",
                schema: "PRJ",
                table: "Unit",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FloorFilename2",
                schema: "PRJ",
                table: "Unit",
                maxLength: 1000,
                nullable: true);
        }
    }
}
