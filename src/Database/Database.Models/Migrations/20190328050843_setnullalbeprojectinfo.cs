using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class setnullalbeprojectinfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "TotalUnit",
                schema: "PRJ",
                table: "Project",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<double>(
                name: "SqaureWa",
                schema: "PRJ",
                table: "Project",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<double>(
                name: "Rai",
                schema: "PRJ",
                table: "Project",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<double>(
                name: "Ngan",
                schema: "PRJ",
                table: "Project",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "MortgageAmount",
                schema: "PRJ",
                table: "Project",
                type: "Money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Money");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "PRJ",
                table: "Project",
                nullable: true,
                oldClrType: typeof(bool));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "TotalUnit",
                schema: "PRJ",
                table: "Project",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "SqaureWa",
                schema: "PRJ",
                table: "Project",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Rai",
                schema: "PRJ",
                table: "Project",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Ngan",
                schema: "PRJ",
                table: "Project",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "MortgageAmount",
                schema: "PRJ",
                table: "Project",
                type: "Money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Money",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "PRJ",
                table: "Project",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);
        }
    }
}
