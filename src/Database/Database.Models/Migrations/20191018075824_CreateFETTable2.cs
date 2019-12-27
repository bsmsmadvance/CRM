using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class CreateFETTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DirectCreditDebitApprovalForm_MasterCenter_DirectApprovalFormStatusID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectCreditDebitApprovalForm_MasterCenter_DirectApprovalFormTypeID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectCreditDebitExportDetail_Booking_BookingID",
                schema: "FIN",
                table: "DirectCreditDebitExportDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectCreditDebitExportHeader_BankAccount_DirectFormTypeID",
                schema: "FIN",
                table: "DirectCreditDebitExportHeader");

            migrationBuilder.DropColumn(
                name: "IsComplete",
                schema: "FIN",
                table: "DirectCreditDebitExportDetail");

            migrationBuilder.DropColumn(
                name: "IsImport",
                schema: "FIN",
                table: "DirectCreditDebitExportDetail");

            migrationBuilder.RenameColumn(
                name: "DirectFormTypeID",
                schema: "FIN",
                table: "DirectCreditDebitExportHeader",
                newName: "DirectFormTypeMasterCenterID");

            migrationBuilder.RenameIndex(
                name: "IX_DirectCreditDebitExportHeader_DirectFormTypeID",
                schema: "FIN",
                table: "DirectCreditDebitExportHeader",
                newName: "IX_DirectCreditDebitExportHeader_DirectFormTypeMasterCenterID");

            migrationBuilder.RenameColumn(
                name: "BookingID",
                schema: "FIN",
                table: "DirectCreditDebitExportDetail",
                newName: "DirectCreditDebitExportDetailStatusMasterCenterID");

            migrationBuilder.RenameIndex(
                name: "IX_DirectCreditDebitExportDetail_BookingID",
                schema: "FIN",
                table: "DirectCreditDebitExportDetail",
                newName: "IX_DirectCreditDebitExportDetail_DirectCreditDebitExportDetailStatusMasterCenterID");

            migrationBuilder.RenameColumn(
                name: "DirectApprovalFormTypeID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                newName: "DirectApprovalFormTypeMasterCenterID");

            migrationBuilder.RenameColumn(
                name: "DirectApprovalFormStatusID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                newName: "DirectApprovalFormStatusMasterCenterID");

            migrationBuilder.RenameIndex(
                name: "IX_DirectCreditDebitApprovalForm_DirectApprovalFormTypeID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                newName: "IX_DirectCreditDebitApprovalForm_DirectApprovalFormTypeMasterCenterID");

            migrationBuilder.RenameIndex(
                name: "IX_DirectCreditDebitApprovalForm_DirectApprovalFormStatusID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                newName: "IX_DirectCreditDebitApprovalForm_DirectApprovalFormStatusMasterCenterID");

            migrationBuilder.AddColumn<string>(
                name: "CancelRemark",
                schema: "FIN",
                table: "DirectCreditDebitExportHeader",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReceiveDate",
                schema: "FIN",
                table: "DirectCreditDebitExportHeader",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "TotalErrorRecord",
                schema: "FIN",
                table: "DirectCreditDebitExportHeader",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectCreditDebitApprovalForm_MasterCenter_DirectApprovalFormStatusMasterCenterID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                column: "DirectApprovalFormStatusMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectCreditDebitApprovalForm_MasterCenter_DirectApprovalFormTypeMasterCenterID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                column: "DirectApprovalFormTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectCreditDebitExportDetail_MasterCenter_DirectCreditDebitExportDetailStatusMasterCenterID",
                schema: "FIN",
                table: "DirectCreditDebitExportDetail",
                column: "DirectCreditDebitExportDetailStatusMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectCreditDebitExportHeader_MasterCenter_DirectFormTypeMasterCenterID",
                schema: "FIN",
                table: "DirectCreditDebitExportHeader",
                column: "DirectFormTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DirectCreditDebitApprovalForm_MasterCenter_DirectApprovalFormStatusMasterCenterID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectCreditDebitApprovalForm_MasterCenter_DirectApprovalFormTypeMasterCenterID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectCreditDebitExportDetail_MasterCenter_DirectCreditDebitExportDetailStatusMasterCenterID",
                schema: "FIN",
                table: "DirectCreditDebitExportDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectCreditDebitExportHeader_MasterCenter_DirectFormTypeMasterCenterID",
                schema: "FIN",
                table: "DirectCreditDebitExportHeader");

            migrationBuilder.DropColumn(
                name: "CancelRemark",
                schema: "FIN",
                table: "DirectCreditDebitExportHeader");

            migrationBuilder.DropColumn(
                name: "ReceiveDate",
                schema: "FIN",
                table: "DirectCreditDebitExportHeader");

            migrationBuilder.DropColumn(
                name: "TotalErrorRecord",
                schema: "FIN",
                table: "DirectCreditDebitExportHeader");

            migrationBuilder.RenameColumn(
                name: "DirectFormTypeMasterCenterID",
                schema: "FIN",
                table: "DirectCreditDebitExportHeader",
                newName: "DirectFormTypeID");

            migrationBuilder.RenameIndex(
                name: "IX_DirectCreditDebitExportHeader_DirectFormTypeMasterCenterID",
                schema: "FIN",
                table: "DirectCreditDebitExportHeader",
                newName: "IX_DirectCreditDebitExportHeader_DirectFormTypeID");

            migrationBuilder.RenameColumn(
                name: "DirectCreditDebitExportDetailStatusMasterCenterID",
                schema: "FIN",
                table: "DirectCreditDebitExportDetail",
                newName: "BookingID");

            migrationBuilder.RenameIndex(
                name: "IX_DirectCreditDebitExportDetail_DirectCreditDebitExportDetailStatusMasterCenterID",
                schema: "FIN",
                table: "DirectCreditDebitExportDetail",
                newName: "IX_DirectCreditDebitExportDetail_BookingID");

            migrationBuilder.RenameColumn(
                name: "DirectApprovalFormTypeMasterCenterID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                newName: "DirectApprovalFormTypeID");

            migrationBuilder.RenameColumn(
                name: "DirectApprovalFormStatusMasterCenterID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                newName: "DirectApprovalFormStatusID");

            migrationBuilder.RenameIndex(
                name: "IX_DirectCreditDebitApprovalForm_DirectApprovalFormTypeMasterCenterID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                newName: "IX_DirectCreditDebitApprovalForm_DirectApprovalFormTypeID");

            migrationBuilder.RenameIndex(
                name: "IX_DirectCreditDebitApprovalForm_DirectApprovalFormStatusMasterCenterID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                newName: "IX_DirectCreditDebitApprovalForm_DirectApprovalFormStatusID");

            migrationBuilder.AddColumn<bool>(
                name: "IsComplete",
                schema: "FIN",
                table: "DirectCreditDebitExportDetail",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsImport",
                schema: "FIN",
                table: "DirectCreditDebitExportDetail",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectCreditDebitApprovalForm_MasterCenter_DirectApprovalFormStatusID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                column: "DirectApprovalFormStatusID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectCreditDebitApprovalForm_MasterCenter_DirectApprovalFormTypeID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                column: "DirectApprovalFormTypeID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectCreditDebitExportDetail_Booking_BookingID",
                schema: "FIN",
                table: "DirectCreditDebitExportDetail",
                column: "BookingID",
                principalSchema: "SAL",
                principalTable: "Booking",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectCreditDebitExportHeader_BankAccount_DirectFormTypeID",
                schema: "FIN",
                table: "DirectCreditDebitExportHeader",
                column: "DirectFormTypeID",
                principalSchema: "MST",
                principalTable: "BankAccount",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
