using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class CreatePRCancelJobTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PRCancelJob",
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
                    table.PrimaryKey("PK_PRCancelJob", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PRCancelJob_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PRCancelJob_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PRCancelJobItem",
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
                    PRCancelJobStatusMasterCenterID = table.Column<Guid>(nullable: true),
                    PreSalePromotionRequestItemID = table.Column<Guid>(nullable: true),
                    PRCancelJobID = table.Column<Guid>(nullable: true),
                    Retry = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(maxLength: 100, nullable: true),
                    PRNo = table.Column<string>(maxLength: 100, nullable: true),
                    ItemNo = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRCancelJobItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PRCancelJobItem_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PRCancelJobItem_PRCancelJob_PRCancelJobID",
                        column: x => x.PRCancelJobID,
                        principalSchema: "PRM",
                        principalTable: "PRCancelJob",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PRCancelJobItem_MasterCenter_PRCancelJobStatusMasterCenterID",
                        column: x => x.PRCancelJobStatusMasterCenterID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PRCancelJobItem_PreSalePromotionRequestItem_PreSalePromotionRequestItemID",
                        column: x => x.PreSalePromotionRequestItemID,
                        principalSchema: "PRM",
                        principalTable: "PreSalePromotionRequestItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PRCancelJobItem_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PRCancelJobItemResult",
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
                    PRCancelJobItemID = table.Column<Guid>(nullable: true),
                    IsError = table.Column<bool>(nullable: false),
                    ErrorCode = table.Column<string>(maxLength: 10, nullable: true),
                    ErrorDescription = table.Column<string>(maxLength: 100, nullable: true),
                    IsFMCreatePR = table.Column<bool>(nullable: false),
                    PRNo = table.Column<string>(maxLength: 100, nullable: true),
                    ItemNo = table.Column<string>(maxLength: 100, nullable: true),
                    SAPDeleteFlag = table.Column<bool>(nullable: false),
                    SAPCreateBy = table.Column<string>(maxLength: 100, nullable: true),
                    SAPCreateDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRCancelJobItemResult", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PRCancelJobItemResult_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PRCancelJobItemResult_PRCancelJobItem_PRCancelJobItemID",
                        column: x => x.PRCancelJobItemID,
                        principalSchema: "PRM",
                        principalTable: "PRCancelJobItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PRCancelJobItemResult_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PRCancelJob_CreatedByUserID",
                schema: "PRM",
                table: "PRCancelJob",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PRCancelJob_UpdatedByUserID",
                schema: "PRM",
                table: "PRCancelJob",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PRCancelJobItem_CreatedByUserID",
                schema: "PRM",
                table: "PRCancelJobItem",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PRCancelJobItem_PRCancelJobID",
                schema: "PRM",
                table: "PRCancelJobItem",
                column: "PRCancelJobID");

            migrationBuilder.CreateIndex(
                name: "IX_PRCancelJobItem_PRCancelJobStatusMasterCenterID",
                schema: "PRM",
                table: "PRCancelJobItem",
                column: "PRCancelJobStatusMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_PRCancelJobItem_PreSalePromotionRequestItemID",
                schema: "PRM",
                table: "PRCancelJobItem",
                column: "PreSalePromotionRequestItemID");

            migrationBuilder.CreateIndex(
                name: "IX_PRCancelJobItem_UpdatedByUserID",
                schema: "PRM",
                table: "PRCancelJobItem",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PRCancelJobItemResult_CreatedByUserID",
                schema: "PRM",
                table: "PRCancelJobItemResult",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PRCancelJobItemResult_PRCancelJobItemID",
                schema: "PRM",
                table: "PRCancelJobItemResult",
                column: "PRCancelJobItemID");

            migrationBuilder.CreateIndex(
                name: "IX_PRCancelJobItemResult_UpdatedByUserID",
                schema: "PRM",
                table: "PRCancelJobItemResult",
                column: "UpdatedByUserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PRCancelJobItemResult",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "PRCancelJobItem",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "PRCancelJob",
                schema: "PRM");
        }
    }
}
