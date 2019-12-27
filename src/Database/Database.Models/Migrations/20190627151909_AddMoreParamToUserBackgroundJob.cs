using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddMoreParamToUserBackgroundJob : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "USR",
                table: "UserBackgroundJob",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ErrorMessage",
                schema: "USR",
                table: "UserBackgroundJob",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Params",
                schema: "USR",
                table: "UserBackgroundJob",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Progress",
                schema: "USR",
                table: "UserBackgroundJob",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "USR",
                table: "UserBackgroundJob",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ErrorMessage",
                schema: "USR",
                table: "UserBackgroundJob");

            migrationBuilder.DropColumn(
                name: "Params",
                schema: "USR",
                table: "UserBackgroundJob");

            migrationBuilder.DropColumn(
                name: "Progress",
                schema: "USR",
                table: "UserBackgroundJob");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "USR",
                table: "UserBackgroundJob");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "USR",
                table: "UserBackgroundJob",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
