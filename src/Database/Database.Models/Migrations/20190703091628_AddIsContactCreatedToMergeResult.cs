using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddIsContactCreatedToMergeResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsContactCreated",
                schema: "DMT",
                table: "MergeContactResult",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Score",
                schema: "DMT",
                table: "MergeContactResult",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsContactCreated",
                schema: "DMT",
                table: "MergeContactResult");

            migrationBuilder.DropColumn(
                name: "Score",
                schema: "DMT",
                table: "MergeContactResult");
        }
    }
}
