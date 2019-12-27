using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class UpdateCommissionTables2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalculateOther",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "CalculatePerMonth",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "CalculateSale",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "CalculateTransfer",
                schema: "CMS");

            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                schema: "CMS",
                table: "RateSettingSale",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.CreateTable(
                name: "CalculateHighRiseSale",
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
                    PeriodYear = table.Column<int>(nullable: false),
                    PeriodMonth = table.Column<int>(nullable: false),
                    AgreementID = table.Column<Guid>(nullable: true),
                    ProjectNo = table.Column<string>(nullable: true),
                    UnitNo = table.Column<string>(nullable: true),
                    CommissionPercentType = table.Column<string>(nullable: true),
                    CommissionPercentRate = table.Column<decimal>(nullable: true),
                    RateFixSaleModelAmount = table.Column<decimal>(type: "Money", nullable: true),
                    RateAgentAmount = table.Column<decimal>(type: "Money", nullable: true),
                    SaleOfficerTypeMasterCenterID = table.Column<Guid>(nullable: true),
                    AgentID = table.Column<Guid>(nullable: true),
                    AgentEmployeeID = table.Column<Guid>(nullable: true),
                    SaleUserID = table.Column<Guid>(nullable: true),
                    SaleUserSalePaid = table.Column<decimal>(type: "Money", nullable: true),
                    ProjectSaleUserID = table.Column<Guid>(nullable: true),
                    ProjectSaleSalePaid = table.Column<decimal>(type: "Money", nullable: true),
                    TotalCommissionPaid = table.Column<decimal>(type: "Money", nullable: true),
                    CommissionForThisMonth = table.Column<decimal>(type: "Money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculateHighRiseSale", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CalculateHighRiseSale_AgentEmployee_AgentEmployeeID",
                        column: x => x.AgentEmployeeID,
                        principalSchema: "MST",
                        principalTable: "AgentEmployee",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalculateHighRiseSale_Agent_AgentID",
                        column: x => x.AgentID,
                        principalSchema: "MST",
                        principalTable: "Agent",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalculateHighRiseSale_Agreement_AgreementID",
                        column: x => x.AgreementID,
                        principalSchema: "SAL",
                        principalTable: "Agreement",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalculateHighRiseSale_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalculateHighRiseSale_User_ProjectSaleUserID",
                        column: x => x.ProjectSaleUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalculateHighRiseSale_MasterCenter_SaleOfficerTypeMasterCenterID",
                        column: x => x.SaleOfficerTypeMasterCenterID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalculateHighRiseSale_User_SaleUserID",
                        column: x => x.SaleUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalculateHighRiseSale_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CalculateHighRiseTransfer",
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
                    PeriodYear = table.Column<int>(nullable: false),
                    PeriodMonth = table.Column<int>(nullable: false),
                    TransferID = table.Column<Guid>(nullable: true),
                    ProjectNo = table.Column<string>(nullable: true),
                    UnitNo = table.Column<string>(nullable: true),
                    CommissionPercentType = table.Column<string>(nullable: true),
                    CommissionPercentRate = table.Column<decimal>(nullable: true),
                    RateFixTransferModelAmount = table.Column<decimal>(type: "Money", nullable: true),
                    LCTransferID = table.Column<Guid>(nullable: true),
                    LCTransferPaid = table.Column<decimal>(type: "Money", nullable: true),
                    TotalCommissionPaid = table.Column<decimal>(type: "Money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculateHighRiseTransfer", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CalculateHighRiseTransfer_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalculateHighRiseTransfer_User_LCTransferID",
                        column: x => x.LCTransferID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalculateHighRiseTransfer_Transfer_TransferID",
                        column: x => x.TransferID,
                        principalSchema: "SAL",
                        principalTable: "Transfer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalculateHighRiseTransfer_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CalculateIncreaseDeductMoney",
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
                    PeriodYear = table.Column<int>(nullable: false),
                    PeriodMonth = table.Column<int>(nullable: false),
                    ProjectID = table.Column<Guid>(nullable: true),
                    SaleUserID = table.Column<Guid>(nullable: true),
                    IncreaseAmount = table.Column<decimal>(type: "Money", nullable: true),
                    DeductAmount = table.Column<decimal>(type: "Money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculateIncreaseDeductMoney", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CalculateIncreaseDeductMoney_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalculateIncreaseDeductMoney_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "PRJ",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalculateIncreaseDeductMoney_User_SaleUserID",
                        column: x => x.SaleUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalculateIncreaseDeductMoney_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CalculateLowRiseSale",
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
                    PeriodYear = table.Column<int>(nullable: false),
                    PeriodMonth = table.Column<int>(nullable: false),
                    AgreementID = table.Column<Guid>(nullable: true),
                    ProjectNo = table.Column<string>(nullable: true),
                    UnitNo = table.Column<string>(nullable: true),
                    CommissionPercentType = table.Column<string>(nullable: true),
                    CommissionPercentRate = table.Column<decimal>(nullable: true),
                    RateFixSaleModelAmount = table.Column<decimal>(type: "Money", nullable: true),
                    SaleOfficerTypeMasterCenterID = table.Column<Guid>(nullable: true),
                    SaleUserID = table.Column<Guid>(nullable: true),
                    SaleUserSalePaid = table.Column<decimal>(type: "Money", nullable: true),
                    SaleUserTransferPaid = table.Column<decimal>(type: "Money", nullable: true),
                    SaleUserNewLaunchPaid = table.Column<decimal>(type: "Money", nullable: true),
                    ProjectSaleUserID = table.Column<Guid>(nullable: true),
                    ProjectSaleSalePaid = table.Column<decimal>(type: "Money", nullable: true),
                    ProjectSaleTransferPaid = table.Column<decimal>(type: "Money", nullable: true),
                    ProjectSaleNewLaunchPaid = table.Column<decimal>(type: "Money", nullable: true),
                    TotalCommissionPaid = table.Column<decimal>(type: "Money", nullable: true),
                    CommissionForThisMonth = table.Column<decimal>(type: "Money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculateLowRiseSale", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CalculateLowRiseSale_Agreement_AgreementID",
                        column: x => x.AgreementID,
                        principalSchema: "SAL",
                        principalTable: "Agreement",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalculateLowRiseSale_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalculateLowRiseSale_User_ProjectSaleUserID",
                        column: x => x.ProjectSaleUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalculateLowRiseSale_MasterCenter_SaleOfficerTypeMasterCenterID",
                        column: x => x.SaleOfficerTypeMasterCenterID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalculateLowRiseSale_User_SaleUserID",
                        column: x => x.SaleUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalculateLowRiseSale_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CalculateLowRiseTransfer",
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
                    PeriodYear = table.Column<int>(nullable: false),
                    PeriodMonth = table.Column<int>(nullable: false),
                    TransferID = table.Column<Guid>(nullable: true),
                    ProjectNo = table.Column<string>(nullable: true),
                    UnitNo = table.Column<string>(nullable: true),
                    CommissionPercentType = table.Column<string>(nullable: true),
                    CommissionPercentRate = table.Column<decimal>(nullable: true),
                    RateFixTransferModelAmount = table.Column<decimal>(type: "Money", nullable: true),
                    SaleUserID = table.Column<Guid>(nullable: true),
                    SaleUserSalePaid = table.Column<decimal>(type: "Money", nullable: true),
                    ProjectSaleUserID = table.Column<Guid>(nullable: true),
                    ProjectSaleSalePaid = table.Column<decimal>(type: "Money", nullable: true),
                    TotalCommissionPaid = table.Column<decimal>(type: "Money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculateLowRiseTransfer", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CalculateLowRiseTransfer_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalculateLowRiseTransfer_User_ProjectSaleUserID",
                        column: x => x.ProjectSaleUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalculateLowRiseTransfer_User_SaleUserID",
                        column: x => x.SaleUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalculateLowRiseTransfer_Transfer_TransferID",
                        column: x => x.TransferID,
                        principalSchema: "SAL",
                        principalTable: "Transfer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalculateLowRiseTransfer_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CalculatePerMonthHighRiseSale",
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
                    PeriodMonth = table.Column<int>(nullable: false),
                    PeriodYear = table.Column<int>(nullable: false),
                    ProjectID = table.Column<Guid>(nullable: true),
                    TotalContractAmount = table.Column<decimal>(type: "Money", nullable: true),
                    TotalContractUnit = table.Column<int>(nullable: true),
                    TotalContractCancelAmount = table.Column<decimal>(type: "Money", nullable: true),
                    TotalContractCancelUnit = table.Column<int>(nullable: true),
                    TotalContractNetAmount = table.Column<decimal>(type: "Money", nullable: true),
                    TotalContractNetUnit = table.Column<int>(nullable: true),
                    CommissionPercentType = table.Column<string>(nullable: true),
                    CommissionPercentRate = table.Column<decimal>(nullable: true),
                    IsApprove = table.Column<bool>(nullable: true),
                    ApproveDate = table.Column<DateTime>(nullable: true),
                    ApproveUserBy = table.Column<Guid>(nullable: true),
                    CancelApproveDate = table.Column<DateTime>(nullable: true),
                    CancelApproveUserBy = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculatePerMonthHighRiseSale", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CalculatePerMonthHighRiseSale_User_ApproveUserBy",
                        column: x => x.ApproveUserBy,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalculatePerMonthHighRiseSale_User_CancelApproveUserBy",
                        column: x => x.CancelApproveUserBy,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalculatePerMonthHighRiseSale_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalculatePerMonthHighRiseSale_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "PRJ",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalculatePerMonthHighRiseSale_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CalculatePerMonthHighRiseTransfer",
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
                    PeriodMonth = table.Column<int>(nullable: false),
                    PeriodYear = table.Column<int>(nullable: false),
                    ProjectID = table.Column<Guid>(nullable: true),
                    TotalTransferAmount = table.Column<decimal>(type: "Money", nullable: true),
                    TotalTransferUnit = table.Column<int>(nullable: true),
                    CommissionPercentType = table.Column<string>(nullable: true),
                    CommissionPercentRate = table.Column<decimal>(nullable: true),
                    IsApprove = table.Column<bool>(nullable: true),
                    ApproveDate = table.Column<DateTime>(nullable: true),
                    ApproveUserBy = table.Column<Guid>(nullable: true),
                    CancelApproveDate = table.Column<DateTime>(nullable: true),
                    CancelApproveUserBy = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculatePerMonthHighRiseTransfer", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CalculatePerMonthHighRiseTransfer_User_ApproveUserBy",
                        column: x => x.ApproveUserBy,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalculatePerMonthHighRiseTransfer_User_CancelApproveUserBy",
                        column: x => x.CancelApproveUserBy,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalculatePerMonthHighRiseTransfer_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalculatePerMonthHighRiseTransfer_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "PRJ",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalculatePerMonthHighRiseTransfer_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CalculatePerMonthLowRise",
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
                    PeriodMonth = table.Column<int>(nullable: false),
                    PeriodYear = table.Column<int>(nullable: false),
                    ProjectID = table.Column<Guid>(nullable: true),
                    SaleMonthAmount = table.Column<int>(nullable: false),
                    TotalContractAmount = table.Column<decimal>(type: "Money", nullable: true),
                    TotalContractUnit = table.Column<int>(nullable: true),
                    TotalContractCancelAmount = table.Column<decimal>(type: "Money", nullable: true),
                    TotalContractCancelUnit = table.Column<int>(nullable: true),
                    TotalContractNetAmount = table.Column<decimal>(type: "Money", nullable: true),
                    TotalContractNetUnit = table.Column<int>(nullable: true),
                    CommissionPercentType = table.Column<string>(nullable: true),
                    CommissionPercentRate = table.Column<decimal>(nullable: true),
                    NewLaunchPerUnit = table.Column<decimal>(type: "Money", nullable: true),
                    IsApprove = table.Column<bool>(nullable: true),
                    ApproveDate = table.Column<DateTime>(nullable: true),
                    ApproveUserBy = table.Column<Guid>(nullable: true),
                    CancelApproveDate = table.Column<DateTime>(nullable: true),
                    CancelApproveUserBy = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculatePerMonthLowRise", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CalculatePerMonthLowRise_User_ApproveUserBy",
                        column: x => x.ApproveUserBy,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalculatePerMonthLowRise_User_CancelApproveUserBy",
                        column: x => x.CancelApproveUserBy,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalculatePerMonthLowRise_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalculatePerMonthLowRise_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "PRJ",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalculatePerMonthLowRise_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CommissionContract",
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
                    PeriodMonth = table.Column<int>(nullable: false),
                    PeriodYear = table.Column<int>(nullable: false),
                    AgreementID = table.Column<Guid>(nullable: true),
                    ContractID = table.Column<Guid>(nullable: true),
                    ProjectNo = table.Column<string>(nullable: true),
                    UnitNo = table.Column<string>(nullable: true),
                    BookingDate = table.Column<DateTime>(nullable: false),
                    ContractDate = table.Column<DateTime>(nullable: false),
                    ApproveDate = table.Column<DateTime>(nullable: true),
                    SignContractApproveDate = table.Column<DateTime>(nullable: true),
                    ReceiptDate = table.Column<DateTime>(nullable: true),
                    ContractAmount = table.Column<decimal>(nullable: true),
                    ContractPaidAmount = table.Column<decimal>(type: "Money", nullable: true),
                    SellingPrice = table.Column<decimal>(type: "Money", nullable: false),
                    TransferDiscount = table.Column<decimal>(type: "Money", nullable: true),
                    FreeDownAmount = table.Column<decimal>(type: "Money", nullable: true),
                    CancelType = table.Column<string>(nullable: true),
                    CancelDate = table.Column<DateTime>(nullable: true),
                    SaleOfficerTypeMasterCenterID = table.Column<Guid>(nullable: true),
                    AgentID = table.Column<Guid>(nullable: true),
                    AgentEmployeeID = table.Column<Guid>(nullable: true),
                    SaleUserID = table.Column<Guid>(nullable: true),
                    ProjectSaleUserID = table.Column<Guid>(nullable: true),
                    AgreementIDReferent = table.Column<Guid>(nullable: true),
                    SellingPriceReferent = table.Column<decimal>(type: "Money", nullable: true),
                    TransferDiscountReferent = table.Column<decimal>(type: "Money", nullable: true),
                    CalTimeType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommissionContract", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CommissionContract_AgentEmployee_AgentEmployeeID",
                        column: x => x.AgentEmployeeID,
                        principalSchema: "MST",
                        principalTable: "AgentEmployee",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommissionContract_Agent_AgentID",
                        column: x => x.AgentID,
                        principalSchema: "MST",
                        principalTable: "Agent",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommissionContract_Agreement_AgreementIDReferent",
                        column: x => x.AgreementIDReferent,
                        principalSchema: "SAL",
                        principalTable: "Agreement",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommissionContract_Agreement_ContractID",
                        column: x => x.ContractID,
                        principalSchema: "SAL",
                        principalTable: "Agreement",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommissionContract_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommissionContract_User_ProjectSaleUserID",
                        column: x => x.ProjectSaleUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommissionContract_MasterCenter_SaleOfficerTypeMasterCenterID",
                        column: x => x.SaleOfficerTypeMasterCenterID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommissionContract_User_SaleUserID",
                        column: x => x.SaleUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommissionContract_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CommissionTransfer",
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
                    PeriodMonth = table.Column<int>(nullable: false),
                    PeriodYear = table.Column<int>(nullable: false),
                    TransferID = table.Column<Guid>(nullable: true),
                    ProjectNo = table.Column<string>(nullable: true),
                    UnitNo = table.Column<string>(nullable: true),
                    TransferDate = table.Column<DateTime>(nullable: true),
                    SignContractApproveDate = table.Column<DateTime>(nullable: true),
                    SellingPrice = table.Column<decimal>(nullable: false),
                    TransferDiscount = table.Column<decimal>(nullable: true),
                    FreeDownAmount = table.Column<decimal>(nullable: true),
                    NetSellPrice = table.Column<decimal>(nullable: true),
                    LCTransferID = table.Column<Guid>(nullable: true),
                    IsPreTransfer = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommissionTransfer", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CommissionTransfer_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommissionTransfer_User_LCTransferID",
                        column: x => x.LCTransferID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommissionTransfer_Transfer_TransferID",
                        column: x => x.TransferID,
                        principalSchema: "SAL",
                        principalTable: "Transfer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommissionTransfer_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RateSale",
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
                    BGNo = table.Column<string>(nullable: true),
                    Sequence = table.Column<int>(nullable: false),
                    Rate = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RateSale", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RateSale_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RateSale_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RateTransfer",
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
                    BG = table.Column<string>(nullable: true),
                    Sequence = table.Column<int>(nullable: false),
                    Rate = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RateTransfer", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RateTransfer_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RateTransfer_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CalculateHighRiseSale_AgentEmployeeID",
                schema: "CMS",
                table: "CalculateHighRiseSale",
                column: "AgentEmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculateHighRiseSale_AgentID",
                schema: "CMS",
                table: "CalculateHighRiseSale",
                column: "AgentID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculateHighRiseSale_AgreementID",
                schema: "CMS",
                table: "CalculateHighRiseSale",
                column: "AgreementID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculateHighRiseSale_CreatedByUserID",
                schema: "CMS",
                table: "CalculateHighRiseSale",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculateHighRiseSale_ProjectSaleUserID",
                schema: "CMS",
                table: "CalculateHighRiseSale",
                column: "ProjectSaleUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculateHighRiseSale_SaleOfficerTypeMasterCenterID",
                schema: "CMS",
                table: "CalculateHighRiseSale",
                column: "SaleOfficerTypeMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculateHighRiseSale_SaleUserID",
                schema: "CMS",
                table: "CalculateHighRiseSale",
                column: "SaleUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculateHighRiseSale_UpdatedByUserID",
                schema: "CMS",
                table: "CalculateHighRiseSale",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculateHighRiseTransfer_CreatedByUserID",
                schema: "CMS",
                table: "CalculateHighRiseTransfer",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculateHighRiseTransfer_LCTransferID",
                schema: "CMS",
                table: "CalculateHighRiseTransfer",
                column: "LCTransferID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculateHighRiseTransfer_TransferID",
                schema: "CMS",
                table: "CalculateHighRiseTransfer",
                column: "TransferID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculateHighRiseTransfer_UpdatedByUserID",
                schema: "CMS",
                table: "CalculateHighRiseTransfer",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculateIncreaseDeductMoney_CreatedByUserID",
                schema: "CMS",
                table: "CalculateIncreaseDeductMoney",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculateIncreaseDeductMoney_ProjectID",
                schema: "CMS",
                table: "CalculateIncreaseDeductMoney",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculateIncreaseDeductMoney_SaleUserID",
                schema: "CMS",
                table: "CalculateIncreaseDeductMoney",
                column: "SaleUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculateIncreaseDeductMoney_UpdatedByUserID",
                schema: "CMS",
                table: "CalculateIncreaseDeductMoney",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculateLowRiseSale_AgreementID",
                schema: "CMS",
                table: "CalculateLowRiseSale",
                column: "AgreementID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculateLowRiseSale_CreatedByUserID",
                schema: "CMS",
                table: "CalculateLowRiseSale",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculateLowRiseSale_ProjectSaleUserID",
                schema: "CMS",
                table: "CalculateLowRiseSale",
                column: "ProjectSaleUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculateLowRiseSale_SaleOfficerTypeMasterCenterID",
                schema: "CMS",
                table: "CalculateLowRiseSale",
                column: "SaleOfficerTypeMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculateLowRiseSale_SaleUserID",
                schema: "CMS",
                table: "CalculateLowRiseSale",
                column: "SaleUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculateLowRiseSale_UpdatedByUserID",
                schema: "CMS",
                table: "CalculateLowRiseSale",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculateLowRiseTransfer_CreatedByUserID",
                schema: "CMS",
                table: "CalculateLowRiseTransfer",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculateLowRiseTransfer_ProjectSaleUserID",
                schema: "CMS",
                table: "CalculateLowRiseTransfer",
                column: "ProjectSaleUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculateLowRiseTransfer_SaleUserID",
                schema: "CMS",
                table: "CalculateLowRiseTransfer",
                column: "SaleUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculateLowRiseTransfer_TransferID",
                schema: "CMS",
                table: "CalculateLowRiseTransfer",
                column: "TransferID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculateLowRiseTransfer_UpdatedByUserID",
                schema: "CMS",
                table: "CalculateLowRiseTransfer",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculatePerMonthHighRiseSale_ApproveUserBy",
                schema: "CMS",
                table: "CalculatePerMonthHighRiseSale",
                column: "ApproveUserBy");

            migrationBuilder.CreateIndex(
                name: "IX_CalculatePerMonthHighRiseSale_CancelApproveUserBy",
                schema: "CMS",
                table: "CalculatePerMonthHighRiseSale",
                column: "CancelApproveUserBy");

            migrationBuilder.CreateIndex(
                name: "IX_CalculatePerMonthHighRiseSale_CreatedByUserID",
                schema: "CMS",
                table: "CalculatePerMonthHighRiseSale",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculatePerMonthHighRiseSale_ProjectID",
                schema: "CMS",
                table: "CalculatePerMonthHighRiseSale",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculatePerMonthHighRiseSale_UpdatedByUserID",
                schema: "CMS",
                table: "CalculatePerMonthHighRiseSale",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculatePerMonthHighRiseTransfer_ApproveUserBy",
                schema: "CMS",
                table: "CalculatePerMonthHighRiseTransfer",
                column: "ApproveUserBy");

            migrationBuilder.CreateIndex(
                name: "IX_CalculatePerMonthHighRiseTransfer_CancelApproveUserBy",
                schema: "CMS",
                table: "CalculatePerMonthHighRiseTransfer",
                column: "CancelApproveUserBy");

            migrationBuilder.CreateIndex(
                name: "IX_CalculatePerMonthHighRiseTransfer_CreatedByUserID",
                schema: "CMS",
                table: "CalculatePerMonthHighRiseTransfer",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculatePerMonthHighRiseTransfer_ProjectID",
                schema: "CMS",
                table: "CalculatePerMonthHighRiseTransfer",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculatePerMonthHighRiseTransfer_UpdatedByUserID",
                schema: "CMS",
                table: "CalculatePerMonthHighRiseTransfer",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculatePerMonthLowRise_ApproveUserBy",
                schema: "CMS",
                table: "CalculatePerMonthLowRise",
                column: "ApproveUserBy");

            migrationBuilder.CreateIndex(
                name: "IX_CalculatePerMonthLowRise_CancelApproveUserBy",
                schema: "CMS",
                table: "CalculatePerMonthLowRise",
                column: "CancelApproveUserBy");

            migrationBuilder.CreateIndex(
                name: "IX_CalculatePerMonthLowRise_CreatedByUserID",
                schema: "CMS",
                table: "CalculatePerMonthLowRise",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculatePerMonthLowRise_ProjectID",
                schema: "CMS",
                table: "CalculatePerMonthLowRise",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculatePerMonthLowRise_UpdatedByUserID",
                schema: "CMS",
                table: "CalculatePerMonthLowRise",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CommissionContract_AgentEmployeeID",
                schema: "CMS",
                table: "CommissionContract",
                column: "AgentEmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_CommissionContract_AgentID",
                schema: "CMS",
                table: "CommissionContract",
                column: "AgentID");

            migrationBuilder.CreateIndex(
                name: "IX_CommissionContract_AgreementIDReferent",
                schema: "CMS",
                table: "CommissionContract",
                column: "AgreementIDReferent");

            migrationBuilder.CreateIndex(
                name: "IX_CommissionContract_ContractID",
                schema: "CMS",
                table: "CommissionContract",
                column: "ContractID");

            migrationBuilder.CreateIndex(
                name: "IX_CommissionContract_CreatedByUserID",
                schema: "CMS",
                table: "CommissionContract",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CommissionContract_ProjectSaleUserID",
                schema: "CMS",
                table: "CommissionContract",
                column: "ProjectSaleUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CommissionContract_SaleOfficerTypeMasterCenterID",
                schema: "CMS",
                table: "CommissionContract",
                column: "SaleOfficerTypeMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_CommissionContract_SaleUserID",
                schema: "CMS",
                table: "CommissionContract",
                column: "SaleUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CommissionContract_UpdatedByUserID",
                schema: "CMS",
                table: "CommissionContract",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CommissionTransfer_CreatedByUserID",
                schema: "CMS",
                table: "CommissionTransfer",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CommissionTransfer_LCTransferID",
                schema: "CMS",
                table: "CommissionTransfer",
                column: "LCTransferID");

            migrationBuilder.CreateIndex(
                name: "IX_CommissionTransfer_TransferID",
                schema: "CMS",
                table: "CommissionTransfer",
                column: "TransferID");

            migrationBuilder.CreateIndex(
                name: "IX_CommissionTransfer_UpdatedByUserID",
                schema: "CMS",
                table: "CommissionTransfer",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_RateSale_CreatedByUserID",
                schema: "CMS",
                table: "RateSale",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_RateSale_UpdatedByUserID",
                schema: "CMS",
                table: "RateSale",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_RateTransfer_CreatedByUserID",
                schema: "CMS",
                table: "RateTransfer",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_RateTransfer_UpdatedByUserID",
                schema: "CMS",
                table: "RateTransfer",
                column: "UpdatedByUserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalculateHighRiseSale",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "CalculateHighRiseTransfer",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "CalculateIncreaseDeductMoney",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "CalculateLowRiseSale",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "CalculateLowRiseTransfer",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "CalculatePerMonthHighRiseSale",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "CalculatePerMonthHighRiseTransfer",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "CalculatePerMonthLowRise",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "CommissionContract",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "CommissionTransfer",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "RateSale",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "RateTransfer",
                schema: "CMS");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                schema: "CMS",
                table: "RateSettingSale",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.CreateTable(
                name: "CalculateOther",
                schema: "CMS",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(type: "Money", nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    DeductDate = table.Column<DateTime>(nullable: true),
                    EffectiveDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsFromMigration = table.Column<bool>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false),
                    LCUserID = table.Column<Guid>(nullable: true),
                    ProjectID = table.Column<Guid>(nullable: true),
                    Remark = table.Column<string>(maxLength: 5000, nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculateOther", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CalculateOther_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalculateOther_User_LCUserID",
                        column: x => x.LCUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalculateOther_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "PRJ",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalculateOther_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CalculatePerMonth",
                schema: "CMS",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CommissionForThisMonth = table.Column<decimal>(type: "Money", nullable: false),
                    ContractValue = table.Column<decimal>(type: "Money", nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsFromMigration = table.Column<bool>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false),
                    LCAtProjectID = table.Column<Guid>(nullable: true),
                    LCC = table.Column<string>(nullable: true),
                    LCClosedDealUserID = table.Column<Guid>(nullable: true),
                    NewLaunchAtProject = table.Column<decimal>(type: "Money", nullable: false),
                    NewLaunchClosedDeal = table.Column<decimal>(type: "Money", nullable: false),
                    NewLaunchTotalCommission = table.Column<decimal>(type: "Money", nullable: false),
                    NewRate = table.Column<decimal>(nullable: false),
                    Rate = table.Column<decimal>(nullable: false),
                    SaleCommissionLCCenter = table.Column<decimal>(type: "Money", nullable: false),
                    SigningContractCommission = table.Column<decimal>(type: "Money", nullable: false),
                    TransferAtProjectCommission = table.Column<decimal>(type: "Money", nullable: false),
                    TransferClosedDealCommission = table.Column<decimal>(type: "Money", nullable: false),
                    TransferDate = table.Column<DateTime>(nullable: true),
                    TransferTotalCommission = table.Column<decimal>(type: "Money", nullable: false),
                    UnitID = table.Column<Guid>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculatePerMonth", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CalculatePerMonth_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalculatePerMonth_User_LCAtProjectID",
                        column: x => x.LCAtProjectID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalculatePerMonth_User_LCClosedDealUserID",
                        column: x => x.LCClosedDealUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalculatePerMonth_Unit_UnitID",
                        column: x => x.UnitID,
                        principalSchema: "PRJ",
                        principalTable: "Unit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalculatePerMonth_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CalculateSale",
                schema: "CMS",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    AtProjectCommission = table.Column<decimal>(type: "Money", nullable: false),
                    ClosedDealCommission = table.Column<decimal>(type: "Money", nullable: false),
                    CommissionForThisMonth = table.Column<decimal>(type: "Money", nullable: false),
                    ContractApprovedDate = table.Column<DateTime>(nullable: true),
                    ContractValue = table.Column<decimal>(type: "Money", nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsFromMigration = table.Column<bool>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false),
                    LCAtProjectID = table.Column<Guid>(nullable: true),
                    LCCenter = table.Column<string>(nullable: true),
                    LCClosedDealID = table.Column<Guid>(nullable: true),
                    Rate = table.Column<decimal>(nullable: false),
                    SaleCommissionLCCenter = table.Column<decimal>(type: "Money", nullable: false),
                    TotalCommission = table.Column<decimal>(type: "Money", nullable: false),
                    UnitID = table.Column<Guid>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculateSale", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CalculateSale_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalculateSale_User_LCAtProjectID",
                        column: x => x.LCAtProjectID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalculateSale_User_LCClosedDealID",
                        column: x => x.LCClosedDealID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalculateSale_Unit_UnitID",
                        column: x => x.UnitID,
                        principalSchema: "PRJ",
                        principalTable: "Unit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalculateSale_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CalculateTransfer",
                schema: "CMS",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ActualContractTransfer = table.Column<decimal>(type: "Money", nullable: false),
                    CommissionForThisMonth = table.Column<decimal>(type: "Money", nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsFromMigration = table.Column<bool>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false),
                    LCCTransferID = table.Column<Guid>(nullable: true),
                    LCCenterTransfer = table.Column<string>(nullable: true),
                    LCCenterTransferCommission = table.Column<decimal>(type: "Money", nullable: false),
                    Rate = table.Column<decimal>(nullable: false),
                    TransferCommission = table.Column<decimal>(type: "Money", nullable: false),
                    TransferDate = table.Column<DateTime>(nullable: true),
                    UnitID = table.Column<Guid>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculateTransfer", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CalculateTransfer_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalculateTransfer_User_LCCTransferID",
                        column: x => x.LCCTransferID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalculateTransfer_Unit_UnitID",
                        column: x => x.UnitID,
                        principalSchema: "PRJ",
                        principalTable: "Unit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalculateTransfer_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CalculateOther_CreatedByUserID",
                schema: "CMS",
                table: "CalculateOther",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculateOther_LCUserID",
                schema: "CMS",
                table: "CalculateOther",
                column: "LCUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculateOther_ProjectID",
                schema: "CMS",
                table: "CalculateOther",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculateOther_UpdatedByUserID",
                schema: "CMS",
                table: "CalculateOther",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculatePerMonth_CreatedByUserID",
                schema: "CMS",
                table: "CalculatePerMonth",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculatePerMonth_LCAtProjectID",
                schema: "CMS",
                table: "CalculatePerMonth",
                column: "LCAtProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculatePerMonth_LCClosedDealUserID",
                schema: "CMS",
                table: "CalculatePerMonth",
                column: "LCClosedDealUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculatePerMonth_UnitID",
                schema: "CMS",
                table: "CalculatePerMonth",
                column: "UnitID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculatePerMonth_UpdatedByUserID",
                schema: "CMS",
                table: "CalculatePerMonth",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculateSale_CreatedByUserID",
                schema: "CMS",
                table: "CalculateSale",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculateSale_LCAtProjectID",
                schema: "CMS",
                table: "CalculateSale",
                column: "LCAtProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculateSale_LCClosedDealID",
                schema: "CMS",
                table: "CalculateSale",
                column: "LCClosedDealID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculateSale_UnitID",
                schema: "CMS",
                table: "CalculateSale",
                column: "UnitID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculateSale_UpdatedByUserID",
                schema: "CMS",
                table: "CalculateSale",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculateTransfer_CreatedByUserID",
                schema: "CMS",
                table: "CalculateTransfer",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculateTransfer_LCCTransferID",
                schema: "CMS",
                table: "CalculateTransfer",
                column: "LCCTransferID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculateTransfer_UnitID",
                schema: "CMS",
                table: "CalculateTransfer",
                column: "UnitID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculateTransfer_UpdatedByUserID",
                schema: "CMS",
                table: "CalculateTransfer",
                column: "UpdatedByUserID");
        }
    }
}
