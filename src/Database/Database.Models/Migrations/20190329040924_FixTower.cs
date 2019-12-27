using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class FixTower : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TitledeedNo",
                schema: "PRJ",
                table: "Tower",
                newName: "CondominiumNo");

            migrationBuilder.RenameColumn(
                name: "TitledeedName",
                schema: "PRJ",
                table: "Tower",
                newName: "CondominiumName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CondominiumNo",
                schema: "PRJ",
                table: "Tower",
                newName: "TitledeedNo");

            migrationBuilder.RenameColumn(
                name: "CondominiumName",
                schema: "PRJ",
                table: "Tower",
                newName: "TitledeedName");
        }
    }
}
