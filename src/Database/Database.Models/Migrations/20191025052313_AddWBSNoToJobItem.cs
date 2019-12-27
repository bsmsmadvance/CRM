using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddWBSNoToJobItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SAPWBSNo_P",
                schema: "PRM",
                table: "PRRequestJobItem",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SAPWBSNo_P",
                schema: "PRM",
                table: "PRRequestJobItem");
        }
    }
}
