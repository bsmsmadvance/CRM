using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddMasterMinPriceAndMasterBudgetProToBooking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "MasterMinPrice",
                schema: "SAL",
                table: "Booking",
                type: "Money",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MasterSaleBudgetPromotion",
                schema: "SAL",
                table: "Booking",
                type: "Money",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DirectPeriod",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_FET_RejectByUserID",
                schema: "FIN",
                table: "FET",
                column: "RejectByUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_FET_User_RejectByUserID",
                schema: "FIN",
                table: "FET",
                column: "RejectByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FET_User_RejectByUserID",
                schema: "FIN",
                table: "FET");

            migrationBuilder.DropIndex(
                name: "IX_FET_RejectByUserID",
                schema: "FIN",
                table: "FET");

            migrationBuilder.DropColumn(
                name: "MasterMinPrice",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "MasterSaleBudgetPromotion",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.AlterColumn<int>(
                name: "DirectPeriod",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
