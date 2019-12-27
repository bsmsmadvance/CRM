using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddIsSendPRToMasterPromo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSendPR",
                schema: "PRM",
                table: "MasterTransferPromotion",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UsedDate",
                schema: "PRM",
                table: "MasterTransferPromotion",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSendPR",
                schema: "PRM",
                table: "MasterBookingPromotion",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UsedDate",
                schema: "PRM",
                table: "MasterBookingPromotion",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSendPR",
                schema: "PRM",
                table: "MasterTransferPromotion");

            migrationBuilder.DropColumn(
                name: "UsedDate",
                schema: "PRM",
                table: "MasterTransferPromotion");

            migrationBuilder.DropColumn(
                name: "IsSendPR",
                schema: "PRM",
                table: "MasterBookingPromotion");

            migrationBuilder.DropColumn(
                name: "UsedDate",
                schema: "PRM",
                table: "MasterBookingPromotion");
        }
    }
}
