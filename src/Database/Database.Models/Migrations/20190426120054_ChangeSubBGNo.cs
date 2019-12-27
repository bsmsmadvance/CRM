using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ChangeSubBGNo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BGNo",
                schema: "MST",
                table: "SubBG",
                newName: "SubBGNo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubBGNo",
                schema: "MST",
                table: "SubBG",
                newName: "BGNo");
        }
    }
}
