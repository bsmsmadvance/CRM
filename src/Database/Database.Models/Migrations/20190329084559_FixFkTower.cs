using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class FixFkTower : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Unit_TowerID",
                schema: "PRJ",
                table: "Unit",
                column: "TowerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Unit_Tower_TowerID",
                schema: "PRJ",
                table: "Unit",
                column: "TowerID",
                principalSchema: "PRJ",
                principalTable: "Tower",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Unit_Tower_TowerID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropIndex(
                name: "IX_Unit_TowerID",
                schema: "PRJ",
                table: "Unit");
        }
    }
}
