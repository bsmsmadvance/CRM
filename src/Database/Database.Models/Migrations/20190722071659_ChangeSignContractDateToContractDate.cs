using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ChangeSignContractDateToContractDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContractSignDate",
                schema: "SAL",
                table: "Quotation",
                newName: "ContractDate");

            migrationBuilder.RenameColumn(
                name: "SignContractDate",
                schema: "SAL",
                table: "Booking",
                newName: "ContractDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContractDate",
                schema: "SAL",
                table: "Quotation",
                newName: "ContractSignDate");

            migrationBuilder.RenameColumn(
                name: "ContractDate",
                schema: "SAL",
                table: "Booking",
                newName: "SignContractDate");
        }
    }
}
