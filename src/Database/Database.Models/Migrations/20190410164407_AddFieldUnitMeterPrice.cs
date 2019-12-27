using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddFieldUnitMeterPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ElectricMeterPrice",
                schema: "PRJ",
                table: "Unit",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "WaterMeterPrice",
                schema: "PRJ",
                table: "Unit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ElectricMeterPrice",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "WaterMeterPrice",
                schema: "PRJ",
                table: "Unit");
        }
    }
}
