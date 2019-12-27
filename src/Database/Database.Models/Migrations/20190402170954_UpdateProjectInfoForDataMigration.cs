using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class UpdateProjectInfoForDataMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "PRJ",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "EIAApproved",
                schema: "PRJ",
                table: "AgreementConfig");

            migrationBuilder.RenameColumn(
                name: "PowerAttorneyDate",
                schema: "PRJ",
                table: "AgreementConfig",
                newName: "PreLicenseLandIssueDate");

            migrationBuilder.RenameColumn(
                name: "ParkingSpace",
                schema: "PRJ",
                table: "AgreementConfig",
                newName: "PublicFundMonthsAP");

            migrationBuilder.RenameColumn(
                name: "CentralValue",
                schema: "PRJ",
                table: "AgreementConfig",
                newName: "PublicFundRateAP");

            migrationBuilder.RenameColumn(
                name: "CentralMonth",
                schema: "PRJ",
                table: "AgreementConfig",
                newName: "PublicFundMonths");

            migrationBuilder.RenameColumn(
                name: "AttorneyNameFree",
                schema: "PRJ",
                table: "AgreementConfig",
                newName: "PreferApprovePosition");

            migrationBuilder.RenameColumn(
                name: "AttorneyFreePosition",
                schema: "PRJ",
                table: "AgreementConfig",
                newName: "PreferApproveName");

            migrationBuilder.RenameColumn(
                name: "APCentralValue",
                schema: "PRJ",
                table: "AgreementConfig",
                newName: "PublicFundRate");

            migrationBuilder.RenameColumn(
                name: "APCentralMonth",
                schema: "PRJ",
                table: "AgreementConfig",
                newName: "ParkingUnits");

            migrationBuilder.AddColumn<DateTime>(
                name: "AttorneyIssueDate",
                schema: "PRJ",
                table: "AgreementConfig",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AttorneyNameTransfer",
                schema: "PRJ",
                table: "AgreementConfig",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsIncludeDoubleParking",
                schema: "PRJ",
                table: "AgreementConfig",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsNotLicenseLand",
                schema: "PRJ",
                table: "AgreementConfig",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LicenseLandExpireDate",
                schema: "PRJ",
                table: "AgreementConfig",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LicenseLandIssueDate",
                schema: "PRJ",
                table: "AgreementConfig",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LicenseLandNo",
                schema: "PRJ",
                table: "AgreementConfig",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LicenseProductExpireDate",
                schema: "PRJ",
                table: "AgreementConfig",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LicenseProductIssueDate",
                schema: "PRJ",
                table: "AgreementConfig",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LicenseProductNo",
                schema: "PRJ",
                table: "AgreementConfig",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LicenseProductRemark",
                schema: "PRJ",
                table: "AgreementConfig",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PreLicenseLandExpireDate",
                schema: "PRJ",
                table: "AgreementConfig",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreLicenseLandNo",
                schema: "PRJ",
                table: "AgreementConfig",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttorneyIssueDate",
                schema: "PRJ",
                table: "AgreementConfig");

            migrationBuilder.DropColumn(
                name: "AttorneyNameTransfer",
                schema: "PRJ",
                table: "AgreementConfig");

            migrationBuilder.DropColumn(
                name: "IsIncludeDoubleParking",
                schema: "PRJ",
                table: "AgreementConfig");

            migrationBuilder.DropColumn(
                name: "IsNotLicenseLand",
                schema: "PRJ",
                table: "AgreementConfig");

            migrationBuilder.DropColumn(
                name: "LicenseLandExpireDate",
                schema: "PRJ",
                table: "AgreementConfig");

            migrationBuilder.DropColumn(
                name: "LicenseLandIssueDate",
                schema: "PRJ",
                table: "AgreementConfig");

            migrationBuilder.DropColumn(
                name: "LicenseLandNo",
                schema: "PRJ",
                table: "AgreementConfig");

            migrationBuilder.DropColumn(
                name: "LicenseProductExpireDate",
                schema: "PRJ",
                table: "AgreementConfig");

            migrationBuilder.DropColumn(
                name: "LicenseProductIssueDate",
                schema: "PRJ",
                table: "AgreementConfig");

            migrationBuilder.DropColumn(
                name: "LicenseProductNo",
                schema: "PRJ",
                table: "AgreementConfig");

            migrationBuilder.DropColumn(
                name: "LicenseProductRemark",
                schema: "PRJ",
                table: "AgreementConfig");

            migrationBuilder.DropColumn(
                name: "PreLicenseLandExpireDate",
                schema: "PRJ",
                table: "AgreementConfig");

            migrationBuilder.DropColumn(
                name: "PreLicenseLandNo",
                schema: "PRJ",
                table: "AgreementConfig");

            migrationBuilder.RenameColumn(
                name: "PublicFundRateAP",
                schema: "PRJ",
                table: "AgreementConfig",
                newName: "CentralValue");

            migrationBuilder.RenameColumn(
                name: "PublicFundRate",
                schema: "PRJ",
                table: "AgreementConfig",
                newName: "APCentralValue");

            migrationBuilder.RenameColumn(
                name: "PublicFundMonthsAP",
                schema: "PRJ",
                table: "AgreementConfig",
                newName: "ParkingSpace");

            migrationBuilder.RenameColumn(
                name: "PublicFundMonths",
                schema: "PRJ",
                table: "AgreementConfig",
                newName: "CentralMonth");

            migrationBuilder.RenameColumn(
                name: "PreferApprovePosition",
                schema: "PRJ",
                table: "AgreementConfig",
                newName: "AttorneyNameFree");

            migrationBuilder.RenameColumn(
                name: "PreferApproveName",
                schema: "PRJ",
                table: "AgreementConfig",
                newName: "AttorneyFreePosition");

            migrationBuilder.RenameColumn(
                name: "PreLicenseLandIssueDate",
                schema: "PRJ",
                table: "AgreementConfig",
                newName: "PowerAttorneyDate");

            migrationBuilder.RenameColumn(
                name: "ParkingUnits",
                schema: "PRJ",
                table: "AgreementConfig",
                newName: "APCentralMonth");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "PRJ",
                table: "Project",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EIAApproved",
                schema: "PRJ",
                table: "AgreementConfig",
                nullable: true);
        }
    }
}
