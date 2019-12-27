using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class UpdateRateTransferCMS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChangeLCSale_Agreement_ContractID",
                schema: "CMS",
                table: "ChangeLCSale");

            migrationBuilder.DropForeignKey(
                name: "FK_CommissionContract_Agreement_ContractID",
                schema: "CMS",
                table: "CommissionContract");

            migrationBuilder.DropIndex(
                name: "IX_CommissionContract_ContractID",
                schema: "CMS",
                table: "CommissionContract");

            migrationBuilder.DropColumn(
                name: "ContractID",
                schema: "CMS",
                table: "CommissionContract");

            migrationBuilder.RenameColumn(
                name: "BG",
                schema: "CMS",
                table: "RateTransfer",
                newName: "BGNo");

            migrationBuilder.RenameColumn(
                name: "ContractID",
                schema: "CMS",
                table: "ChangeLCSale",
                newName: "AgreementID");

            migrationBuilder.RenameIndex(
                name: "IX_ChangeLCSale_ContractID",
                schema: "CMS",
                table: "ChangeLCSale",
                newName: "IX_ChangeLCSale_AgreementID");

            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                schema: "CMS",
                table: "RateSettingTransfer",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AddColumn<decimal>(
                name: "CommissionFixAmount",
                schema: "CMS",
                table: "CalculatePerMonthLowRise",
                type: "Money",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CommissionFixAmount",
                schema: "CMS",
                table: "CalculatePerMonthHighRiseTransfer",
                type: "Money",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CommissionFixAmount",
                schema: "CMS",
                table: "CalculatePerMonthHighRiseSale",
                type: "Money",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommissionContract_AgreementID",
                schema: "CMS",
                table: "CommissionContract",
                column: "AgreementID");

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeLCSale_Agreement_AgreementID",
                schema: "CMS",
                table: "ChangeLCSale",
                column: "AgreementID",
                principalSchema: "SAL",
                principalTable: "Agreement",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CommissionContract_Agreement_AgreementID",
                schema: "CMS",
                table: "CommissionContract",
                column: "AgreementID",
                principalSchema: "SAL",
                principalTable: "Agreement",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChangeLCSale_Agreement_AgreementID",
                schema: "CMS",
                table: "ChangeLCSale");

            migrationBuilder.DropForeignKey(
                name: "FK_CommissionContract_Agreement_AgreementID",
                schema: "CMS",
                table: "CommissionContract");

            migrationBuilder.DropIndex(
                name: "IX_CommissionContract_AgreementID",
                schema: "CMS",
                table: "CommissionContract");

            migrationBuilder.DropColumn(
                name: "CommissionFixAmount",
                schema: "CMS",
                table: "CalculatePerMonthLowRise");

            migrationBuilder.DropColumn(
                name: "CommissionFixAmount",
                schema: "CMS",
                table: "CalculatePerMonthHighRiseTransfer");

            migrationBuilder.DropColumn(
                name: "CommissionFixAmount",
                schema: "CMS",
                table: "CalculatePerMonthHighRiseSale");

            migrationBuilder.RenameColumn(
                name: "BGNo",
                schema: "CMS",
                table: "RateTransfer",
                newName: "BG");

            migrationBuilder.RenameColumn(
                name: "AgreementID",
                schema: "CMS",
                table: "ChangeLCSale",
                newName: "ContractID");

            migrationBuilder.RenameIndex(
                name: "IX_ChangeLCSale_AgreementID",
                schema: "CMS",
                table: "ChangeLCSale",
                newName: "IX_ChangeLCSale_ContractID");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                schema: "CMS",
                table: "RateSettingTransfer",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AddColumn<Guid>(
                name: "ContractID",
                schema: "CMS",
                table: "CommissionContract",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommissionContract_ContractID",
                schema: "CMS",
                table: "CommissionContract",
                column: "ContractID");

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeLCSale_Agreement_ContractID",
                schema: "CMS",
                table: "ChangeLCSale",
                column: "ContractID",
                principalSchema: "SAL",
                principalTable: "Agreement",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CommissionContract_Agreement_ContractID",
                schema: "CMS",
                table: "CommissionContract",
                column: "ContractID",
                principalSchema: "SAL",
                principalTable: "Agreement",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
