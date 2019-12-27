using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ChangeFeeInDeposit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalFeeAmount",
                schema: "FIN",
                table: "DepositHeader",
                newName: "TotalFee");

            migrationBuilder.RenameColumn(
                name: "FeeAmount",
                schema: "FIN",
                table: "DepositDetail",
                newName: "Fee");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                schema: "FIN",
                table: "DepositHeader",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 5000,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalFee",
                schema: "FIN",
                table: "DepositHeader",
                newName: "TotalFeeAmount");

            migrationBuilder.RenameColumn(
                name: "Fee",
                schema: "FIN",
                table: "DepositDetail",
                newName: "FeeAmount");

            migrationBuilder.AlterColumn<string>(
                name: "Remark",
                schema: "FIN",
                table: "DepositHeader",
                maxLength: 5000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true);
        }
    }
}
