using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class SplitOtherNationalENTH : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OtherNational",
                schema: "CTM",
                table: "Contact",
                newName: "OtherNationalTH");

            migrationBuilder.AddColumn<bool>(
                name: "IsFreeMortgageCharge",
                schema: "PRM",
                table: "TransferPromotion",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUnlocked3PercentTransferDiscount",
                schema: "PRM",
                table: "TransferPromotion",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUnlockedTransferDiscount",
                schema: "PRM",
                table: "TransferPromotion",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "Unlocked3PercentTransferDiscountByUserID",
                schema: "PRM",
                table: "TransferPromotion",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Unlocked3PercentTransferDiscountDate",
                schema: "PRM",
                table: "TransferPromotion",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UnlockedTransferDiscountByUserID",
                schema: "PRM",
                table: "TransferPromotion",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UnlockedTransferDiscountDate",
                schema: "PRM",
                table: "TransferPromotion",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherNationalEN",
                schema: "CTM",
                table: "Contact",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransferPromotion_Unlocked3PercentTransferDiscountByUserID",
                schema: "PRM",
                table: "TransferPromotion",
                column: "Unlocked3PercentTransferDiscountByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferPromotion_UnlockedTransferDiscountByUserID",
                schema: "PRM",
                table: "TransferPromotion",
                column: "UnlockedTransferDiscountByUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_TransferPromotion_User_Unlocked3PercentTransferDiscountByUserID",
                schema: "PRM",
                table: "TransferPromotion",
                column: "Unlocked3PercentTransferDiscountByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferPromotion_User_UnlockedTransferDiscountByUserID",
                schema: "PRM",
                table: "TransferPromotion",
                column: "UnlockedTransferDiscountByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransferPromotion_User_Unlocked3PercentTransferDiscountByUserID",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferPromotion_User_UnlockedTransferDiscountByUserID",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropIndex(
                name: "IX_TransferPromotion_Unlocked3PercentTransferDiscountByUserID",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropIndex(
                name: "IX_TransferPromotion_UnlockedTransferDiscountByUserID",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropColumn(
                name: "IsFreeMortgageCharge",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropColumn(
                name: "IsUnlocked3PercentTransferDiscount",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropColumn(
                name: "IsUnlockedTransferDiscount",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropColumn(
                name: "Unlocked3PercentTransferDiscountByUserID",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropColumn(
                name: "Unlocked3PercentTransferDiscountDate",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropColumn(
                name: "UnlockedTransferDiscountByUserID",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropColumn(
                name: "UnlockedTransferDiscountDate",
                schema: "PRM",
                table: "TransferPromotion");

            migrationBuilder.DropColumn(
                name: "OtherNationalEN",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.RenameColumn(
                name: "OtherNationalTH",
                schema: "CTM",
                table: "Contact",
                newName: "OtherNational");
        }
    }
}
