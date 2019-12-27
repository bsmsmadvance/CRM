using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddTransferDateToBooking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Contact_IntroducerContactID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.RenameColumn(
                name: "IntroducerContactID",
                schema: "SAL",
                table: "Booking",
                newName: "SaleOfficerTypeMasterCenterID");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_IntroducerContactID",
                schema: "SAL",
                table: "Booking",
                newName: "IX_Booking_SaleOfficerTypeMasterCenterID");

            migrationBuilder.AddColumn<Guid>(
                name: "ReferContactID",
                schema: "SAL",
                table: "Booking",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TransferOwnershipDate",
                schema: "SAL",
                table: "Booking",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Booking_ReferContactID",
                schema: "SAL",
                table: "Booking",
                column: "ReferContactID");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Contact_ReferContactID",
                schema: "SAL",
                table: "Booking",
                column: "ReferContactID",
                principalSchema: "CTM",
                principalTable: "Contact",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_MasterCenter_SaleOfficerTypeMasterCenterID",
                schema: "SAL",
                table: "Booking",
                column: "SaleOfficerTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Contact_ReferContactID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_MasterCenter_SaleOfficerTypeMasterCenterID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_ReferContactID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "ReferContactID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "TransferOwnershipDate",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.RenameColumn(
                name: "SaleOfficerTypeMasterCenterID",
                schema: "SAL",
                table: "Booking",
                newName: "IntroducerContactID");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_SaleOfficerTypeMasterCenterID",
                schema: "SAL",
                table: "Booking",
                newName: "IX_Booking_IntroducerContactID");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Contact_IntroducerContactID",
                schema: "SAL",
                table: "Booking",
                column: "IntroducerContactID",
                principalSchema: "CTM",
                principalTable: "Contact",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
