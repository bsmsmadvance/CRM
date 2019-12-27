using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class RemoveDuplicateFieldsInUnit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AirArea",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "BuiltInArea",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "CurvedSteelArea",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "FactorArea",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "ParkingArea",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "VerandaArea",
                schema: "PRJ",
                table: "Unit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AirArea",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "BuiltInArea",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "CurvedSteelArea",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FactorArea",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ParkingArea",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "VerandaArea",
                schema: "PRJ",
                table: "Unit",
                nullable: true);
        }
    }
}
