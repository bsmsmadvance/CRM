using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class CreateCancelReasonTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CancelReason",
                schema: "MST",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Key = table.Column<string>(maxLength: 100, nullable: true),
                    Description = table.Column<string>(maxLength: 5000, nullable: true),
                    GroupOfCancelReasonMasterCenterID = table.Column<Guid>(nullable: true),
                    CancelApproveFlowMasterCenterID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CancelReason", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CancelReason_MasterCenter_CancelApproveFlowMasterCenterID",
                        column: x => x.CancelApproveFlowMasterCenterID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CancelReason_MasterCenter_GroupOfCancelReasonMasterCenterID",
                        column: x => x.GroupOfCancelReasonMasterCenterID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CancelReturnSetting",
                schema: "MST",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ChiefReturnLessThanPercent = table.Column<double>(nullable: false),
                    HandlingFee = table.Column<decimal>(type: "Money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CancelReturnSetting", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CancelReason_CancelApproveFlowMasterCenterID",
                schema: "MST",
                table: "CancelReason",
                column: "CancelApproveFlowMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_CancelReason_GroupOfCancelReasonMasterCenterID",
                schema: "MST",
                table: "CancelReason",
                column: "GroupOfCancelReasonMasterCenterID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CancelReason",
                schema: "MST");

            migrationBuilder.DropTable(
                name: "CancelReturnSetting",
                schema: "MST");
        }
    }
}
