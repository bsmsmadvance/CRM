using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class CreatePromoStockReceiveItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RemainingReceiveQuantity",
                schema: "PRM",
                table: "BookingPromotionRequestItem",
                newName: "RequestQuantity");

            migrationBuilder.RenameColumn(
                name: "ReceiveQuantity",
                schema: "PRM",
                table: "BookingPromotionRequestItem",
                newName: "RemainingRequestQuantity");

            migrationBuilder.RenameColumn(
                name: "EstimateReceiveDate",
                schema: "PRM",
                table: "BookingPromotionRequestItem",
                newName: "EstimateRequestDate");

            migrationBuilder.CreateTable(
                name: "BookingPromotionStockReceiveItem",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    BookingPromotionRequestID = table.Column<Guid>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(maxLength: 5000, nullable: true),
                    UnitTH = table.Column<string>(maxLength: 100, nullable: true),
                    UnitEN = table.Column<string>(maxLength: 100, nullable: true),
                    PricePerUnit = table.Column<decimal>(type: "Money", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "Money", nullable: false),
                    AgreementNo = table.Column<string>(maxLength: 100, nullable: true),
                    ItemNo = table.Column<string>(maxLength: 100, nullable: true),
                    MaterialCode = table.Column<string>(maxLength: 100, nullable: true),
                    NameTH = table.Column<string>(maxLength: 1000, nullable: true),
                    NameEN = table.Column<string>(maxLength: 1000, nullable: true),
                    ExpireDate = table.Column<DateTime>(nullable: true),
                    Plant = table.Column<string>(maxLength: 1000, nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingPromotionStockReceiveItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BookingPromotionStockReceiveItem_BookingPromotionRequest_BookingPromotionRequestID",
                        column: x => x.BookingPromotionRequestID,
                        principalSchema: "PRM",
                        principalTable: "BookingPromotionRequest",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransferPromotionStockReceiveItem",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    TransferPromotionRequestID = table.Column<Guid>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(maxLength: 5000, nullable: true),
                    UnitTH = table.Column<string>(maxLength: 100, nullable: true),
                    UnitEN = table.Column<string>(maxLength: 100, nullable: true),
                    PricePerUnit = table.Column<decimal>(type: "Money", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "Money", nullable: false),
                    AgreementNo = table.Column<string>(maxLength: 100, nullable: true),
                    ItemNo = table.Column<string>(maxLength: 100, nullable: true),
                    MaterialCode = table.Column<string>(maxLength: 100, nullable: true),
                    NameTH = table.Column<string>(maxLength: 1000, nullable: true),
                    NameEN = table.Column<string>(maxLength: 1000, nullable: true),
                    ExpireDate = table.Column<DateTime>(nullable: true),
                    Plant = table.Column<string>(maxLength: 1000, nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferPromotionStockReceiveItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TransferPromotionStockReceiveItem_TransferPromotionRequest_TransferPromotionRequestID",
                        column: x => x.TransferPromotionRequestID,
                        principalSchema: "PRM",
                        principalTable: "TransferPromotionRequest",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingPromotionStockReceiveItem_BookingPromotionRequestID",
                schema: "PRM",
                table: "BookingPromotionStockReceiveItem",
                column: "BookingPromotionRequestID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferPromotionStockReceiveItem_TransferPromotionRequestID",
                schema: "PRM",
                table: "TransferPromotionStockReceiveItem",
                column: "TransferPromotionRequestID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingPromotionStockReceiveItem",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "TransferPromotionStockReceiveItem",
                schema: "PRM");

            migrationBuilder.RenameColumn(
                name: "RequestQuantity",
                schema: "PRM",
                table: "BookingPromotionRequestItem",
                newName: "RemainingReceiveQuantity");

            migrationBuilder.RenameColumn(
                name: "RemainingRequestQuantity",
                schema: "PRM",
                table: "BookingPromotionRequestItem",
                newName: "ReceiveQuantity");

            migrationBuilder.RenameColumn(
                name: "EstimateRequestDate",
                schema: "PRM",
                table: "BookingPromotionRequestItem",
                newName: "EstimateReceiveDate");
        }
    }
}
