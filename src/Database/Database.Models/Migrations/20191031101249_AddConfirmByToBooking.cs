using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddConfirmByToBooking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ConfirmByID",
                schema: "SAL",
                table: "Booking",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ConfirmByUserID",
                schema: "SAL",
                table: "Booking",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ConfirmDate",
                schema: "SAL",
                table: "Booking",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectID",
                schema: "FIN",
                table: "FET",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Booking_ConfirmByID",
                schema: "SAL",
                table: "Booking",
                column: "ConfirmByID");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_User_ConfirmByID",
                schema: "SAL",
                table: "Booking",
                column: "ConfirmByID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_User_ConfirmByID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_ConfirmByID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "ConfirmByID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "ConfirmByUserID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "ConfirmDate",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "ProjectID",
                schema: "FIN",
                table: "FET");
        }
    }
}
