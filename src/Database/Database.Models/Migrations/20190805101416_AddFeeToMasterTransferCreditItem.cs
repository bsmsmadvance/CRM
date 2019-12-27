using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddFeeToMasterTransferCreditItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                schema: "PRM",
                table: "MasterTransferCreditCardItem");

            migrationBuilder.AddColumn<double>(
                name: "Fee",
                schema: "PRM",
                table: "MasterTransferCreditCardItem",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fee",
                schema: "PRM",
                table: "MasterTransferCreditCardItem");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                schema: "PRM",
                table: "MasterTransferCreditCardItem",
                type: "Money",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
