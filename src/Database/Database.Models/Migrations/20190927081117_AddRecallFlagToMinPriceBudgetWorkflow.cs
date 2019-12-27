using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddRecallFlagToMinPriceBudgetWorkflow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCancelled",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRecalled",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "RecalledByUserID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RecalledTime",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DepositDate",
                schema: "FIN",
                table: "DepositHeader",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.CreateIndex(
                name: "IX_MinPriceBudgetWorkflow_RecalledByUserID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                column: "RecalledByUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_MinPriceBudgetWorkflow_User_RecalledByUserID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                column: "RecalledByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MinPriceBudgetWorkflow_User_RecalledByUserID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow");

            migrationBuilder.DropIndex(
                name: "IX_MinPriceBudgetWorkflow_RecalledByUserID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow");

            migrationBuilder.DropColumn(
                name: "IsCancelled",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow");

            migrationBuilder.DropColumn(
                name: "IsRecalled",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow");

            migrationBuilder.DropColumn(
                name: "RecalledByUserID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow");

            migrationBuilder.DropColumn(
                name: "RecalledTime",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DepositDate",
                schema: "FIN",
                table: "DepositHeader",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
