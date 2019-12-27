using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class alter_col_AGOwnerWorkFlow_serm_kim : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ChangeAgreementOwnerStatusMasterCenterID",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChangeAgreementOwnerWorkflow_ChangeAgreementOwnerStatusMasterCenterID",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow",
                column: "ChangeAgreementOwnerStatusMasterCenterID");

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeAgreementOwnerWorkflow_MasterCenter_ChangeAgreementOwnerStatusMasterCenterID",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow",
                column: "ChangeAgreementOwnerStatusMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChangeAgreementOwnerWorkflow_MasterCenter_ChangeAgreementOwnerStatusMasterCenterID",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow");

            migrationBuilder.DropIndex(
                name: "IX_ChangeAgreementOwnerWorkflow_ChangeAgreementOwnerStatusMasterCenterID",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow");

            migrationBuilder.DropColumn(
                name: "ChangeAgreementOwnerStatusMasterCenterID",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow");
        }
    }
}
