using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ModifyMasterCenterAndCountry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSystemOnly",
                schema: "MST",
                table: "MasterCenter",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                schema: "MST",
                table: "Country",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSystemOnly",
                schema: "MST",
                table: "MasterCenter");

            migrationBuilder.DropColumn(
                name: "Code",
                schema: "MST",
                table: "Country");
        }
    }
}
