using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class UpdateBudgetMinPriceAmount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalBudgetPerUnit",
                schema: "PRJ",
                table: "BudgetMinPrice");

            migrationBuilder.RenameColumn(
                name: "TotalBudgetTransfer",
                schema: "PRJ",
                table: "BudgetMinPrice",
                newName: "UnitAmount");

            migrationBuilder.RenameColumn(
                name: "TotalBudgetSale",
                schema: "PRJ",
                table: "BudgetMinPrice",
                newName: "TotalAmount");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UnitAmount",
                schema: "PRJ",
                table: "BudgetMinPrice",
                newName: "TotalBudgetTransfer");

            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                schema: "PRJ",
                table: "BudgetMinPrice",
                newName: "TotalBudgetSale");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalBudgetPerUnit",
                schema: "PRJ",
                table: "BudgetMinPrice",
                type: "Money",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
