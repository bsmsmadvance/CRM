using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddTowerAndFloorToHighRiseFee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FloorID",
                schema: "PRJ",
                table: "HighRiseFee",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TowerID",
                schema: "PRJ",
                table: "HighRiseFee",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HighRiseFee_FloorID",
                schema: "PRJ",
                table: "HighRiseFee",
                column: "FloorID");

            migrationBuilder.AddForeignKey(
                name: "FK_HighRiseFee_Floor_FloorID",
                schema: "PRJ",
                table: "HighRiseFee",
                column: "FloorID",
                principalSchema: "PRJ",
                principalTable: "Floor",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HighRiseFee_Floor_FloorID",
                schema: "PRJ",
                table: "HighRiseFee");

            migrationBuilder.DropIndex(
                name: "IX_HighRiseFee_FloorID",
                schema: "PRJ",
                table: "HighRiseFee");

            migrationBuilder.DropColumn(
                name: "FloorID",
                schema: "PRJ",
                table: "HighRiseFee");

            migrationBuilder.DropColumn(
                name: "TowerID",
                schema: "PRJ",
                table: "HighRiseFee");
        }
    }
}
