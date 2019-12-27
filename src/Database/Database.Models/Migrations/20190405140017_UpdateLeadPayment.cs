using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class UpdateLeadPayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Lastname",
                schema: "CTM",
                table: "Lead",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Firstname",
                schema: "CTM",
                table: "Lead",
                newName: "FirstName");

            migrationBuilder.AddColumn<string>(
                name: "Payment",
                schema: "CTM",
                table: "Lead",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Payment",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.RenameColumn(
                name: "LastName",
                schema: "CTM",
                table: "Lead",
                newName: "Lastname");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                schema: "CTM",
                table: "Lead",
                newName: "Firstname");
        }
    }
}
