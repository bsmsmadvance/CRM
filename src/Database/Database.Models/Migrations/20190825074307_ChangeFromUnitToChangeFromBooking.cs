using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ChangeFromUnitToChangeFromBooking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Unit_ChangeFromUnitID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Unit_ChangeToUnitID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.RenameColumn(
                name: "ChangeToUnitID",
                schema: "SAL",
                table: "Booking",
                newName: "ChangeToBookingID");

            migrationBuilder.RenameColumn(
                name: "ChangeFromUnitID",
                schema: "SAL",
                table: "Booking",
                newName: "ChangeFromBookingID");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_ChangeToUnitID",
                schema: "SAL",
                table: "Booking",
                newName: "IX_Booking_ChangeToBookingID");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_ChangeFromUnitID",
                schema: "SAL",
                table: "Booking",
                newName: "IX_Booking_ChangeFromBookingID");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Unit_ChangeFromBookingID",
                schema: "SAL",
                table: "Booking",
                column: "ChangeFromBookingID",
                principalSchema: "PRJ",
                principalTable: "Unit",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Unit_ChangeToBookingID",
                schema: "SAL",
                table: "Booking",
                column: "ChangeToBookingID",
                principalSchema: "PRJ",
                principalTable: "Unit",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Unit_ChangeFromBookingID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Unit_ChangeToBookingID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.RenameColumn(
                name: "ChangeToBookingID",
                schema: "SAL",
                table: "Booking",
                newName: "ChangeToUnitID");

            migrationBuilder.RenameColumn(
                name: "ChangeFromBookingID",
                schema: "SAL",
                table: "Booking",
                newName: "ChangeFromUnitID");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_ChangeToBookingID",
                schema: "SAL",
                table: "Booking",
                newName: "IX_Booking_ChangeToUnitID");

            migrationBuilder.RenameIndex(
                name: "IX_Booking_ChangeFromBookingID",
                schema: "SAL",
                table: "Booking",
                newName: "IX_Booking_ChangeFromUnitID");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Unit_ChangeFromUnitID",
                schema: "SAL",
                table: "Booking",
                column: "ChangeFromUnitID",
                principalSchema: "PRJ",
                principalTable: "Unit",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Unit_ChangeToUnitID",
                schema: "SAL",
                table: "Booking",
                column: "ChangeToUnitID",
                principalSchema: "PRJ",
                principalTable: "Unit",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
