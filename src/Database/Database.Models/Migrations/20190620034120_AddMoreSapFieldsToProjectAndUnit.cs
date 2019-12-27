using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddMoreSapFieldsToProjectAndUnit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SAPWBSNo_P",
                schema: "PRJ",
                table: "Unit",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SAPWBSObject_P",
                schema: "PRJ",
                table: "Unit",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Plant",
                schema: "PRJ",
                table: "Project",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SAPWBSNo_P",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "SAPWBSObject_P",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "Plant",
                schema: "PRJ",
                table: "Project");
        }
    }
}
