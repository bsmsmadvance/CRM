using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddEmailAndPhoneNumberConfirm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEmailConfirmed",
                schema: "CTM",
                table: "Lead",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPhoneNumberConfirmed",
                schema: "CTM",
                table: "Lead",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEmailConfirmed",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "IsPhoneNumberConfirmed",
                schema: "CTM",
                table: "Lead");
        }
    }
}
