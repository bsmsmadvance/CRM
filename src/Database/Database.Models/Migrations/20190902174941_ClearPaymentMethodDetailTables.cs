using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ClearPaymentMethodDetailTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentForeignBankTransfer_Bank_BankID",
                schema: "FIN",
                table: "PaymentForeignBankTransfer");

            migrationBuilder.DropColumn(
                name: "ForeignBank",
                schema: "FIN",
                table: "PaymentForeignBankTransfer");

            migrationBuilder.DropColumn(
                name: "ForeignTransferType",
                schema: "FIN",
                table: "PaymentForeignBankTransfer");

            migrationBuilder.DropColumn(
                name: "NotifyMemo",
                schema: "FIN",
                table: "PaymentForeignBankTransfer");

            migrationBuilder.DropColumn(
                name: "SourceCurrency",
                schema: "FIN",
                table: "PaymentForeignBankTransfer");

            migrationBuilder.RenameColumn(
                name: "IsNotifyForEdit",
                schema: "FIN",
                table: "PaymentForeignBankTransfer",
                newName: "IsWrongAccount");

            migrationBuilder.RenameColumn(
                name: "BankID",
                schema: "FIN",
                table: "PaymentForeignBankTransfer",
                newName: "ForeignTransferTypeMasterCenterID");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentForeignBankTransfer_BankID",
                schema: "FIN",
                table: "PaymentForeignBankTransfer",
                newName: "IX_PaymentForeignBankTransfer_ForeignTransferTypeMasterCenterID");

            migrationBuilder.AddColumn<bool>(
                name: "IsWrongAccount",
                schema: "FIN",
                table: "PaymentQRCode",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "ChequeNo",
                schema: "FIN",
                table: "PaymentPersonalCheque",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsWrongCompany",
                schema: "FIN",
                table: "PaymentPersonalCheque",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "TransferorName",
                schema: "FIN",
                table: "PaymentForeignBankTransfer",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IR",
                schema: "FIN",
                table: "PaymentForeignBankTransfer",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BankAccountID",
                schema: "FIN",
                table: "PaymentForeignBankTransfer",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ForeignBankID",
                schema: "FIN",
                table: "PaymentForeignBankTransfer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsNotifyFET",
                schema: "FIN",
                table: "PaymentForeignBankTransfer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "NotifyFETMemo",
                schema: "FIN",
                table: "PaymentForeignBankTransfer",
                maxLength: 5000,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ChequeNo",
                schema: "FIN",
                table: "PaymentCashierCheque",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsWrongCompany",
                schema: "FIN",
                table: "PaymentCashierCheque",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsWrongAccount",
                schema: "FIN",
                table: "PaymentBankTransfer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "PaymentDebitCard",
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
                    PaymentMethodID = table.Column<Guid>(nullable: false),
                    Fee = table.Column<decimal>(type: "Money", nullable: false),
                    CardNo = table.Column<string>(maxLength: 50, nullable: true),
                    BankID = table.Column<Guid>(nullable: true),
                    EDCID = table.Column<Guid>(nullable: true),
                    IsWrongAccount = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentDebitCard", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PaymentDebitCard_Bank_BankID",
                        column: x => x.BankID,
                        principalSchema: "MST",
                        principalTable: "Bank",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentDebitCard_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentDebitCard_EDC_EDCID",
                        column: x => x.EDCID,
                        principalSchema: "MST",
                        principalTable: "EDC",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentDebitCard_PaymentMethod_PaymentMethodID",
                        column: x => x.PaymentMethodID,
                        principalSchema: "FIN",
                        principalTable: "PaymentMethod",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaymentDebitCard_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentForeignBankTransfer_BankAccountID",
                schema: "FIN",
                table: "PaymentForeignBankTransfer",
                column: "BankAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentForeignBankTransfer_ForeignBankID",
                schema: "FIN",
                table: "PaymentForeignBankTransfer",
                column: "ForeignBankID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentDebitCard_BankID",
                schema: "FIN",
                table: "PaymentDebitCard",
                column: "BankID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentDebitCard_CreatedByUserID",
                schema: "FIN",
                table: "PaymentDebitCard",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentDebitCard_EDCID",
                schema: "FIN",
                table: "PaymentDebitCard",
                column: "EDCID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentDebitCard_PaymentMethodID",
                schema: "FIN",
                table: "PaymentDebitCard",
                column: "PaymentMethodID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentDebitCard_UpdatedByUserID",
                schema: "FIN",
                table: "PaymentDebitCard",
                column: "UpdatedByUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentForeignBankTransfer_BankAccount_BankAccountID",
                schema: "FIN",
                table: "PaymentForeignBankTransfer",
                column: "BankAccountID",
                principalSchema: "MST",
                principalTable: "BankAccount",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentForeignBankTransfer_Bank_ForeignBankID",
                schema: "FIN",
                table: "PaymentForeignBankTransfer",
                column: "ForeignBankID",
                principalSchema: "MST",
                principalTable: "Bank",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentForeignBankTransfer_MasterCenter_ForeignTransferTypeMasterCenterID",
                schema: "FIN",
                table: "PaymentForeignBankTransfer",
                column: "ForeignTransferTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentForeignBankTransfer_BankAccount_BankAccountID",
                schema: "FIN",
                table: "PaymentForeignBankTransfer");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentForeignBankTransfer_Bank_ForeignBankID",
                schema: "FIN",
                table: "PaymentForeignBankTransfer");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentForeignBankTransfer_MasterCenter_ForeignTransferTypeMasterCenterID",
                schema: "FIN",
                table: "PaymentForeignBankTransfer");

            migrationBuilder.DropTable(
                name: "PaymentDebitCard",
                schema: "FIN");

            migrationBuilder.DropIndex(
                name: "IX_PaymentForeignBankTransfer_BankAccountID",
                schema: "FIN",
                table: "PaymentForeignBankTransfer");

            migrationBuilder.DropIndex(
                name: "IX_PaymentForeignBankTransfer_ForeignBankID",
                schema: "FIN",
                table: "PaymentForeignBankTransfer");

            migrationBuilder.DropColumn(
                name: "IsWrongAccount",
                schema: "FIN",
                table: "PaymentQRCode");

            migrationBuilder.DropColumn(
                name: "IsWrongCompany",
                schema: "FIN",
                table: "PaymentPersonalCheque");

            migrationBuilder.DropColumn(
                name: "BankAccountID",
                schema: "FIN",
                table: "PaymentForeignBankTransfer");

            migrationBuilder.DropColumn(
                name: "ForeignBankID",
                schema: "FIN",
                table: "PaymentForeignBankTransfer");

            migrationBuilder.DropColumn(
                name: "IsNotifyFET",
                schema: "FIN",
                table: "PaymentForeignBankTransfer");

            migrationBuilder.DropColumn(
                name: "NotifyFETMemo",
                schema: "FIN",
                table: "PaymentForeignBankTransfer");

            migrationBuilder.DropColumn(
                name: "IsWrongCompany",
                schema: "FIN",
                table: "PaymentCashierCheque");

            migrationBuilder.DropColumn(
                name: "IsWrongAccount",
                schema: "FIN",
                table: "PaymentBankTransfer");

            migrationBuilder.RenameColumn(
                name: "IsWrongAccount",
                schema: "FIN",
                table: "PaymentForeignBankTransfer",
                newName: "IsNotifyForEdit");

            migrationBuilder.RenameColumn(
                name: "ForeignTransferTypeMasterCenterID",
                schema: "FIN",
                table: "PaymentForeignBankTransfer",
                newName: "BankID");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentForeignBankTransfer_ForeignTransferTypeMasterCenterID",
                schema: "FIN",
                table: "PaymentForeignBankTransfer",
                newName: "IX_PaymentForeignBankTransfer_BankID");

            migrationBuilder.AlterColumn<string>(
                name: "ChequeNo",
                schema: "FIN",
                table: "PaymentPersonalCheque",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TransferorName",
                schema: "FIN",
                table: "PaymentForeignBankTransfer",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IR",
                schema: "FIN",
                table: "PaymentForeignBankTransfer",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ForeignBank",
                schema: "FIN",
                table: "PaymentForeignBankTransfer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ForeignTransferType",
                schema: "FIN",
                table: "PaymentForeignBankTransfer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NotifyMemo",
                schema: "FIN",
                table: "PaymentForeignBankTransfer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SourceCurrency",
                schema: "FIN",
                table: "PaymentForeignBankTransfer",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ChequeNo",
                schema: "FIN",
                table: "PaymentCashierCheque",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentForeignBankTransfer_Bank_BankID",
                schema: "FIN",
                table: "PaymentForeignBankTransfer",
                column: "BankID",
                principalSchema: "MST",
                principalTable: "Bank",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
