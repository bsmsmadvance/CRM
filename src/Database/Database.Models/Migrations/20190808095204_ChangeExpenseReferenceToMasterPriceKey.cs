using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ChangeExpenseReferenceToMasterPriceKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingPromotionExpense_UnitPriceItem_UnitPriceItemID",
                schema: "PRM",
                table: "BookingPromotionExpense");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationPromotionExpense_QuotationUnitPriceItem_QuotationUnitPriceItemID",
                schema: "PRM",
                table: "QuotationPromotionExpense");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferPromotionExpense_UnitPriceItem_UnitPriceItemID",
                schema: "PRM",
                table: "TransferPromotionExpense");

            migrationBuilder.RenameColumn(
                name: "UnitPriceItemID",
                schema: "PRM",
                table: "TransferPromotionExpense",
                newName: "MasterPriceItemID");

            migrationBuilder.RenameIndex(
                name: "IX_TransferPromotionExpense_UnitPriceItemID",
                schema: "PRM",
                table: "TransferPromotionExpense",
                newName: "IX_TransferPromotionExpense_MasterPriceItemID");

            migrationBuilder.RenameColumn(
                name: "QuotationUnitPriceItemID",
                schema: "PRM",
                table: "QuotationPromotionExpense",
                newName: "MasterPriceItemID");

            migrationBuilder.RenameIndex(
                name: "IX_QuotationPromotionExpense_QuotationUnitPriceItemID",
                schema: "PRM",
                table: "QuotationPromotionExpense",
                newName: "IX_QuotationPromotionExpense_MasterPriceItemID");

            migrationBuilder.RenameColumn(
                name: "UnitPriceItemID",
                schema: "PRM",
                table: "BookingPromotionExpense",
                newName: "MasterPriceItemID");

            migrationBuilder.RenameIndex(
                name: "IX_BookingPromotionExpense_UnitPriceItemID",
                schema: "PRM",
                table: "BookingPromotionExpense",
                newName: "IX_BookingPromotionExpense_MasterPriceItemID");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingPromotionExpense_MasterPriceItem_MasterPriceItemID",
                schema: "PRM",
                table: "BookingPromotionExpense",
                column: "MasterPriceItemID",
                principalSchema: "MST",
                principalTable: "MasterPriceItem",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationPromotionExpense_MasterPriceItem_MasterPriceItemID",
                schema: "PRM",
                table: "QuotationPromotionExpense",
                column: "MasterPriceItemID",
                principalSchema: "MST",
                principalTable: "MasterPriceItem",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferPromotionExpense_MasterPriceItem_MasterPriceItemID",
                schema: "PRM",
                table: "TransferPromotionExpense",
                column: "MasterPriceItemID",
                principalSchema: "MST",
                principalTable: "MasterPriceItem",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingPromotionExpense_MasterPriceItem_MasterPriceItemID",
                schema: "PRM",
                table: "BookingPromotionExpense");

            migrationBuilder.DropForeignKey(
                name: "FK_QuotationPromotionExpense_MasterPriceItem_MasterPriceItemID",
                schema: "PRM",
                table: "QuotationPromotionExpense");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferPromotionExpense_MasterPriceItem_MasterPriceItemID",
                schema: "PRM",
                table: "TransferPromotionExpense");

            migrationBuilder.RenameColumn(
                name: "MasterPriceItemID",
                schema: "PRM",
                table: "TransferPromotionExpense",
                newName: "UnitPriceItemID");

            migrationBuilder.RenameIndex(
                name: "IX_TransferPromotionExpense_MasterPriceItemID",
                schema: "PRM",
                table: "TransferPromotionExpense",
                newName: "IX_TransferPromotionExpense_UnitPriceItemID");

            migrationBuilder.RenameColumn(
                name: "MasterPriceItemID",
                schema: "PRM",
                table: "QuotationPromotionExpense",
                newName: "QuotationUnitPriceItemID");

            migrationBuilder.RenameIndex(
                name: "IX_QuotationPromotionExpense_MasterPriceItemID",
                schema: "PRM",
                table: "QuotationPromotionExpense",
                newName: "IX_QuotationPromotionExpense_QuotationUnitPriceItemID");

            migrationBuilder.RenameColumn(
                name: "MasterPriceItemID",
                schema: "PRM",
                table: "BookingPromotionExpense",
                newName: "UnitPriceItemID");

            migrationBuilder.RenameIndex(
                name: "IX_BookingPromotionExpense_MasterPriceItemID",
                schema: "PRM",
                table: "BookingPromotionExpense",
                newName: "IX_BookingPromotionExpense_UnitPriceItemID");

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
                name: "FK_QuotationPromotionExpense_QuotationUnitPriceItem_QuotationUnitPriceItemID",
                schema: "PRM",
                table: "QuotationPromotionExpense",
                column: "QuotationUnitPriceItemID",
                principalSchema: "SAL",
                principalTable: "QuotationUnitPriceItem",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferPromotionExpense_UnitPriceItem_UnitPriceItemID",
                schema: "PRM",
                table: "TransferPromotionExpense",
                column: "UnitPriceItemID",
                principalSchema: "SAL",
                principalTable: "UnitPriceItem",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
