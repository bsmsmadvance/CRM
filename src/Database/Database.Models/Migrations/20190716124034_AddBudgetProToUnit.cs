using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddBudgetProToUnit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Position",
                schema: "PRJ",
                table: "Unit",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SAPBudgetProAmount",
                schema: "PRJ",
                table: "Unit",
                type: "Money",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SAPBudgetProUpdated",
                schema: "PRJ",
                table: "Unit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SAPBudgetProAmount",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropColumn(
                name: "SAPBudgetProUpdated",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.AlterColumn<string>(
                name: "Position",
                schema: "PRJ",
                table: "Unit",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
