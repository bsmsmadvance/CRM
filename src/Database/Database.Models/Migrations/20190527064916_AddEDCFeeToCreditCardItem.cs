using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddEDCFeeToCreditCardItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EDCFeeID",
                schema: "PRM",
                table: "MasterTransferCreditCardItem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EDCFeeID",
                schema: "PRM",
                table: "MasterBookingCreditCardItem",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MasterTransferCreditCardItem_EDCFeeID",
                schema: "PRM",
                table: "MasterTransferCreditCardItem",
                column: "EDCFeeID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterBookingCreditCardItem_EDCFeeID",
                schema: "PRM",
                table: "MasterBookingCreditCardItem",
                column: "EDCFeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_MasterBookingCreditCardItem_EDCFee_EDCFeeID",
                schema: "PRM",
                table: "MasterBookingCreditCardItem",
                column: "EDCFeeID",
                principalSchema: "MST",
                principalTable: "EDCFee",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MasterTransferCreditCardItem_EDCFee_EDCFeeID",
                schema: "PRM",
                table: "MasterTransferCreditCardItem",
                column: "EDCFeeID",
                principalSchema: "MST",
                principalTable: "EDCFee",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MasterBookingCreditCardItem_EDCFee_EDCFeeID",
                schema: "PRM",
                table: "MasterBookingCreditCardItem");

            migrationBuilder.DropForeignKey(
                name: "FK_MasterTransferCreditCardItem_EDCFee_EDCFeeID",
                schema: "PRM",
                table: "MasterTransferCreditCardItem");

            migrationBuilder.DropIndex(
                name: "IX_MasterTransferCreditCardItem_EDCFeeID",
                schema: "PRM",
                table: "MasterTransferCreditCardItem");

            migrationBuilder.DropIndex(
                name: "IX_MasterBookingCreditCardItem_EDCFeeID",
                schema: "PRM",
                table: "MasterBookingCreditCardItem");

            migrationBuilder.DropColumn(
                name: "EDCFeeID",
                schema: "PRM",
                table: "MasterTransferCreditCardItem");

            migrationBuilder.DropColumn(
                name: "EDCFeeID",
                schema: "PRM",
                table: "MasterBookingCreditCardItem");
        }
    }
}
