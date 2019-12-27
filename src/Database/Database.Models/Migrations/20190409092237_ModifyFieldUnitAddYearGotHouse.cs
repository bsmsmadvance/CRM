using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ModifyFieldUnitAddYearGotHouse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Reamark",
                schema: "PRJ",
                table: "Unit",
                newName: "Remark");

            migrationBuilder.AddColumn<int>(
                name: "YearGotHouseNo",
                schema: "PRJ",
                table: "Unit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YearGotHouseNo",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.RenameColumn(
                name: "Remark",
                schema: "PRJ",
                table: "Unit",
                newName: "Reamark");
        }
    }
}
