using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class RemoveTaxFromMasterPriceItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tax",
                schema: "MST",
                table: "MasterPriceItem");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Tax",
                schema: "MST",
                table: "MasterPriceItem",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
