using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class addBrandNoBankNoCounterNo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CounterNo",
                schema: "MST",
                table: "Counter",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BrandNo",
                schema: "MST",
                table: "Brand",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankNo",
                schema: "MST",
                table: "Bank",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CounterNo",
                schema: "MST",
                table: "Counter");

            migrationBuilder.DropColumn(
                name: "BrandNo",
                schema: "MST",
                table: "Brand");

            migrationBuilder.DropColumn(
                name: "BankNo",
                schema: "MST",
                table: "Bank");
        }
    }
}
