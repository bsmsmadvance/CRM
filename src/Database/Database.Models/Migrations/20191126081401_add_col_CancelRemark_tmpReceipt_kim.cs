using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class add_col_CancelRemark_tmpReceipt_kim : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "USR",
                table: "RefreshToken");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "SAP_ZRFCMM02");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "PRM",
                table: "SAP_ZRFCMM01");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "NTF",
                table: "NotificationTemplate");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "MST",
                table: "RunningNumberCounter");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "MST",
                table: "MasterCenterGroup");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                schema: "MST",
                table: "ErrorMessage");

            migrationBuilder.AddColumn<string>(
                name: "CancelRemark",
                schema: "FIN",
                table: "ReceiptTempHeader",
                maxLength: 5000,
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TotalRecord",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<bool>(
                name: "IsLock",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory",
                nullable: true,
                oldClrType: typeof(bool));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CancelRemark",
                schema: "FIN",
                table: "ReceiptTempHeader");

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "USR",
                table: "RefreshToken",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "SAP_ZRFCMM02",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "PRM",
                table: "SAP_ZRFCMM01",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "NTF",
                table: "NotificationTemplate",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "MST",
                table: "RunningNumberCounter",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "MST",
                table: "MasterCenterGroup",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                schema: "MST",
                table: "ErrorMessage",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "TotalRecord",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsLock",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);
        }
    }
}
