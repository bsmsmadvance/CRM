using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddPrintAgreementFlag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPrintAgreementEmpty",
                schema: "PRJ",
                table: "AgreementConfig",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPrintAgreementForBuyer",
                schema: "PRJ",
                table: "AgreementConfig",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPrintAgreementForRevenue",
                schema: "PRJ",
                table: "AgreementConfig",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPrintAgreementForSeller",
                schema: "PRJ",
                table: "AgreementConfig",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPrintAgreementEmpty",
                schema: "PRJ",
                table: "AgreementConfig");

            migrationBuilder.DropColumn(
                name: "IsPrintAgreementForBuyer",
                schema: "PRJ",
                table: "AgreementConfig");

            migrationBuilder.DropColumn(
                name: "IsPrintAgreementForRevenue",
                schema: "PRJ",
                table: "AgreementConfig");

            migrationBuilder.DropColumn(
                name: "IsPrintAgreementForSeller",
                schema: "PRJ",
                table: "AgreementConfig");
        }
    }
}
