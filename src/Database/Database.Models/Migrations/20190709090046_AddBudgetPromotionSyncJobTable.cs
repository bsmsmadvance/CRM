using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddBudgetPromotionSyncJobTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BudgetPromotionSyncJob",
                schema: "PRJ",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    FileName = table.Column<string>(maxLength: 1000, nullable: true),
                    SAPResultFileName = table.Column<string>(maxLength: 1000, nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetPromotionSyncJob", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BudgetPromotionSyncItem",
                schema: "PRJ",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    SaleBudgetPromotionID = table.Column<Guid>(nullable: true),
                    TransferBudgetPromotionID = table.Column<Guid>(nullable: true),
                    BudgetPromotionSyncStatusMasterCenterID = table.Column<Guid>(nullable: true),
                    BudgetPromotionSyncJobID = table.Column<Guid>(nullable: true),
                    UserName = table.Column<string>(maxLength: 100, nullable: true),
                    SAPWBSObject_P = table.Column<string>(maxLength: 100, nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    Currency = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetPromotionSyncItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BudgetPromotionSyncItem_BudgetPromotionSyncJob_BudgetPromotionSyncJobID",
                        column: x => x.BudgetPromotionSyncJobID,
                        principalSchema: "PRJ",
                        principalTable: "BudgetPromotionSyncJob",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BudgetPromotionSyncItem_MasterCenter_BudgetPromotionSyncStatusMasterCenterID",
                        column: x => x.BudgetPromotionSyncStatusMasterCenterID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BudgetPromotionSyncItem_BudgetPromotion_SaleBudgetPromotionID",
                        column: x => x.SaleBudgetPromotionID,
                        principalSchema: "PRJ",
                        principalTable: "BudgetPromotion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BudgetPromotionSyncItem_BudgetPromotion_TransferBudgetPromotionID",
                        column: x => x.TransferBudgetPromotionID,
                        principalSchema: "PRJ",
                        principalTable: "BudgetPromotion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BudgetPromotionSyncItemResult",
                schema: "PRJ",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    BudgetPromotionSyncItemID = table.Column<Guid>(nullable: true),
                    IsError = table.Column<bool>(nullable: false),
                    ErrorCode = table.Column<string>(maxLength: 10, nullable: true),
                    ErrorDescription = table.Column<string>(maxLength: 100, nullable: true),
                    IsFMUpdateBudget = table.Column<bool>(nullable: false),
                    SAPWBSObject_P = table.Column<string>(maxLength: 100, nullable: true),
                    LastUpdateBudgetFromSAP = table.Column<string>(maxLength: 20, nullable: true),
                    UserSAP = table.Column<string>(maxLength: 40, nullable: true),
                    SAPCreateDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetPromotionSyncItemResult", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BudgetPromotionSyncItemResult_BudgetPromotionSyncItem_BudgetPromotionSyncItemID",
                        column: x => x.BudgetPromotionSyncItemID,
                        principalSchema: "PRJ",
                        principalTable: "BudgetPromotionSyncItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BudgetPromotionSyncItem_BudgetPromotionSyncJobID",
                schema: "PRJ",
                table: "BudgetPromotionSyncItem",
                column: "BudgetPromotionSyncJobID");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetPromotionSyncItem_BudgetPromotionSyncStatusMasterCenterID",
                schema: "PRJ",
                table: "BudgetPromotionSyncItem",
                column: "BudgetPromotionSyncStatusMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetPromotionSyncItem_SaleBudgetPromotionID",
                schema: "PRJ",
                table: "BudgetPromotionSyncItem",
                column: "SaleBudgetPromotionID");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetPromotionSyncItem_TransferBudgetPromotionID",
                schema: "PRJ",
                table: "BudgetPromotionSyncItem",
                column: "TransferBudgetPromotionID");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetPromotionSyncItemResult_BudgetPromotionSyncItemID",
                schema: "PRJ",
                table: "BudgetPromotionSyncItemResult",
                column: "BudgetPromotionSyncItemID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BudgetPromotionSyncItemResult",
                schema: "PRJ");

            migrationBuilder.DropTable(
                name: "BudgetPromotionSyncItem",
                schema: "PRJ");

            migrationBuilder.DropTable(
                name: "BudgetPromotionSyncJob",
                schema: "PRJ");
        }
    }
}
