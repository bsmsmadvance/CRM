using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class add_col_CustomerName_BillPaymentDetail_kim : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                schema: "FIN",
                table: "BillPaymentTransactionTemp",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                schema: "FIN",
                table: "BillPaymentDetail",
                maxLength: 1000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerName",
                schema: "FIN",
                table: "BillPaymentTransactionTemp");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                schema: "FIN",
                table: "BillPaymentDetail");
        }
    }
}
