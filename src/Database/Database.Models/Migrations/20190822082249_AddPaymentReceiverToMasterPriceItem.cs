using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddPaymentReceiverToMasterPriceItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PaymentReceiverMasterCenterID",
                schema: "MST",
                table: "MasterPriceItem",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MasterPriceItem_PaymentReceiverMasterCenterID",
                schema: "MST",
                table: "MasterPriceItem",
                column: "PaymentReceiverMasterCenterID");

            migrationBuilder.AddForeignKey(
                name: "FK_MasterPriceItem_MasterCenter_PaymentReceiverMasterCenterID",
                schema: "MST",
                table: "MasterPriceItem",
                column: "PaymentReceiverMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MasterPriceItem_MasterCenter_PaymentReceiverMasterCenterID",
                schema: "MST",
                table: "MasterPriceItem");

            migrationBuilder.DropIndex(
                name: "IX_MasterPriceItem_PaymentReceiverMasterCenterID",
                schema: "MST",
                table: "MasterPriceItem");

            migrationBuilder.DropColumn(
                name: "PaymentReceiverMasterCenterID",
                schema: "MST",
                table: "MasterPriceItem");
        }
    }
}
