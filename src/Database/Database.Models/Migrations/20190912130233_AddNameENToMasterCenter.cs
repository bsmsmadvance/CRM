using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddNameENToMasterCenter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSystemOnly",
                schema: "MST",
                table: "MasterCenter");

            migrationBuilder.AddColumn<bool>(
                name: "IsSystemOnly",
                schema: "MST",
                table: "MasterCenterGroup",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "MST",
                table: "MasterCenter",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameEN",
                schema: "MST",
                table: "MasterCenter",
                maxLength: 1000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSystemOnly",
                schema: "MST",
                table: "MasterCenterGroup");

            migrationBuilder.DropColumn(
                name: "NameEN",
                schema: "MST",
                table: "MasterCenter");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "MST",
                table: "MasterCenter",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSystemOnly",
                schema: "MST",
                table: "MasterCenter",
                nullable: false,
                defaultValue: false);
        }
    }
}
