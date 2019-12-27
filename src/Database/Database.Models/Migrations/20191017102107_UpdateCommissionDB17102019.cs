using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class UpdateCommissionDB17102019 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommissionForThisMonth",
                schema: "CMS",
                table: "CalculateLowRiseSale");

            migrationBuilder.DropColumn(
                name: "CommissionForThisMonth",
                schema: "CMS",
                table: "CalculateHighRiseSale");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CommissionForThisMonth",
                schema: "CMS",
                table: "CalculateLowRiseSale",
                type: "Money",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CommissionForThisMonth",
                schema: "CMS",
                table: "CalculateHighRiseSale",
                type: "Money",
                nullable: true);
        }
    }
}
