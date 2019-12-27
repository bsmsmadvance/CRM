using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class CreateMinPriceWorkflowTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentMethodType",
                schema: "FIN",
                table: "PaymentMethod");

            migrationBuilder.DropColumn(
                name: "UnitPriceItemKey",
                schema: "FIN",
                table: "PaymentItem");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedTime",
                schema: "SAL",
                table: "PriceListWorkflow",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RoleID",
                schema: "SAL",
                table: "PriceListWorkflow",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReceiptNo",
                schema: "FIN",
                table: "Receipt",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentMethodTypeMasterCenterID",
                schema: "FIN",
                table: "PaymentMethod",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UnitPriceInstallmentID",
                schema: "FIN",
                table: "PaymentItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UnitPriceItemID",
                schema: "FIN",
                table: "PaymentItem",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReceiptNo",
                schema: "FIN",
                table: "Payment",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "MinPriceBudgetApproval",
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
                    RoleID = table.Column<Guid>(nullable: true),
                    UserID = table.Column<Guid>(nullable: true),
                    IsApproved = table.Column<bool>(nullable: true),
                    ApprovedTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MinPriceBudgetApproval", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MinPriceBudgetApproval_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MinPriceBudgetApproval_Role_RoleID",
                        column: x => x.RoleID,
                        principalSchema: "USR",
                        principalTable: "Role",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MinPriceBudgetApproval_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MinPriceBudgetApproval_User_UserID",
                        column: x => x.UserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MinPriceBudgetWorkflow",
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
                    BookingID = table.Column<Guid>(nullable: true),
                    MinPriceBudgetWorkflowStageMasterCenterID = table.Column<Guid>(nullable: true),
                    SellingPrice = table.Column<decimal>(type: "Money", nullable: false),
                    MasterMinPrice = table.Column<decimal>(type: "Money", nullable: false),
                    FromMasterMinPriceID = table.Column<Guid>(nullable: true),
                    RequestMinPrice = table.Column<decimal>(type: "Money", nullable: false),
                    TotalBudgetMinPrice = table.Column<decimal>(type: "Money", nullable: false),
                    MinPriceWorkflowTypeMasterCenterID = table.Column<Guid>(nullable: true),
                    MasterBudgetPromotion = table.Column<decimal>(type: "Money", nullable: false),
                    FromMasterBudgetPromotionID = table.Column<Guid>(nullable: true),
                    RequestBudgetPromotion = table.Column<decimal>(type: "Money", nullable: false),
                    PromotionTypeMasterCenterID = table.Column<Guid>(nullable: true),
                    BookingPromotionID = table.Column<Guid>(nullable: true),
                    TransferPromotionID = table.Column<Guid>(nullable: true),
                    RejectComment = table.Column<string>(maxLength: 5000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MinPriceBudgetWorkflow", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MinPriceBudgetWorkflow_Booking_BookingID",
                        column: x => x.BookingID,
                        principalSchema: "SAL",
                        principalTable: "Booking",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MinPriceBudgetWorkflow_BookingPromotion_BookingPromotionID",
                        column: x => x.BookingPromotionID,
                        principalSchema: "PRM",
                        principalTable: "BookingPromotion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MinPriceBudgetWorkflow_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MinPriceBudgetWorkflow_BudgetPromotion_FromMasterBudgetPromotionID",
                        column: x => x.FromMasterBudgetPromotionID,
                        principalSchema: "PRJ",
                        principalTable: "BudgetPromotion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MinPriceBudgetWorkflow_MinPrice_FromMasterMinPriceID",
                        column: x => x.FromMasterMinPriceID,
                        principalSchema: "PRJ",
                        principalTable: "MinPrice",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MinPriceBudgetWorkflow_MasterCenter_MinPriceBudgetWorkflowStageMasterCenterID",
                        column: x => x.MinPriceBudgetWorkflowStageMasterCenterID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MinPriceBudgetWorkflow_MasterCenter_MinPriceWorkflowTypeMasterCenterID",
                        column: x => x.MinPriceWorkflowTypeMasterCenterID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MinPriceBudgetWorkflow_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "PRJ",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MinPriceBudgetWorkflow_MasterCenter_PromotionTypeMasterCenterID",
                        column: x => x.PromotionTypeMasterCenterID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MinPriceBudgetWorkflow_TransferPromotion_TransferPromotionID",
                        column: x => x.TransferPromotionID,
                        principalSchema: "PRM",
                        principalTable: "TransferPromotion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MinPriceBudgetWorkflow_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PriceListWorkflow_RoleID",
                schema: "SAL",
                table: "PriceListWorkflow",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethod_PaymentMethodTypeMasterCenterID",
                schema: "FIN",
                table: "PaymentMethod",
                column: "PaymentMethodTypeMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentItem_UnitPriceInstallmentID",
                schema: "FIN",
                table: "PaymentItem",
                column: "UnitPriceInstallmentID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentItem_UnitPriceItemID",
                schema: "FIN",
                table: "PaymentItem",
                column: "UnitPriceItemID");

            migrationBuilder.CreateIndex(
                name: "IX_MinPriceBudgetApproval_CreatedByUserID",
                schema: "SAL",
                table: "MinPriceBudgetApproval",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MinPriceBudgetApproval_RoleID",
                schema: "SAL",
                table: "MinPriceBudgetApproval",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_MinPriceBudgetApproval_UpdatedByUserID",
                schema: "SAL",
                table: "MinPriceBudgetApproval",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MinPriceBudgetApproval_UserID",
                schema: "SAL",
                table: "MinPriceBudgetApproval",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_MinPriceBudgetWorkflow_BookingID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                column: "BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_MinPriceBudgetWorkflow_BookingPromotionID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                column: "BookingPromotionID");

            migrationBuilder.CreateIndex(
                name: "IX_MinPriceBudgetWorkflow_CreatedByUserID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MinPriceBudgetWorkflow_FromMasterBudgetPromotionID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                column: "FromMasterBudgetPromotionID");

            migrationBuilder.CreateIndex(
                name: "IX_MinPriceBudgetWorkflow_FromMasterMinPriceID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                column: "FromMasterMinPriceID");

            migrationBuilder.CreateIndex(
                name: "IX_MinPriceBudgetWorkflow_MinPriceBudgetWorkflowStageMasterCenterID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                column: "MinPriceBudgetWorkflowStageMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_MinPriceBudgetWorkflow_MinPriceWorkflowTypeMasterCenterID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                column: "MinPriceWorkflowTypeMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_MinPriceBudgetWorkflow_ProjectID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_MinPriceBudgetWorkflow_PromotionTypeMasterCenterID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                column: "PromotionTypeMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_MinPriceBudgetWorkflow_TransferPromotionID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                column: "TransferPromotionID");

            migrationBuilder.CreateIndex(
                name: "IX_MinPriceBudgetWorkflow_UpdatedByUserID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                column: "UpdatedByUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentItem_UnitPriceInstallment_UnitPriceInstallmentID",
                schema: "FIN",
                table: "PaymentItem",
                column: "UnitPriceInstallmentID",
                principalSchema: "SAL",
                principalTable: "UnitPriceInstallment",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentItem_UnitPriceItem_UnitPriceItemID",
                schema: "FIN",
                table: "PaymentItem",
                column: "UnitPriceItemID",
                principalSchema: "SAL",
                principalTable: "UnitPriceItem",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentMethod_MasterCenter_PaymentMethodTypeMasterCenterID",
                schema: "FIN",
                table: "PaymentMethod",
                column: "PaymentMethodTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PriceListWorkflow_Role_RoleID",
                schema: "SAL",
                table: "PriceListWorkflow",
                column: "RoleID",
                principalSchema: "USR",
                principalTable: "Role",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentItem_UnitPriceInstallment_UnitPriceInstallmentID",
                schema: "FIN",
                table: "PaymentItem");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentItem_UnitPriceItem_UnitPriceItemID",
                schema: "FIN",
                table: "PaymentItem");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentMethod_MasterCenter_PaymentMethodTypeMasterCenterID",
                schema: "FIN",
                table: "PaymentMethod");

            migrationBuilder.DropForeignKey(
                name: "FK_PriceListWorkflow_Role_RoleID",
                schema: "SAL",
                table: "PriceListWorkflow");

            migrationBuilder.DropTable(
                name: "MinPriceBudgetApproval",
                schema: "SAL");

            migrationBuilder.DropTable(
                name: "MinPriceBudgetWorkflow",
                schema: "SAL");

            migrationBuilder.DropIndex(
                name: "IX_PriceListWorkflow_RoleID",
                schema: "SAL",
                table: "PriceListWorkflow");

            migrationBuilder.DropIndex(
                name: "IX_PaymentMethod_PaymentMethodTypeMasterCenterID",
                schema: "FIN",
                table: "PaymentMethod");

            migrationBuilder.DropIndex(
                name: "IX_PaymentItem_UnitPriceInstallmentID",
                schema: "FIN",
                table: "PaymentItem");

            migrationBuilder.DropIndex(
                name: "IX_PaymentItem_UnitPriceItemID",
                schema: "FIN",
                table: "PaymentItem");

            migrationBuilder.DropColumn(
                name: "ApprovedTime",
                schema: "SAL",
                table: "PriceListWorkflow");

            migrationBuilder.DropColumn(
                name: "RoleID",
                schema: "SAL",
                table: "PriceListWorkflow");

            migrationBuilder.DropColumn(
                name: "PaymentMethodTypeMasterCenterID",
                schema: "FIN",
                table: "PaymentMethod");

            migrationBuilder.DropColumn(
                name: "UnitPriceInstallmentID",
                schema: "FIN",
                table: "PaymentItem");

            migrationBuilder.DropColumn(
                name: "UnitPriceItemID",
                schema: "FIN",
                table: "PaymentItem");

            migrationBuilder.AlterColumn<string>(
                name: "ReceiptNo",
                schema: "FIN",
                table: "Receipt",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentMethodType",
                schema: "FIN",
                table: "PaymentMethod",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UnitPriceItemKey",
                schema: "FIN",
                table: "PaymentItem",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReceiptNo",
                schema: "FIN",
                table: "Payment",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
