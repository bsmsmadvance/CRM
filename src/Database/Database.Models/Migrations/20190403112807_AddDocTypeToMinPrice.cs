using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddDocTypeToMinPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DocTypeMasterCenterID",
                schema: "PRJ",
                table: "MinPrice",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MinPrice_DocTypeMasterCenterID",
                schema: "PRJ",
                table: "MinPrice",
                column: "DocTypeMasterCenterID");

            migrationBuilder.AddForeignKey(
                name: "FK_MinPrice_MasterCenter_DocTypeMasterCenterID",
                schema: "PRJ",
                table: "MinPrice",
                column: "DocTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MinPrice_MasterCenter_DocTypeMasterCenterID",
                schema: "PRJ",
                table: "MinPrice");

            migrationBuilder.DropIndex(
                name: "IX_MinPrice_DocTypeMasterCenterID",
                schema: "PRJ",
                table: "MinPrice");

            migrationBuilder.DropColumn(
                name: "DocTypeMasterCenterID",
                schema: "PRJ",
                table: "MinPrice");
        }
    }
}
