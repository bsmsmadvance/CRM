using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddFieldAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Moo",
                schema: "PRJ",
                table: "Address",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoadEN",
                schema: "PRJ",
                table: "Address",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoadTH",
                schema: "PRJ",
                table: "Address",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SoiEN",
                schema: "PRJ",
                table: "Address",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SoiTH",
                schema: "PRJ",
                table: "Address",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Moo",
                schema: "PRJ",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "RoadEN",
                schema: "PRJ",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "RoadTH",
                schema: "PRJ",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "SoiEN",
                schema: "PRJ",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "SoiTH",
                schema: "PRJ",
                table: "Address");
        }
    }
}
