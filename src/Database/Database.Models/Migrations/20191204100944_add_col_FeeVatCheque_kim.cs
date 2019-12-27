using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class add_col_FeeVatCheque_kim : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "FeeIncludingVat",
                schema: "FIN",
                table: "PaymentPersonalCheque",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "FeePercent",
                schema: "FIN",
                table: "PaymentPersonalCheque",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "FeeIncludingVat",
                schema: "FIN",
                table: "PaymentCashierCheque",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "FeePercent",
                schema: "FIN",
                table: "PaymentCashierCheque",
                type: "Money",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FeeIncludingVat",
                schema: "FIN",
                table: "PaymentPersonalCheque");

            migrationBuilder.DropColumn(
                name: "FeePercent",
                schema: "FIN",
                table: "PaymentPersonalCheque");

            migrationBuilder.DropColumn(
                name: "FeeIncludingVat",
                schema: "FIN",
                table: "PaymentCashierCheque");

            migrationBuilder.DropColumn(
                name: "FeePercent",
                schema: "FIN",
                table: "PaymentCashierCheque");
        }
    }
}
