using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class addsocialfieldprojectinfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LineId",
                schema: "PRJ",
                table: "Project",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WeChat",
                schema: "PRJ",
                table: "Project",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WhatsApp",
                schema: "PRJ",
                table: "Project",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LineId",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "WeChat",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "WhatsApp",
                schema: "PRJ",
                table: "Project");
        }
    }
}
