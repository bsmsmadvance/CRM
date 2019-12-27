using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddTypeToRunningNumberCounter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RunningNumberCounter",
                schema: "MST",
                table: "RunningNumberCounter");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                schema: "MST",
                table: "RunningNumberCounter",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RunningNumberCounter",
                schema: "MST",
                table: "RunningNumberCounter",
                columns: new[] { "Key", "Type" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RunningNumberCounter",
                schema: "MST",
                table: "RunningNumberCounter");

            migrationBuilder.DropColumn(
                name: "Type",
                schema: "MST",
                table: "RunningNumberCounter");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RunningNumberCounter",
                schema: "MST",
                table: "RunningNumberCounter",
                column: "Key");
        }
    }
}
