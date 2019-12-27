using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddMoreMinpriceFlowFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RecallReason",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RejectedByUserID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RejectedTime",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalBudget",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "UseBudget",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_MinPriceBudgetWorkflow_RejectedByUserID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                column: "RejectedByUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_MinPriceBudgetWorkflow_User_RejectedByUserID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                column: "RejectedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MinPriceBudgetWorkflow_User_RejectedByUserID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow");

            migrationBuilder.DropIndex(
                name: "IX_MinPriceBudgetWorkflow_RejectedByUserID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow");

            migrationBuilder.DropColumn(
                name: "RecallReason",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow");

            migrationBuilder.DropColumn(
                name: "RejectedByUserID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow");

            migrationBuilder.DropColumn(
                name: "RejectedTime",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow");

            migrationBuilder.DropColumn(
                name: "TotalBudget",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow");

            migrationBuilder.DropColumn(
                name: "UseBudget",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow");
        }
    }
}
