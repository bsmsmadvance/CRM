using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ModifyFieldBudgetPromotion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WBSCRM",
                schema: "PRJ",
                table: "BudgetPromotion",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WBSSAP",
                schema: "PRJ",
                table: "BudgetPromotion",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WBSCRM",
                schema: "PRJ",
                table: "BudgetPromotion");

            migrationBuilder.DropColumn(
                name: "WBSSAP",
                schema: "PRJ",
                table: "BudgetPromotion");
        }
    }
}
