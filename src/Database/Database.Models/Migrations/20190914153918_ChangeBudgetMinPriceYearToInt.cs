using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ChangeBudgetMinPriceYearToInt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Year",
                schema: "PRJ",
                table: "BudgetMinPrice",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Quarter",
                schema: "PRJ",
                table: "BudgetMinPrice",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Year",
                schema: "PRJ",
                table: "BudgetMinPrice",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Quarter",
                schema: "PRJ",
                table: "BudgetMinPrice",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
