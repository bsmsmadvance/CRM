using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class UpdatePriceUnitInPriceList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PricePerUnitAmount",
                schema: "SAL",
                table: "UnitPriceItem",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "PriceUnit",
                schema: "SAL",
                table: "UnitPriceItem",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "PriceUnitAmount",
                schema: "SAL",
                table: "UnitPriceItem",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerUnitAmount",
                schema: "PRJ",
                table: "PriceListItem",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "PriceUnit",
                schema: "PRJ",
                table: "PriceListItem",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "PriceUnitAmount",
                schema: "PRJ",
                table: "PriceListItem",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PricePerUnitAmount",
                schema: "SAL",
                table: "UnitPriceItem");

            migrationBuilder.DropColumn(
                name: "PriceUnit",
                schema: "SAL",
                table: "UnitPriceItem");

            migrationBuilder.DropColumn(
                name: "PriceUnitAmount",
                schema: "SAL",
                table: "UnitPriceItem");

            migrationBuilder.DropColumn(
                name: "PricePerUnitAmount",
                schema: "PRJ",
                table: "PriceListItem");

            migrationBuilder.DropColumn(
                name: "PriceUnit",
                schema: "PRJ",
                table: "PriceListItem");

            migrationBuilder.DropColumn(
                name: "PriceUnitAmount",
                schema: "PRJ",
                table: "PriceListItem");
        }
    }
}
