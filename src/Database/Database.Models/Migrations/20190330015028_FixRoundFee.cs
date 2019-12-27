using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class FixRoundFee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LandOffice",
                schema: "PRJ",
                table: "RoundFee");

            migrationBuilder.DropColumn(
                name: "RoundBusinessFee",
                schema: "PRJ",
                table: "RoundFee");

            migrationBuilder.DropColumn(
                name: "RoundIncomeFee",
                schema: "PRJ",
                table: "RoundFee");

            migrationBuilder.DropColumn(
                name: "RoundLocalFee",
                schema: "PRJ",
                table: "RoundFee");

            migrationBuilder.DropColumn(
                name: "RoundTransferFee",
                schema: "PRJ",
                table: "RoundFee");

            migrationBuilder.AlterColumn<decimal>(
                name: "OtherFee",
                schema: "PRJ",
                table: "RoundFee",
                type: "Money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Money");

            migrationBuilder.AddColumn<Guid>(
                name: "LandOfficeID",
                schema: "PRJ",
                table: "RoundFee",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "MasterCenterRoundBusinessFeeID",
                schema: "PRJ",
                table: "RoundFee",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MasterCenterRoundIncomeFeeID",
                schema: "PRJ",
                table: "RoundFee",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MasterCenterRoundLocalFeeID",
                schema: "PRJ",
                table: "RoundFee",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MasterCenterRoundTransferFeeID",
                schema: "PRJ",
                table: "RoundFee",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoundFee_LandOfficeID",
                schema: "PRJ",
                table: "RoundFee",
                column: "LandOfficeID");

            migrationBuilder.CreateIndex(
                name: "IX_RoundFee_MasterCenterRoundBusinessFeeID",
                schema: "PRJ",
                table: "RoundFee",
                column: "MasterCenterRoundBusinessFeeID");

            migrationBuilder.CreateIndex(
                name: "IX_RoundFee_MasterCenterRoundIncomeFeeID",
                schema: "PRJ",
                table: "RoundFee",
                column: "MasterCenterRoundIncomeFeeID");

            migrationBuilder.CreateIndex(
                name: "IX_RoundFee_MasterCenterRoundLocalFeeID",
                schema: "PRJ",
                table: "RoundFee",
                column: "MasterCenterRoundLocalFeeID");

            migrationBuilder.CreateIndex(
                name: "IX_RoundFee_MasterCenterRoundTransferFeeID",
                schema: "PRJ",
                table: "RoundFee",
                column: "MasterCenterRoundTransferFeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_RoundFee_LandOffice_LandOfficeID",
                schema: "PRJ",
                table: "RoundFee",
                column: "LandOfficeID",
                principalSchema: "MST",
                principalTable: "LandOffice",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoundFee_MasterCenter_MasterCenterRoundBusinessFeeID",
                schema: "PRJ",
                table: "RoundFee",
                column: "MasterCenterRoundBusinessFeeID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoundFee_MasterCenter_MasterCenterRoundIncomeFeeID",
                schema: "PRJ",
                table: "RoundFee",
                column: "MasterCenterRoundIncomeFeeID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoundFee_MasterCenter_MasterCenterRoundLocalFeeID",
                schema: "PRJ",
                table: "RoundFee",
                column: "MasterCenterRoundLocalFeeID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoundFee_MasterCenter_MasterCenterRoundTransferFeeID",
                schema: "PRJ",
                table: "RoundFee",
                column: "MasterCenterRoundTransferFeeID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoundFee_LandOffice_LandOfficeID",
                schema: "PRJ",
                table: "RoundFee");

            migrationBuilder.DropForeignKey(
                name: "FK_RoundFee_MasterCenter_MasterCenterRoundBusinessFeeID",
                schema: "PRJ",
                table: "RoundFee");

            migrationBuilder.DropForeignKey(
                name: "FK_RoundFee_MasterCenter_MasterCenterRoundIncomeFeeID",
                schema: "PRJ",
                table: "RoundFee");

            migrationBuilder.DropForeignKey(
                name: "FK_RoundFee_MasterCenter_MasterCenterRoundLocalFeeID",
                schema: "PRJ",
                table: "RoundFee");

            migrationBuilder.DropForeignKey(
                name: "FK_RoundFee_MasterCenter_MasterCenterRoundTransferFeeID",
                schema: "PRJ",
                table: "RoundFee");

            migrationBuilder.DropIndex(
                name: "IX_RoundFee_LandOfficeID",
                schema: "PRJ",
                table: "RoundFee");

            migrationBuilder.DropIndex(
                name: "IX_RoundFee_MasterCenterRoundBusinessFeeID",
                schema: "PRJ",
                table: "RoundFee");

            migrationBuilder.DropIndex(
                name: "IX_RoundFee_MasterCenterRoundIncomeFeeID",
                schema: "PRJ",
                table: "RoundFee");

            migrationBuilder.DropIndex(
                name: "IX_RoundFee_MasterCenterRoundLocalFeeID",
                schema: "PRJ",
                table: "RoundFee");

            migrationBuilder.DropIndex(
                name: "IX_RoundFee_MasterCenterRoundTransferFeeID",
                schema: "PRJ",
                table: "RoundFee");

            migrationBuilder.DropColumn(
                name: "LandOfficeID",
                schema: "PRJ",
                table: "RoundFee");

            migrationBuilder.DropColumn(
                name: "MasterCenterRoundBusinessFeeID",
                schema: "PRJ",
                table: "RoundFee");

            migrationBuilder.DropColumn(
                name: "MasterCenterRoundIncomeFeeID",
                schema: "PRJ",
                table: "RoundFee");

            migrationBuilder.DropColumn(
                name: "MasterCenterRoundLocalFeeID",
                schema: "PRJ",
                table: "RoundFee");

            migrationBuilder.DropColumn(
                name: "MasterCenterRoundTransferFeeID",
                schema: "PRJ",
                table: "RoundFee");

            migrationBuilder.AlterColumn<decimal>(
                name: "OtherFee",
                schema: "PRJ",
                table: "RoundFee",
                type: "Money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Money",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LandOffice",
                schema: "PRJ",
                table: "RoundFee",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoundBusinessFee",
                schema: "PRJ",
                table: "RoundFee",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoundIncomeFee",
                schema: "PRJ",
                table: "RoundFee",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoundLocalFee",
                schema: "PRJ",
                table: "RoundFee",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoundTransferFee",
                schema: "PRJ",
                table: "RoundFee",
                nullable: true);
        }
    }
}
