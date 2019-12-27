using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ModifyUnknownFieldNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnknownPayment_MasterCenter_UnknowPaymentStatusID",
                schema: "FIN",
                table: "UnknownPayment");

            migrationBuilder.RenameColumn(
                name: "UnknowPaymentStatusID",
                schema: "FIN",
                table: "UnknownPayment",
                newName: "UnknownPaymentStatusID");

            migrationBuilder.RenameColumn(
                name: "UnknowPaymentCode",
                schema: "FIN",
                table: "UnknownPayment",
                newName: "UnknownPaymentCode");

            migrationBuilder.RenameIndex(
                name: "IX_UnknownPayment_UnknowPaymentStatusID",
                schema: "FIN",
                table: "UnknownPayment",
                newName: "IX_UnknownPayment_UnknownPaymentStatusID");

            migrationBuilder.AddColumn<Guid>(
                name: "BankAccountID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankBranchName",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PostGLFormatTextFileDetail",
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
                    table.PrimaryKey("PK_PostGLFormatTextFileDetail", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PostGLFormatTextFileDetail_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostGLFormatTextFileDetail_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostGLHeader",
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
                    BatchID = table.Column<string>(maxLength: 50, nullable: true),
                    BatchTypeID = table.Column<Guid>(nullable: true),
                    CompanyID = table.Column<Guid>(nullable: true),
                    DocumentDate = table.Column<DateTime>(nullable: false),
                    PostingDate = table.Column<DateTime>(nullable: false),
                    ReferentGLID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostGLHeader", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PostGLHeader_MasterCenter_BatchTypeID",
                        column: x => x.BatchTypeID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostGLHeader_Company_CompanyID",
                        column: x => x.CompanyID,
                        principalSchema: "MST",
                        principalTable: "Company",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostGLHeader_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostGLHeader_PostGLHeader_ReferentGLID",
                        column: x => x.ReferentGLID,
                        principalSchema: "ACC",
                        principalTable: "PostGLHeader",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostGLHeader_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostGLDetail",
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
                    PostGLHeaderID = table.Column<Guid>(nullable: true),
                    PostingKey = table.Column<string>(nullable: true),
                    TaxCodeID = table.Column<Guid>(nullable: true),
                    GLAccountID = table.Column<Guid>(nullable: true),
                    FormatTextFileID = table.Column<Guid>(nullable: true),
                    Amount = table.Column<decimal>(type: "Money", nullable: false),
                    BookingID = table.Column<Guid>(nullable: true),
                    ValueDate = table.Column<DateTime>(nullable: true),
                    AccountCode = table.Column<string>(nullable: true),
                    WBSNumber = table.Column<string>(nullable: true),
                    ProfitCenter = table.Column<string>(nullable: true),
                    CostCenter = table.Column<string>(nullable: true),
                    Quantity = table.Column<string>(nullable: true),
                    Unit = table.Column<string>(nullable: true),
                    Assignment = table.Column<string>(nullable: true),
                    ItemText = table.Column<string>(nullable: true),
                    ProjectCode = table.Column<string>(nullable: true),
                    UnitNumber = table.Column<string>(nullable: true),
                    ObjectNumber = table.Column<string>(nullable: true),
                    CustomerName = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    PostCode = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostGLDetail", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PostGLDetail_Booking_BookingID",
                        column: x => x.BookingID,
                        principalSchema: "SAL",
                        principalTable: "Booking",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostGLDetail_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostGLDetail_PostGLFormatTextFileHeader_FormatTextFileID",
                        column: x => x.FormatTextFileID,
                        principalSchema: "ACC",
                        principalTable: "PostGLFormatTextFileHeader",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostGLDetail_PostGLAccount_GLAccountID",
                        column: x => x.GLAccountID,
                        principalSchema: "ACC",
                        principalTable: "PostGLAccount",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostGLDetail_PostGLHeader_PostGLHeaderID",
                        column: x => x.PostGLHeaderID,
                        principalSchema: "ACC",
                        principalTable: "PostGLHeader",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostGLDetail_MasterCenter_TaxCodeID",
                        column: x => x.TaxCodeID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostGLDetail_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DirectCreditDebitApprovalForm_BankAccountID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                column: "BankAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_PostGLDetail_BookingID",
                schema: "ACC",
                table: "PostGLDetail",
                column: "BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_PostGLDetail_CreatedByUserID",
                schema: "ACC",
                table: "PostGLDetail",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PostGLDetail_FormatTextFileID",
                schema: "ACC",
                table: "PostGLDetail",
                column: "FormatTextFileID");

            migrationBuilder.CreateIndex(
                name: "IX_PostGLDetail_GLAccountID",
                schema: "ACC",
                table: "PostGLDetail",
                column: "GLAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_PostGLDetail_PostGLHeaderID",
                schema: "ACC",
                table: "PostGLDetail",
                column: "PostGLHeaderID");

            migrationBuilder.CreateIndex(
                name: "IX_PostGLDetail_TaxCodeID",
                schema: "ACC",
                table: "PostGLDetail",
                column: "TaxCodeID");

            migrationBuilder.CreateIndex(
                name: "IX_PostGLDetail_UpdatedByUserID",
                schema: "ACC",
                table: "PostGLDetail",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PostGLFormatTextFileDetail_CreatedByUserID",
                schema: "ACC",
                table: "PostGLFormatTextFileDetail",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PostGLFormatTextFileDetail_UpdatedByUserID",
                schema: "ACC",
                table: "PostGLFormatTextFileDetail",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PostGLHeader_BatchTypeID",
                schema: "ACC",
                table: "PostGLHeader",
                column: "BatchTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_PostGLHeader_CompanyID",
                schema: "ACC",
                table: "PostGLHeader",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_PostGLHeader_CreatedByUserID",
                schema: "ACC",
                table: "PostGLHeader",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PostGLHeader_ReferentGLID",
                schema: "ACC",
                table: "PostGLHeader",
                column: "ReferentGLID");

            migrationBuilder.CreateIndex(
                name: "IX_PostGLHeader_UpdatedByUserID",
                schema: "ACC",
                table: "PostGLHeader",
                column: "UpdatedByUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_DirectCreditDebitApprovalForm_BankAccount_BankAccountID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                column: "BankAccountID",
                principalSchema: "MST",
                principalTable: "BankAccount",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnknownPayment_MasterCenter_UnknownPaymentStatusID",
                schema: "FIN",
                table: "UnknownPayment",
                column: "UnknownPaymentStatusID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DirectCreditDebitApprovalForm_BankAccount_BankAccountID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm");

            migrationBuilder.DropForeignKey(
                name: "FK_UnknownPayment_MasterCenter_UnknownPaymentStatusID",
                schema: "FIN",
                table: "UnknownPayment");

            migrationBuilder.DropTable(
                name: "PostGLDetail",
                schema: "ACC");

            migrationBuilder.DropTable(
                name: "PostGLFormatTextFileDetail",
                schema: "ACC");

            migrationBuilder.DropTable(
                name: "PostGLHeader",
                schema: "ACC");

            migrationBuilder.DropIndex(
                name: "IX_DirectCreditDebitApprovalForm_BankAccountID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm");

            migrationBuilder.DropColumn(
                name: "BankAccountID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm");

            migrationBuilder.DropColumn(
                name: "BankBranchName",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm");

            migrationBuilder.RenameColumn(
                name: "UnknownPaymentStatusID",
                schema: "FIN",
                table: "UnknownPayment",
                newName: "UnknowPaymentStatusID");

            migrationBuilder.RenameColumn(
                name: "UnknownPaymentCode",
                schema: "FIN",
                table: "UnknownPayment",
                newName: "UnknowPaymentCode");

            migrationBuilder.RenameIndex(
                name: "IX_UnknownPayment_UnknownPaymentStatusID",
                schema: "FIN",
                table: "UnknownPayment",
                newName: "IX_UnknownPayment_UnknowPaymentStatusID");

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
    }
}
