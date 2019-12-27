using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddBudgetWorkflowToApproval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MinPriceBudgetWorkflowID",
                schema: "SAL",
                table: "MinPriceBudgetApproval",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_MinPriceBudgetApproval_MinPriceBudgetWorkflowID",
                schema: "SAL",
                table: "MinPriceBudgetApproval",
                column: "MinPriceBudgetWorkflowID");

            migrationBuilder.AddForeignKey(
                name: "FK_MinPriceBudgetApproval_MinPriceBudgetWorkflow_MinPriceBudgetWorkflowID",
                schema: "SAL",
                table: "MinPriceBudgetApproval",
                column: "MinPriceBudgetWorkflowID",
                principalSchema: "SAL",
                principalTable: "MinPriceBudgetWorkflow",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MinPriceBudgetApproval_MinPriceBudgetWorkflow_MinPriceBudgetWorkflowID",
                schema: "SAL",
                table: "MinPriceBudgetApproval");

            migrationBuilder.DropIndex(
                name: "IX_MinPriceBudgetApproval_MinPriceBudgetWorkflowID",
                schema: "SAL",
                table: "MinPriceBudgetApproval");

            migrationBuilder.DropColumn(
                name: "MinPriceBudgetWorkflowID",
                schema: "SAL",
                table: "MinPriceBudgetApproval");
        }
    }
}
