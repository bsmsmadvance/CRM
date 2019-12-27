using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class CreatePreSalePromotionRequestTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PreSalePromotionItem_MasterPreSalePromotionUnitItem_MasterPreSalePromotionUnitItemID",
                schema: "PRM",
                table: "PreSalePromotionItem");

            migrationBuilder.DropTable(
                name: "MasterPreSalePromotionUnitItem",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "MasterPreSalePromotionUnit",
                schema: "PRM");

            migrationBuilder.RenameColumn(
                name: "ReceiveNo",
                schema: "PRM",
                table: "TransferPromotionRequest",
                newName: "RequestNo");

            migrationBuilder.RenameColumn(
                name: "ReceiveDate",
                schema: "PRM",
                table: "TransferPromotionRequest",
                newName: "RequestDate");

            migrationBuilder.RenameColumn(
                name: "MasterPreSalePromotionUnitItemID",
                schema: "PRM",
                table: "PreSalePromotionItem",
                newName: "PreSalePromotionRequestItemID");

            migrationBuilder.RenameIndex(
                name: "IX_PreSalePromotionItem_MasterPreSalePromotionUnitItemID",
                schema: "PRM",
                table: "PreSalePromotionItem",
                newName: "IX_PreSalePromotionItem_PreSalePromotionRequestItemID");

            migrationBuilder.RenameColumn(
                name: "ReceiveNo",
                schema: "PRM",
                table: "BookingPromotionRequest",
                newName: "RequestNo");

            migrationBuilder.RenameColumn(
                name: "ReceiveDate",
                schema: "PRM",
                table: "BookingPromotionRequest",
                newName: "RequestDate");

            migrationBuilder.CreateTable(
                name: "PreSalePromotionRequest",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    RequestNo = table.Column<string>(maxLength: 100, nullable: true),
                    RequestDate = table.Column<DateTime>(nullable: true),
                    PRCompletedDate = table.Column<DateTime>(nullable: true),
                    PromotionRequestPRStatusMasterCenterID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreSalePromotionRequest", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PreSalePromotionRequest_MasterCenter_PromotionRequestPRStatusMasterCenterID",
                        column: x => x.PromotionRequestPRStatusMasterCenterID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PreSalePromotionRequestUnit",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MasterPreSalePromotionID = table.Column<Guid>(nullable: false),
                    PreSalePromotionRequestID = table.Column<Guid>(nullable: false),
                    UnitID = table.Column<Guid>(nullable: false),
                    SAPPRStatusMasterCenterID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreSalePromotionRequestUnit", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PreSalePromotionRequestUnit_MasterPreSalePromotion_MasterPreSalePromotionID",
                        column: x => x.MasterPreSalePromotionID,
                        principalSchema: "PRM",
                        principalTable: "MasterPreSalePromotion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PreSalePromotionRequestUnit_PreSalePromotionRequest_PreSalePromotionRequestID",
                        column: x => x.PreSalePromotionRequestID,
                        principalSchema: "PRM",
                        principalTable: "PreSalePromotionRequest",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PreSalePromotionRequestUnit_MasterCenter_SAPPRStatusMasterCenterID",
                        column: x => x.SAPPRStatusMasterCenterID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PreSalePromotionRequestUnit_Unit_UnitID",
                        column: x => x.UnitID,
                        principalSchema: "PRJ",
                        principalTable: "Unit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PreSalePromotionRequestItem",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MasterPreSalePromotionItemID = table.Column<Guid>(nullable: true),
                    PreSalePromotionRequestUnitID = table.Column<Guid>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    UnitTH = table.Column<string>(maxLength: 100, nullable: true),
                    UnitEN = table.Column<string>(maxLength: 100, nullable: true),
                    PricePerUnit = table.Column<decimal>(type: "Money", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "Money", nullable: false),
                    Remark = table.Column<string>(maxLength: 5000, nullable: true),
                    ReceiveDate = table.Column<DateTime>(nullable: true),
                    PRNo = table.Column<string>(maxLength: 100, nullable: true),
                    PreSalePromotionRequestUnitID1 = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreSalePromotionRequestItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PreSalePromotionRequestItem_MasterPreSalePromotionItem_MasterPreSalePromotionItemID",
                        column: x => x.MasterPreSalePromotionItemID,
                        principalSchema: "PRM",
                        principalTable: "MasterPreSalePromotionItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PreSalePromotionRequestItem_MasterCenter_PreSalePromotionRequestUnitID",
                        column: x => x.PreSalePromotionRequestUnitID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PreSalePromotionRequestItem_PreSalePromotionRequestUnit_PreSalePromotionRequestUnitID1",
                        column: x => x.PreSalePromotionRequestUnitID1,
                        principalSchema: "PRM",
                        principalTable: "PreSalePromotionRequestUnit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PreSalePromotionRequest_PromotionRequestPRStatusMasterCenterID",
                schema: "PRM",
                table: "PreSalePromotionRequest",
                column: "PromotionRequestPRStatusMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_PreSalePromotionRequestItem_MasterPreSalePromotionItemID",
                schema: "PRM",
                table: "PreSalePromotionRequestItem",
                column: "MasterPreSalePromotionItemID");

            migrationBuilder.CreateIndex(
                name: "IX_PreSalePromotionRequestItem_PreSalePromotionRequestUnitID",
                schema: "PRM",
                table: "PreSalePromotionRequestItem",
                column: "PreSalePromotionRequestUnitID");

            migrationBuilder.CreateIndex(
                name: "IX_PreSalePromotionRequestItem_PreSalePromotionRequestUnitID1",
                schema: "PRM",
                table: "PreSalePromotionRequestItem",
                column: "PreSalePromotionRequestUnitID1");

            migrationBuilder.CreateIndex(
                name: "IX_PreSalePromotionRequestUnit_MasterPreSalePromotionID",
                schema: "PRM",
                table: "PreSalePromotionRequestUnit",
                column: "MasterPreSalePromotionID");

            migrationBuilder.CreateIndex(
                name: "IX_PreSalePromotionRequestUnit_PreSalePromotionRequestID",
                schema: "PRM",
                table: "PreSalePromotionRequestUnit",
                column: "PreSalePromotionRequestID");

            migrationBuilder.CreateIndex(
                name: "IX_PreSalePromotionRequestUnit_SAPPRStatusMasterCenterID",
                schema: "PRM",
                table: "PreSalePromotionRequestUnit",
                column: "SAPPRStatusMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_PreSalePromotionRequestUnit_UnitID",
                schema: "PRM",
                table: "PreSalePromotionRequestUnit",
                column: "UnitID");

            migrationBuilder.AddForeignKey(
                name: "FK_PreSalePromotionItem_PreSalePromotionRequestItem_PreSalePromotionRequestItemID",
                schema: "PRM",
                table: "PreSalePromotionItem",
                column: "PreSalePromotionRequestItemID",
                principalSchema: "PRM",
                principalTable: "PreSalePromotionRequestItem",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PreSalePromotionItem_PreSalePromotionRequestItem_PreSalePromotionRequestItemID",
                schema: "PRM",
                table: "PreSalePromotionItem");

            migrationBuilder.DropTable(
                name: "PreSalePromotionRequestItem",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "PreSalePromotionRequestUnit",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "PreSalePromotionRequest",
                schema: "PRM");

            migrationBuilder.RenameColumn(
                name: "RequestNo",
                schema: "PRM",
                table: "TransferPromotionRequest",
                newName: "ReceiveNo");

            migrationBuilder.RenameColumn(
                name: "RequestDate",
                schema: "PRM",
                table: "TransferPromotionRequest",
                newName: "ReceiveDate");

            migrationBuilder.RenameColumn(
                name: "PreSalePromotionRequestItemID",
                schema: "PRM",
                table: "PreSalePromotionItem",
                newName: "MasterPreSalePromotionUnitItemID");

            migrationBuilder.RenameIndex(
                name: "IX_PreSalePromotionItem_PreSalePromotionRequestItemID",
                schema: "PRM",
                table: "PreSalePromotionItem",
                newName: "IX_PreSalePromotionItem_MasterPreSalePromotionUnitItemID");

            migrationBuilder.RenameColumn(
                name: "RequestNo",
                schema: "PRM",
                table: "BookingPromotionRequest",
                newName: "ReceiveNo");

            migrationBuilder.RenameColumn(
                name: "RequestDate",
                schema: "PRM",
                table: "BookingPromotionRequest",
                newName: "ReceiveDate");

            migrationBuilder.CreateTable(
                name: "MasterPreSalePromotionUnit",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MasterPreSalePromotionID = table.Column<Guid>(nullable: false),
                    Remark = table.Column<string>(maxLength: 5000, nullable: true),
                    UnitID = table.Column<Guid>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterPreSalePromotionUnit", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MasterPreSalePromotionUnit_MasterPreSalePromotion_MasterPreSalePromotionID",
                        column: x => x.MasterPreSalePromotionID,
                        principalSchema: "PRM",
                        principalTable: "MasterPreSalePromotion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MasterPreSalePromotionUnit_Unit_UnitID",
                        column: x => x.UnitID,
                        principalSchema: "PRJ",
                        principalTable: "Unit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MasterPreSalePromotionUnitItem",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CancelTime = table.Column<DateTime>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsCanceled = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MasterPreSalePromotionItemID = table.Column<Guid>(nullable: true),
                    MasterPreSalePromotionUnitID = table.Column<Guid>(nullable: true),
                    PRNo = table.Column<string>(maxLength: 100, nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterPreSalePromotionUnitItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MasterPreSalePromotionUnitItem_MasterPreSalePromotionItem_MasterPreSalePromotionItemID",
                        column: x => x.MasterPreSalePromotionItemID,
                        principalSchema: "PRM",
                        principalTable: "MasterPreSalePromotionItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MasterPreSalePromotionUnitItem_MasterPreSalePromotionUnit_MasterPreSalePromotionUnitID",
                        column: x => x.MasterPreSalePromotionUnitID,
                        principalSchema: "PRM",
                        principalTable: "MasterPreSalePromotionUnit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MasterPreSalePromotionUnit_MasterPreSalePromotionID",
                schema: "PRM",
                table: "MasterPreSalePromotionUnit",
                column: "MasterPreSalePromotionID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterPreSalePromotionUnit_UnitID",
                schema: "PRM",
                table: "MasterPreSalePromotionUnit",
                column: "UnitID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterPreSalePromotionUnitItem_MasterPreSalePromotionItemID",
                schema: "PRM",
                table: "MasterPreSalePromotionUnitItem",
                column: "MasterPreSalePromotionItemID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterPreSalePromotionUnitItem_MasterPreSalePromotionUnitID",
                schema: "PRM",
                table: "MasterPreSalePromotionUnitItem",
                column: "MasterPreSalePromotionUnitID");

            migrationBuilder.AddForeignKey(
                name: "FK_PreSalePromotionItem_MasterPreSalePromotionUnitItem_MasterPreSalePromotionUnitItemID",
                schema: "PRM",
                table: "PreSalePromotionItem",
                column: "MasterPreSalePromotionUnitItemID",
                principalSchema: "PRM",
                principalTable: "MasterPreSalePromotionUnitItem",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
