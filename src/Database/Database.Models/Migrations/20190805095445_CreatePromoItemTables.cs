using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class CreatePromoItemTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuotationBookingPromotionItem_MasterBookingPromotionFreeItem_MasterBookingPromotionFreeItemID",
                schema: "PRM",
                table: "QuotationBookingPromotionItem");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationTransferPromotionItem_MasterTransferPromotionFreeItem_MasterTransferPromotionFreeItemID",
                schema: "PRM",
                table: "QuotationTransferPromotionItem");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferPromotion_Booking_BookingID",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropIndex(
                name: "IX_QuotationTransferPromotionItem_MasterTransferPromotionFreeItemID",
                schema: "PRM",
                table: "QuotationTransferPromotionItem");

            migrationBuilder.DropIndex(
                name: "IX_QuotationBookingPromotionItem_MasterBookingPromotionFreeItemID",
                schema: "PRM",
                table: "QuotationBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "Unit",
                schema: "PRM",
                table: "TransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "IsFreeMortgageFee",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropColumn(
                name: "IsFreeItem",
                schema: "PRM",
                table: "QuotationTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "MasterTransferPromotionFreeItemID",
                schema: "PRM",
                table: "QuotationTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "PricePerUnit",
                schema: "PRM",
                table: "QuotationTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                schema: "PRM",
                table: "QuotationTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "Unit",
                schema: "PRM",
                table: "QuotationTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "Remark",
                schema: "PRM",
                table: "QuotationTransferPromotion");

            migrationBuilder.DropColumn(
                name: "IsFreeItem",
                schema: "PRM",
                table: "QuotationBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "MasterBookingPromotionFreeItemID",
                schema: "PRM",
                table: "QuotationBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "PricePerUnit",
                schema: "PRM",
                table: "QuotationBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                schema: "PRM",
                table: "QuotationBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "Unit",
                schema: "PRM",
                table: "QuotationBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "Price",
                schema: "PRM",
                table: "MasterBookingCreditCardItem");

            migrationBuilder.DropColumn(
                name: "Unit",
                schema: "PRM",
                table: "BookingPromotionItem");

            migrationBuilder.RenameColumn(
                name: "BookingID",
                schema: "PRM",
                table: "TransferPromotion",
                newName: "TransferID");

            migrationBuilder.RenameIndex(
                name: "IX_TransferPromotion_BookingID",
                schema: "PRM",
                table: "TransferPromotion",
                newName: "IX_TransferPromotion_TransferID");

            migrationBuilder.AddColumn<Guid>(
                name: "QuotationTransferPromotionItemID",
                schema: "PRM",
                table: "TransferPromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PresentByUserID",
                schema: "PRM",
                table: "TransferPromotion",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Fee",
                schema: "PRM",
                table: "MasterBookingCreditCardItem",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<Guid>(
                name: "QuotationBookingPromotionItemID",
                schema: "PRM",
                table: "BookingPromotionItem",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "QuotationBookingCreditCardItem",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    QuotationBookingPromotionID = table.Column<Guid>(nullable: false),
                    MasterBookingCreditCardItemID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotationBookingCreditCardItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_QuotationBookingCreditCardItem_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuotationBookingCreditCardItem_MasterBookingCreditCardItem_MasterBookingCreditCardItemID",
                        column: x => x.MasterBookingCreditCardItemID,
                        principalSchema: "PRM",
                        principalTable: "MasterBookingCreditCardItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuotationBookingCreditCardItem_QuotationBookingPromotion_QuotationBookingPromotionID",
                        column: x => x.QuotationBookingPromotionID,
                        principalSchema: "PRM",
                        principalTable: "QuotationBookingPromotion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuotationBookingCreditCardItem_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuotationBookingPromotionFreeItem",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    QuotationBookingPromotionID = table.Column<Guid>(nullable: false),
                    MasterBookingPromotionFreeItemID = table.Column<Guid>(nullable: true),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotationBookingPromotionFreeItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_QuotationBookingPromotionFreeItem_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuotationBookingPromotionFreeItem_MasterBookingPromotionFreeItem_MasterBookingPromotionFreeItemID",
                        column: x => x.MasterBookingPromotionFreeItemID,
                        principalSchema: "PRM",
                        principalTable: "MasterBookingPromotionFreeItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuotationBookingPromotionFreeItem_QuotationBookingPromotion_QuotationBookingPromotionID",
                        column: x => x.QuotationBookingPromotionID,
                        principalSchema: "PRM",
                        principalTable: "QuotationBookingPromotion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuotationBookingPromotionFreeItem_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuotationTransferCreditCardItem",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    QuotationTransferPromotionID = table.Column<Guid>(nullable: false),
                    MasterTransferCreditCardItemID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotationTransferCreditCardItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_QuotationTransferCreditCardItem_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuotationTransferCreditCardItem_MasterTransferCreditCardItem_MasterTransferCreditCardItemID",
                        column: x => x.MasterTransferCreditCardItemID,
                        principalSchema: "PRM",
                        principalTable: "MasterTransferCreditCardItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuotationTransferCreditCardItem_QuotationTransferPromotion_QuotationTransferPromotionID",
                        column: x => x.QuotationTransferPromotionID,
                        principalSchema: "PRM",
                        principalTable: "QuotationTransferPromotion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuotationTransferCreditCardItem_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuotationTransferPromotionFreeItem",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    QuotationTransferPromotionID = table.Column<Guid>(nullable: false),
                    MasterTransferPromotionFreeItemID = table.Column<Guid>(nullable: true),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotationTransferPromotionFreeItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_QuotationTransferPromotionFreeItem_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuotationTransferPromotionFreeItem_MasterTransferPromotionFreeItem_MasterTransferPromotionFreeItemID",
                        column: x => x.MasterTransferPromotionFreeItemID,
                        principalSchema: "PRM",
                        principalTable: "MasterTransferPromotionFreeItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuotationTransferPromotionFreeItem_QuotationTransferPromotion_QuotationTransferPromotionID",
                        column: x => x.QuotationTransferPromotionID,
                        principalSchema: "PRM",
                        principalTable: "QuotationTransferPromotion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuotationTransferPromotionFreeItem_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookingCreditCardItem",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    BookingPromotionID = table.Column<Guid>(nullable: false),
                    MasterBookingCreditCardItemID = table.Column<Guid>(nullable: true),
                    QuotationBookingCreditCardItemID = table.Column<Guid>(nullable: true),
                    QuotationBookingPromotionFreeItemID = table.Column<Guid>(nullable: true),
                    Fee = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingCreditCardItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BookingCreditCardItem_BookingPromotion_BookingPromotionID",
                        column: x => x.BookingPromotionID,
                        principalSchema: "PRM",
                        principalTable: "BookingPromotion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingCreditCardItem_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingCreditCardItem_MasterBookingCreditCardItem_MasterBookingCreditCardItemID",
                        column: x => x.MasterBookingCreditCardItemID,
                        principalSchema: "PRM",
                        principalTable: "MasterBookingCreditCardItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingCreditCardItem_QuotationBookingPromotionFreeItem_QuotationBookingPromotionFreeItemID",
                        column: x => x.QuotationBookingPromotionFreeItemID,
                        principalSchema: "PRM",
                        principalTable: "QuotationBookingPromotionFreeItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingCreditCardItem_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookingPromotionFreeItem",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    BookingPromotionID = table.Column<Guid>(nullable: false),
                    MasterBookingPromotionFreeItemID = table.Column<Guid>(nullable: true),
                    QuotationBookingPromotionFreeItemID = table.Column<Guid>(nullable: true),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingPromotionFreeItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BookingPromotionFreeItem_BookingPromotion_BookingPromotionID",
                        column: x => x.BookingPromotionID,
                        principalSchema: "PRM",
                        principalTable: "BookingPromotion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingPromotionFreeItem_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingPromotionFreeItem_MasterBookingPromotionFreeItem_MasterBookingPromotionFreeItemID",
                        column: x => x.MasterBookingPromotionFreeItemID,
                        principalSchema: "PRM",
                        principalTable: "MasterBookingPromotionFreeItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingPromotionFreeItem_QuotationBookingPromotionFreeItem_QuotationBookingPromotionFreeItemID",
                        column: x => x.QuotationBookingPromotionFreeItemID,
                        principalSchema: "PRM",
                        principalTable: "QuotationBookingPromotionFreeItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingPromotionFreeItem_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransferCreditCardItem",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    TransferPromotionID = table.Column<Guid>(nullable: false),
                    MasterTransferCreditCardItemID = table.Column<Guid>(nullable: true),
                    QuotationTransferCreditCardItemID = table.Column<Guid>(nullable: true),
                    QuotationTransferPromotionFreeItemID = table.Column<Guid>(nullable: true),
                    Fee = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferCreditCardItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TransferCreditCardItem_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferCreditCardItem_MasterTransferCreditCardItem_MasterTransferCreditCardItemID",
                        column: x => x.MasterTransferCreditCardItemID,
                        principalSchema: "PRM",
                        principalTable: "MasterTransferCreditCardItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferCreditCardItem_QuotationTransferPromotionFreeItem_QuotationTransferPromotionFreeItemID",
                        column: x => x.QuotationTransferPromotionFreeItemID,
                        principalSchema: "PRM",
                        principalTable: "QuotationTransferPromotionFreeItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferCreditCardItem_TransferPromotion_TransferPromotionID",
                        column: x => x.TransferPromotionID,
                        principalSchema: "PRM",
                        principalTable: "TransferPromotion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransferCreditCardItem_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransferPromotionFreeItem",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    TransferPromotionID = table.Column<Guid>(nullable: false),
                    MasterTransferPromotionFreeItemID = table.Column<Guid>(nullable: true),
                    QuotationTransferPromotionFreeItemID = table.Column<Guid>(nullable: true),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferPromotionFreeItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TransferPromotionFreeItem_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferPromotionFreeItem_MasterTransferPromotionFreeItem_MasterTransferPromotionFreeItemID",
                        column: x => x.MasterTransferPromotionFreeItemID,
                        principalSchema: "PRM",
                        principalTable: "MasterTransferPromotionFreeItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferPromotionFreeItem_QuotationTransferPromotionFreeItem_QuotationTransferPromotionFreeItemID",
                        column: x => x.QuotationTransferPromotionFreeItemID,
                        principalSchema: "PRM",
                        principalTable: "QuotationTransferPromotionFreeItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferPromotionFreeItem_TransferPromotion_TransferPromotionID",
                        column: x => x.TransferPromotionID,
                        principalSchema: "PRM",
                        principalTable: "TransferPromotion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransferPromotionFreeItem_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransferPromotionItem_QuotationTransferPromotionItemID",
                schema: "PRM",
                table: "TransferPromotionItem",
                column: "QuotationTransferPromotionItemID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferPromotion_PresentByUserID",
                schema: "PRM",
                table: "TransferPromotion",
                column: "PresentByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingPromotionItem_QuotationBookingPromotionItemID",
                schema: "PRM",
                table: "BookingPromotionItem",
                column: "QuotationBookingPromotionItemID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingCreditCardItem_BookingPromotionID",
                schema: "PRM",
                table: "BookingCreditCardItem",
                column: "BookingPromotionID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingCreditCardItem_CreatedByUserID",
                schema: "PRM",
                table: "BookingCreditCardItem",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingCreditCardItem_MasterBookingCreditCardItemID",
                schema: "PRM",
                table: "BookingCreditCardItem",
                column: "MasterBookingCreditCardItemID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingCreditCardItem_QuotationBookingPromotionFreeItemID",
                schema: "PRM",
                table: "BookingCreditCardItem",
                column: "QuotationBookingPromotionFreeItemID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingCreditCardItem_UpdatedByUserID",
                schema: "PRM",
                table: "BookingCreditCardItem",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingPromotionFreeItem_BookingPromotionID",
                schema: "PRM",
                table: "BookingPromotionFreeItem",
                column: "BookingPromotionID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingPromotionFreeItem_CreatedByUserID",
                schema: "PRM",
                table: "BookingPromotionFreeItem",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingPromotionFreeItem_MasterBookingPromotionFreeItemID",
                schema: "PRM",
                table: "BookingPromotionFreeItem",
                column: "MasterBookingPromotionFreeItemID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingPromotionFreeItem_QuotationBookingPromotionFreeItemID",
                schema: "PRM",
                table: "BookingPromotionFreeItem",
                column: "QuotationBookingPromotionFreeItemID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingPromotionFreeItem_UpdatedByUserID",
                schema: "PRM",
                table: "BookingPromotionFreeItem",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationBookingCreditCardItem_CreatedByUserID",
                schema: "PRM",
                table: "QuotationBookingCreditCardItem",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationBookingCreditCardItem_MasterBookingCreditCardItemID",
                schema: "PRM",
                table: "QuotationBookingCreditCardItem",
                column: "MasterBookingCreditCardItemID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationBookingCreditCardItem_QuotationBookingPromotionID",
                schema: "PRM",
                table: "QuotationBookingCreditCardItem",
                column: "QuotationBookingPromotionID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationBookingCreditCardItem_UpdatedByUserID",
                schema: "PRM",
                table: "QuotationBookingCreditCardItem",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationBookingPromotionFreeItem_CreatedByUserID",
                schema: "PRM",
                table: "QuotationBookingPromotionFreeItem",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationBookingPromotionFreeItem_MasterBookingPromotionFreeItemID",
                schema: "PRM",
                table: "QuotationBookingPromotionFreeItem",
                column: "MasterBookingPromotionFreeItemID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationBookingPromotionFreeItem_QuotationBookingPromotionID",
                schema: "PRM",
                table: "QuotationBookingPromotionFreeItem",
                column: "QuotationBookingPromotionID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationBookingPromotionFreeItem_UpdatedByUserID",
                schema: "PRM",
                table: "QuotationBookingPromotionFreeItem",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationTransferCreditCardItem_CreatedByUserID",
                schema: "PRM",
                table: "QuotationTransferCreditCardItem",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationTransferCreditCardItem_MasterTransferCreditCardItemID",
                schema: "PRM",
                table: "QuotationTransferCreditCardItem",
                column: "MasterTransferCreditCardItemID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationTransferCreditCardItem_QuotationTransferPromotionID",
                schema: "PRM",
                table: "QuotationTransferCreditCardItem",
                column: "QuotationTransferPromotionID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationTransferCreditCardItem_UpdatedByUserID",
                schema: "PRM",
                table: "QuotationTransferCreditCardItem",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationTransferPromotionFreeItem_CreatedByUserID",
                schema: "PRM",
                table: "QuotationTransferPromotionFreeItem",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationTransferPromotionFreeItem_MasterTransferPromotionFreeItemID",
                schema: "PRM",
                table: "QuotationTransferPromotionFreeItem",
                column: "MasterTransferPromotionFreeItemID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationTransferPromotionFreeItem_QuotationTransferPromotionID",
                schema: "PRM",
                table: "QuotationTransferPromotionFreeItem",
                column: "QuotationTransferPromotionID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationTransferPromotionFreeItem_UpdatedByUserID",
                schema: "PRM",
                table: "QuotationTransferPromotionFreeItem",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferCreditCardItem_CreatedByUserID",
                schema: "PRM",
                table: "TransferCreditCardItem",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferCreditCardItem_MasterTransferCreditCardItemID",
                schema: "PRM",
                table: "TransferCreditCardItem",
                column: "MasterTransferCreditCardItemID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferCreditCardItem_QuotationTransferPromotionFreeItemID",
                schema: "PRM",
                table: "TransferCreditCardItem",
                column: "QuotationTransferPromotionFreeItemID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferCreditCardItem_TransferPromotionID",
                schema: "PRM",
                table: "TransferCreditCardItem",
                column: "TransferPromotionID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferCreditCardItem_UpdatedByUserID",
                schema: "PRM",
                table: "TransferCreditCardItem",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferPromotionFreeItem_CreatedByUserID",
                schema: "PRM",
                table: "TransferPromotionFreeItem",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferPromotionFreeItem_MasterTransferPromotionFreeItemID",
                schema: "PRM",
                table: "TransferPromotionFreeItem",
                column: "MasterTransferPromotionFreeItemID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferPromotionFreeItem_QuotationTransferPromotionFreeItemID",
                schema: "PRM",
                table: "TransferPromotionFreeItem",
                column: "QuotationTransferPromotionFreeItemID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferPromotionFreeItem_TransferPromotionID",
                schema: "PRM",
                table: "TransferPromotionFreeItem",
                column: "TransferPromotionID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferPromotionFreeItem_UpdatedByUserID",
                schema: "PRM",
                table: "TransferPromotionFreeItem",
                column: "UpdatedByUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingPromotionItem_QuotationBookingPromotionItem_QuotationBookingPromotionItemID",
                schema: "PRM",
                table: "BookingPromotionItem",
                column: "QuotationBookingPromotionItemID",
                principalSchema: "PRM",
                principalTable: "QuotationBookingPromotionItem",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferPromotion_User_PresentByUserID",
                schema: "PRM",
                table: "TransferPromotion",
                column: "PresentByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferPromotion_Transfer_TransferID",
                schema: "PRM",
                table: "TransferPromotion",
                column: "TransferID",
                principalSchema: "SAL",
                principalTable: "Transfer",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferPromotionItem_QuotationTransferPromotionItem_QuotationTransferPromotionItemID",
                schema: "PRM",
                table: "TransferPromotionItem",
                column: "QuotationTransferPromotionItemID",
                principalSchema: "PRM",
                principalTable: "QuotationTransferPromotionItem",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingPromotionItem_QuotationBookingPromotionItem_QuotationBookingPromotionItemID",
                schema: "PRM",
                table: "BookingPromotionItem");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferPromotion_User_PresentByUserID",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferPromotion_Transfer_TransferID",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferPromotionItem_QuotationTransferPromotionItem_QuotationTransferPromotionItemID",
                schema: "PRM",
                table: "TransferPromotionItem");

            migrationBuilder.DropTable(
                name: "BookingCreditCardItem",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "BookingPromotionFreeItem",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "QuotationBookingCreditCardItem",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "QuotationTransferCreditCardItem",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "TransferCreditCardItem",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "TransferPromotionFreeItem",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "QuotationBookingPromotionFreeItem",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "QuotationTransferPromotionFreeItem",
                schema: "PRM");

            migrationBuilder.DropIndex(
                name: "IX_TransferPromotionItem_QuotationTransferPromotionItemID",
                schema: "PRM",
                table: "TransferPromotionItem");

            migrationBuilder.DropIndex(
                name: "IX_TransferPromotion_PresentByUserID",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropIndex(
                name: "IX_BookingPromotionItem_QuotationBookingPromotionItemID",
                schema: "PRM",
                table: "BookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "QuotationTransferPromotionItemID",
                schema: "PRM",
                table: "TransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "PresentByUserID",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropColumn(
                name: "Fee",
                schema: "PRM",
                table: "MasterBookingCreditCardItem");

            migrationBuilder.DropColumn(
                name: "QuotationBookingPromotionItemID",
                schema: "PRM",
                table: "BookingPromotionItem");

            migrationBuilder.RenameColumn(
                name: "TransferID",
                schema: "PRM",
                table: "TransferPromotion",
                newName: "BookingID");

            migrationBuilder.RenameIndex(
                name: "IX_TransferPromotion_TransferID",
                schema: "PRM",
                table: "TransferPromotion",
                newName: "IX_TransferPromotion_BookingID");

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                schema: "PRM",
                table: "TransferPromotionItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFreeMortgageFee",
                schema: "PRM",
                table: "TransferPromotion",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFreeItem",
                schema: "PRM",
                table: "QuotationTransferPromotionItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "MasterTransferPromotionFreeItemID",
                schema: "PRM",
                table: "QuotationTransferPromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerUnit",
                schema: "PRM",
                table: "QuotationTransferPromotionItem",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                schema: "PRM",
                table: "QuotationTransferPromotionItem",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                schema: "PRM",
                table: "QuotationTransferPromotionItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                schema: "PRM",
                table: "QuotationTransferPromotion",
                maxLength: 5000,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFreeItem",
                schema: "PRM",
                table: "QuotationBookingPromotionItem",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "MasterBookingPromotionFreeItemID",
                schema: "PRM",
                table: "QuotationBookingPromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerUnit",
                schema: "PRM",
                table: "QuotationBookingPromotionItem",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                schema: "PRM",
                table: "QuotationBookingPromotionItem",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                schema: "PRM",
                table: "QuotationBookingPromotionItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                schema: "PRM",
                table: "MasterBookingCreditCardItem",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                schema: "PRM",
                table: "BookingPromotionItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuotationTransferPromotionItem_MasterTransferPromotionFreeItemID",
                schema: "PRM",
                table: "QuotationTransferPromotionItem",
                column: "MasterTransferPromotionFreeItemID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationBookingPromotionItem_MasterBookingPromotionFreeItemID",
                schema: "PRM",
                table: "QuotationBookingPromotionItem",
                column: "MasterBookingPromotionFreeItemID");

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationBookingPromotionItem_MasterBookingPromotionFreeItem_MasterBookingPromotionFreeItemID",
                schema: "PRM",
                table: "QuotationBookingPromotionItem",
                column: "MasterBookingPromotionFreeItemID",
                principalSchema: "PRM",
                principalTable: "MasterBookingPromotionFreeItem",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationTransferPromotionItem_MasterTransferPromotionFreeItem_MasterTransferPromotionFreeItemID",
                schema: "PRM",
                table: "QuotationTransferPromotionItem",
                column: "MasterTransferPromotionFreeItemID",
                principalSchema: "PRM",
                principalTable: "MasterTransferPromotionFreeItem",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferPromotion_Booking_BookingID",
                schema: "PRM",
                table: "TransferPromotion",
                column: "BookingID",
                principalSchema: "SAL",
                principalTable: "Booking",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
