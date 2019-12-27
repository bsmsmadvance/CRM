using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class RemoveBookingFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Booking_ChangeFromBookingID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Booking_ChangeToBookingID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_ChangeFromBookingID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_ChangeToBookingID",
                schema: "SAL",
                table: "Booking");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Booking_ChangeFromBookingID",
                schema: "SAL",
                table: "Booking",
                column: "ChangeFromBookingID",
                unique: true,
                filter: "[ChangeFromBookingID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_ChangeToBookingID",
                schema: "SAL",
                table: "Booking",
                column: "ChangeToBookingID");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Booking_ChangeFromBookingID",
                schema: "SAL",
                table: "Booking",
                column: "ChangeFromBookingID",
                principalSchema: "SAL",
                principalTable: "Booking",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Booking_ChangeToBookingID",
                schema: "SAL",
                table: "Booking",
                column: "ChangeToBookingID",
                principalSchema: "SAL",
                principalTable: "Booking",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
