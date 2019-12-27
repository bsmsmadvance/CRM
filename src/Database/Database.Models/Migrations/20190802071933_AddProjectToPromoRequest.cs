using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddProjectToPromoRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProjectID",
                schema: "PRM",
                table: "PreSalePromotionRequest",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_PreSalePromotionRequest_ProjectID",
                schema: "PRM",
                table: "PreSalePromotionRequest",
                column: "ProjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_PreSalePromotionRequest_Project_ProjectID",
                schema: "PRM",
                table: "PreSalePromotionRequest",
                column: "ProjectID",
                principalSchema: "PRJ",
                principalTable: "Project",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PreSalePromotionRequest_Project_ProjectID",
                schema: "PRM",
                table: "PreSalePromotionRequest");

            migrationBuilder.DropIndex(
                name: "IX_PreSalePromotionRequest_ProjectID",
                schema: "PRM",
                table: "PreSalePromotionRequest");

            migrationBuilder.DropColumn(
                name: "ProjectID",
                schema: "PRM",
                table: "PreSalePromotionRequest");
        }
    }
}
