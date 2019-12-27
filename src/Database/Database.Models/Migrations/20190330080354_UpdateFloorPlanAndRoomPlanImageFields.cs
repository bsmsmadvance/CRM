using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class UpdateFloorPlanAndRoomPlanImageFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                schema: "PRJ",
                table: "RoomPlanImage");

            migrationBuilder.DropColumn(
                name: "ContentType",
                schema: "PRJ",
                table: "FloorPlanImage");

            migrationBuilder.RenameColumn(
                name: "Filename",
                schema: "PRJ",
                table: "RoomPlanImage",
                newName: "FileName");

            migrationBuilder.RenameColumn(
                name: "PathFile",
                schema: "PRJ",
                table: "RoomPlanImage",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Filename",
                schema: "PRJ",
                table: "FloorPlanImage",
                newName: "FileName");

            migrationBuilder.RenameColumn(
                name: "PathFile",
                schema: "PRJ",
                table: "FloorPlanImage",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileName",
                schema: "PRJ",
                table: "RoomPlanImage",
                newName: "Filename");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "PRJ",
                table: "RoomPlanImage",
                newName: "PathFile");

            migrationBuilder.RenameColumn(
                name: "FileName",
                schema: "PRJ",
                table: "FloorPlanImage",
                newName: "Filename");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "PRJ",
                table: "FloorPlanImage",
                newName: "PathFile");

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                schema: "PRJ",
                table: "RoomPlanImage",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                schema: "PRJ",
                table: "FloorPlanImage",
                nullable: true);
        }
    }
}
