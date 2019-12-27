using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class add_col_bass_serm_kim : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                schema: "SAL",
                table: "UnitPrice",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AgentOther",
                schema: "SAL",
                table: "Booking",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCancel",
                schema: "SAL",
                table: "Agreement",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPrintPaymentCard",
                schema: "SAL",
                table: "Agreement",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsToBePay",
                schema: "MST",
                table: "MasterPriceItem",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                schema: "MST",
                table: "MasterPriceItem",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                schema: "SAL",
                table: "UnitPrice");

            migrationBuilder.DropColumn(
                name: "AgentOther",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "IsCancel",
                schema: "SAL",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "IsPrintPaymentCard",
                schema: "SAL",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "IsToBePay",
                schema: "MST",
                table: "MasterPriceItem");

            migrationBuilder.DropColumn(
                name: "Order",
                schema: "MST",
                table: "MasterPriceItem");
        }
    }
}
