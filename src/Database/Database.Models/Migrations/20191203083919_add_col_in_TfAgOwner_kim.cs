using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class add_col_in_TfAgOwner_kim : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AgreementOwner_ChangeAgreementOwnerWorkflow_ChangeAgreementOwnerWorkflowID",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropTable(
                name: "TransferAgreementOwner",
                schema: "SAL");

            migrationBuilder.DropIndex(
                name: "IX_AgreementOwner_ChangeAgreementOwnerWorkflowID",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.RenameColumn(
                name: "ChangeAgreementOwnerWorkflowID",
                schema: "SAL",
                table: "AgreementOwner",
                newName: "ToChangeAgreementOwnerWorkflowID");

            migrationBuilder.AddColumn<Guid>(
                name: "FromChangeAgreementOwnerWorkflowID",
                schema: "SAL",
                table: "AgreementOwner",
                nullable: true);

            //migrationBuilder.AddColumn<Guid>(
            //    name: "CompanyID",
            //    schema: "FIN",
            //    table: "ReceiptTempHeader",
            //    nullable: true);

            //migrationBuilder.AddColumn<Guid>(
            //    name: "ProjectID",
            //    schema: "FIN",
            //    table: "ReceiptTempHeader",
            //    nullable: true);

            //migrationBuilder.AddColumn<string>(
            //    name: "ProjectNameEN",
            //    schema: "FIN",
            //    table: "ReceiptTempHeader",
            //    maxLength: 1000,
            //    nullable: true);

            //migrationBuilder.AddColumn<Guid>(
            //    name: "UnitID",
            //    schema: "FIN",
            //    table: "ReceiptTempHeader",
            //    nullable: true);

            migrationBuilder.CreateTable(
                name: "AgreementTransferOwner",
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
                    FromAgreementOwnerID = table.Column<Guid>(nullable: true),
                    ToAgreementOwnerID = table.Column<Guid>(nullable: true),
                    TransferAgreementOwnerTypeMasterCenterID = table.Column<Guid>(nullable: true),
                    AgreementOwnerID = table.Column<Guid>(nullable: true),
                    ChangeAgreementOwnerWorkflowID = table.Column<Guid>(nullable: true)
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

            //migrationBuilder.CreateIndex(
            //    name: "IX_ReceiptTempHeader_CompanyID",
            //    schema: "FIN",
            //    table: "ReceiptTempHeader",
            //    column: "CompanyID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ReceiptTempHeader_ProjectID",
            //    schema: "FIN",
            //    table: "ReceiptTempHeader",
            //    column: "ProjectID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ReceiptTempHeader_UnitID",
            //    schema: "FIN",
            //    table: "ReceiptTempHeader",
            //    column: "UnitID");

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

            //migrationBuilder.AddForeignKey(
            //    name: "FK_ReceiptTempHeader_Company_CompanyID",
            //    schema: "FIN",
            //    table: "ReceiptTempHeader",
            //    column: "CompanyID",
            //    principalSchema: "MST",
            //    principalTable: "Company",
            //    principalColumn: "ID",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_ReceiptTempHeader_Project_ProjectID",
            //    schema: "FIN",
            //    table: "ReceiptTempHeader",
            //    column: "ProjectID",
            //    principalSchema: "PRJ",
            //    principalTable: "Project",
            //    principalColumn: "ID",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_ReceiptTempHeader_Unit_UnitID",
            //    schema: "FIN",
            //    table: "ReceiptTempHeader",
            //    column: "UnitID",
            //    principalSchema: "PRJ",
            //    principalTable: "Unit",
            //    principalColumn: "ID",
            //    onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_ReceiptTempHeader_Company_CompanyID",
            //    schema: "FIN",
            //    table: "ReceiptTempHeader");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_ReceiptTempHeader_Project_ProjectID",
            //    schema: "FIN",
            //    table: "ReceiptTempHeader");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_ReceiptTempHeader_Unit_UnitID",
            //    schema: "FIN",
            //    table: "ReceiptTempHeader");

            migrationBuilder.DropTable(
                name: "AgreementTransferOwner",
                schema: "SAL");

            //migrationBuilder.DropIndex(
            //    name: "IX_ReceiptTempHeader_CompanyID",
            //    schema: "FIN",
            //    table: "ReceiptTempHeader");

            //migrationBuilder.DropIndex(
            //    name: "IX_ReceiptTempHeader_ProjectID",
            //    schema: "FIN",
            //    table: "ReceiptTempHeader");

            //migrationBuilder.DropIndex(
            //    name: "IX_ReceiptTempHeader_UnitID",
            //    schema: "FIN",
            //    table: "ReceiptTempHeader");

            migrationBuilder.DropColumn(
                name: "FromChangeAgreementOwnerWorkflowID",
                schema: "SAL",
                table: "AgreementOwner");

            //migrationBuilder.DropColumn(
            //    name: "CompanyID",
            //    schema: "FIN",
            //    table: "ReceiptTempHeader");

            //migrationBuilder.DropColumn(
            //    name: "ProjectID",
            //    schema: "FIN",
            //    table: "ReceiptTempHeader");

            //migrationBuilder.DropColumn(
            //    name: "ProjectNameEN",
            //    schema: "FIN",
            //    table: "ReceiptTempHeader");

            //migrationBuilder.DropColumn(
            //    name: "UnitID",
            //    schema: "FIN",
            //    table: "ReceiptTempHeader");

            migrationBuilder.RenameColumn(
                name: "ToChangeAgreementOwnerWorkflowID",
                schema: "SAL",
                table: "AgreementOwner",
                newName: "ChangeAgreementOwnerWorkflowID");

            migrationBuilder.CreateTable(
                name: "TransferAgreementOwner",
                schema: "SAL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    FromAgreementOwnerID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastMigrateDate = table.Column<DateTime>(nullable: true),
                    RefMigrateID1 = table.Column<string>(maxLength: 100, nullable: true),
                    RefMigrateID2 = table.Column<string>(maxLength: 100, nullable: true),
                    RefMigrateID3 = table.Column<string>(maxLength: 100, nullable: true),
                    ToAgreementOwnerID = table.Column<Guid>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferAgreementOwner", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TransferAgreementOwner_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferAgreementOwner_AgreementOwner_FromAgreementOwnerID",
                        column: x => x.FromAgreementOwnerID,
                        principalSchema: "SAL",
                        principalTable: "AgreementOwner",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferAgreementOwner_AgreementOwner_ToAgreementOwnerID",
                        column: x => x.ToAgreementOwnerID,
                        principalSchema: "SAL",
                        principalTable: "AgreementOwner",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferAgreementOwner_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgreementOwner_ChangeAgreementOwnerWorkflowID",
                schema: "SAL",
                table: "AgreementOwner",
                column: "ChangeAgreementOwnerWorkflowID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferAgreementOwner_CreatedByUserID",
                schema: "SAL",
                table: "TransferAgreementOwner",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferAgreementOwner_FromAgreementOwnerID",
                schema: "SAL",
                table: "TransferAgreementOwner",
                column: "FromAgreementOwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferAgreementOwner_ToAgreementOwnerID",
                schema: "SAL",
                table: "TransferAgreementOwner",
                column: "ToAgreementOwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferAgreementOwner_UpdatedByUserID",
                schema: "SAL",
                table: "TransferAgreementOwner",
                column: "UpdatedByUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_AgreementOwner_ChangeAgreementOwnerWorkflow_ChangeAgreementOwnerWorkflowID",
                schema: "SAL",
                table: "AgreementOwner",
                column: "ChangeAgreementOwnerWorkflowID",
                principalSchema: "SAL",
                principalTable: "ChangeAgreementOwnerWorkflow",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
