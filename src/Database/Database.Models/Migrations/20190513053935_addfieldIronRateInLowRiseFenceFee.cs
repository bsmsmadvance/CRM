using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class addfieldIronRateInLowRiseFenceFee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "IronRate",
                schema: "PRJ",
                table: "LowRiseFenceFee",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IronRate",
                schema: "PRJ",
                table: "LowRiseFenceFee");
        }
    }
}
