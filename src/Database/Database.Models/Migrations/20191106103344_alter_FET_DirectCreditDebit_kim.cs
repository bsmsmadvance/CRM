using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class alter_FET_DirectCreditDebit_kim : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillPayment_MasterCenter_BillPaymentImportTypeID",
                schema: "FIN",
                table: "BillPayment");

            migrationBuilder.DropForeignKey(
                name: "FK_BillPaymentTransaction_MasterCenter_BillPaymentStatusID",
                schema: "FIN",
                table: "BillPaymentTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectCreditDebitApprovalForm_Booking_BookingID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm");

            migrationBuilder.DropForeignKey(
                name: "FK_FET_Contact_ContactID",
                schema: "FIN",
                table: "FET");

            migrationBuilder.DropIndex(
                name: "IX_FET_ContactID",
                schema: "FIN",
                table: "FET");

            migrationBuilder.DropColumn(
                name: "ContactID",
                schema: "FIN",
                table: "FET");

            migrationBuilder.DropColumn(
                name: "DeleteReason",
                schema: "FIN",
                table: "BillPaymentTransaction");

            migrationBuilder.RenameColumn(
                name: "PaymentDate",
                schema: "FIN",
                table: "BillPaymentTransaction",
                newName: "ReceiveDate");

            migrationBuilder.RenameColumn(
                name: "BillPaymentStatusID",
                schema: "FIN",
                table: "BillPaymentTransaction",
                newName: "BillPaymentStatusMasterCenterID");

            migrationBuilder.RenameIndex(
                name: "IX_BillPaymentTransaction_BillPaymentStatusID",
                schema: "FIN",
                table: "BillPaymentTransaction",
                newName: "IX_BillPaymentTransaction_BillPaymentStatusMasterCenterID");

            migrationBuilder.RenameColumn(
                name: "ImportDate",
                schema: "FIN",
                table: "BillPayment",
                newName: "ReceiveDate");

            migrationBuilder.RenameColumn(
                name: "BillPaymentImportTypeID",
                schema: "FIN",
                table: "BillPayment",
                newName: "BillPaymentImportTypeMasterCenterID");

            migrationBuilder.RenameIndex(
                name: "IX_BillPayment_BillPaymentImportTypeID",
                schema: "FIN",
                table: "BillPayment",
                newName: "IX_BillPayment_BillPaymentImportTypeMasterCenterID");

            migrationBuilder.RenameColumn(
                name: "UnitNumber",
                schema: "ACC",
                table: "PostGLDetail",
                newName: "UnitNo");

            migrationBuilder.RenameColumn(
                name: "ProjectCode",
                schema: "ACC",
                table: "PostGLDetail",
                newName: "TaxCode");

            migrationBuilder.RenameColumn(
                name: "ItemText",
                schema: "ACC",
                table: "PostGLDetail",
                newName: "ProjectNo");

            migrationBuilder.AddColumn<Guid>(
                name: "DirectCreditDebitExportDetailID",
                schema: "FIN",
                table: "PaymentDirectCreditDebit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                schema: "FIN",
                table: "FET",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "BookingID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BillPaymentDeleteReasonMasterCenterID",
                schema: "FIN",
                table: "BillPaymentTransaction",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentDirectCreditDebit_DirectCreditDebitExportDetailID",
                schema: "FIN",
                table: "PaymentDirectCreditDebit",
                column: "DirectCreditDebitExportDetailID");

            migrationBuilder.CreateIndex(
                name: "IX_BillPaymentTransaction_BillPaymentDeleteReasonMasterCenterID",
                schema: "FIN",
                table: "BillPaymentTransaction",
                column: "BillPaymentDeleteReasonMasterCenterID");

            migrationBuilder.AddForeignKey(
                name: "FK_BillPayment_MasterCenter_BillPaymentImportTypeMasterCenterID",
                schema: "FIN",
                table: "BillPayment",
                column: "BillPaymentImportTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_BillPaymentTransaction_MasterCenter_BillPaymentStatusMasterCenterID",
                schema: "FIN",
                table: "BillPaymentTransaction",
                column: "BillPaymentStatusMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectCreditDebitApprovalForm_Booking_BookingID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                column: "BookingID",
                principalSchema: "SAL",
                principalTable: "Booking",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentDirectCreditDebit_DirectCreditDebitExportDetail_DirectCreditDebitExportDetailID",
                schema: "FIN",
                table: "PaymentDirectCreditDebit",
                column: "DirectCreditDebitExportDetailID",
                principalSchema: "FIN",
                principalTable: "DirectCreditDebitExportDetail",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillPayment_MasterCenter_BillPaymentImportTypeMasterCenterID",
                schema: "FIN",
                table: "BillPayment");

            migrationBuilder.DropForeignKey(
                name: "FK_BillPaymentTransaction_MasterCenter_BillPaymentDeleteReasonMasterCenterID",
                schema: "FIN",
                table: "BillPaymentTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_BillPaymentTransaction_MasterCenter_BillPaymentStatusMasterCenterID",
                schema: "FIN",
                table: "BillPaymentTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectCreditDebitApprovalForm_Booking_BookingID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentDirectCreditDebit_DirectCreditDebitExportDetail_DirectCreditDebitExportDetailID",
                schema: "FIN",
                table: "PaymentDirectCreditDebit");

            migrationBuilder.DropIndex(
                name: "IX_PaymentDirectCreditDebit_DirectCreditDebitExportDetailID",
                schema: "FIN",
                table: "PaymentDirectCreditDebit");

            migrationBuilder.DropIndex(
                name: "IX_BillPaymentTransaction_BillPaymentDeleteReasonMasterCenterID",
                schema: "FIN",
                table: "BillPaymentTransaction");

            migrationBuilder.DropColumn(
                name: "DirectCreditDebitExportDetailID",
                schema: "FIN",
                table: "PaymentDirectCreditDebit");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                schema: "FIN",
                table: "FET");

            migrationBuilder.DropColumn(
                name: "BillPaymentDeleteReasonMasterCenterID",
                schema: "FIN",
                table: "BillPaymentTransaction");

            migrationBuilder.RenameColumn(
                name: "ReceiveDate",
                schema: "FIN",
                table: "BillPaymentTransaction",
                newName: "PaymentDate");

            migrationBuilder.RenameColumn(
                name: "BillPaymentStatusMasterCenterID",
                schema: "FIN",
                table: "BillPaymentTransaction",
                newName: "BillPaymentStatusID");

            migrationBuilder.RenameIndex(
                name: "IX_BillPaymentTransaction_BillPaymentStatusMasterCenterID",
                schema: "FIN",
                table: "BillPaymentTransaction",
                newName: "IX_BillPaymentTransaction_BillPaymentStatusID");

            migrationBuilder.RenameColumn(
                name: "ReceiveDate",
                schema: "FIN",
                table: "BillPayment",
                newName: "ImportDate");

            migrationBuilder.RenameColumn(
                name: "BillPaymentImportTypeMasterCenterID",
                schema: "FIN",
                table: "BillPayment",
                newName: "BillPaymentImportTypeID");

            migrationBuilder.RenameIndex(
                name: "IX_BillPayment_BillPaymentImportTypeMasterCenterID",
                schema: "FIN",
                table: "BillPayment",
                newName: "IX_BillPayment_BillPaymentImportTypeID");

            migrationBuilder.RenameColumn(
                name: "UnitNo",
                schema: "ACC",
                table: "PostGLDetail",
                newName: "UnitNumber");

            migrationBuilder.RenameColumn(
                name: "TaxCode",
                schema: "ACC",
                table: "PostGLDetail",
                newName: "ProjectCode");

            migrationBuilder.RenameColumn(
                name: "ProjectNo",
                schema: "ACC",
                table: "PostGLDetail",
                newName: "ItemText");

            migrationBuilder.AddColumn<Guid>(
                name: "ContactID",
                schema: "FIN",
                table: "FET",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "BookingID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<string>(
                name: "DeleteReason",
                schema: "FIN",
                table: "BillPaymentTransaction",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FET_ContactID",
                schema: "FIN",
                table: "FET",
                column: "ContactID");

            migrationBuilder.AddForeignKey(
                name: "FK_BillPayment_MasterCenter_BillPaymentImportTypeID",
                schema: "FIN",
                table: "BillPayment",
                column: "BillPaymentImportTypeID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BillPaymentTransaction_MasterCenter_BillPaymentStatusID",
                schema: "FIN",
                table: "BillPaymentTransaction",
                column: "BillPaymentStatusID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectCreditDebitApprovalForm_Booking_BookingID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                column: "BookingID",
                principalSchema: "SAL",
                principalTable: "Booking",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FET_Contact_ContactID",
                schema: "FIN",
                table: "FET",
                column: "ContactID",
                principalSchema: "CTM",
                principalTable: "Contact",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
