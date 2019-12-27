using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class UpdateMasterCenterKeyAndNullablePriceUnitEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isMainAddress",
                schema: "PRJ",
                table: "Address",
                newName: "IsMainAddress");

            migrationBuilder.AlterColumn<double>(
                name: "PriceUnitAmount",
                schema: "SAL",
                table: "UnitPriceItem",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<int>(
                name: "PriceUnit",
                schema: "SAL",
                table: "UnitPriceItem",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<decimal>(
                name: "PricePerUnitAmount",
                schema: "SAL",
                table: "UnitPriceItem",
                type: "Money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Money");

            migrationBuilder.AlterColumn<double>(
                name: "PriceUnitAmount",
                schema: "PRJ",
                table: "PriceListItem",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<int>(
                name: "PriceUnit",
                schema: "PRJ",
                table: "PriceListItem",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<decimal>(
                name: "PricePerUnitAmount",
                schema: "PRJ",
                table: "PriceListItem",
                type: "Money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Money");

            migrationBuilder.AlterColumn<int>(
                name: "Key",
                schema: "MST",
                table: "MasterCenter",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsMainAddress",
                schema: "PRJ",
                table: "Address",
                newName: "isMainAddress");

            migrationBuilder.AlterColumn<double>(
                name: "PriceUnitAmount",
                schema: "SAL",
                table: "UnitPriceItem",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PriceUnit",
                schema: "SAL",
                table: "UnitPriceItem",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PricePerUnitAmount",
                schema: "SAL",
                table: "UnitPriceItem",
                type: "Money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Money",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "PriceUnitAmount",
                schema: "PRJ",
                table: "PriceListItem",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PriceUnit",
                schema: "PRJ",
                table: "PriceListItem",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PricePerUnitAmount",
                schema: "PRJ",
                table: "PriceListItem",
                type: "Money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Money",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                schema: "MST",
                table: "MasterCenter",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
