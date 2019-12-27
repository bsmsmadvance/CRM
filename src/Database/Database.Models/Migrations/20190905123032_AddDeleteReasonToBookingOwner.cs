using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddDeleteReasonToBookingOwner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ChangePromotionWorkflowID",
                schema: "SAL",
                table: "PriceListWorkflow",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ChangePromotionWorkflowID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeleteReason",
                schema: "SAL",
                table: "BookingOwner",
                maxLength: 5000,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ChangePromotionWorkflowID",
                schema: "PRM",
                table: "TransferPromotion",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ChangePromotionWorkflowID",
                schema: "PRM",
                table: "BookingPromotion",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ChangePromotionWorkflow",
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
                    IsUsed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangePromotionWorkflow", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ChangePromotionWorkflow_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChangePromotionWorkflow_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PriceListWorkflow_ChangePromotionWorkflowID",
                schema: "SAL",
                table: "PriceListWorkflow",
                column: "ChangePromotionWorkflowID");

            migrationBuilder.CreateIndex(
                name: "IX_MinPriceBudgetWorkflow_ChangePromotionWorkflowID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                column: "ChangePromotionWorkflowID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferPromotion_ChangePromotionWorkflowID",
                schema: "PRM",
                table: "TransferPromotion",
                column: "ChangePromotionWorkflowID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingPromotion_ChangePromotionWorkflowID",
                schema: "PRM",
                table: "BookingPromotion",
                column: "ChangePromotionWorkflowID");

            migrationBuilder.CreateIndex(
                name: "IX_ChangePromotionWorkflow_CreatedByUserID",
                schema: "PRM",
                table: "ChangePromotionWorkflow",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ChangePromotionWorkflow_UpdatedByUserID",
                schema: "PRM",
                table: "ChangePromotionWorkflow",
                column: "UpdatedByUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingPromotion_ChangePromotionWorkflow_ChangePromotionWorkflowID",
                schema: "PRM",
                table: "BookingPromotion",
                column: "ChangePromotionWorkflowID",
                principalSchema: "PRM",
                principalTable: "ChangePromotionWorkflow",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferPromotion_ChangePromotionWorkflow_ChangePromotionWorkflowID",
                schema: "PRM",
                table: "TransferPromotion",
                column: "ChangePromotionWorkflowID",
                principalSchema: "PRM",
                principalTable: "ChangePromotionWorkflow",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MinPriceBudgetWorkflow_ChangePromotionWorkflow_ChangePromotionWorkflowID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow",
                column: "ChangePromotionWorkflowID",
                principalSchema: "PRM",
                principalTable: "ChangePromotionWorkflow",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PriceListWorkflow_ChangePromotionWorkflow_ChangePromotionWorkflowID",
                schema: "SAL",
                table: "PriceListWorkflow",
                column: "ChangePromotionWorkflowID",
                principalSchema: "PRM",
                principalTable: "ChangePromotionWorkflow",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingPromotion_ChangePromotionWorkflow_ChangePromotionWorkflowID",
                schema: "PRM",
                table: "BookingPromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferPromotion_ChangePromotionWorkflow_ChangePromotionWorkflowID",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_MinPriceBudgetWorkflow_ChangePromotionWorkflow_ChangePromotionWorkflowID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow");

            migrationBuilder.DropForeignKey(
                name: "FK_PriceListWorkflow_ChangePromotionWorkflow_ChangePromotionWorkflowID",
                schema: "SAL",
                table: "PriceListWorkflow");

            migrationBuilder.DropTable(
                name: "ChangePromotionWorkflow",
                schema: "PRM");

            migrationBuilder.DropIndex(
                name: "IX_PriceListWorkflow_ChangePromotionWorkflowID",
                schema: "SAL",
                table: "PriceListWorkflow");

            migrationBuilder.DropIndex(
                name: "IX_MinPriceBudgetWorkflow_ChangePromotionWorkflowID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow");

            migrationBuilder.DropIndex(
                name: "IX_TransferPromotion_ChangePromotionWorkflowID",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropIndex(
                name: "IX_BookingPromotion_ChangePromotionWorkflowID",
                schema: "PRM",
                table: "BookingPromotion");

            migrationBuilder.DropColumn(
                name: "ChangePromotionWorkflowID",
                schema: "SAL",
                table: "PriceListWorkflow");

            migrationBuilder.DropColumn(
                name: "ChangePromotionWorkflowID",
                schema: "SAL",
                table: "MinPriceBudgetWorkflow");

            migrationBuilder.DropColumn(
                name: "DeleteReason",
                schema: "SAL",
                table: "BookingOwner");

            migrationBuilder.DropColumn(
                name: "ChangePromotionWorkflowID",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropColumn(
                name: "ChangePromotionWorkflowID",
                schema: "PRM",
                table: "BookingPromotion");
        }
    }
}
