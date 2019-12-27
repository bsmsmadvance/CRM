using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ChangePromotionReceiveTableNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingPromotionReceiveItem",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "TransferPromotionReceiveItem",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "BookingPromotionReceive",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "TransferPromotionReceive",
                schema: "PRM");

            migrationBuilder.CreateTable(
                name: "BookingPromotionRequest",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    BookingPromotionID = table.Column<Guid>(nullable: true),
                    ReceiveNo = table.Column<string>(maxLength: 100, nullable: true),
                    ReceiveDate = table.Column<DateTime>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingPromotionRequest", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BookingPromotionRequest_BookingPromotion_BookingPromotionID",
                        column: x => x.BookingPromotionID,
                        principalSchema: "PRM",
                        principalTable: "BookingPromotion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransferPromotionRequest",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    TransferPromotionID = table.Column<Guid>(nullable: true),
                    ReceiveNo = table.Column<string>(maxLength: 100, nullable: true),
                    ReceiveDate = table.Column<DateTime>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferPromotionRequest", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TransferPromotionRequest_TransferPromotion_TransferPromotionID",
                        column: x => x.TransferPromotionID,
                        principalSchema: "PRM",
                        principalTable: "TransferPromotion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookingPromotionRequestItem",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    BookingPromotionRequestID = table.Column<Guid>(nullable: true),
                    BookingPromotionItemID = table.Column<Guid>(nullable: true),
                    ReceiveQuantity = table.Column<int>(nullable: false),
                    RemainingReceiveQuantity = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    EstimateReceiveDate = table.Column<DateTime>(nullable: true),
                    PRNo = table.Column<string>(maxLength: 100, nullable: true),
                    DenyRemark = table.Column<string>(maxLength: 5000, nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingPromotionRequestItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BookingPromotionRequestItem_BookingPromotionItem_BookingPromotionItemID",
                        column: x => x.BookingPromotionItemID,
                        principalSchema: "PRM",
                        principalTable: "BookingPromotionItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingPromotionRequestItem_BookingPromotionRequest_BookingPromotionRequestID",
                        column: x => x.BookingPromotionRequestID,
                        principalSchema: "PRM",
                        principalTable: "BookingPromotionRequest",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransferPromotionRequestItem",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    TransferPromotionRequestID = table.Column<Guid>(nullable: true),
                    TransferPromotionItemID = table.Column<Guid>(nullable: true),
                    RequestQuantity = table.Column<int>(nullable: false),
                    RemainingRequestQuantity = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    EstimateRequestDate = table.Column<DateTime>(nullable: true),
                    PRNo = table.Column<string>(maxLength: 100, nullable: true),
                    DenyRemark = table.Column<string>(maxLength: 5000, nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferPromotionRequestItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TransferPromotionRequestItem_TransferPromotionItem_TransferPromotionItemID",
                        column: x => x.TransferPromotionItemID,
                        principalSchema: "PRM",
                        principalTable: "TransferPromotionItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferPromotionRequestItem_TransferPromotionRequest_TransferPromotionRequestID",
                        column: x => x.TransferPromotionRequestID,
                        principalSchema: "PRM",
                        principalTable: "TransferPromotionRequest",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingPromotionRequest_BookingPromotionID",
                schema: "PRM",
                table: "BookingPromotionRequest",
                column: "BookingPromotionID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingPromotionRequestItem_BookingPromotionItemID",
                schema: "PRM",
                table: "BookingPromotionRequestItem",
                column: "BookingPromotionItemID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingPromotionRequestItem_BookingPromotionRequestID",
                schema: "PRM",
                table: "BookingPromotionRequestItem",
                column: "BookingPromotionRequestID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferPromotionRequest_TransferPromotionID",
                schema: "PRM",
                table: "TransferPromotionRequest",
                column: "TransferPromotionID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferPromotionRequestItem_TransferPromotionItemID",
                schema: "PRM",
                table: "TransferPromotionRequestItem",
                column: "TransferPromotionItemID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferPromotionRequestItem_TransferPromotionRequestID",
                schema: "PRM",
                table: "TransferPromotionRequestItem",
                column: "TransferPromotionRequestID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingPromotionRequestItem",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "TransferPromotionRequestItem",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "BookingPromotionRequest",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "TransferPromotionRequest",
                schema: "PRM");

            migrationBuilder.CreateTable(
                name: "BookingPromotionReceive",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    BookingPromotionID = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ReceiveDate = table.Column<DateTime>(nullable: true),
                    ReceiveNo = table.Column<string>(maxLength: 100, nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingPromotionReceive", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BookingPromotionReceive_BookingPromotion_BookingPromotionID",
                        column: x => x.BookingPromotionID,
                        principalSchema: "PRM",
                        principalTable: "BookingPromotion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransferPromotionReceive",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ReceiveDate = table.Column<DateTime>(nullable: true),
                    ReceiveNo = table.Column<string>(maxLength: 100, nullable: true),
                    TransferPromotionID = table.Column<Guid>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferPromotionReceive", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TransferPromotionReceive_TransferPromotion_TransferPromotionID",
                        column: x => x.TransferPromotionID,
                        principalSchema: "PRM",
                        principalTable: "TransferPromotion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookingPromotionReceiveItem",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    BookingPromotionItemID = table.Column<Guid>(nullable: true),
                    BookingPromotionReceiveID = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    DenyRemark = table.Column<string>(maxLength: 5000, nullable: true),
                    EstimateReceiveDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PRNo = table.Column<string>(maxLength: 100, nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    ReceiveQuantity = table.Column<int>(nullable: false),
                    RemainingReceiveQuantity = table.Column<int>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingPromotionReceiveItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BookingPromotionReceiveItem_BookingPromotionItem_BookingPromotionItemID",
                        column: x => x.BookingPromotionItemID,
                        principalSchema: "PRM",
                        principalTable: "BookingPromotionItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingPromotionReceiveItem_BookingPromotionReceive_BookingPromotionReceiveID",
                        column: x => x.BookingPromotionReceiveID,
                        principalSchema: "PRM",
                        principalTable: "BookingPromotionReceive",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransferPromotionReceiveItem",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    DenyRemark = table.Column<string>(maxLength: 5000, nullable: true),
                    EstimateReceiveDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PRNo = table.Column<string>(maxLength: 100, nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    ReceiveQuantity = table.Column<int>(nullable: false),
                    RemainingReceiveQuantity = table.Column<int>(nullable: false),
                    TransferPromotionItemID = table.Column<Guid>(nullable: true),
                    TransferPromotionReceiveID = table.Column<Guid>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferPromotionReceiveItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TransferPromotionReceiveItem_TransferPromotionItem_TransferPromotionItemID",
                        column: x => x.TransferPromotionItemID,
                        principalSchema: "PRM",
                        principalTable: "TransferPromotionItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferPromotionReceiveItem_TransferPromotionReceive_TransferPromotionReceiveID",
                        column: x => x.TransferPromotionReceiveID,
                        principalSchema: "PRM",
                        principalTable: "TransferPromotionReceive",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingPromotionReceive_BookingPromotionID",
                schema: "PRM",
                table: "BookingPromotionReceive",
                column: "BookingPromotionID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingPromotionReceiveItem_BookingPromotionItemID",
                schema: "PRM",
                table: "BookingPromotionReceiveItem",
                column: "BookingPromotionItemID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingPromotionReceiveItem_BookingPromotionReceiveID",
                schema: "PRM",
                table: "BookingPromotionReceiveItem",
                column: "BookingPromotionReceiveID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferPromotionReceive_TransferPromotionID",
                schema: "PRM",
                table: "TransferPromotionReceive",
                column: "TransferPromotionID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferPromotionReceiveItem_TransferPromotionItemID",
                schema: "PRM",
                table: "TransferPromotionReceiveItem",
                column: "TransferPromotionItemID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferPromotionReceiveItem_TransferPromotionReceiveID",
                schema: "PRM",
                table: "TransferPromotionReceiveItem",
                column: "TransferPromotionReceiveID");
        }
    }
}
