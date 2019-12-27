using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class CreateNewCommissionTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RateOnTop",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "RateSettingSaleFix",
                schema: "CMS");

            migrationBuilder.DropColumn(
                name: "AmountUnit",
                schema: "CMS",
                table: "RateSettingTransfer");

            migrationBuilder.DropColumn(
                name: "RangeUnit",
                schema: "CMS",
                table: "RateSettingTransfer");

            migrationBuilder.DropColumn(
                name: "AmountUnit",
                schema: "CMS",
                table: "RateSettingSale");

            migrationBuilder.DropColumn(
                name: "RangeUnit",
                schema: "CMS",
                table: "RateSettingSale");

            migrationBuilder.DropColumn(
                name: "LCCenterAfterSale",
                schema: "CMS",
                table: "GeneralSetting");

            migrationBuilder.DropColumn(
                name: "LCCenterAfterSaleAmount",
                schema: "CMS",
                table: "GeneralSetting");

            migrationBuilder.DropColumn(
                name: "LCCenterAfterTransfer",
                schema: "CMS",
                table: "GeneralSetting");

            migrationBuilder.DropColumn(
                name: "LCCenterAfterTransferAmount",
                schema: "CMS",
                table: "GeneralSetting");

            migrationBuilder.DropColumn(
                name: "LCCenterGuaranteeAmount",
                schema: "CMS",
                table: "GeneralSetting");

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectID",
                schema: "CMS",
                table: "RateSettingTransfer",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectID",
                schema: "CMS",
                table: "RateSettingSale",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectID",
                schema: "CMS",
                table: "GeneralSetting",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ChangeLCSale",
                schema: "CMS",
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
                    ActiveDate = table.Column<DateTime>(nullable: true),
                    ContractID = table.Column<Guid>(nullable: true),
                    OldSaleOfficerTypeMasterCenterID = table.Column<Guid>(nullable: true),
                    OldAgentID = table.Column<Guid>(nullable: true),
                    OldAgentEmployeeID = table.Column<Guid>(nullable: true),
                    OldSaleUserID = table.Column<Guid>(nullable: true),
                    OldProjectSaleUserID = table.Column<Guid>(nullable: true),
                    NewSaleOfficerTypeMasterCenterID = table.Column<Guid>(nullable: true),
                    NewAgentID = table.Column<Guid>(nullable: true),
                    NewAgentEmployeeID = table.Column<Guid>(nullable: true),
                    NewSaleUserID = table.Column<Guid>(nullable: true),
                    NewProjectSaleUserID = table.Column<Guid>(nullable: true),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeLCSale", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ChangeLCSale_Agreement_ContractID",
                        column: x => x.ContractID,
                        principalSchema: "SAL",
                        principalTable: "Agreement",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChangeLCSale_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChangeLCSale_AgentEmployee_NewAgentEmployeeID",
                        column: x => x.NewAgentEmployeeID,
                        principalSchema: "MST",
                        principalTable: "AgentEmployee",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChangeLCSale_Agent_NewAgentID",
                        column: x => x.NewAgentID,
                        principalSchema: "MST",
                        principalTable: "Agent",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChangeLCSale_User_NewProjectSaleUserID",
                        column: x => x.NewProjectSaleUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChangeLCSale_MasterCenter_NewSaleOfficerTypeMasterCenterID",
                        column: x => x.NewSaleOfficerTypeMasterCenterID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChangeLCSale_User_NewSaleUserID",
                        column: x => x.NewSaleUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChangeLCSale_AgentEmployee_OldAgentEmployeeID",
                        column: x => x.OldAgentEmployeeID,
                        principalSchema: "MST",
                        principalTable: "AgentEmployee",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChangeLCSale_Agent_OldAgentID",
                        column: x => x.OldAgentID,
                        principalSchema: "MST",
                        principalTable: "Agent",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChangeLCSale_User_OldProjectSaleUserID",
                        column: x => x.OldProjectSaleUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChangeLCSale_MasterCenter_OldSaleOfficerTypeMasterCenterID",
                        column: x => x.OldSaleOfficerTypeMasterCenterID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChangeLCSale_User_OldSaleUserID",
                        column: x => x.OldSaleUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChangeLCSale_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChangeLCTransfer",
                schema: "CMS",
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
                    ActiveDate = table.Column<DateTime>(nullable: true),
                    TransferID = table.Column<Guid>(nullable: true),
                    OldLCTransferID = table.Column<Guid>(nullable: true),
                    NewLCTransferID = table.Column<Guid>(nullable: true),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeLCTransfer", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ChangeLCTransfer_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChangeLCTransfer_User_NewLCTransferID",
                        column: x => x.NewLCTransferID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChangeLCTransfer_User_OldLCTransferID",
                        column: x => x.OldLCTransferID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChangeLCTransfer_Transfer_TransferID",
                        column: x => x.TransferID,
                        principalSchema: "SAL",
                        principalTable: "Transfer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChangeLCTransfer_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeductMoney",
                schema: "CMS",
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
                    ActiveDate = table.Column<DateTime>(nullable: true),
                    ProjectID = table.Column<Guid>(nullable: true),
                    SaleUserID = table.Column<Guid>(nullable: true),
                    Amount = table.Column<decimal>(type: "Money", nullable: false),
                    Remark = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeductMoney", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DeductMoney_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeductMoney_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "PRJ",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeductMoney_User_SaleUserID",
                        column: x => x.SaleUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeductMoney_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IncreaseMoney",
                schema: "CMS",
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
                    ActiveDate = table.Column<DateTime>(nullable: true),
                    ProjectID = table.Column<Guid>(nullable: true),
                    SaleUserID = table.Column<Guid>(nullable: true),
                    Amount = table.Column<decimal>(type: "Money", nullable: true),
                    Remark = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncreaseMoney", x => x.ID);
                    table.ForeignKey(
                        name: "FK_IncreaseMoney_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IncreaseMoney_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "PRJ",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IncreaseMoney_User_SaleUserID",
                        column: x => x.SaleUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IncreaseMoney_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RateSettingAgent",
                schema: "CMS",
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
                    ActiveDate = table.Column<DateTime>(nullable: true),
                    ProjectID = table.Column<Guid>(nullable: true),
                    AgentID = table.Column<Guid>(nullable: true),
                    Amount = table.Column<decimal>(type: "Money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RateSettingAgent", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RateSettingAgent_Agent_AgentID",
                        column: x => x.AgentID,
                        principalSchema: "MST",
                        principalTable: "Agent",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RateSettingAgent_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RateSettingAgent_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "PRJ",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RateSettingAgent_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RateSettingFixSale",
                schema: "CMS",
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
                    ActiveDate = table.Column<DateTime>(nullable: true),
                    ProjectID = table.Column<Guid>(nullable: true),
                    Amount = table.Column<decimal>(type: "Money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RateSettingFixSale", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RateSettingFixSale_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RateSettingFixSale_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "PRJ",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RateSettingFixSale_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RateSettingFixSaleModel",
                schema: "CMS",
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
                    ActiveDate = table.Column<DateTime>(nullable: true),
                    ProjectID = table.Column<Guid>(nullable: true),
                    ModelID = table.Column<Guid>(nullable: true),
                    Amount = table.Column<decimal>(type: "Money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RateSettingFixSaleModel", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RateSettingFixSaleModel_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RateSettingFixSaleModel_Model_ModelID",
                        column: x => x.ModelID,
                        principalSchema: "PRJ",
                        principalTable: "Model",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RateSettingFixSaleModel_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "PRJ",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RateSettingFixSaleModel_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RateSettingFixTransfer",
                schema: "CMS",
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
                    ActiveDate = table.Column<DateTime>(nullable: true),
                    ProjectID = table.Column<Guid>(nullable: true),
                    Amount = table.Column<decimal>(type: "Money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RateSettingFixTransfer", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RateSettingFixTransfer_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RateSettingFixTransfer_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "PRJ",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RateSettingFixTransfer_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RateSettingFixTransferModel",
                schema: "CMS",
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
                    ActiveDate = table.Column<DateTime>(nullable: true),
                    ProjectID = table.Column<Guid>(nullable: true),
                    ModelID = table.Column<Guid>(nullable: true),
                    Amount = table.Column<decimal>(type: "Money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RateSettingFixTransferModel", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RateSettingFixTransferModel_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RateSettingFixTransferModel_Model_ModelID",
                        column: x => x.ModelID,
                        principalSchema: "PRJ",
                        principalTable: "Model",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RateSettingFixTransferModel_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "PRJ",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RateSettingFixTransferModel_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RateSettingTransfer_ProjectID",
                schema: "CMS",
                table: "RateSettingTransfer",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_RateSettingSale_ProjectID",
                schema: "CMS",
                table: "RateSettingSale",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_GeneralSetting_ProjectID",
                schema: "CMS",
                table: "GeneralSetting",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeLCSale_ContractID",
                schema: "CMS",
                table: "ChangeLCSale",
                column: "ContractID");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeLCSale_CreatedByUserID",
                schema: "CMS",
                table: "ChangeLCSale",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeLCSale_NewAgentEmployeeID",
                schema: "CMS",
                table: "ChangeLCSale",
                column: "NewAgentEmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeLCSale_NewAgentID",
                schema: "CMS",
                table: "ChangeLCSale",
                column: "NewAgentID");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeLCSale_NewProjectSaleUserID",
                schema: "CMS",
                table: "ChangeLCSale",
                column: "NewProjectSaleUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeLCSale_NewSaleOfficerTypeMasterCenterID",
                schema: "CMS",
                table: "ChangeLCSale",
                column: "NewSaleOfficerTypeMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeLCSale_NewSaleUserID",
                schema: "CMS",
                table: "ChangeLCSale",
                column: "NewSaleUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeLCSale_OldAgentEmployeeID",
                schema: "CMS",
                table: "ChangeLCSale",
                column: "OldAgentEmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeLCSale_OldAgentID",
                schema: "CMS",
                table: "ChangeLCSale",
                column: "OldAgentID");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeLCSale_OldProjectSaleUserID",
                schema: "CMS",
                table: "ChangeLCSale",
                column: "OldProjectSaleUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeLCSale_OldSaleOfficerTypeMasterCenterID",
                schema: "CMS",
                table: "ChangeLCSale",
                column: "OldSaleOfficerTypeMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeLCSale_OldSaleUserID",
                schema: "CMS",
                table: "ChangeLCSale",
                column: "OldSaleUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeLCSale_UpdatedByUserID",
                schema: "CMS",
                table: "ChangeLCSale",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeLCTransfer_CreatedByUserID",
                schema: "CMS",
                table: "ChangeLCTransfer",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeLCTransfer_NewLCTransferID",
                schema: "CMS",
                table: "ChangeLCTransfer",
                column: "NewLCTransferID");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeLCTransfer_OldLCTransferID",
                schema: "CMS",
                table: "ChangeLCTransfer",
                column: "OldLCTransferID");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeLCTransfer_TransferID",
                schema: "CMS",
                table: "ChangeLCTransfer",
                column: "TransferID");

            migrationBuilder.CreateIndex(
                name: "IX_ChangeLCTransfer_UpdatedByUserID",
                schema: "CMS",
                table: "ChangeLCTransfer",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_DeductMoney_CreatedByUserID",
                schema: "CMS",
                table: "DeductMoney",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_DeductMoney_ProjectID",
                schema: "CMS",
                table: "DeductMoney",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_DeductMoney_SaleUserID",
                schema: "CMS",
                table: "DeductMoney",
                column: "SaleUserID");

            migrationBuilder.CreateIndex(
                name: "IX_DeductMoney_UpdatedByUserID",
                schema: "CMS",
                table: "DeductMoney",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_IncreaseMoney_CreatedByUserID",
                schema: "CMS",
                table: "IncreaseMoney",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_IncreaseMoney_ProjectID",
                schema: "CMS",
                table: "IncreaseMoney",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_IncreaseMoney_SaleUserID",
                schema: "CMS",
                table: "IncreaseMoney",
                column: "SaleUserID");

            migrationBuilder.CreateIndex(
                name: "IX_IncreaseMoney_UpdatedByUserID",
                schema: "CMS",
                table: "IncreaseMoney",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_RateSettingAgent_AgentID",
                schema: "CMS",
                table: "RateSettingAgent",
                column: "AgentID");

            migrationBuilder.CreateIndex(
                name: "IX_RateSettingAgent_CreatedByUserID",
                schema: "CMS",
                table: "RateSettingAgent",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_RateSettingAgent_ProjectID",
                schema: "CMS",
                table: "RateSettingAgent",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_RateSettingAgent_UpdatedByUserID",
                schema: "CMS",
                table: "RateSettingAgent",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_RateSettingFixSale_CreatedByUserID",
                schema: "CMS",
                table: "RateSettingFixSale",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_RateSettingFixSale_ProjectID",
                schema: "CMS",
                table: "RateSettingFixSale",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_RateSettingFixSale_UpdatedByUserID",
                schema: "CMS",
                table: "RateSettingFixSale",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_RateSettingFixSaleModel_CreatedByUserID",
                schema: "CMS",
                table: "RateSettingFixSaleModel",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_RateSettingFixSaleModel_ModelID",
                schema: "CMS",
                table: "RateSettingFixSaleModel",
                column: "ModelID");

            migrationBuilder.CreateIndex(
                name: "IX_RateSettingFixSaleModel_ProjectID",
                schema: "CMS",
                table: "RateSettingFixSaleModel",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_RateSettingFixSaleModel_UpdatedByUserID",
                schema: "CMS",
                table: "RateSettingFixSaleModel",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_RateSettingFixTransfer_CreatedByUserID",
                schema: "CMS",
                table: "RateSettingFixTransfer",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_RateSettingFixTransfer_ProjectID",
                schema: "CMS",
                table: "RateSettingFixTransfer",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_RateSettingFixTransfer_UpdatedByUserID",
                schema: "CMS",
                table: "RateSettingFixTransfer",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_RateSettingFixTransferModel_CreatedByUserID",
                schema: "CMS",
                table: "RateSettingFixTransferModel",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_RateSettingFixTransferModel_ModelID",
                schema: "CMS",
                table: "RateSettingFixTransferModel",
                column: "ModelID");

            migrationBuilder.CreateIndex(
                name: "IX_RateSettingFixTransferModel_ProjectID",
                schema: "CMS",
                table: "RateSettingFixTransferModel",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_RateSettingFixTransferModel_UpdatedByUserID",
                schema: "CMS",
                table: "RateSettingFixTransferModel",
                column: "UpdatedByUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_GeneralSetting_Project_ProjectID",
                schema: "CMS",
                table: "GeneralSetting",
                column: "ProjectID",
                principalSchema: "PRJ",
                principalTable: "Project",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RateSettingSale_Project_ProjectID",
                schema: "CMS",
                table: "RateSettingSale",
                column: "ProjectID",
                principalSchema: "PRJ",
                principalTable: "Project",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RateSettingTransfer_Project_ProjectID",
                schema: "CMS",
                table: "RateSettingTransfer",
                column: "ProjectID",
                principalSchema: "PRJ",
                principalTable: "Project",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GeneralSetting_Project_ProjectID",
                schema: "CMS",
                table: "GeneralSetting");

            migrationBuilder.DropForeignKey(
                name: "FK_RateSettingSale_Project_ProjectID",
                schema: "CMS",
                table: "RateSettingSale");

            migrationBuilder.DropForeignKey(
                name: "FK_RateSettingTransfer_Project_ProjectID",
                schema: "CMS",
                table: "RateSettingTransfer");

            migrationBuilder.DropTable(
                name: "ChangeLCSale",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "ChangeLCTransfer",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "DeductMoney",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "IncreaseMoney",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "RateSettingAgent",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "RateSettingFixSale",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "RateSettingFixSaleModel",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "RateSettingFixTransfer",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "RateSettingFixTransferModel",
                schema: "CMS");

            migrationBuilder.DropIndex(
                name: "IX_RateSettingTransfer_ProjectID",
                schema: "CMS",
                table: "RateSettingTransfer");

            migrationBuilder.DropIndex(
                name: "IX_RateSettingSale_ProjectID",
                schema: "CMS",
                table: "RateSettingSale");

            migrationBuilder.DropIndex(
                name: "IX_GeneralSetting_ProjectID",
                schema: "CMS",
                table: "GeneralSetting");

            migrationBuilder.DropColumn(
                name: "ProjectID",
                schema: "CMS",
                table: "RateSettingTransfer");

            migrationBuilder.DropColumn(
                name: "ProjectID",
                schema: "CMS",
                table: "RateSettingSale");

            migrationBuilder.DropColumn(
                name: "ProjectID",
                schema: "CMS",
                table: "GeneralSetting");

            migrationBuilder.AddColumn<string>(
                name: "AmountUnit",
                schema: "CMS",
                table: "RateSettingTransfer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RangeUnit",
                schema: "CMS",
                table: "RateSettingTransfer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AmountUnit",
                schema: "CMS",
                table: "RateSettingSale",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RangeUnit",
                schema: "CMS",
                table: "RateSettingSale",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LCCenterAfterSale",
                schema: "CMS",
                table: "GeneralSetting",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "LCCenterAfterSaleAmount",
                schema: "CMS",
                table: "GeneralSetting",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "LCCenterAfterTransfer",
                schema: "CMS",
                table: "GeneralSetting",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "LCCenterAfterTransferAmount",
                schema: "CMS",
                table: "GeneralSetting",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "LCCenterGuaranteeAmount",
                schema: "CMS",
                table: "GeneralSetting",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "RateOnTop",
                schema: "CMS",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ActiveDate = table.Column<DateTime>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    AmountUnit = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    EndRange = table.Column<decimal>(type: "Money", nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsFromMigration = table.Column<bool>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false),
                    StartRange = table.Column<decimal>(type: "Money", nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RateOnTop", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RateOnTop_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RateOnTop_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RateSettingSaleFix",
                schema: "CMS",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ActiveDate = table.Column<DateTime>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    AmountUnit = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsFromMigration = table.Column<bool>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RateSettingSaleFix", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RateSettingSaleFix_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RateSettingSaleFix_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RateOnTop_CreatedByUserID",
                schema: "CMS",
                table: "RateOnTop",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_RateOnTop_UpdatedByUserID",
                schema: "CMS",
                table: "RateOnTop",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_RateSettingSaleFix_CreatedByUserID",
                schema: "CMS",
                table: "RateSettingSaleFix",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_RateSettingSaleFix_UpdatedByUserID",
                schema: "CMS",
                table: "RateSettingSaleFix",
                column: "UpdatedByUserID");
        }
    }
}
