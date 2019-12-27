using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddTestingBGDeleteBehavior : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubBG_BG_BGID",
                schema: "MST",
                table: "SubBG");

            migrationBuilder.AddForeignKey(
                name: "FK_SubBG_BG_BGID",
                schema: "MST",
                table: "SubBG",
                column: "BGID",
                principalSchema: "MST",
                principalTable: "BG",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubBG_BG_BGID",
                schema: "MST",
                table: "SubBG");

            migrationBuilder.AddForeignKey(
                name: "FK_SubBG_BG_BGID",
                schema: "MST",
                table: "SubBG",
                column: "BGID",
                principalSchema: "MST",
                principalTable: "BG",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
