using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ModifyLeadContactVisitor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CitizenIdentityNo",
                schema: "CTM",
                table: "Visitor",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VehicleBrand",
                schema: "CTM",
                table: "Visitor",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VehicleColor",
                schema: "CTM",
                table: "Visitor",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VehicleMasterCenterID",
                schema: "CTM",
                table: "Visitor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VehicleRegistrationNo",
                schema: "CTM",
                table: "Visitor",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WalkStatusMasterCenterID",
                schema: "CTM",
                table: "Visitor",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ActualDate",
                schema: "CTM",
                table: "OpportunityActivity",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsVIP",
                schema: "CTM",
                table: "Contact",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                schema: "CTM",
                table: "Contact",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CitizenIdentityNo",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "VehicleBrand",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "VehicleColor",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "VehicleMasterCenterID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "VehicleRegistrationNo",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "WalkStatusMasterCenterID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "ActualDate",
                schema: "CTM",
                table: "OpportunityActivity");

            migrationBuilder.DropColumn(
                name: "IsVIP",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "Order",
                schema: "CTM",
                table: "Contact");
        }
    }
}
