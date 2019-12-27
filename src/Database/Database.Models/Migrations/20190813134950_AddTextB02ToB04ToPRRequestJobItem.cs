using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddTextB02ToB04ToPRRequestJobItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TextB02",
                schema: "PRM",
                table: "PRRequestJobItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TextB03",
                schema: "PRM",
                table: "PRRequestJobItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TextB04",
                schema: "PRM",
                table: "PRRequestJobItem",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TextB02",
                schema: "PRM",
                table: "PRRequestJobItem");

            migrationBuilder.DropColumn(
                name: "TextB03",
                schema: "PRM",
                table: "PRRequestJobItem");

            migrationBuilder.DropColumn(
                name: "TextB04",
                schema: "PRM",
                table: "PRRequestJobItem");
        }
    }
}
