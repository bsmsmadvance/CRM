using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddTowerForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_HighRiseFee_TowerID",
                schema: "PRJ",
                table: "HighRiseFee",
                column: "TowerID");

            migrationBuilder.AddForeignKey(
                name: "FK_HighRiseFee_Tower_TowerID",
                schema: "PRJ",
                table: "HighRiseFee",
                column: "TowerID",
                principalSchema: "PRJ",
                principalTable: "Tower",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HighRiseFee_Tower_TowerID",
                schema: "PRJ",
                table: "HighRiseFee");

            migrationBuilder.DropIndex(
                name: "IX_HighRiseFee_TowerID",
                schema: "PRJ",
                table: "HighRiseFee");
        }
    }
}
