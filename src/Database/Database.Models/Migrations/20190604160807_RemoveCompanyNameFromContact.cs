using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class RemoveCompanyNameFromContact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyNameEN",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "CompanyNameTH",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.AddColumn<string>(
                name: "Key",
                schema: "CTM",
                table: "LeadScoringType",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Key",
                schema: "CTM",
                table: "LeadScoringType");

            migrationBuilder.AddColumn<string>(
                name: "CompanyNameEN",
                schema: "CTM",
                table: "Contact",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyNameTH",
                schema: "CTM",
                table: "Contact",
                maxLength: 100,
                nullable: true);
        }
    }
}
