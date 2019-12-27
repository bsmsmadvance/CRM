using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ModifyAddnewcolumnandfixnullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "SalePrice",
                schema: "PRJ",
                table: "MinPrice",
                type: "Money",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                schema: "PRJ",
                table: "LowRiseBuildingPriceFee",
                type: "Money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Money");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SalePrice",
                schema: "PRJ",
                table: "MinPrice");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                schema: "PRJ",
                table: "LowRiseBuildingPriceFee",
                type: "Money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Money",
                oldNullable: true);
        }
    }
}
