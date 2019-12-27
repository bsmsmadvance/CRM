using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class alter_tb_PostGLHeader_non_kim : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostGLDetail_PostGLAccount_GLAccountID",
                schema: "ACC",
                table: "PostGLDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_PostGLHeader_MasterCenter_BatchTypeID",
                schema: "ACC",
                table: "PostGLHeader");

            migrationBuilder.RenameColumn(
                name: "BatchTypeMasterCenterID",
                schema: "ACC",
                table: "PostGLHeader",
                newName: "PostGLDocumentTypeMasterCenterID");

            migrationBuilder.RenameColumn(
                name: "BatchTypeID",
                schema: "ACC",
                table: "PostGLHeader",
                newName: "PostGLDocumentTypeID");

            migrationBuilder.RenameColumn(
                name: "BatchID",
                schema: "ACC",
                table: "PostGLHeader",
                newName: "DocumentNo");

            migrationBuilder.RenameIndex(
                name: "IX_PostGLHeader_BatchTypeID",
                schema: "ACC",
                table: "PostGLHeader",
                newName: "IX_PostGLHeader_PostGLDocumentTypeID");

            migrationBuilder.AlterColumn<string>(
                name: "ReferentType",
                schema: "ACC",
                table: "PostGLHeader",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                schema: "ACC",
                table: "PostGLHeader",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "DeleteReason",
                schema: "ACC",
                table: "PostGLHeader",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExportedTimes",
                schema: "ACC",
                table: "PostGLHeader",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Fee",
                schema: "ACC",
                table: "PostGLHeader",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "LastExportedDate",
                schema: "ACC",
                table: "PostGLHeader",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "WBSNumber",
                schema: "ACC",
                table: "PostGLDetail",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UnitNo",
                schema: "ACC",
                table: "PostGLDetail",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Unit",
                schema: "ACC",
                table: "PostGLDetail",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TaxCode",
                schema: "ACC",
                table: "PostGLDetail",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                schema: "ACC",
                table: "PostGLDetail",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Quantity",
                schema: "ACC",
                table: "PostGLDetail",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProjectNo",
                schema: "ACC",
                table: "PostGLDetail",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProfitCenter",
                schema: "ACC",
                table: "PostGLDetail",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PostingKey",
                schema: "ACC",
                table: "PostGLDetail",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PostCode",
                schema: "ACC",
                table: "PostGLDetail",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ObjectNumber",
                schema: "ACC",
                table: "PostGLDetail",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CustomerName",
                schema: "ACC",
                table: "PostGLDetail",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                schema: "ACC",
                table: "PostGLDetail",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CostCenter",
                schema: "ACC",
                table: "PostGLDetail",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                schema: "ACC",
                table: "PostGLDetail",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Assignment",
                schema: "ACC",
                table: "PostGLDetail",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AccountCode",
                schema: "ACC",
                table: "PostGLDetail",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostGLType",
                schema: "ACC",
                table: "PostGLDetail",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PostGLDetail_BankAccount_GLAccountID",
                schema: "ACC",
                table: "PostGLDetail",
                column: "GLAccountID",
                principalSchema: "MST",
                principalTable: "BankAccount",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostGLHeader_MasterCenter_PostGLDocumentTypeID",
                schema: "ACC",
                table: "PostGLHeader",
                column: "PostGLDocumentTypeID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostGLDetail_BankAccount_GLAccountID",
                schema: "ACC",
                table: "PostGLDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_PostGLHeader_MasterCenter_PostGLDocumentTypeID",
                schema: "ACC",
                table: "PostGLHeader");

            migrationBuilder.DropColumn(
                name: "Amount",
                schema: "ACC",
                table: "PostGLHeader");

            migrationBuilder.DropColumn(
                name: "DeleteReason",
                schema: "ACC",
                table: "PostGLHeader");

            migrationBuilder.DropColumn(
                name: "ExportedTimes",
                schema: "ACC",
                table: "PostGLHeader");

            migrationBuilder.DropColumn(
                name: "Fee",
                schema: "ACC",
                table: "PostGLHeader");

            migrationBuilder.DropColumn(
                name: "LastExportedDate",
                schema: "ACC",
                table: "PostGLHeader");

            migrationBuilder.DropColumn(
                name: "PostGLType",
                schema: "ACC",
                table: "PostGLDetail");

            migrationBuilder.RenameColumn(
                name: "PostGLDocumentTypeMasterCenterID",
                schema: "ACC",
                table: "PostGLHeader",
                newName: "BatchTypeMasterCenterID");

            migrationBuilder.RenameColumn(
                name: "PostGLDocumentTypeID",
                schema: "ACC",
                table: "PostGLHeader",
                newName: "BatchTypeID");

            migrationBuilder.RenameColumn(
                name: "DocumentNo",
                schema: "ACC",
                table: "PostGLHeader",
                newName: "BatchID");

            migrationBuilder.RenameIndex(
                name: "IX_PostGLHeader_PostGLDocumentTypeID",
                schema: "ACC",
                table: "PostGLHeader",
                newName: "IX_PostGLHeader_BatchTypeID");

            migrationBuilder.AlterColumn<string>(
                name: "ReferentType",
                schema: "ACC",
                table: "PostGLHeader",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "WBSNumber",
                schema: "ACC",
                table: "PostGLDetail",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UnitNo",
                schema: "ACC",
                table: "PostGLDetail",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Unit",
                schema: "ACC",
                table: "PostGLDetail",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TaxCode",
                schema: "ACC",
                table: "PostGLDetail",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                schema: "ACC",
                table: "PostGLDetail",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Quantity",
                schema: "ACC",
                table: "PostGLDetail",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProjectNo",
                schema: "ACC",
                table: "PostGLDetail",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProfitCenter",
                schema: "ACC",
                table: "PostGLDetail",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PostingKey",
                schema: "ACC",
                table: "PostGLDetail",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PostCode",
                schema: "ACC",
                table: "PostGLDetail",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ObjectNumber",
                schema: "ACC",
                table: "PostGLDetail",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CustomerName",
                schema: "ACC",
                table: "PostGLDetail",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                schema: "ACC",
                table: "PostGLDetail",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CostCenter",
                schema: "ACC",
                table: "PostGLDetail",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                schema: "ACC",
                table: "PostGLDetail",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Assignment",
                schema: "ACC",
                table: "PostGLDetail",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AccountCode",
                schema: "ACC",
                table: "PostGLDetail",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PostGLDetail_PostGLAccount_GLAccountID",
                schema: "ACC",
                table: "PostGLDetail",
                column: "GLAccountID",
                principalSchema: "ACC",
                principalTable: "PostGLAccount",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostGLHeader_MasterCenter_BatchTypeID",
                schema: "ACC",
                table: "PostGLHeader",
                column: "BatchTypeID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
