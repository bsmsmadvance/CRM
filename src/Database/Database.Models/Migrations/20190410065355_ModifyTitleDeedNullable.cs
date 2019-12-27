using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ModifyTitleDeedNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsSameAddressAsTitledeed",
                schema: "PRJ",
                table: "TitledeedDetail",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<decimal>(
                name: "EstimatePrice",
                schema: "PRJ",
                table: "TitledeedDetail",
                type: "Money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Money");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsSameAddressAsTitledeed",
                schema: "PRJ",
                table: "TitledeedDetail",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "EstimatePrice",
                schema: "PRJ",
                table: "TitledeedDetail",
                type: "Money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Money",
                oldNullable: true);
        }
    }
}
