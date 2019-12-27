using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddFKOfRequestByUserInChangePromotion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "RequestByUserID",
                schema: "PRM",
                table: "ChangePromotionWorkflow",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateIndex(
                name: "IX_ChangePromotionWorkflow_RequestByUserID",
                schema: "PRM",
                table: "ChangePromotionWorkflow",
                column: "RequestByUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_ChangePromotionWorkflow_User_RequestByUserID",
                schema: "PRM",
                table: "ChangePromotionWorkflow",
                column: "RequestByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChangePromotionWorkflow_User_RequestByUserID",
                schema: "PRM",
                table: "ChangePromotionWorkflow");

            migrationBuilder.DropIndex(
                name: "IX_ChangePromotionWorkflow_RequestByUserID",
                schema: "PRM",
                table: "ChangePromotionWorkflow");

            migrationBuilder.AlterColumn<Guid>(
                name: "RequestByUserID",
                schema: "PRM",
                table: "ChangePromotionWorkflow",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);
        }
    }
}
