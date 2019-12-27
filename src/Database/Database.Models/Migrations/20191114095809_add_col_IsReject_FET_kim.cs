using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class add_col_IsReject_FET_kim : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillPaymentTransactionTemp_MasterCenter_BillPaymentDeleteReasonMasterCenterID",
                schema: "FIN",
                table: "BillPaymentTransactionTemp");

            migrationBuilder.DropForeignKey(
                name: "FK_BillPaymentTransactionTemp_BillPaymentTemp_BillPaymentHeaderID",
                schema: "FIN",
                table: "BillPaymentTransactionTemp");

            migrationBuilder.DropForeignKey(
                name: "FK_BillPaymentTransactionTemp_MasterCenter_BillPaymentStatusMasterCenterID",
                schema: "FIN",
                table: "BillPaymentTransactionTemp");

            migrationBuilder.DropForeignKey(
                name: "FK_BillPaymentTransactionTemp_Booking_BookingID",
                schema: "FIN",
                table: "BillPaymentTransactionTemp");

            migrationBuilder.DropForeignKey(
                name: "FK_BillPaymentTransactionTemp_User_CreatedByUserID",
                schema: "FIN",
                table: "BillPaymentTransactionTemp");

            migrationBuilder.DropForeignKey(
                name: "FK_BillPaymentTransactionTemp_User_UpdatedByUserID",
                schema: "FIN",
                table: "BillPaymentTransactionTemp");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BillPaymentTransactionTemp",
                schema: "FIN",
                table: "BillPaymentTransactionTemp");

            migrationBuilder.RenameTable(
                name: "BillPaymentTransactionTemp",
                schema: "FIN",
                newName: "BillPaymentDetailTemp",
                newSchema: "FIN");

            migrationBuilder.RenameIndex(
                name: "IX_BillPaymentTransactionTemp_UpdatedByUserID",
                schema: "FIN",
                table: "BillPaymentDetailTemp",
                newName: "IX_BillPaymentDetailTemp_UpdatedByUserID");

            migrationBuilder.RenameIndex(
                name: "IX_BillPaymentTransactionTemp_CreatedByUserID",
                schema: "FIN",
                table: "BillPaymentDetailTemp",
                newName: "IX_BillPaymentDetailTemp_CreatedByUserID");

            migrationBuilder.RenameIndex(
                name: "IX_BillPaymentTransactionTemp_BookingID",
                schema: "FIN",
                table: "BillPaymentDetailTemp",
                newName: "IX_BillPaymentDetailTemp_BookingID");

            migrationBuilder.RenameIndex(
                name: "IX_BillPaymentTransactionTemp_BillPaymentStatusMasterCenterID",
                schema: "FIN",
                table: "BillPaymentDetailTemp",
                newName: "IX_BillPaymentDetailTemp_BillPaymentStatusMasterCenterID");

            migrationBuilder.RenameIndex(
                name: "IX_BillPaymentTransactionTemp_BillPaymentHeaderID",
                schema: "FIN",
                table: "BillPaymentDetailTemp",
                newName: "IX_BillPaymentDetailTemp_BillPaymentHeaderID");

            migrationBuilder.RenameIndex(
                name: "IX_BillPaymentTransactionTemp_BillPaymentDeleteReasonMasterCenterID",
                schema: "FIN",
                table: "BillPaymentDetailTemp",
                newName: "IX_BillPaymentDetailTemp_BillPaymentDeleteReasonMasterCenterID");

            migrationBuilder.AddColumn<bool>(
                name: "IsReject",
                schema: "FIN",
                table: "FET",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsWrongAccount",
                schema: "FIN",
                table: "BillPaymentDetailTemp",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BillPaymentDetailTemp",
                schema: "FIN",
                table: "BillPaymentDetailTemp",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BillPaymentDetailTemp_MasterCenter_BillPaymentDeleteReasonMasterCenterID",
                schema: "FIN",
                table: "BillPaymentDetailTemp",
                column: "BillPaymentDeleteReasonMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BillPaymentDetailTemp_BillPaymentTemp_BillPaymentHeaderID",
                schema: "FIN",
                table: "BillPaymentDetailTemp",
                column: "BillPaymentHeaderID",
                principalSchema: "FIN",
                principalTable: "BillPaymentTemp",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BillPaymentDetailTemp_MasterCenter_BillPaymentStatusMasterCenterID",
                schema: "FIN",
                table: "BillPaymentDetailTemp",
                column: "BillPaymentStatusMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BillPaymentDetailTemp_Booking_BookingID",
                schema: "FIN",
                table: "BillPaymentDetailTemp",
                column: "BookingID",
                principalSchema: "SAL",
                principalTable: "Booking",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BillPaymentDetailTemp_User_CreatedByUserID",
                schema: "FIN",
                table: "BillPaymentDetailTemp",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BillPaymentDetailTemp_User_UpdatedByUserID",
                schema: "FIN",
                table: "BillPaymentDetailTemp",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillPaymentDetailTemp_MasterCenter_BillPaymentDeleteReasonMasterCenterID",
                schema: "FIN",
                table: "BillPaymentDetailTemp");

            migrationBuilder.DropForeignKey(
                name: "FK_BillPaymentDetailTemp_BillPaymentTemp_BillPaymentHeaderID",
                schema: "FIN",
                table: "BillPaymentDetailTemp");

            migrationBuilder.DropForeignKey(
                name: "FK_BillPaymentDetailTemp_MasterCenter_BillPaymentStatusMasterCenterID",
                schema: "FIN",
                table: "BillPaymentDetailTemp");

            migrationBuilder.DropForeignKey(
                name: "FK_BillPaymentDetailTemp_Booking_BookingID",
                schema: "FIN",
                table: "BillPaymentDetailTemp");

            migrationBuilder.DropForeignKey(
                name: "FK_BillPaymentDetailTemp_User_CreatedByUserID",
                schema: "FIN",
                table: "BillPaymentDetailTemp");

            migrationBuilder.DropForeignKey(
                name: "FK_BillPaymentDetailTemp_User_UpdatedByUserID",
                schema: "FIN",
                table: "BillPaymentDetailTemp");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BillPaymentDetailTemp",
                schema: "FIN",
                table: "BillPaymentDetailTemp");

            migrationBuilder.DropColumn(
                name: "IsReject",
                schema: "FIN",
                table: "FET");

            migrationBuilder.DropColumn(
                name: "IsWrongAccount",
                schema: "FIN",
                table: "BillPaymentDetailTemp");

            migrationBuilder.RenameTable(
                name: "BillPaymentDetailTemp",
                schema: "FIN",
                newName: "BillPaymentTransactionTemp",
                newSchema: "FIN");

            migrationBuilder.RenameIndex(
                name: "IX_BillPaymentDetailTemp_UpdatedByUserID",
                schema: "FIN",
                table: "BillPaymentTransactionTemp",
                newName: "IX_BillPaymentTransactionTemp_UpdatedByUserID");

            migrationBuilder.RenameIndex(
                name: "IX_BillPaymentDetailTemp_CreatedByUserID",
                schema: "FIN",
                table: "BillPaymentTransactionTemp",
                newName: "IX_BillPaymentTransactionTemp_CreatedByUserID");

            migrationBuilder.RenameIndex(
                name: "IX_BillPaymentDetailTemp_BookingID",
                schema: "FIN",
                table: "BillPaymentTransactionTemp",
                newName: "IX_BillPaymentTransactionTemp_BookingID");

            migrationBuilder.RenameIndex(
                name: "IX_BillPaymentDetailTemp_BillPaymentStatusMasterCenterID",
                schema: "FIN",
                table: "BillPaymentTransactionTemp",
                newName: "IX_BillPaymentTransactionTemp_BillPaymentStatusMasterCenterID");

            migrationBuilder.RenameIndex(
                name: "IX_BillPaymentDetailTemp_BillPaymentHeaderID",
                schema: "FIN",
                table: "BillPaymentTransactionTemp",
                newName: "IX_BillPaymentTransactionTemp_BillPaymentHeaderID");

            migrationBuilder.RenameIndex(
                name: "IX_BillPaymentDetailTemp_BillPaymentDeleteReasonMasterCenterID",
                schema: "FIN",
                table: "BillPaymentTransactionTemp",
                newName: "IX_BillPaymentTransactionTemp_BillPaymentDeleteReasonMasterCenterID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BillPaymentTransactionTemp",
                schema: "FIN",
                table: "BillPaymentTransactionTemp",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BillPaymentTransactionTemp_MasterCenter_BillPaymentDeleteReasonMasterCenterID",
                schema: "FIN",
                table: "BillPaymentTransactionTemp",
                column: "BillPaymentDeleteReasonMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BillPaymentTransactionTemp_BillPaymentTemp_BillPaymentHeaderID",
                schema: "FIN",
                table: "BillPaymentTransactionTemp",
                column: "BillPaymentHeaderID",
                principalSchema: "FIN",
                principalTable: "BillPaymentTemp",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BillPaymentTransactionTemp_MasterCenter_BillPaymentStatusMasterCenterID",
                schema: "FIN",
                table: "BillPaymentTransactionTemp",
                column: "BillPaymentStatusMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BillPaymentTransactionTemp_Booking_BookingID",
                schema: "FIN",
                table: "BillPaymentTransactionTemp",
                column: "BookingID",
                principalSchema: "SAL",
                principalTable: "Booking",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BillPaymentTransactionTemp_User_CreatedByUserID",
                schema: "FIN",
                table: "BillPaymentTransactionTemp",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BillPaymentTransactionTemp_User_UpdatedByUserID",
                schema: "FIN",
                table: "BillPaymentTransactionTemp",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
