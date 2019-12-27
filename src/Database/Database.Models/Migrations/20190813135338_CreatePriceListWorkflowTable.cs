using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class CreatePriceListWorkflowTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PriceListWorkflow",
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
                    ProjectID = table.Column<Guid>(nullable: true),
                    UnitID = table.Column<Guid>(nullable: true),
                    QuotationID = table.Column<Guid>(nullable: true),
                    BookingID = table.Column<Guid>(nullable: true),
                    UnitStatusMasterCenterID = table.Column<Guid>(nullable: true),
                    PriceListWorkflowStageMasterCenterID = table.Column<Guid>(nullable: true),
                    MasterSellingPrice = table.Column<decimal>(type: "Money", nullable: true),
                    MasterBookingAmount = table.Column<decimal>(type: "Money", nullable: true),
                    MasterContractAmount = table.Column<decimal>(type: "Money", nullable: true),
                    MasterInstallment = table.Column<decimal>(type: "Money", nullable: true),
                    MasterNormalInstallment = table.Column<decimal>(type: "Money", nullable: true),
                    MasterInstallmentAmount = table.Column<decimal>(type: "Money", nullable: true),
                    MasterSpecialInstallments = table.Column<string>(maxLength: 1000, nullable: true),
                    MasterSpecialInstallmentAmounts = table.Column<string>(maxLength: 1000, nullable: true),
                    SellingPrice = table.Column<decimal>(type: "Money", nullable: true),
                    BookingAmount = table.Column<decimal>(type: "Money", nullable: true),
                    ContractAmount = table.Column<decimal>(type: "Money", nullable: true),
                    Installment = table.Column<decimal>(type: "Money", nullable: true),
                    NormalInstallment = table.Column<decimal>(type: "Money", nullable: true),
                    InstallmentAmount = table.Column<decimal>(type: "Money", nullable: true),
                    SpecialInstallments = table.Column<string>(maxLength: 1000, nullable: true),
                    SpecialInstallmentAmounts = table.Column<string>(maxLength: 1000, nullable: true),
                    IsApproved = table.Column<bool>(nullable: true),
                    ApprovedByUserID = table.Column<Guid>(nullable: true),
                    RejectComment = table.Column<string>(maxLength: 5000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceListWorkflow", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PriceListWorkflow_User_ApprovedByUserID",
                        column: x => x.ApprovedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PriceListWorkflow_Booking_BookingID",
                        column: x => x.BookingID,
                        principalSchema: "SAL",
                        principalTable: "Booking",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PriceListWorkflow_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PriceListWorkflow_MasterCenter_PriceListWorkflowStageMasterCenterID",
                        column: x => x.PriceListWorkflowStageMasterCenterID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PriceListWorkflow_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "PRJ",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PriceListWorkflow_Quotation_QuotationID",
                        column: x => x.QuotationID,
                        principalSchema: "SAL",
                        principalTable: "Quotation",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PriceListWorkflow_Unit_UnitID",
                        column: x => x.UnitID,
                        principalSchema: "PRJ",
                        principalTable: "Unit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PriceListWorkflow_MasterCenter_UnitStatusMasterCenterID",
                        column: x => x.UnitStatusMasterCenterID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PriceListWorkflow_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PriceListWorkflow_ApprovedByUserID",
                schema: "SAL",
                table: "PriceListWorkflow",
                column: "ApprovedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PriceListWorkflow_BookingID",
                schema: "SAL",
                table: "PriceListWorkflow",
                column: "BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_PriceListWorkflow_CreatedByUserID",
                schema: "SAL",
                table: "PriceListWorkflow",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PriceListWorkflow_PriceListWorkflowStageMasterCenterID",
                schema: "SAL",
                table: "PriceListWorkflow",
                column: "PriceListWorkflowStageMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_PriceListWorkflow_ProjectID",
                schema: "SAL",
                table: "PriceListWorkflow",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_PriceListWorkflow_QuotationID",
                schema: "SAL",
                table: "PriceListWorkflow",
                column: "QuotationID");

            migrationBuilder.CreateIndex(
                name: "IX_PriceListWorkflow_UnitID",
                schema: "SAL",
                table: "PriceListWorkflow",
                column: "UnitID");

            migrationBuilder.CreateIndex(
                name: "IX_PriceListWorkflow_UnitStatusMasterCenterID",
                schema: "SAL",
                table: "PriceListWorkflow",
                column: "UnitStatusMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_PriceListWorkflow_UpdatedByUserID",
                schema: "SAL",
                table: "PriceListWorkflow",
                column: "UpdatedByUserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriceListWorkflow",
                schema: "SAL");
        }
    }
}
