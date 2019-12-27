using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class UpdateLeadDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Firstname",
                schema: "CTM",
                table: "Lead",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lastname",
                schema: "CTM",
                table: "Lead",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                schema: "CTM",
                table: "Lead",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Firstname",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "Lastname",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                schema: "CTM",
                table: "Lead");
        }
    }
}
