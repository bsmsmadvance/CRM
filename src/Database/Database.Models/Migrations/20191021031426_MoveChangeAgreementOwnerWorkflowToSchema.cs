using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class MoveChangeAgreementOwnerWorkflowToSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChangeAgreementOwnerWorkflows_Role_ApproverRoleID",
                table: "ChangeAgreementOwnerWorkflows");

            migrationBuilder.DropForeignKey(
                name: "FK_ChangeAgreementOwnerWorkflows_User_ApproverUserID",
                table: "ChangeAgreementOwnerWorkflows");

            migrationBuilder.DropForeignKey(
                name: "FK_ChangeAgreementOwnerWorkflows_MasterCenter_ChangeAgreementOwnerTypeMasterCenterID",
                table: "ChangeAgreementOwnerWorkflows");

            migrationBuilder.DropForeignKey(
                name: "FK_ChangeAgreementOwnerWorkflows_User_CreatedByUserID",
                table: "ChangeAgreementOwnerWorkflows");

            migrationBuilder.DropForeignKey(
                name: "FK_ChangeAgreementOwnerWorkflows_Role_RequestApproverRoleID",
                table: "ChangeAgreementOwnerWorkflows");

            migrationBuilder.DropForeignKey(
                name: "FK_ChangeAgreementOwnerWorkflows_User_RequestApproverUserID",
                table: "ChangeAgreementOwnerWorkflows");

            migrationBuilder.DropForeignKey(
                name: "FK_ChangeAgreementOwnerWorkflows_User_UpdatedByUserID",
                table: "ChangeAgreementOwnerWorkflows");

            migrationBuilder.DropForeignKey(
                name: "FK_AgreementOwner_ChangeAgreementOwnerWorkflows_ChangeAgreementOwnerWorkflowID",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_ChangeAgreementOwnerFile_ChangeAgreementOwnerWorkflows_ChangeAgreementOwnerWorkflowID",
                schema: "SAL",
                table: "ChangeAgreementOwnerFile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChangeAgreementOwnerWorkflows",
                table: "ChangeAgreementOwnerWorkflows");

            migrationBuilder.RenameTable(
                name: "ChangeAgreementOwnerWorkflows",
                newName: "ChangeAgreementOwnerWorkflow",
                newSchema: "SAL");

            migrationBuilder.RenameIndex(
                name: "IX_ChangeAgreementOwnerWorkflows_UpdatedByUserID",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow",
                newName: "IX_ChangeAgreementOwnerWorkflow_UpdatedByUserID");

            migrationBuilder.RenameIndex(
                name: "IX_ChangeAgreementOwnerWorkflows_RequestApproverUserID",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow",
                newName: "IX_ChangeAgreementOwnerWorkflow_RequestApproverUserID");

            migrationBuilder.RenameIndex(
                name: "IX_ChangeAgreementOwnerWorkflows_RequestApproverRoleID",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow",
                newName: "IX_ChangeAgreementOwnerWorkflow_RequestApproverRoleID");

            migrationBuilder.RenameIndex(
                name: "IX_ChangeAgreementOwnerWorkflows_CreatedByUserID",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow",
                newName: "IX_ChangeAgreementOwnerWorkflow_CreatedByUserID");

            migrationBuilder.RenameIndex(
                name: "IX_ChangeAgreementOwnerWorkflows_ChangeAgreementOwnerTypeMasterCenterID",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow",
                newName: "IX_ChangeAgreementOwnerWorkflow_ChangeAgreementOwnerTypeMasterCenterID");

            migrationBuilder.RenameIndex(
                name: "IX_ChangeAgreementOwnerWorkflows_ApproverUserID",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow",
                newName: "IX_ChangeAgreementOwnerWorkflow_ApproverUserID");

            migrationBuilder.RenameIndex(
                name: "IX_ChangeAgreementOwnerWorkflows_ApproverRoleID",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow",
                newName: "IX_ChangeAgreementOwnerWorkflow_ApproverRoleID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChangeAgreementOwnerWorkflow",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AgreementOwner_ChangeAgreementOwnerWorkflow_ChangeAgreementOwnerWorkflowID",
                schema: "SAL",
                table: "AgreementOwner",
                column: "ChangeAgreementOwnerWorkflowID",
                principalSchema: "SAL",
                principalTable: "ChangeAgreementOwnerWorkflow",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeAgreementOwnerFile_ChangeAgreementOwnerWorkflow_ChangeAgreementOwnerWorkflowID",
                schema: "SAL",
                table: "ChangeAgreementOwnerFile",
                column: "ChangeAgreementOwnerWorkflowID",
                principalSchema: "SAL",
                principalTable: "ChangeAgreementOwnerWorkflow",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeAgreementOwnerWorkflow_Role_ApproverRoleID",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow",
                column: "ApproverRoleID",
                principalSchema: "USR",
                principalTable: "Role",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeAgreementOwnerWorkflow_User_ApproverUserID",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow",
                column: "ApproverUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeAgreementOwnerWorkflow_MasterCenter_ChangeAgreementOwnerTypeMasterCenterID",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow",
                column: "ChangeAgreementOwnerTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeAgreementOwnerWorkflow_User_CreatedByUserID",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeAgreementOwnerWorkflow_Role_RequestApproverRoleID",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow",
                column: "RequestApproverRoleID",
                principalSchema: "USR",
                principalTable: "Role",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeAgreementOwnerWorkflow_User_RequestApproverUserID",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow",
                column: "RequestApproverUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeAgreementOwnerWorkflow_User_UpdatedByUserID",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AgreementOwner_ChangeAgreementOwnerWorkflow_ChangeAgreementOwnerWorkflowID",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_ChangeAgreementOwnerFile_ChangeAgreementOwnerWorkflow_ChangeAgreementOwnerWorkflowID",
                schema: "SAL",
                table: "ChangeAgreementOwnerFile");

            migrationBuilder.DropForeignKey(
                name: "FK_ChangeAgreementOwnerWorkflow_Role_ApproverRoleID",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow");

            migrationBuilder.DropForeignKey(
                name: "FK_ChangeAgreementOwnerWorkflow_User_ApproverUserID",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow");

            migrationBuilder.DropForeignKey(
                name: "FK_ChangeAgreementOwnerWorkflow_MasterCenter_ChangeAgreementOwnerTypeMasterCenterID",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow");

            migrationBuilder.DropForeignKey(
                name: "FK_ChangeAgreementOwnerWorkflow_User_CreatedByUserID",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow");

            migrationBuilder.DropForeignKey(
                name: "FK_ChangeAgreementOwnerWorkflow_Role_RequestApproverRoleID",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow");

            migrationBuilder.DropForeignKey(
                name: "FK_ChangeAgreementOwnerWorkflow_User_RequestApproverUserID",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow");

            migrationBuilder.DropForeignKey(
                name: "FK_ChangeAgreementOwnerWorkflow_User_UpdatedByUserID",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChangeAgreementOwnerWorkflow",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow");

            migrationBuilder.RenameTable(
                name: "ChangeAgreementOwnerWorkflow",
                schema: "SAL",
                newName: "ChangeAgreementOwnerWorkflows");

            migrationBuilder.RenameIndex(
                name: "IX_ChangeAgreementOwnerWorkflow_UpdatedByUserID",
                table: "ChangeAgreementOwnerWorkflows",
                newName: "IX_ChangeAgreementOwnerWorkflows_UpdatedByUserID");

            migrationBuilder.RenameIndex(
                name: "IX_ChangeAgreementOwnerWorkflow_RequestApproverUserID",
                table: "ChangeAgreementOwnerWorkflows",
                newName: "IX_ChangeAgreementOwnerWorkflows_RequestApproverUserID");

            migrationBuilder.RenameIndex(
                name: "IX_ChangeAgreementOwnerWorkflow_RequestApproverRoleID",
                table: "ChangeAgreementOwnerWorkflows",
                newName: "IX_ChangeAgreementOwnerWorkflows_RequestApproverRoleID");

            migrationBuilder.RenameIndex(
                name: "IX_ChangeAgreementOwnerWorkflow_CreatedByUserID",
                table: "ChangeAgreementOwnerWorkflows",
                newName: "IX_ChangeAgreementOwnerWorkflows_CreatedByUserID");

            migrationBuilder.RenameIndex(
                name: "IX_ChangeAgreementOwnerWorkflow_ChangeAgreementOwnerTypeMasterCenterID",
                table: "ChangeAgreementOwnerWorkflows",
                newName: "IX_ChangeAgreementOwnerWorkflows_ChangeAgreementOwnerTypeMasterCenterID");

            migrationBuilder.RenameIndex(
                name: "IX_ChangeAgreementOwnerWorkflow_ApproverUserID",
                table: "ChangeAgreementOwnerWorkflows",
                newName: "IX_ChangeAgreementOwnerWorkflows_ApproverUserID");

            migrationBuilder.RenameIndex(
                name: "IX_ChangeAgreementOwnerWorkflow_ApproverRoleID",
                table: "ChangeAgreementOwnerWorkflows",
                newName: "IX_ChangeAgreementOwnerWorkflows_ApproverRoleID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChangeAgreementOwnerWorkflows",
                table: "ChangeAgreementOwnerWorkflows",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeAgreementOwnerWorkflows_Role_ApproverRoleID",
                table: "ChangeAgreementOwnerWorkflows",
                column: "ApproverRoleID",
                principalSchema: "USR",
                principalTable: "Role",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeAgreementOwnerWorkflows_User_ApproverUserID",
                table: "ChangeAgreementOwnerWorkflows",
                column: "ApproverUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeAgreementOwnerWorkflows_MasterCenter_ChangeAgreementOwnerTypeMasterCenterID",
                table: "ChangeAgreementOwnerWorkflows",
                column: "ChangeAgreementOwnerTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeAgreementOwnerWorkflows_User_CreatedByUserID",
                table: "ChangeAgreementOwnerWorkflows",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeAgreementOwnerWorkflows_Role_RequestApproverRoleID",
                table: "ChangeAgreementOwnerWorkflows",
                column: "RequestApproverRoleID",
                principalSchema: "USR",
                principalTable: "Role",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeAgreementOwnerWorkflows_User_RequestApproverUserID",
                table: "ChangeAgreementOwnerWorkflows",
                column: "RequestApproverUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeAgreementOwnerWorkflows_User_UpdatedByUserID",
                table: "ChangeAgreementOwnerWorkflows",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AgreementOwner_ChangeAgreementOwnerWorkflows_ChangeAgreementOwnerWorkflowID",
                schema: "SAL",
                table: "AgreementOwner",
                column: "ChangeAgreementOwnerWorkflowID",
                principalTable: "ChangeAgreementOwnerWorkflows",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeAgreementOwnerFile_ChangeAgreementOwnerWorkflows_ChangeAgreementOwnerWorkflowID",
                schema: "SAL",
                table: "ChangeAgreementOwnerFile",
                column: "ChangeAgreementOwnerWorkflowID",
                principalTable: "ChangeAgreementOwnerWorkflows",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
