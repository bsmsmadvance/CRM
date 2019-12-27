using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class CreateFETTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptSendEmailHistory_Receipt_ReceiptID",
                schema: "FIN",
                table: "ReceiptSendEmailHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptSendEmailHistory_Contact_SendToContactID",
                schema: "FIN",
                table: "ReceiptSendEmailHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptSendPrintingHistory_Receipt_ReceiptID",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory");

            migrationBuilder.DropIndex(
                name: "IX_ReceiptSendEmailHistory_SendToContactID",
                schema: "FIN",
                table: "ReceiptSendEmailHistory");

            migrationBuilder.DropColumn(
                name: "UnknowPaymentStatus",
                schema: "FIN",
                table: "UnknownPayment");

            migrationBuilder.DropColumn(
                name: "SendToContactID",
                schema: "FIN",
                table: "ReceiptSendEmailHistory");

            migrationBuilder.DropColumn(
                name: "DirectFormType",
                schema: "FIN",
                table: "DirectCreditDebitExportHeader");

            migrationBuilder.DropColumn(
                name: "FormStatus",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm");

            migrationBuilder.DropColumn(
                name: "FormType",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm");

            migrationBuilder.DropColumn(
                name: "BillPaymentStatus",
                schema: "FIN",
                table: "BillPaymentTransaction");

            migrationBuilder.RenameColumn(
                name: "ReceiptID",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory",
                newName: "ReceiptHeaderID");

            migrationBuilder.RenameIndex(
                name: "IX_ReceiptSendPrintingHistory_ReceiptID",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory",
                newName: "IX_ReceiptSendPrintingHistory_ReceiptHeaderID");

            migrationBuilder.RenameColumn(
                name: "ReceiptID",
                schema: "FIN",
                table: "ReceiptSendEmailHistory",
                newName: "ReceiptHeaderID");

            migrationBuilder.RenameIndex(
                name: "IX_ReceiptSendEmailHistory_ReceiptID",
                schema: "FIN",
                table: "ReceiptSendEmailHistory",
                newName: "IX_ReceiptSendEmailHistory_ReceiptHeaderID");

            migrationBuilder.AddColumn<Guid>(
                name: "UnknowPaymentStatusID",
                schema: "FIN",
                table: "UnknownPayment",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExportDate",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LotNo",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SendDate",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "TotalRecord",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "SendDate",
                schema: "FIN",
                table: "ReceiptSendEmailHistory",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "DirectFormTypeID",
                schema: "FIN",
                table: "DirectCreditDebitExportHeader",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DirectApprovalFormStatusID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DirectApprovalFormTypeID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BillPaymentStatusID",
                schema: "FIN",
                table: "BillPaymentTransaction",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BillPaymentImportTypeID",
                schema: "FIN",
                table: "BillPayment",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FormatTextFileID",
                schema: "ACC",
                table: "PostGLAccount",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PostGLFormatTextFile",
                schema: "ACC",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsFromMigration = table.Column<bool>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false),
                    PostingType = table.Column<string>(maxLength: 10, nullable: true),
                    PostingKey = table.Column<string>(maxLength: 10, nullable: true),
                    Seq = table.Column<int>(nullable: false),
                    ColumnName = table.Column<string>(maxLength: 50, nullable: true),
                    FixValue = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostGLFormatTextFile", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PostGLFormatTextFile_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostGLFormatTextFile_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptHeader",
                schema: "FIN",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsFromMigration = table.Column<bool>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false),
                    ReceiptNo = table.Column<string>(maxLength: 100, nullable: true),
                    PaymentID = table.Column<Guid>(nullable: true),
                    CompanyNameTH = table.Column<string>(maxLength: 1000, nullable: true),
                    CompanyNameEN = table.Column<string>(maxLength: 1000, nullable: true),
                    CompanyHouseNoTH = table.Column<string>(maxLength: 1000, nullable: true),
                    CompanyHouseNoEN = table.Column<string>(maxLength: 1000, nullable: true),
                    CompanyBuildingTH = table.Column<string>(maxLength: 1000, nullable: true),
                    CompanyBuildingEN = table.Column<string>(maxLength: 1000, nullable: true),
                    CompanySoiTH = table.Column<string>(maxLength: 1000, nullable: true),
                    CompanySoiEN = table.Column<string>(maxLength: 1000, nullable: true),
                    CompanyRoadTH = table.Column<string>(maxLength: 1000, nullable: true),
                    CompanyRoadEN = table.Column<string>(maxLength: 1000, nullable: true),
                    CompanyProvinceEN = table.Column<string>(maxLength: 1000, nullable: true),
                    CompanyProvinceTH = table.Column<string>(maxLength: 1000, nullable: true),
                    CompanyDistrictEN = table.Column<string>(maxLength: 1000, nullable: true),
                    CompanyDistrictTH = table.Column<string>(maxLength: 1000, nullable: true),
                    CompanySubDistrictEN = table.Column<string>(maxLength: 1000, nullable: true),
                    CompanySubDistrictTH = table.Column<string>(maxLength: 1000, nullable: true),
                    CompanyPostalCode = table.Column<string>(maxLength: 50, nullable: true),
                    CompanyTelephone = table.Column<string>(maxLength: 50, nullable: true),
                    CompanyFax = table.Column<string>(maxLength: 50, nullable: true),
                    IsForeigner = table.Column<bool>(nullable: false),
                    SendToContactID = table.Column<Guid>(nullable: true),
                    ContactTitle = table.Column<string>(maxLength: 100, nullable: true),
                    ContactFirstNameTH = table.Column<string>(maxLength: 100, nullable: true),
                    ContactMiddleNameTH = table.Column<string>(maxLength: 100, nullable: true),
                    ContactLastNameTH = table.Column<string>(maxLength: 100, nullable: true),
                    ContactTitleExtEN = table.Column<string>(maxLength: 100, nullable: true),
                    ContactFirstNameEN = table.Column<string>(maxLength: 100, nullable: true),
                    ContactMiddleNameEN = table.Column<string>(maxLength: 100, nullable: true),
                    ContactLastNameEN = table.Column<string>(maxLength: 100, nullable: true),
                    ContactHouseNoTH = table.Column<string>(maxLength: 100, nullable: true),
                    ContactMooTH = table.Column<string>(maxLength: 100, nullable: true),
                    ContactVillageTH = table.Column<string>(maxLength: 1000, nullable: true),
                    ContactSoiTH = table.Column<string>(maxLength: 100, nullable: true),
                    ContactRoadTH = table.Column<string>(maxLength: 100, nullable: true),
                    ContactHouseNoEN = table.Column<string>(maxLength: 100, nullable: true),
                    ContactMooEN = table.Column<string>(maxLength: 100, nullable: true),
                    ContactVillageEN = table.Column<string>(maxLength: 1000, nullable: true),
                    ContactSoiEN = table.Column<string>(maxLength: 100, nullable: true),
                    ContactRoadEN = table.Column<string>(maxLength: 100, nullable: true),
                    ContactPostalCode = table.Column<string>(maxLength: 50, nullable: true),
                    ContactCountryTH = table.Column<string>(maxLength: 1000, nullable: true),
                    ContactCountryEN = table.Column<string>(maxLength: 1000, nullable: true),
                    ContactProvinceTH = table.Column<string>(maxLength: 1000, nullable: true),
                    ContactProvinceEN = table.Column<string>(maxLength: 1000, nullable: true),
                    ContactDistrictTH = table.Column<string>(maxLength: 1000, nullable: true),
                    ContactDistrictEN = table.Column<string>(maxLength: 1000, nullable: true),
                    ContactSubDistrictTH = table.Column<string>(maxLength: 1000, nullable: true),
                    ContactSubDistrictEN = table.Column<string>(maxLength: 1000, nullable: true),
                    ReceiveDate = table.Column<DateTime>(nullable: false),
                    ProjectNo = table.Column<string>(maxLength: 1000, nullable: true),
                    ProjectName = table.Column<string>(maxLength: 1000, nullable: true),
                    UnitNo = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptHeader", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ReceiptHeader_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiptHeader_Payment_PaymentID",
                        column: x => x.PaymentID,
                        principalSchema: "FIN",
                        principalTable: "Payment",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiptHeader_Contact_SendToContactID",
                        column: x => x.SendToContactID,
                        principalSchema: "CTM",
                        principalTable: "Contact",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiptHeader_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UnknownPayment_UnknowPaymentStatusID",
                schema: "FIN",
                table: "UnknownPayment",
                column: "UnknowPaymentStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_DirectCreditDebitExportHeader_DirectFormTypeID",
                schema: "FIN",
                table: "DirectCreditDebitExportHeader",
                column: "DirectFormTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_DirectCreditDebitApprovalForm_DirectApprovalFormStatusID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                column: "DirectApprovalFormStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_DirectCreditDebitApprovalForm_DirectApprovalFormTypeID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                column: "DirectApprovalFormTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_BillPaymentTransaction_BillPaymentStatusID",
                schema: "FIN",
                table: "BillPaymentTransaction",
                column: "BillPaymentStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_BillPayment_BillPaymentImportTypeID",
                schema: "FIN",
                table: "BillPayment",
                column: "BillPaymentImportTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_PostGLAccount_FormatTextFileID",
                schema: "ACC",
                table: "PostGLAccount",
                column: "FormatTextFileID");

            migrationBuilder.CreateIndex(
                name: "IX_PostGLFormatTextFile_CreatedByUserID",
                schema: "ACC",
                table: "PostGLFormatTextFile",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PostGLFormatTextFile_UpdatedByUserID",
                schema: "ACC",
                table: "PostGLFormatTextFile",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptHeader_CreatedByUserID",
                schema: "FIN",
                table: "ReceiptHeader",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptHeader_PaymentID",
                schema: "FIN",
                table: "ReceiptHeader",
                column: "PaymentID");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptHeader_SendToContactID",
                schema: "FIN",
                table: "ReceiptHeader",
                column: "SendToContactID");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptHeader_UpdatedByUserID",
                schema: "FIN",
                table: "ReceiptHeader",
                column: "UpdatedByUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_PostGLAccount_PostGLFormatTextFile_FormatTextFileID",
                schema: "ACC",
                table: "PostGLAccount",
                column: "FormatTextFileID",
                principalSchema: "ACC",
                principalTable: "PostGLFormatTextFile",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

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
                name: "FK_DirectCreditDebitExportHeader_BankAccount_DirectFormTypeID",
                schema: "FIN",
                table: "DirectCreditDebitExportHeader",
                column: "DirectFormTypeID",
                principalSchema: "MST",
                principalTable: "BankAccount",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptSendEmailHistory_ReceiptHeader_ReceiptHeaderID",
                schema: "FIN",
                table: "ReceiptSendEmailHistory",
                column: "ReceiptHeaderID",
                principalSchema: "FIN",
                principalTable: "ReceiptHeader",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptSendPrintingHistory_ReceiptHeader_ReceiptHeaderID",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory",
                column: "ReceiptHeaderID",
                principalSchema: "FIN",
                principalTable: "ReceiptHeader",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UnknownPayment_MasterCenter_UnknowPaymentStatusID",
                schema: "FIN",
                table: "UnknownPayment",
                column: "UnknowPaymentStatusID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostGLAccount_PostGLFormatTextFile_FormatTextFileID",
                schema: "ACC",
                table: "PostGLAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_BillPayment_MasterCenter_BillPaymentImportTypeID",
                schema: "FIN",
                table: "BillPayment");

            migrationBuilder.DropForeignKey(
                name: "FK_BillPaymentTransaction_MasterCenter_BillPaymentStatusID",
                schema: "FIN",
                table: "BillPaymentTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectCreditDebitApprovalForm_MasterCenter_DirectApprovalFormStatusID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectCreditDebitApprovalForm_MasterCenter_DirectApprovalFormTypeID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectCreditDebitExportHeader_BankAccount_DirectFormTypeID",
                schema: "FIN",
                table: "DirectCreditDebitExportHeader");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptSendEmailHistory_ReceiptHeader_ReceiptHeaderID",
                schema: "FIN",
                table: "ReceiptSendEmailHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiptSendPrintingHistory_ReceiptHeader_ReceiptHeaderID",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_UnknownPayment_MasterCenter_UnknowPaymentStatusID",
                schema: "FIN",
                table: "UnknownPayment");

            migrationBuilder.DropTable(
                name: "PostGLFormatTextFile",
                schema: "ACC");

            migrationBuilder.DropTable(
                name: "ReceiptHeader",
                schema: "FIN");

            migrationBuilder.DropIndex(
                name: "IX_UnknownPayment_UnknowPaymentStatusID",
                schema: "FIN",
                table: "UnknownPayment");

            migrationBuilder.DropIndex(
                name: "IX_DirectCreditDebitExportHeader_DirectFormTypeID",
                schema: "FIN",
                table: "DirectCreditDebitExportHeader");

            migrationBuilder.DropIndex(
                name: "IX_DirectCreditDebitApprovalForm_DirectApprovalFormStatusID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm");

            migrationBuilder.DropIndex(
                name: "IX_DirectCreditDebitApprovalForm_DirectApprovalFormTypeID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm");

            migrationBuilder.DropIndex(
                name: "IX_BillPaymentTransaction_BillPaymentStatusID",
                schema: "FIN",
                table: "BillPaymentTransaction");

            migrationBuilder.DropIndex(
                name: "IX_BillPayment_BillPaymentImportTypeID",
                schema: "FIN",
                table: "BillPayment");

            migrationBuilder.DropIndex(
                name: "IX_PostGLAccount_FormatTextFileID",
                schema: "ACC",
                table: "PostGLAccount");

            migrationBuilder.DropColumn(
                name: "UnknowPaymentStatusID",
                schema: "FIN",
                table: "UnknownPayment");

            migrationBuilder.DropColumn(
                name: "ExportDate",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory");

            migrationBuilder.DropColumn(
                name: "LotNo",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory");

            migrationBuilder.DropColumn(
                name: "SendDate",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory");

            migrationBuilder.DropColumn(
                name: "TotalRecord",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory");

            migrationBuilder.DropColumn(
                name: "SendDate",
                schema: "FIN",
                table: "ReceiptSendEmailHistory");

            migrationBuilder.DropColumn(
                name: "DirectFormTypeID",
                schema: "FIN",
                table: "DirectCreditDebitExportHeader");

            migrationBuilder.DropColumn(
                name: "DirectApprovalFormStatusID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm");

            migrationBuilder.DropColumn(
                name: "DirectApprovalFormTypeID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm");

            migrationBuilder.DropColumn(
                name: "BillPaymentStatusID",
                schema: "FIN",
                table: "BillPaymentTransaction");

            migrationBuilder.DropColumn(
                name: "BillPaymentImportTypeID",
                schema: "FIN",
                table: "BillPayment");

            migrationBuilder.DropColumn(
                name: "FormatTextFileID",
                schema: "ACC",
                table: "PostGLAccount");

            migrationBuilder.RenameColumn(
                name: "ReceiptHeaderID",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory",
                newName: "ReceiptID");

            migrationBuilder.RenameIndex(
                name: "IX_ReceiptSendPrintingHistory_ReceiptHeaderID",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory",
                newName: "IX_ReceiptSendPrintingHistory_ReceiptID");

            migrationBuilder.RenameColumn(
                name: "ReceiptHeaderID",
                schema: "FIN",
                table: "ReceiptSendEmailHistory",
                newName: "ReceiptID");

            migrationBuilder.RenameIndex(
                name: "IX_ReceiptSendEmailHistory_ReceiptHeaderID",
                schema: "FIN",
                table: "ReceiptSendEmailHistory",
                newName: "IX_ReceiptSendEmailHistory_ReceiptID");

            migrationBuilder.AddColumn<int>(
                name: "UnknowPaymentStatus",
                schema: "FIN",
                table: "UnknownPayment",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "SendToContactID",
                schema: "FIN",
                table: "ReceiptSendEmailHistory",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DirectFormType",
                schema: "FIN",
                table: "DirectCreditDebitExportHeader",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FormStatus",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FormType",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BillPaymentStatus",
                schema: "FIN",
                table: "BillPaymentTransaction",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptSendEmailHistory_SendToContactID",
                schema: "FIN",
                table: "ReceiptSendEmailHistory",
                column: "SendToContactID");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptSendEmailHistory_Receipt_ReceiptID",
                schema: "FIN",
                table: "ReceiptSendEmailHistory",
                column: "ReceiptID",
                principalSchema: "FIN",
                principalTable: "Receipt",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptSendEmailHistory_Contact_SendToContactID",
                schema: "FIN",
                table: "ReceiptSendEmailHistory",
                column: "SendToContactID",
                principalSchema: "CTM",
                principalTable: "Contact",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptSendPrintingHistory_Receipt_ReceiptID",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory",
                column: "ReceiptID",
                principalSchema: "FIN",
                principalTable: "Receipt",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
