using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class alter_colIsapgive_Transfer_bas_kim : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsLegalEntityPayWithMemo",
                schema: "SAL",
                table: "Transfer",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsLegalEntityGiveChange",
                schema: "SAL",
                table: "Transfer",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsAPPayWithMemo",
                schema: "SAL",
                table: "Transfer",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "IsAPGiveChange",
                schema: "SAL",
                table: "Transfer",
                nullable: true,
                oldClrType: typeof(bool));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsLegalEntityPayWithMemo",
                schema: "SAL",
                table: "Transfer",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsLegalEntityGiveChange",
                schema: "SAL",
                table: "Transfer",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsAPPayWithMemo",
                schema: "SAL",
                table: "Transfer",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsAPGiveChange",
                schema: "SAL",
                table: "Transfer",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);
        }
    }
}
