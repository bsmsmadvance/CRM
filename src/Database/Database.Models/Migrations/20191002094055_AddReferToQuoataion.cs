using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddReferToQuoataion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriceListWorkflow_ChangePromotionWorkflow_ChangePromotionWorkflowID",
                schema: "SAL",
                table: "PriceListWorkflow");

            migrationBuilder.RenameColumn(
                name: "ChangePromotionWorkflowID",
                schema: "SAL",
                table: "PriceListWorkflow",
                newName: "ChangeUnitWorkflowID");

            migrationBuilder.RenameIndex(
                name: "IX_PriceListWorkflow_ChangePromotionWorkflowID",
                schema: "SAL",
                table: "PriceListWorkflow",
                newName: "IX_PriceListWorkflow_ChangeUnitWorkflowID");

            migrationBuilder.AddColumn<Guid>(
                name: "ReferContactID",
                schema: "SAL",
                table: "Quotation",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReferContactName",
                schema: "SAL",
                table: "Quotation",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Quotation_ReferContactID",
                schema: "SAL",
                table: "Quotation",
                column: "ReferContactID");

            migrationBuilder.AddForeignKey(
                name: "FK_PriceListWorkflow_ChangeUnitWorkflow_ChangeUnitWorkflowID",
                schema: "SAL",
                table: "PriceListWorkflow",
                column: "ChangeUnitWorkflowID",
                principalSchema: "SAL",
                principalTable: "ChangeUnitWorkflow",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Quotation_Contact_ReferContactID",
                schema: "SAL",
                table: "Quotation",
                column: "ReferContactID",
                principalSchema: "CTM",
                principalTable: "Contact",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriceListWorkflow_ChangeUnitWorkflow_ChangeUnitWorkflowID",
                schema: "SAL",
                table: "PriceListWorkflow");

            migrationBuilder.DropForeignKey(
                name: "FK_Quotation_Contact_ReferContactID",
                schema: "SAL",
                table: "Quotation");

            migrationBuilder.DropIndex(
                name: "IX_Quotation_ReferContactID",
                schema: "SAL",
                table: "Quotation");

            migrationBuilder.DropColumn(
                name: "ReferContactID",
                schema: "SAL",
                table: "Quotation");

            migrationBuilder.DropColumn(
                name: "ReferContactName",
                schema: "SAL",
                table: "Quotation");

            migrationBuilder.RenameColumn(
                name: "ChangeUnitWorkflowID",
                schema: "SAL",
                table: "PriceListWorkflow",
                newName: "ChangePromotionWorkflowID");

            migrationBuilder.RenameIndex(
                name: "IX_PriceListWorkflow_ChangeUnitWorkflowID",
                schema: "SAL",
                table: "PriceListWorkflow",
                newName: "IX_PriceListWorkflow_ChangePromotionWorkflowID");

            migrationBuilder.AddForeignKey(
                name: "FK_PriceListWorkflow_ChangePromotionWorkflow_ChangePromotionWorkflowID",
                schema: "SAL",
                table: "PriceListWorkflow",
                column: "ChangePromotionWorkflowID",
                principalSchema: "PRM",
                principalTable: "ChangePromotionWorkflow",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
