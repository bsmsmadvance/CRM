using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class addlegalentityandFixAgreementConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LegalNameEN",
                schema: "PRJ",
                table: "AgreementConfig");

            migrationBuilder.DropColumn(
                name: "LegalNameTH",
                schema: "PRJ",
                table: "AgreementConfig");

            migrationBuilder.AlterColumn<int>(
                name: "VisitFineDay",
                schema: "PRJ",
                table: "AgreementConfig",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<decimal>(
                name: "VisitFine",
                schema: "PRJ",
                table: "AgreementConfig",
                type: "Money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Money");

            migrationBuilder.AlterColumn<decimal>(
                name: "RoomTransferFee",
                schema: "PRJ",
                table: "AgreementConfig",
                type: "Money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Money");

            migrationBuilder.AlterColumn<int>(
                name: "ParkingSpace",
                schema: "PRJ",
                table: "AgreementConfig",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<bool>(
                name: "EIAApproved",
                schema: "PRJ",
                table: "AgreementConfig",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<decimal>(
                name: "DelayTransfer",
                schema: "PRJ",
                table: "AgreementConfig",
                nullable: true,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "CondoFundRate",
                schema: "PRJ",
                table: "AgreementConfig",
                type: "Money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Money");

            migrationBuilder.AlterColumn<decimal>(
                name: "ChangeNameFee",
                schema: "PRJ",
                table: "AgreementConfig",
                type: "Money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Money");

            migrationBuilder.AlterColumn<decimal>(
                name: "CentralValue",
                schema: "PRJ",
                table: "AgreementConfig",
                type: "Money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Money");

            migrationBuilder.AlterColumn<int>(
                name: "CentralMonth",
                schema: "PRJ",
                table: "AgreementConfig",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<decimal>(
                name: "BuildingInsurance",
                schema: "PRJ",
                table: "AgreementConfig",
                type: "Money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Money");

            migrationBuilder.AlterColumn<decimal>(
                name: "APCentralValue",
                schema: "PRJ",
                table: "AgreementConfig",
                type: "Money",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "Money");

            migrationBuilder.AlterColumn<int>(
                name: "APCentralMonth",
                schema: "PRJ",
                table: "AgreementConfig",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<Guid>(
                name: "LegalEntityID",
                schema: "PRJ",
                table: "AgreementConfig",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LegalEntity",
                schema: "MST",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    NameEN = table.Column<string>(nullable: true),
                    NameTH = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalEntity", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgreementConfig_LegalEntityID",
                schema: "PRJ",
                table: "AgreementConfig",
                column: "LegalEntityID");

            migrationBuilder.AddForeignKey(
                name: "FK_AgreementConfig_LegalEntity_LegalEntityID",
                schema: "PRJ",
                table: "AgreementConfig",
                column: "LegalEntityID",
                principalSchema: "MST",
                principalTable: "LegalEntity",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AgreementConfig_LegalEntity_LegalEntityID",
                schema: "PRJ",
                table: "AgreementConfig");

            migrationBuilder.DropTable(
                name: "LegalEntity",
                schema: "MST");

            migrationBuilder.DropIndex(
                name: "IX_AgreementConfig_LegalEntityID",
                schema: "PRJ",
                table: "AgreementConfig");

            migrationBuilder.DropColumn(
                name: "LegalEntityID",
                schema: "PRJ",
                table: "AgreementConfig");

            migrationBuilder.AlterColumn<int>(
                name: "VisitFineDay",
                schema: "PRJ",
                table: "AgreementConfig",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "VisitFine",
                schema: "PRJ",
                table: "AgreementConfig",
                type: "Money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Money",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "RoomTransferFee",
                schema: "PRJ",
                table: "AgreementConfig",
                type: "Money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Money",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ParkingSpace",
                schema: "PRJ",
                table: "AgreementConfig",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "EIAApproved",
                schema: "PRJ",
                table: "AgreementConfig",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "DelayTransfer",
                schema: "PRJ",
                table: "AgreementConfig",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "CondoFundRate",
                schema: "PRJ",
                table: "AgreementConfig",
                type: "Money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Money",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ChangeNameFee",
                schema: "PRJ",
                table: "AgreementConfig",
                type: "Money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Money",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "CentralValue",
                schema: "PRJ",
                table: "AgreementConfig",
                type: "Money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Money",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CentralMonth",
                schema: "PRJ",
                table: "AgreementConfig",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "BuildingInsurance",
                schema: "PRJ",
                table: "AgreementConfig",
                type: "Money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Money",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "APCentralValue",
                schema: "PRJ",
                table: "AgreementConfig",
                type: "Money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "Money",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "APCentralMonth",
                schema: "PRJ",
                table: "AgreementConfig",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LegalNameEN",
                schema: "PRJ",
                table: "AgreementConfig",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LegalNameTH",
                schema: "PRJ",
                table: "AgreementConfig",
                nullable: true);
        }
    }
}
