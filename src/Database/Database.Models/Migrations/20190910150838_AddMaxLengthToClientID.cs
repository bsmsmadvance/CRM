using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddMaxLengthToClientID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agreement_UnitPrice_ActiveUnitPriceID",
                schema: "SAL",
                table: "Agreement");

            migrationBuilder.DropForeignKey(
                name: "FK_Agreement_Contact_ContactID",
                schema: "SAL",
                table: "Agreement");

            migrationBuilder.DropTable(
                name: "AgreementDownPeriod",
                schema: "SAL");

            migrationBuilder.DropColumn(
                name: "AgreementStatus",
                schema: "SAL",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "ApproveStatus",
                schema: "SAL",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "AgreementNo",
                schema: "LET",
                table: "TransferLetter");

            migrationBuilder.DropColumn(
                name: "AgreementNo",
                schema: "LET",
                table: "DownPaymentLetter");

            migrationBuilder.DropColumn(
                name: "ReceiptNo",
                schema: "FIN",
                table: "Payment");

            migrationBuilder.RenameColumn(
                name: "ContactID",
                schema: "SAL",
                table: "Agreement",
                newName: "SignContractRequestUserID");

            migrationBuilder.RenameColumn(
                name: "ActiveUnitPriceID",
                schema: "SAL",
                table: "Agreement",
                newName: "ProjectID");

            migrationBuilder.RenameIndex(
                name: "IX_Agreement_ContactID",
                schema: "SAL",
                table: "Agreement",
                newName: "IX_Agreement_SignContractRequestUserID");

            migrationBuilder.RenameIndex(
                name: "IX_Agreement_ActiveUnitPriceID",
                schema: "SAL",
                table: "Agreement",
                newName: "IX_Agreement_ProjectID");

            migrationBuilder.AddColumn<string>(
                name: "ClientID",
                schema: "USR",
                table: "User",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientSecret",
                schema: "USR",
                table: "User",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsClient",
                schema: "USR",
                table: "User",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSellerPay",
                schema: "SAL",
                table: "UnitPriceInstallment",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "ChangeAgreementOwnerWorkflowID",
                schema: "SAL",
                table: "AgreementOwner",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "SAL",
                table: "AgreementOwner",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAddNewOwner",
                schema: "SAL",
                table: "AgreementOwner",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCancelledOwner",
                schema: "SAL",
                table: "AgreementOwner",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTransferOwner",
                schema: "SAL",
                table: "AgreementOwner",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "AgreementNo",
                schema: "SAL",
                table: "Agreement",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AgreementStatusMasterCenterID",
                schema: "SAL",
                table: "Agreement",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "AreaPricePerUnit",
                schema: "SAL",
                table: "Agreement",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "ContractDate",
                schema: "SAL",
                table: "Agreement",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "HighRiseConstructionStatusMasterCenterID",
                schema: "SAL",
                table: "Agreement",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPrintApproved",
                schema: "SAL",
                table: "Agreement",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSellerPayLastDownInstallment",
                schema: "SAL",
                table: "Agreement",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSignContractApproved",
                schema: "SAL",
                table: "Agreement",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "OffsetArea",
                schema: "SAL",
                table: "Agreement",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OffsetAreaPrice",
                schema: "SAL",
                table: "Agreement",
                type: "Money",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PrintApprovedByUserID",
                schema: "SAL",
                table: "Agreement",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PrintApprovedDate",
                schema: "SAL",
                table: "Agreement",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SignContractApprovedDate",
                schema: "SAL",
                table: "Agreement",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SignContractRequestDate",
                schema: "SAL",
                table: "Agreement",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TransferOwnershipDate",
                schema: "SAL",
                table: "Agreement",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ForeignerRatio",
                schema: "PRJ",
                table: "Project",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<decimal>(
                name: "FeeIncludingVat",
                schema: "FIN",
                table: "PaymentCreditCard",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<double>(
                name: "Vat",
                schema: "FIN",
                table: "PaymentCreditCard",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "ChangeAgreementOwnerWorkflows",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsFromMigration = table.Column<bool>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false),
                    AppointmentDate = table.Column<DateTime>(nullable: true),
                    NewTransferOwnershipDate = table.Column<DateTime>(nullable: true),
                    Fee = table.Column<decimal>(type: "Money", nullable: false),
                    ChangeAgreementOwnerTypeMasterCenterID = table.Column<Guid>(nullable: true),
                    RequestApproverRoleID = table.Column<Guid>(nullable: true),
                    RequestApproverUserID = table.Column<Guid>(nullable: true),
                    RequestApprovedDate = table.Column<DateTime>(nullable: true),
                    IsRequestApproved = table.Column<bool>(nullable: true),
                    RequestRejectComment = table.Column<string>(maxLength: 5000, nullable: true),
                    ApproverRoleID = table.Column<Guid>(nullable: true),
                    ApproverUserID = table.Column<Guid>(nullable: true),
                    ApprovedDate = table.Column<DateTime>(nullable: true),
                    IsApproved = table.Column<bool>(nullable: true),
                    RejectComment = table.Column<string>(maxLength: 5000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeAgreementOwnerWorkflows", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ChangeAgreementOwnerWorkflows_Role_ApproverRoleID",
                        column: x => x.ApproverRoleID,
                        principalSchema: "USR",
                        principalTable: "Role",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChangeAgreementOwnerWorkflows_User_ApproverUserID",
                        column: x => x.ApproverUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChangeAgreementOwnerWorkflows_MasterCenter_ChangeAgreementOwnerTypeMasterCenterID",
                        column: x => x.ChangeAgreementOwnerTypeMasterCenterID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChangeAgreementOwnerWorkflows_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChangeAgreementOwnerWorkflows_Role_RequestApproverRoleID",
                        column: x => x.RequestApproverRoleID,
                        principalSchema: "USR",
                        principalTable: "Role",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChangeAgreementOwnerWorkflows_User_RequestApproverUserID",
                        column: x => x.RequestApproverUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChangeAgreementOwnerWorkflows_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AgreementFile",
                schema: "SAL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsFromMigration = table.Column<bool>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false),
                    AgreementID = table.Column<Guid>(nullable: true),
                    FileName = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgreementFile", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AgreementFile_Agreement_AgreementID",
                        column: x => x.AgreementID,
                        principalSchema: "SAL",
                        principalTable: "Agreement",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AgreementFile_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AgreementFile_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChangeUnitWorkflow",
                schema: "SAL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsFromMigration = table.Column<bool>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false),
                    FromBookingID = table.Column<Guid>(nullable: true),
                    ToBookingID = table.Column<Guid>(nullable: true),
                    FromAgreementID = table.Column<Guid>(nullable: true),
                    ToAgreementID = table.Column<Guid>(nullable: true),
                    RequestApproverRoleID = table.Column<Guid>(nullable: true),
                    RequestApproverUserID = table.Column<Guid>(nullable: true),
                    RequestApprovedDate = table.Column<DateTime>(nullable: true),
                    IsRequestApproved = table.Column<bool>(nullable: true),
                    RequestRejectComment = table.Column<string>(maxLength: 5000, nullable: true),
                    ApproverRoleID = table.Column<Guid>(nullable: true),
                    ApproverUserID = table.Column<Guid>(nullable: true),
                    ApprovedDate = table.Column<DateTime>(nullable: true),
                    IsApproved = table.Column<bool>(nullable: true),
                    RejectComment = table.Column<string>(maxLength: 5000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeUnitWorkflow", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ChangeUnitWorkflow_Role_ApproverRoleID",
                        column: x => x.ApproverRoleID,
                        principalSchema: "USR",
                        principalTable: "Role",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChangeUnitWorkflow_User_ApproverUserID",
                        column: x => x.ApproverUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChangeUnitWorkflow_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChangeUnitWorkflow_Booking_FromAgreementID",
                        column: x => x.FromAgreementID,
                        principalSchema: "SAL",
                        principalTable: "Booking",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChangeUnitWorkflow_Booking_FromBookingID",
                        column: x => x.FromBookingID,
                        principalSchema: "SAL",
                        principalTable: "Booking",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChangeUnitWorkflow_Role_RequestApproverRoleID",
                        column: x => x.RequestApproverRoleID,
                        principalSchema: "USR",
                        principalTable: "Role",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChangeUnitWorkflow_User_RequestApproverUserID",
                        column: x => x.RequestApproverUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChangeUnitWorkflow_Booking_ToAgreementID",
                        column: x => x.ToAgreementID,
                        principalSchema: "SAL",
                        principalTable: "Booking",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChangeUnitWorkflow_Booking_ToBookingID",
                        column: x => x.ToBookingID,
                        principalSchema: "SAL",
                        principalTable: "Booking",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChangeUnitWorkflow_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SignContractWorkflow",
                schema: "SAL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsFromMigration = table.Column<bool>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false),
                    AgreementID = table.Column<Guid>(nullable: true),
                    SignContractActionMasterCenterID = table.Column<Guid>(nullable: true),
                    ActionDate = table.Column<DateTime>(nullable: true),
                    ActionByRoleID = table.Column<Guid>(nullable: true),
                    ActionByUserID = table.Column<Guid>(nullable: true),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SignContractWorkflow", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SignContractWorkflow_Role_ActionByRoleID",
                        column: x => x.ActionByRoleID,
                        principalSchema: "USR",
                        principalTable: "Role",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SignContractWorkflow_User_ActionByUserID",
                        column: x => x.ActionByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SignContractWorkflow_Agreement_AgreementID",
                        column: x => x.AgreementID,
                        principalSchema: "SAL",
                        principalTable: "Agreement",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SignContractWorkflow_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SignContractWorkflow_MasterCenter_SignContractActionMasterCenterID",
                        column: x => x.SignContractActionMasterCenterID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SignContractWorkflow_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransferAgreementOwner",
                schema: "SAL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsFromMigration = table.Column<bool>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false),
                    FromAgreementOwnerID = table.Column<Guid>(nullable: true),
                    ToAgreementOwnerID = table.Column<Guid>(nullable: true)
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

            migrationBuilder.CreateTable(
                name: "ChangeAgreementOwnerFile",
                schema: "SAL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsFromMigration = table.Column<bool>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false),
                    ChangeAgreementOwnerWorkflowID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 1000, nullable: true),
                    File = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeAgreementOwnerFile", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ChangeAgreementOwnerFile_ChangeAgreementOwnerWorkflows_ChangeAgreementOwnerWorkflowID",
                        column: x => x.ChangeAgreementOwnerWorkflowID,
                        principalTable: "ChangeAgreementOwnerWorkflows",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChangeAgreementOwnerFile_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChangeAgreementOwnerFile_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChangeUnitFile",
                schema: "SAL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsFromMigration = table.Column<bool>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false),
                    ChangeUnitWorkflowID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 1000, nullable: true),
                    File = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeUnitFile", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ChangeUnitFile_ChangeUnitWorkflow_ChangeUnitWorkflowID",
                        column: x => x.ChangeUnitWorkflowID,
                        principalSchema: "SAL",
                        principalTable: "ChangeUnitWorkflow",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChangeUnitFile_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChangeUnitFile_User_UpdatedByUserID",
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
                name: "IX_Agreement_AgreementStatusMasterCenterID",
                schema: "SAL",
                table: "Agreement",
                column: "AgreementStatusMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_Agreement_HighRiseConstructionStatusMasterCenterID",
                schema: "SAL",
                table: "Agreement",
                column: "HighRiseConstructionStatusMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_Agreement_PrintApprovedByUserID",
                schema: "SAL",
                table: "Agreement",
                column: "PrintApprovedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeAgreementOwnerWorkflows_ApproverRoleID",
                table: "ChangeAgreementOwnerWorkflows",
                column: "ApproverRoleID");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeAgreementOwnerWorkflows_ApproverUserID",
                table: "ChangeAgreementOwnerWorkflows",
                column: "ApproverUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeAgreementOwnerWorkflows_ChangeAgreementOwnerTypeMasterCenterID",
                table: "ChangeAgreementOwnerWorkflows",
                column: "ChangeAgreementOwnerTypeMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeAgreementOwnerWorkflows_CreatedByUserID",
                table: "ChangeAgreementOwnerWorkflows",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeAgreementOwnerWorkflows_RequestApproverRoleID",
                table: "ChangeAgreementOwnerWorkflows",
                column: "RequestApproverRoleID");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeAgreementOwnerWorkflows_RequestApproverUserID",
                table: "ChangeAgreementOwnerWorkflows",
                column: "RequestApproverUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeAgreementOwnerWorkflows_UpdatedByUserID",
                table: "ChangeAgreementOwnerWorkflows",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_AgreementFile_AgreementID",
                schema: "SAL",
                table: "AgreementFile",
                column: "AgreementID");

            migrationBuilder.CreateIndex(
                name: "IX_AgreementFile_CreatedByUserID",
                schema: "SAL",
                table: "AgreementFile",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_AgreementFile_UpdatedByUserID",
                schema: "SAL",
                table: "AgreementFile",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeAgreementOwnerFile_ChangeAgreementOwnerWorkflowID",
                schema: "SAL",
                table: "ChangeAgreementOwnerFile",
                column: "ChangeAgreementOwnerWorkflowID");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeAgreementOwnerFile_CreatedByUserID",
                schema: "SAL",
                table: "ChangeAgreementOwnerFile",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeAgreementOwnerFile_UpdatedByUserID",
                schema: "SAL",
                table: "ChangeAgreementOwnerFile",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeUnitFile_ChangeUnitWorkflowID",
                schema: "SAL",
                table: "ChangeUnitFile",
                column: "ChangeUnitWorkflowID");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeUnitFile_CreatedByUserID",
                schema: "SAL",
                table: "ChangeUnitFile",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeUnitFile_UpdatedByUserID",
                schema: "SAL",
                table: "ChangeUnitFile",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeUnitWorkflow_ApproverRoleID",
                schema: "SAL",
                table: "ChangeUnitWorkflow",
                column: "ApproverRoleID");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeUnitWorkflow_ApproverUserID",
                schema: "SAL",
                table: "ChangeUnitWorkflow",
                column: "ApproverUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeUnitWorkflow_CreatedByUserID",
                schema: "SAL",
                table: "ChangeUnitWorkflow",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeUnitWorkflow_FromAgreementID",
                schema: "SAL",
                table: "ChangeUnitWorkflow",
                column: "FromAgreementID");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeUnitWorkflow_FromBookingID",
                schema: "SAL",
                table: "ChangeUnitWorkflow",
                column: "FromBookingID");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeUnitWorkflow_RequestApproverRoleID",
                schema: "SAL",
                table: "ChangeUnitWorkflow",
                column: "RequestApproverRoleID");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeUnitWorkflow_RequestApproverUserID",
                schema: "SAL",
                table: "ChangeUnitWorkflow",
                column: "RequestApproverUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeUnitWorkflow_ToAgreementID",
                schema: "SAL",
                table: "ChangeUnitWorkflow",
                column: "ToAgreementID");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeUnitWorkflow_ToBookingID",
                schema: "SAL",
                table: "ChangeUnitWorkflow",
                column: "ToBookingID");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeUnitWorkflow_UpdatedByUserID",
                schema: "SAL",
                table: "ChangeUnitWorkflow",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_SignContractWorkflow_ActionByRoleID",
                schema: "SAL",
                table: "SignContractWorkflow",
                column: "ActionByRoleID");

            migrationBuilder.CreateIndex(
                name: "IX_SignContractWorkflow_ActionByUserID",
                schema: "SAL",
                table: "SignContractWorkflow",
                column: "ActionByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_SignContractWorkflow_AgreementID",
                schema: "SAL",
                table: "SignContractWorkflow",
                column: "AgreementID");

            migrationBuilder.CreateIndex(
                name: "IX_SignContractWorkflow_CreatedByUserID",
                schema: "SAL",
                table: "SignContractWorkflow",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_SignContractWorkflow_SignContractActionMasterCenterID",
                schema: "SAL",
                table: "SignContractWorkflow",
                column: "SignContractActionMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_SignContractWorkflow_UpdatedByUserID",
                schema: "SAL",
                table: "SignContractWorkflow",
                column: "UpdatedByUserID");

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
                name: "FK_Agreement_MasterCenter_AgreementStatusMasterCenterID",
                schema: "SAL",
                table: "Agreement",
                column: "AgreementStatusMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Agreement_MasterCenter_HighRiseConstructionStatusMasterCenterID",
                schema: "SAL",
                table: "Agreement",
                column: "HighRiseConstructionStatusMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Agreement_User_PrintApprovedByUserID",
                schema: "SAL",
                table: "Agreement",
                column: "PrintApprovedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Agreement_Project_ProjectID",
                schema: "SAL",
                table: "Agreement",
                column: "ProjectID",
                principalSchema: "PRJ",
                principalTable: "Project",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Agreement_User_SignContractRequestUserID",
                schema: "SAL",
                table: "Agreement",
                column: "SignContractRequestUserID",
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agreement_MasterCenter_AgreementStatusMasterCenterID",
                schema: "SAL",
                table: "Agreement");

            migrationBuilder.DropForeignKey(
                name: "FK_Agreement_MasterCenter_HighRiseConstructionStatusMasterCenterID",
                schema: "SAL",
                table: "Agreement");

            migrationBuilder.DropForeignKey(
                name: "FK_Agreement_User_PrintApprovedByUserID",
                schema: "SAL",
                table: "Agreement");

            migrationBuilder.DropForeignKey(
                name: "FK_Agreement_Project_ProjectID",
                schema: "SAL",
                table: "Agreement");

            migrationBuilder.DropForeignKey(
                name: "FK_Agreement_User_SignContractRequestUserID",
                schema: "SAL",
                table: "Agreement");

            migrationBuilder.DropForeignKey(
                name: "FK_AgreementOwner_ChangeAgreementOwnerWorkflows_ChangeAgreementOwnerWorkflowID",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropTable(
                name: "AgreementFile",
                schema: "SAL");

            migrationBuilder.DropTable(
                name: "ChangeAgreementOwnerFile",
                schema: "SAL");

            migrationBuilder.DropTable(
                name: "ChangeUnitFile",
                schema: "SAL");

            migrationBuilder.DropTable(
                name: "SignContractWorkflow",
                schema: "SAL");

            migrationBuilder.DropTable(
                name: "TransferAgreementOwner",
                schema: "SAL");

            migrationBuilder.DropTable(
                name: "ChangeAgreementOwnerWorkflows");

            migrationBuilder.DropTable(
                name: "ChangeUnitWorkflow",
                schema: "SAL");

            migrationBuilder.DropIndex(
                name: "IX_AgreementOwner_ChangeAgreementOwnerWorkflowID",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropIndex(
                name: "IX_Agreement_AgreementStatusMasterCenterID",
                schema: "SAL",
                table: "Agreement");

            migrationBuilder.DropIndex(
                name: "IX_Agreement_HighRiseConstructionStatusMasterCenterID",
                schema: "SAL",
                table: "Agreement");

            migrationBuilder.DropIndex(
                name: "IX_Agreement_PrintApprovedByUserID",
                schema: "SAL",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "ClientID",
                schema: "USR",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ClientSecret",
                schema: "USR",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IsClient",
                schema: "USR",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IsSellerPay",
                schema: "SAL",
                table: "UnitPriceInstallment");

            migrationBuilder.DropColumn(
                name: "ChangeAgreementOwnerWorkflowID",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "IsAddNewOwner",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "IsCancelledOwner",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "IsTransferOwner",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "AgreementStatusMasterCenterID",
                schema: "SAL",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "AreaPricePerUnit",
                schema: "SAL",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "ContractDate",
                schema: "SAL",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "HighRiseConstructionStatusMasterCenterID",
                schema: "SAL",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "IsPrintApproved",
                schema: "SAL",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "IsSellerPayLastDownInstallment",
                schema: "SAL",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "IsSignContractApproved",
                schema: "SAL",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "OffsetArea",
                schema: "SAL",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "OffsetAreaPrice",
                schema: "SAL",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "PrintApprovedByUserID",
                schema: "SAL",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "PrintApprovedDate",
                schema: "SAL",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "SignContractApprovedDate",
                schema: "SAL",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "SignContractRequestDate",
                schema: "SAL",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "TransferOwnershipDate",
                schema: "SAL",
                table: "Agreement");

            migrationBuilder.DropColumn(
                name: "ForeignerRatio",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "FeeIncludingVat",
                schema: "FIN",
                table: "PaymentCreditCard");

            migrationBuilder.DropColumn(
                name: "Vat",
                schema: "FIN",
                table: "PaymentCreditCard");

            migrationBuilder.RenameColumn(
                name: "SignContractRequestUserID",
                schema: "SAL",
                table: "Agreement",
                newName: "ContactID");

            migrationBuilder.RenameColumn(
                name: "ProjectID",
                schema: "SAL",
                table: "Agreement",
                newName: "ActiveUnitPriceID");

            migrationBuilder.RenameIndex(
                name: "IX_Agreement_SignContractRequestUserID",
                schema: "SAL",
                table: "Agreement",
                newName: "IX_Agreement_ContactID");

            migrationBuilder.RenameIndex(
                name: "IX_Agreement_ProjectID",
                schema: "SAL",
                table: "Agreement",
                newName: "IX_Agreement_ActiveUnitPriceID");

            migrationBuilder.AlterColumn<string>(
                name: "AgreementNo",
                schema: "SAL",
                table: "Agreement",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AgreementStatus",
                schema: "SAL",
                table: "Agreement",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApproveStatus",
                schema: "SAL",
                table: "Agreement",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AgreementNo",
                schema: "LET",
                table: "TransferLetter",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AgreementNo",
                schema: "LET",
                table: "DownPaymentLetter",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceiptNo",
                schema: "FIN",
                table: "Payment",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AgreementDownPeriod",
                schema: "SAL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    AgreementID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    Detail = table.Column<string>(nullable: true),
                    DownNo = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsFromMigration = table.Column<bool>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false),
                    PayDate = table.Column<DateTime>(nullable: true),
                    ScheduleDate = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgreementDownPeriod", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AgreementDownPeriod_Agreement_AgreementID",
                        column: x => x.AgreementID,
                        principalSchema: "SAL",
                        principalTable: "Agreement",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgreementDownPeriod_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AgreementDownPeriod_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgreementDownPeriod_AgreementID",
                schema: "SAL",
                table: "AgreementDownPeriod",
                column: "AgreementID");

            migrationBuilder.CreateIndex(
                name: "IX_AgreementDownPeriod_CreatedByUserID",
                schema: "SAL",
                table: "AgreementDownPeriod",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_AgreementDownPeriod_UpdatedByUserID",
                schema: "SAL",
                table: "AgreementDownPeriod",
                column: "UpdatedByUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Agreement_UnitPrice_ActiveUnitPriceID",
                schema: "SAL",
                table: "Agreement",
                column: "ActiveUnitPriceID",
                principalSchema: "SAL",
                principalTable: "UnitPrice",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Agreement_Contact_ContactID",
                schema: "SAL",
                table: "Agreement",
                column: "ContactID",
                principalSchema: "CTM",
                principalTable: "Contact",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
