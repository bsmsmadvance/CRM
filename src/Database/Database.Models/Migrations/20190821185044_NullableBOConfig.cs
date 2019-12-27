using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class NullableBOConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "VoidRefund",
                schema: "MST",
                table: "BOConfiguration",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<double>(
                name: "UnitTransferFee",
                schema: "MST",
                table: "BOConfiguration",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<double>(
                name: "TransferFeeRate",
                schema: "MST",
                table: "BOConfiguration",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<double>(
                name: "TaxAccount",
                schema: "MST",
                table: "BOConfiguration",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<double>(
                name: "LocalTaxPercent",
                schema: "MST",
                table: "BOConfiguration",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<double>(
                name: "IncomeTaxPercent",
                schema: "MST",
                table: "BOConfiguration",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<double>(
                name: "DepreciationYear",
                schema: "MST",
                table: "BOConfiguration",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<double>(
                name: "BusinessTaxPercent",
                schema: "MST",
                table: "BOConfiguration",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<double>(
                name: "BOIAmount",
                schema: "MST",
                table: "BOConfiguration",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<double>(
                name: "AdjustAccount",
                schema: "MST",
                table: "BOConfiguration",
                nullable: true,
                oldClrType: typeof(double));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "VoidRefund",
                schema: "MST",
                table: "BOConfiguration",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "UnitTransferFee",
                schema: "MST",
                table: "BOConfiguration",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "TransferFeeRate",
                schema: "MST",
                table: "BOConfiguration",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "TaxAccount",
                schema: "MST",
                table: "BOConfiguration",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "LocalTaxPercent",
                schema: "MST",
                table: "BOConfiguration",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "IncomeTaxPercent",
                schema: "MST",
                table: "BOConfiguration",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "DepreciationYear",
                schema: "MST",
                table: "BOConfiguration",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "BusinessTaxPercent",
                schema: "MST",
                table: "BOConfiguration",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "BOIAmount",
                schema: "MST",
                table: "BOConfiguration",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "AdjustAccount",
                schema: "MST",
                table: "BOConfiguration",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);
        }
    }
}
