using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class add_col_ChangeAgreementOwnerWorkflow_serm_kim : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgreementTransferOwner",
                schema: "SAL");

            migrationBuilder.DropColumn(
                name: "FromChangeAgreementOwnerWorkflowID",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "ToChangeAgreementOwnerWorkflowID",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.AddColumn<Guid>(
                name: "AgreementID",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPrintApproved",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoFeeComment",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow",
                maxLength: 5000,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PrintApprovedByUserID",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PrintApprovedDate",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SaleUserID",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ChangeAgreementOwnerWorkflowDetail",
                schema: "SAL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    RefMigrateID1 = table.Column<string>(maxLength: 100, nullable: true),
                    RefMigrateID2 = table.Column<string>(maxLength: 100, nullable: true),
                    RefMigrateID3 = table.Column<string>(maxLength: 100, nullable: true),
                    LastMigrateDate = table.Column<DateTime>(nullable: true),
                    ChangeAgreementOwnerWorkflowID = table.Column<Guid>(nullable: true),
                    AgreementOwnerID = table.Column<Guid>(nullable: true),
                    ChangeAgreementOwnerInType = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeAgreementOwnerWorkflowDetail", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ChangeAgreementOwnerWorkflowDetail_AgreementOwner_AgreementOwnerID",
                        column: x => x.AgreementOwnerID,
                        principalSchema: "SAL",
                        principalTable: "AgreementOwner",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChangeAgreementOwnerWorkflowDetail_ChangeAgreementOwnerWorkflow_ChangeAgreementOwnerWorkflowID",
                        column: x => x.ChangeAgreementOwnerWorkflowID,
                        principalSchema: "SAL",
                        principalTable: "ChangeAgreementOwnerWorkflow",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChangeAgreementOwnerWorkflowDetail_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChangeAgreementOwnerWorkflowDetail_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChangeAgreementOwnerWorkflow_AgreementID",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow",
                column: "AgreementID");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeAgreementOwnerWorkflow_PrintApprovedByUserID",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow",
                column: "PrintApprovedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeAgreementOwnerWorkflow_SaleUserID",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow",
                column: "SaleUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeAgreementOwnerWorkflowDetail_AgreementOwnerID",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflowDetail",
                column: "AgreementOwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeAgreementOwnerWorkflowDetail_ChangeAgreementOwnerWorkflowID",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflowDetail",
                column: "ChangeAgreementOwnerWorkflowID");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeAgreementOwnerWorkflowDetail_CreatedByUserID",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflowDetail",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeAgreementOwnerWorkflowDetail_UpdatedByUserID",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflowDetail",
                column: "UpdatedByUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeAgreementOwnerWorkflow_Agreement_AgreementID",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow",
                column: "AgreementID",
                principalSchema: "SAL",
                principalTable: "Agreement",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeAgreementOwnerWorkflow_User_PrintApprovedByUserID",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow",
                column: "PrintApprovedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeAgreementOwnerWorkflow_User_SaleUserID",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow",
                column: "SaleUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChangeAgreementOwnerWorkflow_Agreement_AgreementID",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow");

            migrationBuilder.DropForeignKey(
                name: "FK_ChangeAgreementOwnerWorkflow_User_PrintApprovedByUserID",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow");

            migrationBuilder.DropForeignKey(
                name: "FK_ChangeAgreementOwnerWorkflow_User_SaleUserID",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow");

            migrationBuilder.DropTable(
                name: "ChangeAgreementOwnerWorkflowDetail",
                schema: "SAL");

            migrationBuilder.DropIndex(
                name: "IX_ChangeAgreementOwnerWorkflow_AgreementID",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow");

            migrationBuilder.DropIndex(
                name: "IX_ChangeAgreementOwnerWorkflow_PrintApprovedByUserID",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow");

            migrationBuilder.DropIndex(
                name: "IX_ChangeAgreementOwnerWorkflow_SaleUserID",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow");

            migrationBuilder.DropColumn(
                name: "AgreementID",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow");

            migrationBuilder.DropColumn(
                name: "IsPrintApproved",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow");

            migrationBuilder.DropColumn(
                name: "NoFeeComment",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow");

            migrationBuilder.DropColumn(
                name: "PrintApprovedByUserID",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow");

            migrationBuilder.DropColumn(
                name: "PrintApprovedDate",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow");

            migrationBuilder.DropColumn(
                name: "SaleUserID",
                schema: "SAL",
                table: "ChangeAgreementOwnerWorkflow");

            migrationBuilder.AddColumn<Guid>(
                name: "FromChangeAgreementOwnerWorkflowID",
                schema: "SAL",
                table: "AgreementOwner",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ToChangeAgreementOwnerWorkflowID",
                schema: "SAL",
                table: "AgreementOwner",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AgreementTransferOwner",
                schema: "SAL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    AgreementOwnerID = table.Column<Guid>(nullable: true),
                    ChangeAgreementOwnerWorkflowID = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastMigrateDate = table.Column<DateTime>(nullable: true),
                    RefMigrateID1 = table.Column<string>(maxLength: 100, nullable: true),
                    RefMigrateID2 = table.Column<string>(maxLength: 100, nullable: true),
                    RefMigrateID3 = table.Column<string>(maxLength: 100, nullable: true),
                    ToAgreementOwnerID = table.Column<Guid>(nullable: true),
                    TransferAgreementOwnerTypeMasterCenterID = table.Column<Guid>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgreementTransferOwner", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AgreementTransferOwner_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AgreementTransferOwner_MasterCenter_ToAgreementOwnerID",
                        column: x => x.ToAgreementOwnerID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AgreementTransferOwner_AgreementOwner_ToAgreementOwnerID",
                        column: x => x.ToAgreementOwnerID,
                        principalSchema: "SAL",
                        principalTable: "AgreementOwner",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AgreementTransferOwner_ChangeAgreementOwnerWorkflow_ToAgreementOwnerID",
                        column: x => x.ToAgreementOwnerID,
                        principalSchema: "SAL",
                        principalTable: "ChangeAgreementOwnerWorkflow",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AgreementTransferOwner_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgreementTransferOwner_CreatedByUserID",
                schema: "SAL",
                table: "AgreementTransferOwner",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_AgreementTransferOwner_ToAgreementOwnerID",
                schema: "SAL",
                table: "AgreementTransferOwner",
                column: "ToAgreementOwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_AgreementTransferOwner_UpdatedByUserID",
                schema: "SAL",
                table: "AgreementTransferOwner",
                column: "UpdatedByUserID");
        }
    }
}
