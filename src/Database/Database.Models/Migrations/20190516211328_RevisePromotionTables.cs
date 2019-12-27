using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class RevisePromotionTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingPromotion_Promotion_PromotionID",
                schema: "PRM",
                table: "BookingPromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationBookingPromotion_Promotion_PromotionID",
                schema: "PRM",
                table: "QuotationBookingPromotion");

            migrationBuilder.DropTable(
                name: "PromotionCard",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "PromotionDeliveryItem",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "PromotionPreSaleDetail",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "PromotionReceiveItem",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "PromotionSubDetail",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "PromotionCardItem",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "PromotionDelivery",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "PromotionPreSale",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "PromotionReceive",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "PromotionDetail",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "Promotion",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "PromotionItem",
                schema: "PRM");

            migrationBuilder.DropColumn(
                name: "PriceUnit",
                schema: "PRM",
                table: "TransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "PromotionItemID",
                schema: "PRM",
                table: "TransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "BookingPriceItemKey",
                schema: "PRM",
                table: "TransferPromotionExpense");

            migrationBuilder.DropColumn(
                name: "Advisor",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropColumn(
                name: "BookingNo",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropColumn(
                name: "Budget",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropColumn(
                name: "DiscountContact",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropColumn(
                name: "DiscountFGF",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropColumn(
                name: "TransferType",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropColumn(
                name: "PriceUnit",
                schema: "PRM",
                table: "QuotationTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "PromotionItemID",
                schema: "PRM",
                table: "QuotationTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "Advisor",
                schema: "PRM",
                table: "QuotationTransferPromotion");

            migrationBuilder.DropColumn(
                name: "Budget",
                schema: "PRM",
                table: "QuotationTransferPromotion");

            migrationBuilder.DropColumn(
                name: "DiscountContact",
                schema: "PRM",
                table: "QuotationTransferPromotion");

            migrationBuilder.DropColumn(
                name: "DiscountFGF",
                schema: "PRM",
                table: "QuotationTransferPromotion");

            migrationBuilder.DropColumn(
                name: "DiscountTransfer",
                schema: "PRM",
                table: "QuotationTransferPromotion");

            migrationBuilder.DropColumn(
                name: "QuotationNo",
                schema: "PRM",
                table: "QuotationTransferPromotion");

            migrationBuilder.DropColumn(
                name: "TotalValue",
                schema: "PRM",
                table: "QuotationTransferPromotion");

            migrationBuilder.DropColumn(
                name: "TransferDate",
                schema: "PRM",
                table: "QuotationTransferPromotion");

            migrationBuilder.DropColumn(
                name: "TransferType",
                schema: "PRM",
                table: "QuotationTransferPromotion");

            migrationBuilder.DropColumn(
                name: "UnitPriceItemKey",
                schema: "PRM",
                table: "QuotationPromotionExpense");

            migrationBuilder.DropColumn(
                name: "PriceUnit",
                schema: "PRM",
                table: "QuotationBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "PromotionItemID",
                schema: "PRM",
                table: "QuotationBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "Quantity",
                schema: "PRM",
                table: "QuotationBookingPromotion");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                schema: "PRM",
                table: "QuotationBookingPromotion");

            migrationBuilder.DropColumn(
                name: "CompanyCode",
                schema: "PRM",
                table: "PromotionMaterial");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "PRM",
                table: "PromotionMaterial");

            migrationBuilder.DropColumn(
                name: "PriceUnit",
                schema: "PRM",
                table: "PromotionMaterial");

            migrationBuilder.DropColumn(
                name: "ProductBrand",
                schema: "PRM",
                table: "PromotionMaterial");

            migrationBuilder.DropColumn(
                name: "ProductNameEN",
                schema: "PRM",
                table: "PromotionMaterial");

            migrationBuilder.DropColumn(
                name: "ProductNameTH",
                schema: "PRM",
                table: "PromotionMaterial");

            migrationBuilder.DropColumn(
                name: "PriceUnit",
                schema: "PRM",
                table: "BookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "PromotionItemID",
                schema: "PRM",
                table: "BookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "UnitPriceItemKey",
                schema: "PRM",
                table: "BookingPromotionExpense");

            migrationBuilder.RenameColumn(
                name: "Price",
                schema: "PRM",
                table: "TransferPromotionItem",
                newName: "TotalPrice");

            migrationBuilder.RenameColumn(
                name: "TransferDate",
                schema: "PRM",
                table: "TransferPromotion",
                newName: "TransferDateBefore");

            migrationBuilder.RenameColumn(
                name: "TotalValue",
                schema: "PRM",
                table: "TransferPromotion",
                newName: "TotalAmount");

            migrationBuilder.RenameColumn(
                name: "DiscountTransfer",
                schema: "PRM",
                table: "TransferPromotion",
                newName: "BudgetAmount");

            migrationBuilder.RenameColumn(
                name: "Price",
                schema: "PRM",
                table: "QuotationTransferPromotionItem",
                newName: "TotalPrice");

            migrationBuilder.RenameColumn(
                name: "Price",
                schema: "PRM",
                table: "QuotationBookingPromotionItem",
                newName: "TotalPrice");

            migrationBuilder.RenameColumn(
                name: "Price",
                schema: "PRM",
                table: "BookingPromotionItem",
                newName: "TotalPrice");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                schema: "PRM",
                table: "BookingPromotion",
                newName: "TotalAmount");

            migrationBuilder.RenameColumn(
                name: "PromotionID",
                schema: "PRM",
                table: "BookingPromotion",
                newName: "MasterBookingPromotionID");

            migrationBuilder.RenameIndex(
                name: "IX_BookingPromotion_PromotionID",
                schema: "PRM",
                table: "BookingPromotion",
                newName: "IX_BookingPromotion_MasterBookingPromotionID");

            migrationBuilder.AddColumn<Guid>(
                name: "MasterTransferPromotionItemID",
                schema: "PRM",
                table: "TransferPromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerUnit",
                schema: "PRM",
                table: "TransferPromotionItem",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                schema: "PRM",
                table: "TransferPromotionItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UnitPriceItemID",
                schema: "PRM",
                table: "TransferPromotionExpense",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFreeMortgageFee",
                schema: "PRM",
                table: "TransferPromotion",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "MasterTransferPromotionID",
                schema: "PRM",
                table: "TransferPromotion",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<decimal>(
                name: "TransferDiscount",
                schema: "PRM",
                table: "TransferPromotion",
                type: "Money",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TransferPromotionNo",
                schema: "PRM",
                table: "TransferPromotion",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MasterTransferPromotionItemID",
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

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                schema: "PRM",
                table: "QuotationTransferPromotionItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MasterTransferPromotionID",
                schema: "PRM",
                table: "QuotationTransferPromotion",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                schema: "PRM",
                table: "QuotationTransferPromotion",
                maxLength: 5000,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "QuotationUnitPriceItemID",
                schema: "PRM",
                table: "QuotationPromotionExpense",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MasterBookingPromotionItemID",
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

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                schema: "PRM",
                table: "QuotationBookingPromotionItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Plant",
                schema: "PRM",
                table: "PromotionMaterial",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AgreementNo",
                schema: "PRM",
                table: "PromotionMaterial",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpireDate",
                schema: "PRM",
                table: "PromotionMaterial",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ItemNo",
                schema: "PRM",
                table: "PromotionMaterial",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaterialCode",
                schema: "PRM",
                table: "PromotionMaterial",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameEN",
                schema: "PRM",
                table: "PromotionMaterial",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameTH",
                schema: "PRM",
                table: "PromotionMaterial",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                schema: "PRM",
                table: "PromotionMaterial",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MasterBookingPromotionItemID",
                schema: "PRM",
                table: "BookingPromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerUnit",
                schema: "PRM",
                table: "BookingPromotionItem",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                schema: "PRM",
                table: "BookingPromotionItem",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UnitPriceItemID",
                schema: "PRM",
                table: "BookingPromotionExpense",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BookingPromotionNo",
                schema: "PRM",
                table: "BookingPromotion",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "BudgetAmount",
                schema: "PRM",
                table: "BookingPromotion",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ContractDiscount",
                schema: "PRM",
                table: "BookingPromotion",
                type: "Money",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FGFDiscount",
                schema: "PRM",
                table: "BookingPromotion",
                type: "Money",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PresentByUserID",
                schema: "PRM",
                table: "BookingPromotion",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TransferDateBefore",
                schema: "PRM",
                table: "BookingPromotion",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TransferDiscount",
                schema: "PRM",
                table: "BookingPromotion",
                type: "Money",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BookingPromotionDelivery",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    BookingPromotionID = table.Column<Guid>(nullable: false),
                    DeliveryNo = table.Column<string>(maxLength: 100, nullable: true),
                    DeliveryDate = table.Column<DateTime>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingPromotionDelivery", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BookingPromotionDelivery_BookingPromotion_BookingPromotionID",
                        column: x => x.BookingPromotionID,
                        principalSchema: "PRM",
                        principalTable: "BookingPromotion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingPromotionReceive",
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
                name: "MasterBookingPromotion",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    PromotionNo = table.Column<string>(maxLength: 100, nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ProjectID = table.Column<Guid>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    CashDiscount = table.Column<decimal>(type: "Money", nullable: true),
                    FGFDiscount = table.Column<decimal>(type: "Money", nullable: true),
                    TransferDiscount = table.Column<decimal>(type: "Money", nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    UseStatus = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterBookingPromotion", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MasterBookingPromotion_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "PRJ",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MasterPreSalePromotion",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    PromotionNo = table.Column<string>(maxLength: 100, nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ProjectID = table.Column<Guid>(nullable: true),
                    CompanyCode = table.Column<string>(maxLength: 100, nullable: true),
                    Plant = table.Column<string>(maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    UseStatus = table.Column<int>(nullable: false),
                    IsApproved = table.Column<bool>(nullable: false),
                    ApprovedDate = table.Column<DateTime>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterPreSalePromotion", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MasterPreSalePromotion_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "PRJ",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MasterTransferPromotion",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    PromotionNo = table.Column<string>(maxLength: 100, nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ProjectID = table.Column<Guid>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    TransferDiscount = table.Column<decimal>(type: "Money", nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    UseStatus = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterTransferPromotion", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MasterTransferPromotion_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "PRJ",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransferPromotionDelivery",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    TransferPromotionID = table.Column<Guid>(nullable: false),
                    DeliveryNo = table.Column<string>(maxLength: 100, nullable: true),
                    DeliveryDate = table.Column<DateTime>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferPromotionDelivery", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TransferPromotionDelivery_TransferPromotion_TransferPromotionID",
                        column: x => x.TransferPromotionID,
                        principalSchema: "PRM",
                        principalTable: "TransferPromotion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransferPromotionReceive",
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
                name: "BookingPromotionDeliveryItem",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    BookingPromotionDeliveryID = table.Column<Guid>(nullable: true),
                    BookingPromotionItemID = table.Column<Guid>(nullable: true),
                    ReceiveQuantity = table.Column<int>(nullable: false),
                    DeliveryQuantity = table.Column<int>(nullable: false),
                    RemainingReceiveQuantity = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    SerialNo = table.Column<string>(maxLength: 100, nullable: true),
                    Remark = table.Column<string>(maxLength: 5000, nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingPromotionDeliveryItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BookingPromotionDeliveryItem_BookingPromotionDelivery_BookingPromotionDeliveryID",
                        column: x => x.BookingPromotionDeliveryID,
                        principalSchema: "PRM",
                        principalTable: "BookingPromotionDelivery",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingPromotionDeliveryItem_BookingPromotionItem_BookingPromotionItemID",
                        column: x => x.BookingPromotionItemID,
                        principalSchema: "PRM",
                        principalTable: "BookingPromotionItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookingPromotionReceiveItem",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    BookingPromotionReceiveID = table.Column<Guid>(nullable: true),
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
                name: "MasterBookingCreditCardItem",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    MasterBookingPromotionID = table.Column<Guid>(nullable: false),
                    BankID = table.Column<Guid>(nullable: true),
                    NameTH = table.Column<string>(nullable: true),
                    NameEN = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(type: "Money", nullable: false),
                    UnitTH = table.Column<string>(nullable: true),
                    UnitEN = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterBookingCreditCardItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MasterBookingCreditCardItem_Bank_BankID",
                        column: x => x.BankID,
                        principalSchema: "MST",
                        principalTable: "Bank",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MasterBookingCreditCardItem_MasterBookingPromotion_MasterBookingPromotionID",
                        column: x => x.MasterBookingPromotionID,
                        principalSchema: "PRM",
                        principalTable: "MasterBookingPromotion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MasterBookingPromotionFreeItem",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    MasterBookingPromotionID = table.Column<Guid>(nullable: false),
                    NameTH = table.Column<string>(maxLength: 1000, nullable: true),
                    NameEN = table.Column<string>(maxLength: 1000, nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    UnitTH = table.Column<string>(maxLength: 100, nullable: true),
                    UnitEN = table.Column<string>(maxLength: 100, nullable: true),
                    ReceiveDays = table.Column<int>(nullable: false),
                    WhenReceive = table.Column<int>(nullable: false),
                    IsShowInContract = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterBookingPromotionFreeItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MasterBookingPromotionFreeItem_MasterBookingPromotion_MasterBookingPromotionID",
                        column: x => x.MasterBookingPromotionID,
                        principalSchema: "PRM",
                        principalTable: "MasterBookingPromotion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MasterBookingPromotionItem",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    MasterBookingPromotionID = table.Column<Guid>(nullable: false),
                    AgreementNo = table.Column<string>(maxLength: 100, nullable: true),
                    ItemNo = table.Column<string>(maxLength: 100, nullable: true),
                    SAPName = table.Column<string>(maxLength: 1000, nullable: true),
                    NameTH = table.Column<string>(maxLength: 1000, nullable: true),
                    NameEN = table.Column<string>(maxLength: 1000, nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    UnitTH = table.Column<string>(maxLength: 100, nullable: true),
                    UnitEN = table.Column<string>(maxLength: 100, nullable: true),
                    PricePerUnit = table.Column<decimal>(type: "Money", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "Money", nullable: false),
                    ReceiveDays = table.Column<int>(nullable: false),
                    WhenReceive = table.Column<int>(nullable: false),
                    IsPurchasing = table.Column<bool>(nullable: false),
                    IsShowInContract = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    ExpireDate = table.Column<DateTime>(nullable: true),
                    MainPromotionItemID = table.Column<Guid>(nullable: true),
                    PromotionMaterialID = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterBookingPromotionItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MasterBookingPromotionItem_MasterBookingPromotion_MasterBookingPromotionID",
                        column: x => x.MasterBookingPromotionID,
                        principalSchema: "PRM",
                        principalTable: "MasterBookingPromotion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MasterBookingPromotionItem_PromotionMaterial_PromotionMaterialID",
                        column: x => x.PromotionMaterialID,
                        principalSchema: "PRM",
                        principalTable: "PromotionMaterial",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MasterPreSalePromotionItem",
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
                    NameTH = table.Column<string>(maxLength: 1000, nullable: true),
                    NameEN = table.Column<string>(maxLength: 1000, nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    UnitTH = table.Column<string>(maxLength: 100, nullable: true),
                    UnitEN = table.Column<string>(maxLength: 100, nullable: true),
                    PricePerUnit = table.Column<decimal>(type: "Money", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "Money", nullable: false),
                    Remark = table.Column<string>(maxLength: 5000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterPreSalePromotionItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MasterPreSalePromotionItem_MasterPreSalePromotion_MasterPreSalePromotionID",
                        column: x => x.MasterPreSalePromotionID,
                        principalSchema: "PRM",
                        principalTable: "MasterPreSalePromotion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MasterPreSalePromotionUnit",
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
                    UnitID = table.Column<Guid>(nullable: true),
                    Remark = table.Column<string>(maxLength: 5000, nullable: true)
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
                name: "PreSalePromotion",
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
                    BookingID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreSalePromotion", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PreSalePromotion_Booking_BookingID",
                        column: x => x.BookingID,
                        principalSchema: "SAL",
                        principalTable: "Booking",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PreSalePromotion_MasterPreSalePromotion_MasterPreSalePromotionID",
                        column: x => x.MasterPreSalePromotionID,
                        principalSchema: "PRM",
                        principalTable: "MasterPreSalePromotion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MasterTransferPromotionFreeItem",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    MasterTransferPromotionID = table.Column<Guid>(nullable: false),
                    NameTH = table.Column<string>(maxLength: 1000, nullable: true),
                    NameEN = table.Column<string>(maxLength: 1000, nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    UnitTH = table.Column<string>(maxLength: 100, nullable: true),
                    UnitEN = table.Column<string>(maxLength: 100, nullable: true),
                    ReceiveDays = table.Column<int>(nullable: false),
                    WhenReceive = table.Column<int>(nullable: false),
                    IsShowInContract = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterTransferPromotionFreeItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MasterTransferPromotionFreeItem_MasterTransferPromotion_MasterTransferPromotionID",
                        column: x => x.MasterTransferPromotionID,
                        principalSchema: "PRM",
                        principalTable: "MasterTransferPromotion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MasterTransferPromotionItem",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    MasterTransferPromotionID = table.Column<Guid>(nullable: false),
                    AgreementNo = table.Column<string>(maxLength: 100, nullable: true),
                    ItemNo = table.Column<string>(maxLength: 100, nullable: true),
                    SAPName = table.Column<string>(maxLength: 1000, nullable: true),
                    NameTH = table.Column<string>(maxLength: 1000, nullable: true),
                    NameEN = table.Column<string>(maxLength: 1000, nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    UnitTH = table.Column<string>(maxLength: 100, nullable: true),
                    UnitEN = table.Column<string>(maxLength: 100, nullable: true),
                    PricePerUnit = table.Column<decimal>(type: "Money", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "Money", nullable: false),
                    ReceiveDays = table.Column<int>(nullable: false),
                    WhenReceive = table.Column<int>(nullable: false),
                    IsPurchasing = table.Column<bool>(nullable: false),
                    IsShowInContract = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    ExpireDate = table.Column<DateTime>(nullable: true),
                    MainPromotionItemID = table.Column<Guid>(nullable: true),
                    PromotionMaterialID = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterTransferPromotionItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MasterTransferPromotionItem_MasterTransferPromotion_MasterTransferPromotionID",
                        column: x => x.MasterTransferPromotionID,
                        principalSchema: "PRM",
                        principalTable: "MasterTransferPromotion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MasterTransferPromotionItem_PromotionMaterial_PromotionMaterialID",
                        column: x => x.PromotionMaterialID,
                        principalSchema: "PRM",
                        principalTable: "PromotionMaterial",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransferPromotionDeliveryItem",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    TransferPromotionDeliveryID = table.Column<Guid>(nullable: true),
                    TransferPromotionItemID = table.Column<Guid>(nullable: true),
                    ReceiveQuantity = table.Column<int>(nullable: false),
                    DeliveryQuantity = table.Column<int>(nullable: false),
                    RemainingReceiveQuantity = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    SerialNo = table.Column<string>(maxLength: 100, nullable: true),
                    Remark = table.Column<string>(maxLength: 5000, nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferPromotionDeliveryItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TransferPromotionDeliveryItem_TransferPromotionDelivery_TransferPromotionDeliveryID",
                        column: x => x.TransferPromotionDeliveryID,
                        principalSchema: "PRM",
                        principalTable: "TransferPromotionDelivery",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferPromotionDeliveryItem_TransferPromotionItem_TransferPromotionItemID",
                        column: x => x.TransferPromotionItemID,
                        principalSchema: "PRM",
                        principalTable: "TransferPromotionItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransferPromotionReceiveItem",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    TransferPromotionReceiveID = table.Column<Guid>(nullable: true),
                    TransferPromotionItemID = table.Column<Guid>(nullable: true),
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

            migrationBuilder.CreateTable(
                name: "MasterBookingHouseModelFreeItem",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MasterBookingPromotionFreeItemID = table.Column<Guid>(nullable: false),
                    ModelID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterBookingHouseModelFreeItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MasterBookingHouseModelFreeItem_MasterBookingPromotionFreeItem_MasterBookingPromotionFreeItemID",
                        column: x => x.MasterBookingPromotionFreeItemID,
                        principalSchema: "PRM",
                        principalTable: "MasterBookingPromotionFreeItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MasterBookingHouseModelFreeItem_Model_ModelID",
                        column: x => x.ModelID,
                        principalSchema: "PRJ",
                        principalTable: "Model",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MasterBookingHouseModelItem",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MasterBookingPromotionItemID = table.Column<Guid>(nullable: false),
                    ModelID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterBookingHouseModelItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MasterBookingHouseModelItem_MasterBookingPromotionItem_MasterBookingPromotionItemID",
                        column: x => x.MasterBookingPromotionItemID,
                        principalSchema: "PRM",
                        principalTable: "MasterBookingPromotionItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MasterBookingHouseModelItem_Model_ModelID",
                        column: x => x.ModelID,
                        principalSchema: "PRJ",
                        principalTable: "Model",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MasterPreSalePromotionUnitItem",
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
                    MasterPreSalePromotionUnitID = table.Column<Guid>(nullable: true),
                    PRNo = table.Column<string>(maxLength: 100, nullable: true),
                    IsCanceled = table.Column<bool>(nullable: false),
                    CancelTime = table.Column<DateTime>(nullable: true)
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

            migrationBuilder.CreateTable(
                name: "MasterTransferHouseModelFreeItem",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MasterTransferPromotionFreeItemID = table.Column<Guid>(nullable: false),
                    ModelID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterTransferHouseModelFreeItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MasterTransferHouseModelFreeItem_MasterTransferPromotionFreeItem_MasterTransferPromotionFreeItemID",
                        column: x => x.MasterTransferPromotionFreeItemID,
                        principalSchema: "PRM",
                        principalTable: "MasterTransferPromotionFreeItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MasterTransferHouseModelFreeItem_Model_ModelID",
                        column: x => x.ModelID,
                        principalSchema: "PRJ",
                        principalTable: "Model",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MasterTransferHouseModelItem",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MasterTransferPromotionItemID = table.Column<Guid>(nullable: false),
                    ModelID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterTransferHouseModelItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MasterTransferHouseModelItem_MasterTransferPromotionItem_MasterTransferPromotionItemID",
                        column: x => x.MasterTransferPromotionItemID,
                        principalSchema: "PRM",
                        principalTable: "MasterTransferPromotionItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MasterTransferHouseModelItem_Model_ModelID",
                        column: x => x.ModelID,
                        principalSchema: "PRJ",
                        principalTable: "Model",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PreSalePromotionItem",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PreSalePromotionID = table.Column<Guid>(nullable: false),
                    MasterPreSalePromotionUnitItemID = table.Column<Guid>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Unit = table.Column<string>(maxLength: 100, nullable: true),
                    PricePerUnit = table.Column<decimal>(type: "Money", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "Money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreSalePromotionItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PreSalePromotionItem_MasterPreSalePromotionUnitItem_MasterPreSalePromotionUnitItemID",
                        column: x => x.MasterPreSalePromotionUnitItemID,
                        principalSchema: "PRM",
                        principalTable: "MasterPreSalePromotionUnitItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PreSalePromotionItem_PreSalePromotion_PreSalePromotionID",
                        column: x => x.PreSalePromotionID,
                        principalSchema: "PRM",
                        principalTable: "PreSalePromotion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransferPromotionItem_MasterTransferPromotionItemID",
                schema: "PRM",
                table: "TransferPromotionItem",
                column: "MasterTransferPromotionItemID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferPromotionExpense_UnitPriceItemID",
                schema: "PRM",
                table: "TransferPromotionExpense",
                column: "UnitPriceItemID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferPromotion_MasterTransferPromotionID",
                schema: "PRM",
                table: "TransferPromotion",
                column: "MasterTransferPromotionID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationTransferPromotionItem_MasterTransferPromotionItemID",
                schema: "PRM",
                table: "QuotationTransferPromotionItem",
                column: "MasterTransferPromotionItemID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationTransferPromotion_MasterTransferPromotionID",
                schema: "PRM",
                table: "QuotationTransferPromotion",
                column: "MasterTransferPromotionID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationPromotionExpense_QuotationUnitPriceItemID",
                schema: "PRM",
                table: "QuotationPromotionExpense",
                column: "QuotationUnitPriceItemID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationBookingPromotionItem_MasterBookingPromotionItemID",
                schema: "PRM",
                table: "QuotationBookingPromotionItem",
                column: "MasterBookingPromotionItemID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingPromotionItem_MasterBookingPromotionItemID",
                schema: "PRM",
                table: "BookingPromotionItem",
                column: "MasterBookingPromotionItemID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingPromotionExpense_UnitPriceItemID",
                schema: "PRM",
                table: "BookingPromotionExpense",
                column: "UnitPriceItemID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingPromotion_PresentByUserID",
                schema: "PRM",
                table: "BookingPromotion",
                column: "PresentByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingPromotionDelivery_BookingPromotionID",
                schema: "PRM",
                table: "BookingPromotionDelivery",
                column: "BookingPromotionID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingPromotionDeliveryItem_BookingPromotionDeliveryID",
                schema: "PRM",
                table: "BookingPromotionDeliveryItem",
                column: "BookingPromotionDeliveryID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingPromotionDeliveryItem_BookingPromotionItemID",
                schema: "PRM",
                table: "BookingPromotionDeliveryItem",
                column: "BookingPromotionItemID");

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
                name: "IX_MasterBookingCreditCardItem_BankID",
                schema: "PRM",
                table: "MasterBookingCreditCardItem",
                column: "BankID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterBookingCreditCardItem_MasterBookingPromotionID",
                schema: "PRM",
                table: "MasterBookingCreditCardItem",
                column: "MasterBookingPromotionID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterBookingHouseModelFreeItem_MasterBookingPromotionFreeItemID",
                schema: "PRM",
                table: "MasterBookingHouseModelFreeItem",
                column: "MasterBookingPromotionFreeItemID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterBookingHouseModelFreeItem_ModelID",
                schema: "PRM",
                table: "MasterBookingHouseModelFreeItem",
                column: "ModelID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterBookingHouseModelItem_MasterBookingPromotionItemID",
                schema: "PRM",
                table: "MasterBookingHouseModelItem",
                column: "MasterBookingPromotionItemID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterBookingHouseModelItem_ModelID",
                schema: "PRM",
                table: "MasterBookingHouseModelItem",
                column: "ModelID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterBookingPromotion_ProjectID",
                schema: "PRM",
                table: "MasterBookingPromotion",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterBookingPromotionFreeItem_MasterBookingPromotionID",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem",
                column: "MasterBookingPromotionID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterBookingPromotionItem_MasterBookingPromotionID",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                column: "MasterBookingPromotionID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterBookingPromotionItem_PromotionMaterialID",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                column: "PromotionMaterialID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterPreSalePromotion_ProjectID",
                schema: "PRM",
                table: "MasterPreSalePromotion",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterPreSalePromotionItem_MasterPreSalePromotionID",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                column: "MasterPreSalePromotionID");

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

            migrationBuilder.CreateIndex(
                name: "IX_MasterTransferHouseModelFreeItem_MasterTransferPromotionFreeItemID",
                schema: "PRM",
                table: "MasterTransferHouseModelFreeItem",
                column: "MasterTransferPromotionFreeItemID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterTransferHouseModelFreeItem_ModelID",
                schema: "PRM",
                table: "MasterTransferHouseModelFreeItem",
                column: "ModelID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterTransferHouseModelItem_MasterTransferPromotionItemID",
                schema: "PRM",
                table: "MasterTransferHouseModelItem",
                column: "MasterTransferPromotionItemID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterTransferHouseModelItem_ModelID",
                schema: "PRM",
                table: "MasterTransferHouseModelItem",
                column: "ModelID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterTransferPromotion_ProjectID",
                schema: "PRM",
                table: "MasterTransferPromotion",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterTransferPromotionFreeItem_MasterTransferPromotionID",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem",
                column: "MasterTransferPromotionID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterTransferPromotionItem_MasterTransferPromotionID",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                column: "MasterTransferPromotionID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterTransferPromotionItem_PromotionMaterialID",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                column: "PromotionMaterialID");

            migrationBuilder.CreateIndex(
                name: "IX_PreSalePromotion_BookingID",
                schema: "PRM",
                table: "PreSalePromotion",
                column: "BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_PreSalePromotion_MasterPreSalePromotionID",
                schema: "PRM",
                table: "PreSalePromotion",
                column: "MasterPreSalePromotionID");

            migrationBuilder.CreateIndex(
                name: "IX_PreSalePromotionItem_MasterPreSalePromotionUnitItemID",
                schema: "PRM",
                table: "PreSalePromotionItem",
                column: "MasterPreSalePromotionUnitItemID");

            migrationBuilder.CreateIndex(
                name: "IX_PreSalePromotionItem_PreSalePromotionID",
                schema: "PRM",
                table: "PreSalePromotionItem",
                column: "PreSalePromotionID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferPromotionDelivery_TransferPromotionID",
                schema: "PRM",
                table: "TransferPromotionDelivery",
                column: "TransferPromotionID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferPromotionDeliveryItem_TransferPromotionDeliveryID",
                schema: "PRM",
                table: "TransferPromotionDeliveryItem",
                column: "TransferPromotionDeliveryID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferPromotionDeliveryItem_TransferPromotionItemID",
                schema: "PRM",
                table: "TransferPromotionDeliveryItem",
                column: "TransferPromotionItemID");

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

            migrationBuilder.AddForeignKey(
                name: "FK_BookingPromotion_MasterBookingPromotion_MasterBookingPromotionID",
                schema: "PRM",
                table: "BookingPromotion",
                column: "MasterBookingPromotionID",
                principalSchema: "PRM",
                principalTable: "MasterBookingPromotion",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingPromotion_User_PresentByUserID",
                schema: "PRM",
                table: "BookingPromotion",
                column: "PresentByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingPromotionExpense_UnitPriceItem_UnitPriceItemID",
                schema: "PRM",
                table: "BookingPromotionExpense",
                column: "UnitPriceItemID",
                principalSchema: "SAL",
                principalTable: "UnitPriceItem",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingPromotionItem_MasterBookingPromotionItem_MasterBookingPromotionItemID",
                schema: "PRM",
                table: "BookingPromotionItem",
                column: "MasterBookingPromotionItemID",
                principalSchema: "PRM",
                principalTable: "MasterBookingPromotionItem",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationBookingPromotion_MasterBookingPromotion_PromotionID",
                schema: "PRM",
                table: "QuotationBookingPromotion",
                column: "PromotionID",
                principalSchema: "PRM",
                principalTable: "MasterBookingPromotion",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationBookingPromotionItem_MasterBookingPromotionItem_MasterBookingPromotionItemID",
                schema: "PRM",
                table: "QuotationBookingPromotionItem",
                column: "MasterBookingPromotionItemID",
                principalSchema: "PRM",
                principalTable: "MasterBookingPromotionItem",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationPromotionExpense_QuotationUnitPriceItem_QuotationUnitPriceItemID",
                schema: "PRM",
                table: "QuotationPromotionExpense",
                column: "QuotationUnitPriceItemID",
                principalSchema: "SAL",
                principalTable: "QuotationUnitPriceItem",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationTransferPromotion_MasterTransferPromotion_MasterTransferPromotionID",
                schema: "PRM",
                table: "QuotationTransferPromotion",
                column: "MasterTransferPromotionID",
                principalSchema: "PRM",
                principalTable: "MasterTransferPromotion",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationTransferPromotionItem_MasterTransferPromotionItem_MasterTransferPromotionItemID",
                schema: "PRM",
                table: "QuotationTransferPromotionItem",
                column: "MasterTransferPromotionItemID",
                principalSchema: "PRM",
                principalTable: "MasterTransferPromotionItem",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferPromotion_MasterTransferPromotion_MasterTransferPromotionID",
                schema: "PRM",
                table: "TransferPromotion",
                column: "MasterTransferPromotionID",
                principalSchema: "PRM",
                principalTable: "MasterTransferPromotion",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferPromotionExpense_UnitPriceItem_UnitPriceItemID",
                schema: "PRM",
                table: "TransferPromotionExpense",
                column: "UnitPriceItemID",
                principalSchema: "SAL",
                principalTable: "UnitPriceItem",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferPromotionItem_MasterTransferPromotionItem_MasterTransferPromotionItemID",
                schema: "PRM",
                table: "TransferPromotionItem",
                column: "MasterTransferPromotionItemID",
                principalSchema: "PRM",
                principalTable: "MasterTransferPromotionItem",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingPromotion_MasterBookingPromotion_MasterBookingPromotionID",
                schema: "PRM",
                table: "BookingPromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingPromotion_User_PresentByUserID",
                schema: "PRM",
                table: "BookingPromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingPromotionExpense_UnitPriceItem_UnitPriceItemID",
                schema: "PRM",
                table: "BookingPromotionExpense");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingPromotionItem_MasterBookingPromotionItem_MasterBookingPromotionItemID",
                schema: "PRM",
                table: "BookingPromotionItem");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationBookingPromotion_MasterBookingPromotion_PromotionID",
                schema: "PRM",
                table: "QuotationBookingPromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationBookingPromotionItem_MasterBookingPromotionItem_MasterBookingPromotionItemID",
                schema: "PRM",
                table: "QuotationBookingPromotionItem");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationPromotionExpense_QuotationUnitPriceItem_QuotationUnitPriceItemID",
                schema: "PRM",
                table: "QuotationPromotionExpense");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationTransferPromotion_MasterTransferPromotion_MasterTransferPromotionID",
                schema: "PRM",
                table: "QuotationTransferPromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationTransferPromotionItem_MasterTransferPromotionItem_MasterTransferPromotionItemID",
                schema: "PRM",
                table: "QuotationTransferPromotionItem");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferPromotion_MasterTransferPromotion_MasterTransferPromotionID",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferPromotionExpense_UnitPriceItem_UnitPriceItemID",
                schema: "PRM",
                table: "TransferPromotionExpense");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferPromotionItem_MasterTransferPromotionItem_MasterTransferPromotionItemID",
                schema: "PRM",
                table: "TransferPromotionItem");

            migrationBuilder.DropTable(
                name: "BookingPromotionDeliveryItem",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "BookingPromotionReceiveItem",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "MasterBookingCreditCardItem",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "MasterBookingHouseModelFreeItem",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "MasterBookingHouseModelItem",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "MasterTransferHouseModelFreeItem",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "MasterTransferHouseModelItem",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "PreSalePromotionItem",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "TransferPromotionDeliveryItem",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "TransferPromotionReceiveItem",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "BookingPromotionDelivery",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "BookingPromotionReceive",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "MasterBookingPromotionFreeItem",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "MasterBookingPromotionItem",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "MasterTransferPromotionFreeItem",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "MasterTransferPromotionItem",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "MasterPreSalePromotionUnitItem",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "PreSalePromotion",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "TransferPromotionDelivery",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "TransferPromotionReceive",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "MasterBookingPromotion",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "MasterTransferPromotion",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "MasterPreSalePromotionItem",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "MasterPreSalePromotionUnit",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "MasterPreSalePromotion",
                schema: "PRM");

            migrationBuilder.DropIndex(
                name: "IX_TransferPromotionItem_MasterTransferPromotionItemID",
                schema: "PRM",
                table: "TransferPromotionItem");

            migrationBuilder.DropIndex(
                name: "IX_TransferPromotionExpense_UnitPriceItemID",
                schema: "PRM",
                table: "TransferPromotionExpense");

            migrationBuilder.DropIndex(
                name: "IX_TransferPromotion_MasterTransferPromotionID",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropIndex(
                name: "IX_QuotationTransferPromotionItem_MasterTransferPromotionItemID",
                schema: "PRM",
                table: "QuotationTransferPromotionItem");

            migrationBuilder.DropIndex(
                name: "IX_QuotationTransferPromotion_MasterTransferPromotionID",
                schema: "PRM",
                table: "QuotationTransferPromotion");

            migrationBuilder.DropIndex(
                name: "IX_QuotationPromotionExpense_QuotationUnitPriceItemID",
                schema: "PRM",
                table: "QuotationPromotionExpense");

            migrationBuilder.DropIndex(
                name: "IX_QuotationBookingPromotionItem_MasterBookingPromotionItemID",
                schema: "PRM",
                table: "QuotationBookingPromotionItem");

            migrationBuilder.DropIndex(
                name: "IX_BookingPromotionItem_MasterBookingPromotionItemID",
                schema: "PRM",
                table: "BookingPromotionItem");

            migrationBuilder.DropIndex(
                name: "IX_BookingPromotionExpense_UnitPriceItemID",
                schema: "PRM",
                table: "BookingPromotionExpense");

            migrationBuilder.DropIndex(
                name: "IX_BookingPromotion_PresentByUserID",
                schema: "PRM",
                table: "BookingPromotion");

            migrationBuilder.DropColumn(
                name: "MasterTransferPromotionItemID",
                schema: "PRM",
                table: "TransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "PricePerUnit",
                schema: "PRM",
                table: "TransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "Unit",
                schema: "PRM",
                table: "TransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "UnitPriceItemID",
                schema: "PRM",
                table: "TransferPromotionExpense");

            migrationBuilder.DropColumn(
                name: "IsFreeMortgageFee",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropColumn(
                name: "MasterTransferPromotionID",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropColumn(
                name: "TransferDiscount",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropColumn(
                name: "TransferPromotionNo",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropColumn(
                name: "MasterTransferPromotionItemID",
                schema: "PRM",
                table: "QuotationTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "PricePerUnit",
                schema: "PRM",
                table: "QuotationTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "Unit",
                schema: "PRM",
                table: "QuotationTransferPromotionItem");

            migrationBuilder.DropColumn(
                name: "MasterTransferPromotionID",
                schema: "PRM",
                table: "QuotationTransferPromotion");

            migrationBuilder.DropColumn(
                name: "Remark",
                schema: "PRM",
                table: "QuotationTransferPromotion");

            migrationBuilder.DropColumn(
                name: "QuotationUnitPriceItemID",
                schema: "PRM",
                table: "QuotationPromotionExpense");

            migrationBuilder.DropColumn(
                name: "MasterBookingPromotionItemID",
                schema: "PRM",
                table: "QuotationBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "PricePerUnit",
                schema: "PRM",
                table: "QuotationBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "Unit",
                schema: "PRM",
                table: "QuotationBookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "AgreementNo",
                schema: "PRM",
                table: "PromotionMaterial");

            migrationBuilder.DropColumn(
                name: "ExpireDate",
                schema: "PRM",
                table: "PromotionMaterial");

            migrationBuilder.DropColumn(
                name: "ItemNo",
                schema: "PRM",
                table: "PromotionMaterial");

            migrationBuilder.DropColumn(
                name: "MaterialCode",
                schema: "PRM",
                table: "PromotionMaterial");

            migrationBuilder.DropColumn(
                name: "NameEN",
                schema: "PRM",
                table: "PromotionMaterial");

            migrationBuilder.DropColumn(
                name: "NameTH",
                schema: "PRM",
                table: "PromotionMaterial");

            migrationBuilder.DropColumn(
                name: "Unit",
                schema: "PRM",
                table: "PromotionMaterial");

            migrationBuilder.DropColumn(
                name: "MasterBookingPromotionItemID",
                schema: "PRM",
                table: "BookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "PricePerUnit",
                schema: "PRM",
                table: "BookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "Unit",
                schema: "PRM",
                table: "BookingPromotionItem");

            migrationBuilder.DropColumn(
                name: "UnitPriceItemID",
                schema: "PRM",
                table: "BookingPromotionExpense");

            migrationBuilder.DropColumn(
                name: "BookingPromotionNo",
                schema: "PRM",
                table: "BookingPromotion");

            migrationBuilder.DropColumn(
                name: "BudgetAmount",
                schema: "PRM",
                table: "BookingPromotion");

            migrationBuilder.DropColumn(
                name: "ContractDiscount",
                schema: "PRM",
                table: "BookingPromotion");

            migrationBuilder.DropColumn(
                name: "FGFDiscount",
                schema: "PRM",
                table: "BookingPromotion");

            migrationBuilder.DropColumn(
                name: "PresentByUserID",
                schema: "PRM",
                table: "BookingPromotion");

            migrationBuilder.DropColumn(
                name: "TransferDateBefore",
                schema: "PRM",
                table: "BookingPromotion");

            migrationBuilder.DropColumn(
                name: "TransferDiscount",
                schema: "PRM",
                table: "BookingPromotion");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                schema: "PRM",
                table: "TransferPromotionItem",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "TransferDateBefore",
                schema: "PRM",
                table: "TransferPromotion",
                newName: "TransferDate");

            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                schema: "PRM",
                table: "TransferPromotion",
                newName: "TotalValue");

            migrationBuilder.RenameColumn(
                name: "BudgetAmount",
                schema: "PRM",
                table: "TransferPromotion",
                newName: "DiscountTransfer");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                schema: "PRM",
                table: "QuotationTransferPromotionItem",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                schema: "PRM",
                table: "QuotationBookingPromotionItem",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                schema: "PRM",
                table: "BookingPromotionItem",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                schema: "PRM",
                table: "BookingPromotion",
                newName: "TotalPrice");

            migrationBuilder.RenameColumn(
                name: "MasterBookingPromotionID",
                schema: "PRM",
                table: "BookingPromotion",
                newName: "PromotionID");

            migrationBuilder.RenameIndex(
                name: "IX_BookingPromotion_MasterBookingPromotionID",
                schema: "PRM",
                table: "BookingPromotion",
                newName: "IX_BookingPromotion_PromotionID");

            migrationBuilder.AddColumn<string>(
                name: "PriceUnit",
                schema: "PRM",
                table: "TransferPromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PromotionItemID",
                schema: "PRM",
                table: "TransferPromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BookingPriceItemKey",
                schema: "PRM",
                table: "TransferPromotionExpense",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Advisor",
                schema: "PRM",
                table: "TransferPromotion",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BookingNo",
                schema: "PRM",
                table: "TransferPromotion",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Budget",
                schema: "PRM",
                table: "TransferPromotion",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountContact",
                schema: "PRM",
                table: "TransferPromotion",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountFGF",
                schema: "PRM",
                table: "TransferPromotion",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "TransferType",
                schema: "PRM",
                table: "TransferPromotion",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PriceUnit",
                schema: "PRM",
                table: "QuotationTransferPromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PromotionItemID",
                schema: "PRM",
                table: "QuotationTransferPromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Advisor",
                schema: "PRM",
                table: "QuotationTransferPromotion",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Budget",
                schema: "PRM",
                table: "QuotationTransferPromotion",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountContact",
                schema: "PRM",
                table: "QuotationTransferPromotion",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountFGF",
                schema: "PRM",
                table: "QuotationTransferPromotion",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountTransfer",
                schema: "PRM",
                table: "QuotationTransferPromotion",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "QuotationNo",
                schema: "PRM",
                table: "QuotationTransferPromotion",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalValue",
                schema: "PRM",
                table: "QuotationTransferPromotion",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "TransferDate",
                schema: "PRM",
                table: "QuotationTransferPromotion",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TransferType",
                schema: "PRM",
                table: "QuotationTransferPromotion",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitPriceItemKey",
                schema: "PRM",
                table: "QuotationPromotionExpense",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PriceUnit",
                schema: "PRM",
                table: "QuotationBookingPromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PromotionItemID",
                schema: "PRM",
                table: "QuotationBookingPromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                schema: "PRM",
                table: "QuotationBookingPromotion",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                schema: "PRM",
                table: "QuotationBookingPromotion",
                type: "Money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "Plant",
                schema: "PRM",
                table: "PromotionMaterial",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyCode",
                schema: "PRM",
                table: "PromotionMaterial",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "PRM",
                table: "PromotionMaterial",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PriceUnit",
                schema: "PRM",
                table: "PromotionMaterial",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductBrand",
                schema: "PRM",
                table: "PromotionMaterial",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductNameEN",
                schema: "PRM",
                table: "PromotionMaterial",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductNameTH",
                schema: "PRM",
                table: "PromotionMaterial",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PriceUnit",
                schema: "PRM",
                table: "BookingPromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PromotionItemID",
                schema: "PRM",
                table: "BookingPromotionItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitPriceItemKey",
                schema: "PRM",
                table: "BookingPromotionExpense",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Promotion",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    DiscountTransfer = table.Column<decimal>(type: "Money", nullable: false),
                    EndDate = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ProjectID = table.Column<Guid>(nullable: false),
                    PromotionName = table.Column<string>(nullable: true),
                    PromotionNo = table.Column<string>(nullable: true),
                    PromotionType = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UsageStatus = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotion", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Promotion_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "PRJ",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromotionCardItem",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    BankName = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Price = table.Column<decimal>(type: "Money", nullable: false),
                    PriceUnit = table.Column<string>(nullable: true),
                    ProductName = table.Column<string>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionCardItem", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PromotionDelivery",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    BookingNo = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ReceiveDate = table.Column<DateTime>(nullable: true),
                    TransferPromotionID = table.Column<Guid>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionDelivery", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PromotionDelivery_TransferPromotion_TransferPromotionID",
                        column: x => x.TransferPromotionID,
                        principalSchema: "PRM",
                        principalTable: "TransferPromotion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromotionItem",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    AgreementNo = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    ExpireDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemNo = table.Column<string>(nullable: true),
                    MaterialCode = table.Column<string>(nullable: true),
                    Plant = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(type: "Money", nullable: false),
                    PriceUnit = table.Column<string>(nullable: true),
                    ProductName = table.Column<string>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionItem", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PromotionPreSale",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CompanyCode = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Plant = table.Column<string>(nullable: true),
                    ProjectID = table.Column<Guid>(nullable: false),
                    PromotionCode = table.Column<string>(nullable: true),
                    PromotionName = table.Column<string>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UsageStatus = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionPreSale", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PromotionPreSale_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "PRJ",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromotionReceive",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    BookingNo = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ReceiveDate = table.Column<DateTime>(nullable: true),
                    TransferPromotionID = table.Column<Guid>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionReceive", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PromotionReceive_TransferPromotion_TransferPromotionID",
                        column: x => x.TransferPromotionID,
                        principalSchema: "PRM",
                        principalTable: "TransferPromotion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromotionCard",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ProductNameEN = table.Column<string>(nullable: true),
                    ProductNameTH = table.Column<string>(nullable: true),
                    PromotionCardItemID = table.Column<Guid>(nullable: false),
                    PromotionID = table.Column<Guid>(nullable: false),
                    RecieveContact = table.Column<string>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionCard", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PromotionCard_PromotionCardItem_PromotionCardItemID",
                        column: x => x.PromotionCardItemID,
                        principalSchema: "PRM",
                        principalTable: "PromotionCardItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PromotionCard_Promotion_PromotionID",
                        column: x => x.PromotionID,
                        principalSchema: "PRM",
                        principalTable: "Promotion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromotionDeliveryItem",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    EstimateRecieveDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PromotionDeliveryID = table.Column<Guid>(nullable: false),
                    PromotionItemID = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(maxLength: 5000, nullable: true),
                    SerialNo = table.Column<string>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionDeliveryItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PromotionDeliveryItem_PromotionDelivery_PromotionDeliveryID",
                        column: x => x.PromotionDeliveryID,
                        principalSchema: "PRM",
                        principalTable: "PromotionDelivery",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromotionDetail",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    ExpireDate = table.Column<DateTime>(nullable: true),
                    HousePlan = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ProductNameEN = table.Column<string>(nullable: true),
                    ProductNameTH = table.Column<string>(nullable: true),
                    PromotionID = table.Column<Guid>(nullable: false),
                    PromotionItemID = table.Column<Guid>(nullable: false),
                    PromotionType = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    RecieveContact = table.Column<string>(nullable: true),
                    RecieveDate = table.Column<int>(nullable: false),
                    TotalPrice = table.Column<decimal>(type: "Money", nullable: false),
                    UnitEN = table.Column<string>(nullable: true),
                    UnitTH = table.Column<string>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionDetail", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PromotionDetail_Promotion_PromotionID",
                        column: x => x.PromotionID,
                        principalSchema: "PRM",
                        principalTable: "Promotion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PromotionDetail_PromotionItem_PromotionItemID",
                        column: x => x.PromotionItemID,
                        principalSchema: "PRM",
                        principalTable: "PromotionItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromotionPreSaleDetail",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    ExpireDate = table.Column<DateTime>(nullable: true),
                    HousePlan = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ProductNameEN = table.Column<string>(nullable: true),
                    ProductNameTH = table.Column<string>(nullable: true),
                    PromotionItemID = table.Column<Guid>(nullable: false),
                    PromotionPreSaleID = table.Column<Guid>(nullable: false),
                    PromotionType = table.Column<string>(nullable: true),
                    Quantity = table.Column<decimal>(nullable: false),
                    RecieveContact = table.Column<string>(nullable: true),
                    RecieveDate = table.Column<int>(nullable: false),
                    TotalPrice = table.Column<decimal>(type: "Money", nullable: false),
                    UnitEN = table.Column<string>(nullable: true),
                    UnitTH = table.Column<string>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionPreSaleDetail", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PromotionPreSaleDetail_PromotionItem_PromotionItemID",
                        column: x => x.PromotionItemID,
                        principalSchema: "PRM",
                        principalTable: "PromotionItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PromotionPreSaleDetail_PromotionPreSale_PromotionPreSaleID",
                        column: x => x.PromotionPreSaleID,
                        principalSchema: "PRM",
                        principalTable: "PromotionPreSale",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromotionReceiveItem",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    EstimateRecieveDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PRNo = table.Column<string>(nullable: true),
                    PromotionItemID = table.Column<string>(nullable: true),
                    PromotionReceiveID = table.Column<Guid>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(maxLength: 5000, nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionReceiveItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PromotionReceiveItem_PromotionReceive_PromotionReceiveID",
                        column: x => x.PromotionReceiveID,
                        principalSchema: "PRM",
                        principalTable: "PromotionReceive",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromotionSubDetail",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PromotionDetailID = table.Column<Guid>(nullable: false),
                    PromotionItemID = table.Column<Guid>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionSubDetail", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PromotionSubDetail_PromotionDetail_PromotionDetailID",
                        column: x => x.PromotionDetailID,
                        principalSchema: "PRM",
                        principalTable: "PromotionDetail",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PromotionSubDetail_PromotionItem_PromotionItemID",
                        column: x => x.PromotionItemID,
                        principalSchema: "PRM",
                        principalTable: "PromotionItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Promotion_ProjectID",
                schema: "PRM",
                table: "Promotion",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionCard_PromotionCardItemID",
                schema: "PRM",
                table: "PromotionCard",
                column: "PromotionCardItemID");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionCard_PromotionID",
                schema: "PRM",
                table: "PromotionCard",
                column: "PromotionID");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionDelivery_TransferPromotionID",
                schema: "PRM",
                table: "PromotionDelivery",
                column: "TransferPromotionID");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionDeliveryItem_PromotionDeliveryID",
                schema: "PRM",
                table: "PromotionDeliveryItem",
                column: "PromotionDeliveryID");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionDetail_PromotionID",
                schema: "PRM",
                table: "PromotionDetail",
                column: "PromotionID");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionDetail_PromotionItemID",
                schema: "PRM",
                table: "PromotionDetail",
                column: "PromotionItemID");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionPreSale_ProjectID",
                schema: "PRM",
                table: "PromotionPreSale",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionPreSaleDetail_PromotionItemID",
                schema: "PRM",
                table: "PromotionPreSaleDetail",
                column: "PromotionItemID");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionPreSaleDetail_PromotionPreSaleID",
                schema: "PRM",
                table: "PromotionPreSaleDetail",
                column: "PromotionPreSaleID");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionReceive_TransferPromotionID",
                schema: "PRM",
                table: "PromotionReceive",
                column: "TransferPromotionID");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionReceiveItem_PromotionReceiveID",
                schema: "PRM",
                table: "PromotionReceiveItem",
                column: "PromotionReceiveID");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionSubDetail_PromotionDetailID",
                schema: "PRM",
                table: "PromotionSubDetail",
                column: "PromotionDetailID");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionSubDetail_PromotionItemID",
                schema: "PRM",
                table: "PromotionSubDetail",
                column: "PromotionItemID");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingPromotion_Promotion_PromotionID",
                schema: "PRM",
                table: "BookingPromotion",
                column: "PromotionID",
                principalSchema: "PRM",
                principalTable: "Promotion",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationBookingPromotion_Promotion_PromotionID",
                schema: "PRM",
                table: "QuotationBookingPromotion",
                column: "PromotionID",
                principalSchema: "PRM",
                principalTable: "Promotion",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
