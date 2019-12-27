using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class CreatePRRequestJobTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PRRequestJob",
                schema: "PRM",
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
                    FileName = table.Column<string>(maxLength: 1000, nullable: true),
                    SAPResultFileName = table.Column<string>(maxLength: 1000, nullable: true),
                    Status = table.Column<int>(nullable: false),
                    ErrorMessage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRRequestJob", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PRRequestJob_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PRRequestJob_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PRRequestJobItem",
                schema: "PRM",
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
                    PRRequestJobStatusMasterCenterID = table.Column<Guid>(nullable: true),
                    PreSalePromotionRequestItemID = table.Column<Guid>(nullable: true),
                    PRRequestJobID = table.Column<Guid>(nullable: true),
                    Retry = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(maxLength: 100, nullable: true),
                    PromotionNo = table.Column<string>(maxLength: 100, nullable: true),
                    DocType = table.Column<string>(maxLength: 100, nullable: true),
                    PurchasingGroup = table.Column<string>(maxLength: 100, nullable: true),
                    PurchasingOrg = table.Column<string>(maxLength: 100, nullable: true),
                    Requester = table.Column<string>(maxLength: 100, nullable: true),
                    Plant = table.Column<string>(maxLength: 100, nullable: true),
                    AccountAssignmentCategory = table.Column<string>(maxLength: 100, nullable: true),
                    MaterialNo = table.Column<string>(maxLength: 100, nullable: true),
                    NameTH = table.Column<string>(maxLength: 1000, nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    TotalPrice = table.Column<decimal>(type: "Money", nullable: false),
                    PriceUnit = table.Column<string>(maxLength: 100, nullable: true),
                    AgreementNo = table.Column<string>(maxLength: 100, nullable: true),
                    ItemNo = table.Column<string>(maxLength: 100, nullable: true),
                    GoodReceiptIndicator = table.Column<string>(maxLength: 100, nullable: true),
                    InvoiceReceiptIndicator = table.Column<string>(maxLength: 100, nullable: true),
                    CreatedByDisplayName = table.Column<string>(maxLength: 100, nullable: true),
                    SerialNo = table.Column<string>(maxLength: 100, nullable: true),
                    GoodRecipient = table.Column<string>(maxLength: 100, nullable: true),
                    GLAccountNo = table.Column<string>(maxLength: 100, nullable: true),
                    SAPWBSObject_P = table.Column<string>(maxLength: 100, nullable: true),
                    PromotionName = table.Column<string>(maxLength: 100, nullable: true),
                    TextB01 = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRRequestJobItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PRRequestJobItem_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PRRequestJobItem_PRRequestJob_PRRequestJobID",
                        column: x => x.PRRequestJobID,
                        principalSchema: "PRM",
                        principalTable: "PRRequestJob",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PRRequestJobItem_MasterCenter_PRRequestJobStatusMasterCenterID",
                        column: x => x.PRRequestJobStatusMasterCenterID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PRRequestJobItem_PreSalePromotionRequestItem_PreSalePromotionRequestItemID",
                        column: x => x.PreSalePromotionRequestItemID,
                        principalSchema: "PRM",
                        principalTable: "PreSalePromotionRequestItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PRRequestJobItem_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PRRequestJobItemResult",
                schema: "PRM",
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
                    PRRequestJobItemID = table.Column<Guid>(nullable: true),
                    IsError = table.Column<bool>(nullable: false),
                    ErrorCode = table.Column<string>(maxLength: 10, nullable: true),
                    ErrorDescription = table.Column<string>(maxLength: 100, nullable: true),
                    IsFMCreatePR = table.Column<bool>(nullable: false),
                    PRNo = table.Column<string>(maxLength: 100, nullable: true),
                    ItemNo = table.Column<string>(maxLength: 100, nullable: true),
                    MaterialNo = table.Column<string>(maxLength: 100, nullable: true),
                    SAPCreateBy = table.Column<string>(maxLength: 100, nullable: true),
                    SAPCreateDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRRequestJobItemResult", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PRRequestJobItemResult_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PRRequestJobItemResult_PRRequestJobItem_PRRequestJobItemID",
                        column: x => x.PRRequestJobItemID,
                        principalSchema: "PRM",
                        principalTable: "PRRequestJobItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PRRequestJobItemResult_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PRRequestJob_CreatedByUserID",
                schema: "PRM",
                table: "PRRequestJob",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PRRequestJob_UpdatedByUserID",
                schema: "PRM",
                table: "PRRequestJob",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PRRequestJobItem_CreatedByUserID",
                schema: "PRM",
                table: "PRRequestJobItem",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PRRequestJobItem_PRRequestJobID",
                schema: "PRM",
                table: "PRRequestJobItem",
                column: "PRRequestJobID");

            migrationBuilder.CreateIndex(
                name: "IX_PRRequestJobItem_PRRequestJobStatusMasterCenterID",
                schema: "PRM",
                table: "PRRequestJobItem",
                column: "PRRequestJobStatusMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_PRRequestJobItem_PreSalePromotionRequestItemID",
                schema: "PRM",
                table: "PRRequestJobItem",
                column: "PreSalePromotionRequestItemID");

            migrationBuilder.CreateIndex(
                name: "IX_PRRequestJobItem_UpdatedByUserID",
                schema: "PRM",
                table: "PRRequestJobItem",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PRRequestJobItemResult_CreatedByUserID",
                schema: "PRM",
                table: "PRRequestJobItemResult",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PRRequestJobItemResult_PRRequestJobItemID",
                schema: "PRM",
                table: "PRRequestJobItemResult",
                column: "PRRequestJobItemID");

            migrationBuilder.CreateIndex(
                name: "IX_PRRequestJobItemResult_UpdatedByUserID",
                schema: "PRM",
                table: "PRRequestJobItemResult",
                column: "UpdatedByUserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PRRequestJobItemResult",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "PRRequestJobItem",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "PRRequestJob",
                schema: "PRM");
        }
    }
}
