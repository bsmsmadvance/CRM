using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ModifyVisitData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                schema: "CTM",
                table: "Visitor",
                newName: "Village");

            migrationBuilder.AlterColumn<bool>(
                name: "IsWelcome",
                schema: "CTM",
                table: "Visitor",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                schema: "CTM",
                table: "Visitor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BloodType",
                schema: "CTM",
                table: "Visitor",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                schema: "CTM",
                table: "Visitor",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "District",
                schema: "CTM",
                table: "Visitor",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstNameEN",
                schema: "CTM",
                table: "Visitor",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstNameTH",
                schema: "CTM",
                table: "Visitor",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                schema: "CTM",
                table: "Visitor",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HouseNo",
                schema: "CTM",
                table: "Visitor",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Issue",
                schema: "CTM",
                table: "Visitor",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "IssueDate",
                schema: "CTM",
                table: "Visitor",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "IssueExpireDate",
                schema: "CTM",
                table: "Visitor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastNameEN",
                schema: "CTM",
                table: "Visitor",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastNameTH",
                schema: "CTM",
                table: "Visitor",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Moo",
                schema: "CTM",
                table: "Visitor",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "National",
                schema: "CTM",
                table: "Visitor",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                schema: "CTM",
                table: "Visitor",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Province",
                schema: "CTM",
                table: "Visitor",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Road",
                schema: "CTM",
                table: "Visitor",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Soi",
                schema: "CTM",
                table: "Visitor",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubDistrict",
                schema: "CTM",
                table: "Visitor",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleEN",
                schema: "CTM",
                table: "Visitor",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleTH",
                schema: "CTM",
                table: "Visitor",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VehicleDescription",
                schema: "CTM",
                table: "Visitor",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDate",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "BloodType",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "Country",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "District",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "FirstNameEN",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "FirstNameTH",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "Gender",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "HouseNo",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "Issue",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "IssueDate",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "IssueExpireDate",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "LastNameEN",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "LastNameTH",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "Moo",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "National",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "Province",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "Road",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "Soi",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "SubDistrict",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "TitleEN",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "TitleTH",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "VehicleDescription",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.RenameColumn(
                name: "Village",
                schema: "CTM",
                table: "Visitor",
                newName: "Description");

            migrationBuilder.AlterColumn<bool>(
                name: "IsWelcome",
                schema: "CTM",
                table: "Visitor",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);
        }
    }
}
