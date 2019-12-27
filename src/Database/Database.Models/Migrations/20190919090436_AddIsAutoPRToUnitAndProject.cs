using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddIsAutoPRToUnitAndProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPRAutoCost",
                schema: "PRJ",
                table: "Unit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPRAutoExpense",
                schema: "PRJ",
                table: "Unit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPRAutoFGF",
                schema: "PRJ",
                table: "Unit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPRAutoStand",
                schema: "PRJ",
                table: "Unit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPRAutoCost",
                schema: "PRJ",
                table: "Project",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPRAutoExpense",
                schema: "PRJ",
                table: "Project",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPRAutoFGF",
                schema: "PRJ",
                table: "Project",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPRAutoStand",
                schema: "PRJ",
                table: "Project",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPRAutoCost",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "IsPRAutoExpense",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "IsPRAutoFGF",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "IsPRAutoStand",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "IsPRAutoCost",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "IsPRAutoExpense",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "IsPRAutoFGF",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "IsPRAutoStand",
                schema: "PRJ",
                table: "Project");
        }
    }
}
