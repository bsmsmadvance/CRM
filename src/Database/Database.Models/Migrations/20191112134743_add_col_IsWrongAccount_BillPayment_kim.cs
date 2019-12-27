using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class add_col_IsWrongAccount_BillPayment_kim : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillPaymentTransaction_MasterCenter_BillPaymentDeleteReasonMasterCenterID",
                schema: "FIN",
                table: "BillPaymentTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_BillPaymentTransaction_BillPayment_BillPaymentHeaderID",
                schema: "FIN",
                table: "BillPaymentTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_BillPaymentTransaction_MasterCenter_BillPaymentStatusMasterCenterID",
                schema: "FIN",
                table: "BillPaymentTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_BillPaymentTransaction_Booking_BookingID",
                schema: "FIN",
                table: "BillPaymentTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_BillPaymentTransaction_User_CreatedByUserID",
                schema: "FIN",
                table: "BillPaymentTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_BillPaymentTransaction_User_UpdatedByUserID",
                schema: "FIN",
                table: "BillPaymentTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentBillPayment_BillPaymentTransaction_BillPaymentTransactionID",
                schema: "FIN",
                table: "PaymentBillPayment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BillPaymentTransaction",
                schema: "FIN",
                table: "BillPaymentTransaction");

            migrationBuilder.RenameTable(
                name: "BillPaymentTransaction",
                schema: "FIN",
                newName: "BillPaymentDetail",
                newSchema: "FIN");

            migrationBuilder.RenameColumn(
                name: "BillPaymentTransactionID",
                schema: "FIN",
                table: "PaymentBillPayment",
                newName: "BillPaymentDetailID");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentBillPayment_BillPaymentTransactionID",
                schema: "FIN",
                table: "PaymentBillPayment",
                newName: "IX_PaymentBillPayment_BillPaymentDetailID");

            migrationBuilder.RenameIndex(
                name: "IX_BillPaymentTransaction_UpdatedByUserID",
                schema: "FIN",
                table: "BillPaymentDetail",
                newName: "IX_BillPaymentDetail_UpdatedByUserID");

            migrationBuilder.RenameIndex(
                name: "IX_BillPaymentTransaction_CreatedByUserID",
                schema: "FIN",
                table: "BillPaymentDetail",
                newName: "IX_BillPaymentDetail_CreatedByUserID");

            migrationBuilder.RenameIndex(
                name: "IX_BillPaymentTransaction_BookingID",
                schema: "FIN",
                table: "BillPaymentDetail",
                newName: "IX_BillPaymentDetail_BookingID");

            migrationBuilder.RenameIndex(
                name: "IX_BillPaymentTransaction_BillPaymentStatusMasterCenterID",
                schema: "FIN",
                table: "BillPaymentDetail",
                newName: "IX_BillPaymentDetail_BillPaymentStatusMasterCenterID");

            migrationBuilder.RenameIndex(
                name: "IX_BillPaymentTransaction_BillPaymentHeaderID",
                schema: "FIN",
                table: "BillPaymentDetail",
                newName: "IX_BillPaymentDetail_BillPaymentHeaderID");

            migrationBuilder.RenameIndex(
                name: "IX_BillPaymentTransaction_BillPaymentDeleteReasonMasterCenterID",
                schema: "FIN",
                table: "BillPaymentDetail",
                newName: "IX_BillPaymentDetail_BillPaymentDeleteReasonMasterCenterID");

            migrationBuilder.AddColumn<bool>(
                name: "IsWrongAccount",
                schema: "FIN",
                table: "PaymentBillPayment",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsWrongAccount",
                schema: "FIN",
                table: "BillPaymentDetail",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BillPaymentDetail",
                schema: "FIN",
                table: "BillPaymentDetail",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BillPaymentDetail_MasterCenter_BillPaymentDeleteReasonMasterCenterID",
                schema: "FIN",
                table: "BillPaymentDetail",
                column: "BillPaymentDeleteReasonMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BillPaymentDetail_BillPayment_BillPaymentHeaderID",
                schema: "FIN",
                table: "BillPaymentDetail",
                column: "BillPaymentHeaderID",
                principalSchema: "FIN",
                principalTable: "BillPayment",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BillPaymentDetail_MasterCenter_BillPaymentStatusMasterCenterID",
                schema: "FIN",
                table: "BillPaymentDetail",
                column: "BillPaymentStatusMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BillPaymentDetail_Booking_BookingID",
                schema: "FIN",
                table: "BillPaymentDetail",
                column: "BookingID",
                principalSchema: "SAL",
                principalTable: "Booking",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BillPaymentDetail_User_CreatedByUserID",
                schema: "FIN",
                table: "BillPaymentDetail",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BillPaymentDetail_User_UpdatedByUserID",
                schema: "FIN",
                table: "BillPaymentDetail",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentBillPayment_BillPaymentDetail_BillPaymentDetailID",
                schema: "FIN",
                table: "PaymentBillPayment",
                column: "BillPaymentDetailID",
                principalSchema: "FIN",
                principalTable: "BillPaymentDetail",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillPaymentDetail_MasterCenter_BillPaymentDeleteReasonMasterCenterID",
                schema: "FIN",
                table: "BillPaymentDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_BillPaymentDetail_BillPayment_BillPaymentHeaderID",
                schema: "FIN",
                table: "BillPaymentDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_BillPaymentDetail_MasterCenter_BillPaymentStatusMasterCenterID",
                schema: "FIN",
                table: "BillPaymentDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_BillPaymentDetail_Booking_BookingID",
                schema: "FIN",
                table: "BillPaymentDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_BillPaymentDetail_User_CreatedByUserID",
                schema: "FIN",
                table: "BillPaymentDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_BillPaymentDetail_User_UpdatedByUserID",
                schema: "FIN",
                table: "BillPaymentDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentBillPayment_BillPaymentDetail_BillPaymentDetailID",
                schema: "FIN",
                table: "PaymentBillPayment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BillPaymentDetail",
                schema: "FIN",
                table: "BillPaymentDetail");

            migrationBuilder.DropColumn(
                name: "IsWrongAccount",
                schema: "FIN",
                table: "PaymentBillPayment");

            migrationBuilder.DropColumn(
                name: "IsWrongAccount",
                schema: "FIN",
                table: "BillPaymentDetail");

            migrationBuilder.RenameTable(
                name: "BillPaymentDetail",
                schema: "FIN",
                newName: "BillPaymentTransaction",
                newSchema: "FIN");

            migrationBuilder.RenameColumn(
                name: "BillPaymentDetailID",
                schema: "FIN",
                table: "PaymentBillPayment",
                newName: "BillPaymentTransactionID");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentBillPayment_BillPaymentDetailID",
                schema: "FIN",
                table: "PaymentBillPayment",
                newName: "IX_PaymentBillPayment_BillPaymentTransactionID");

            migrationBuilder.RenameIndex(
                name: "IX_BillPaymentDetail_UpdatedByUserID",
                schema: "FIN",
                table: "BillPaymentTransaction",
                newName: "IX_BillPaymentTransaction_UpdatedByUserID");

            migrationBuilder.RenameIndex(
                name: "IX_BillPaymentDetail_CreatedByUserID",
                schema: "FIN",
                table: "BillPaymentTransaction",
                newName: "IX_BillPaymentTransaction_CreatedByUserID");

            migrationBuilder.RenameIndex(
                name: "IX_BillPaymentDetail_BookingID",
                schema: "FIN",
                table: "BillPaymentTransaction",
                newName: "IX_BillPaymentTransaction_BookingID");

            migrationBuilder.RenameIndex(
                name: "IX_BillPaymentDetail_BillPaymentStatusMasterCenterID",
                schema: "FIN",
                table: "BillPaymentTransaction",
                newName: "IX_BillPaymentTransaction_BillPaymentStatusMasterCenterID");

            migrationBuilder.RenameIndex(
                name: "IX_BillPaymentDetail_BillPaymentHeaderID",
                schema: "FIN",
                table: "BillPaymentTransaction",
                newName: "IX_BillPaymentTransaction_BillPaymentHeaderID");

            migrationBuilder.RenameIndex(
                name: "IX_BillPaymentDetail_BillPaymentDeleteReasonMasterCenterID",
                schema: "FIN",
                table: "BillPaymentTransaction",
                newName: "IX_BillPaymentTransaction_BillPaymentDeleteReasonMasterCenterID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BillPaymentTransaction",
                schema: "FIN",
                table: "BillPaymentTransaction",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BillPaymentTransaction_MasterCenter_BillPaymentDeleteReasonMasterCenterID",
                schema: "FIN",
                table: "BillPaymentTransaction",
                column: "BillPaymentDeleteReasonMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BillPaymentTransaction_BillPayment_BillPaymentHeaderID",
                schema: "FIN",
                table: "BillPaymentTransaction",
                column: "BillPaymentHeaderID",
                principalSchema: "FIN",
                principalTable: "BillPayment",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BillPaymentTransaction_MasterCenter_BillPaymentStatusMasterCenterID",
                schema: "FIN",
                table: "BillPaymentTransaction",
                column: "BillPaymentStatusMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BillPaymentTransaction_Booking_BookingID",
                schema: "FIN",
                table: "BillPaymentTransaction",
                column: "BookingID",
                principalSchema: "SAL",
                principalTable: "Booking",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BillPaymentTransaction_User_CreatedByUserID",
                schema: "FIN",
                table: "BillPaymentTransaction",
                column: "CreatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BillPaymentTransaction_User_UpdatedByUserID",
                schema: "FIN",
                table: "BillPaymentTransaction",
                column: "UpdatedByUserID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentBillPayment_BillPaymentTransaction_BillPaymentTransactionID",
                schema: "FIN",
                table: "PaymentBillPayment",
                column: "BillPaymentTransactionID",
                principalSchema: "FIN",
                principalTable: "BillPaymentTransaction",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
