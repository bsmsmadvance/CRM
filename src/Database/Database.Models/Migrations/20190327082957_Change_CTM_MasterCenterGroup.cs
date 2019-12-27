using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class Change_CTM_MasterCenterGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneType",
                schema: "CTM",
                table: "ContactPhone");

            migrationBuilder.DropColumn(
                name: "DistrictEN",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.DropColumn(
                name: "DistrictTH",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.DropColumn(
                name: "ProvinceEN",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.DropColumn(
                name: "ProvinceTH",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.DropColumn(
                name: "SubDistrictEN",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.DropColumn(
                name: "SubDistrictTH",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.DropColumn(
                name: "CitizenDistrictEN",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "CitizenDistrictTH",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "CitizenProvinceEN",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "CitizenProvinceTH",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "CitizenSubDistrictEN",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "CitizenSubDistrictTH",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "Gender",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "HomeDistrictEN",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "HomeDistrictTH",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "HomeProvinceEN",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "HomeProvinceTH",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "HomeSubDistrictEN",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "HomeSubDistrictTH",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "TitleEN",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "TitleTH",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "WorkDistrictEN",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "WorkDistrictTH",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "WorkProvinceEN",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "WorkProvinceTH",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "WorkSubDistrictEN",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "WorkSubDistrictTH",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.AddColumn<string>(
                name: "ContactSupervisor",
                schema: "CTM",
                table: "Visitor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "CTM",
                table: "Visitor",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FileAttachmentID",
                schema: "CTM",
                table: "Visitor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceiveNumber",
                schema: "CTM",
                table: "Visitor",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VehicleCenterID",
                schema: "CTM",
                table: "Visitor",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "VisitMasterCenterID",
                schema: "CTM",
                table: "Visitor",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WalkStatus",
                schema: "CTM",
                table: "Visitor",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PhoneTypeID",
                schema: "CTM",
                table: "ContactPhone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DistrictENID",
                schema: "CTM",
                table: "ContactAddress",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DistrictTHID",
                schema: "CTM",
                table: "ContactAddress",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProvinceENID",
                schema: "CTM",
                table: "ContactAddress",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProvinceTHID",
                schema: "CTM",
                table: "ContactAddress",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SubDistrictENID",
                schema: "CTM",
                table: "ContactAddress",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SubDistrictTHID",
                schema: "CTM",
                table: "ContactAddress",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CitizenDistrictENID",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CitizenDistrictTHID",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CitizenProvinceENID",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CitizenProvinceTHID",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CitizenSubDistrictENID",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CitizenSubDistrictTHID",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GenderMasterCenterID",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "HomeDistrictENID",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "HomeDistrictTHID",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "HomeProvinceENID",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "HomeProvinceTHID",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "HomeSubDistrictENID",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "HomeSubDistrictTHID",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TitleENMasterCenterID",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TitleTHMasterCenterID",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WorkDistrictENID",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WorkDistrictTHID",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WorkProvinceENID",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WorkProvinceTHID",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WorkSubDistrictENID",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WorkSubDistrictTHID",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Visitor_VehicleCenterID",
                schema: "CTM",
                table: "Visitor",
                column: "VehicleCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_Visitor_VisitMasterCenterID",
                schema: "CTM",
                table: "Visitor",
                column: "VisitMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_ContactPhone_PhoneTypeID",
                schema: "CTM",
                table: "ContactPhone",
                column: "PhoneTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ContactAddress_DistrictENID",
                schema: "CTM",
                table: "ContactAddress",
                column: "DistrictENID");

            migrationBuilder.CreateIndex(
                name: "IX_ContactAddress_DistrictTHID",
                schema: "CTM",
                table: "ContactAddress",
                column: "DistrictTHID");

            migrationBuilder.CreateIndex(
                name: "IX_ContactAddress_ProvinceENID",
                schema: "CTM",
                table: "ContactAddress",
                column: "ProvinceENID");

            migrationBuilder.CreateIndex(
                name: "IX_ContactAddress_ProvinceTHID",
                schema: "CTM",
                table: "ContactAddress",
                column: "ProvinceTHID");

            migrationBuilder.CreateIndex(
                name: "IX_ContactAddress_SubDistrictENID",
                schema: "CTM",
                table: "ContactAddress",
                column: "SubDistrictENID");

            migrationBuilder.CreateIndex(
                name: "IX_ContactAddress_SubDistrictTHID",
                schema: "CTM",
                table: "ContactAddress",
                column: "SubDistrictTHID");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_CitizenDistrictENID",
                schema: "CTM",
                table: "Contact",
                column: "CitizenDistrictENID");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_CitizenDistrictTHID",
                schema: "CTM",
                table: "Contact",
                column: "CitizenDistrictTHID");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_CitizenProvinceENID",
                schema: "CTM",
                table: "Contact",
                column: "CitizenProvinceENID");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_CitizenProvinceTHID",
                schema: "CTM",
                table: "Contact",
                column: "CitizenProvinceTHID");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_CitizenSubDistrictENID",
                schema: "CTM",
                table: "Contact",
                column: "CitizenSubDistrictENID");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_CitizenSubDistrictTHID",
                schema: "CTM",
                table: "Contact",
                column: "CitizenSubDistrictTHID");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_GenderMasterCenterID",
                schema: "CTM",
                table: "Contact",
                column: "GenderMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_HomeDistrictENID",
                schema: "CTM",
                table: "Contact",
                column: "HomeDistrictENID");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_HomeDistrictTHID",
                schema: "CTM",
                table: "Contact",
                column: "HomeDistrictTHID");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_HomeProvinceENID",
                schema: "CTM",
                table: "Contact",
                column: "HomeProvinceENID");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_HomeProvinceTHID",
                schema: "CTM",
                table: "Contact",
                column: "HomeProvinceTHID");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_HomeSubDistrictENID",
                schema: "CTM",
                table: "Contact",
                column: "HomeSubDistrictENID");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_HomeSubDistrictTHID",
                schema: "CTM",
                table: "Contact",
                column: "HomeSubDistrictTHID");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_TitleENMasterCenterID",
                schema: "CTM",
                table: "Contact",
                column: "TitleENMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_TitleTHMasterCenterID",
                schema: "CTM",
                table: "Contact",
                column: "TitleTHMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_WorkDistrictENID",
                schema: "CTM",
                table: "Contact",
                column: "WorkDistrictENID");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_WorkDistrictTHID",
                schema: "CTM",
                table: "Contact",
                column: "WorkDistrictTHID");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_WorkProvinceENID",
                schema: "CTM",
                table: "Contact",
                column: "WorkProvinceENID");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_WorkProvinceTHID",
                schema: "CTM",
                table: "Contact",
                column: "WorkProvinceTHID");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_WorkSubDistrictENID",
                schema: "CTM",
                table: "Contact",
                column: "WorkSubDistrictENID");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_WorkSubDistrictTHID",
                schema: "CTM",
                table: "Contact",
                column: "WorkSubDistrictTHID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_District_CitizenDistrictENID",
                schema: "CTM",
                table: "Contact",
                column: "CitizenDistrictENID",
                principalSchema: "MST",
                principalTable: "District",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_District_CitizenDistrictTHID",
                schema: "CTM",
                table: "Contact",
                column: "CitizenDistrictTHID",
                principalSchema: "MST",
                principalTable: "District",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Province_CitizenProvinceENID",
                schema: "CTM",
                table: "Contact",
                column: "CitizenProvinceENID",
                principalSchema: "MST",
                principalTable: "Province",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Province_CitizenProvinceTHID",
                schema: "CTM",
                table: "Contact",
                column: "CitizenProvinceTHID",
                principalSchema: "MST",
                principalTable: "Province",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_SubDistrict_CitizenSubDistrictENID",
                schema: "CTM",
                table: "Contact",
                column: "CitizenSubDistrictENID",
                principalSchema: "MST",
                principalTable: "SubDistrict",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_SubDistrict_CitizenSubDistrictTHID",
                schema: "CTM",
                table: "Contact",
                column: "CitizenSubDistrictTHID",
                principalSchema: "MST",
                principalTable: "SubDistrict",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_MasterCenter_GenderMasterCenterID",
                schema: "CTM",
                table: "Contact",
                column: "GenderMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_District_HomeDistrictENID",
                schema: "CTM",
                table: "Contact",
                column: "HomeDistrictENID",
                principalSchema: "MST",
                principalTable: "District",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_District_HomeDistrictTHID",
                schema: "CTM",
                table: "Contact",
                column: "HomeDistrictTHID",
                principalSchema: "MST",
                principalTable: "District",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Province_HomeProvinceENID",
                schema: "CTM",
                table: "Contact",
                column: "HomeProvinceENID",
                principalSchema: "MST",
                principalTable: "Province",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Province_HomeProvinceTHID",
                schema: "CTM",
                table: "Contact",
                column: "HomeProvinceTHID",
                principalSchema: "MST",
                principalTable: "Province",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_SubDistrict_HomeSubDistrictENID",
                schema: "CTM",
                table: "Contact",
                column: "HomeSubDistrictENID",
                principalSchema: "MST",
                principalTable: "SubDistrict",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_SubDistrict_HomeSubDistrictTHID",
                schema: "CTM",
                table: "Contact",
                column: "HomeSubDistrictTHID",
                principalSchema: "MST",
                principalTable: "SubDistrict",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_MasterCenter_TitleENMasterCenterID",
                schema: "CTM",
                table: "Contact",
                column: "TitleENMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_MasterCenter_TitleTHMasterCenterID",
                schema: "CTM",
                table: "Contact",
                column: "TitleTHMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_District_WorkDistrictENID",
                schema: "CTM",
                table: "Contact",
                column: "WorkDistrictENID",
                principalSchema: "MST",
                principalTable: "District",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_District_WorkDistrictTHID",
                schema: "CTM",
                table: "Contact",
                column: "WorkDistrictTHID",
                principalSchema: "MST",
                principalTable: "District",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Province_WorkProvinceENID",
                schema: "CTM",
                table: "Contact",
                column: "WorkProvinceENID",
                principalSchema: "MST",
                principalTable: "Province",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_Province_WorkProvinceTHID",
                schema: "CTM",
                table: "Contact",
                column: "WorkProvinceTHID",
                principalSchema: "MST",
                principalTable: "Province",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_SubDistrict_WorkSubDistrictENID",
                schema: "CTM",
                table: "Contact",
                column: "WorkSubDistrictENID",
                principalSchema: "MST",
                principalTable: "SubDistrict",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_SubDistrict_WorkSubDistrictTHID",
                schema: "CTM",
                table: "Contact",
                column: "WorkSubDistrictTHID",
                principalSchema: "MST",
                principalTable: "SubDistrict",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactAddress_District_DistrictENID",
                schema: "CTM",
                table: "ContactAddress",
                column: "DistrictENID",
                principalSchema: "MST",
                principalTable: "District",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactAddress_District_DistrictTHID",
                schema: "CTM",
                table: "ContactAddress",
                column: "DistrictTHID",
                principalSchema: "MST",
                principalTable: "District",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactAddress_Province_ProvinceENID",
                schema: "CTM",
                table: "ContactAddress",
                column: "ProvinceENID",
                principalSchema: "MST",
                principalTable: "Province",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactAddress_Province_ProvinceTHID",
                schema: "CTM",
                table: "ContactAddress",
                column: "ProvinceTHID",
                principalSchema: "MST",
                principalTable: "Province",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactAddress_SubDistrict_SubDistrictENID",
                schema: "CTM",
                table: "ContactAddress",
                column: "SubDistrictENID",
                principalSchema: "MST",
                principalTable: "SubDistrict",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactAddress_SubDistrict_SubDistrictTHID",
                schema: "CTM",
                table: "ContactAddress",
                column: "SubDistrictTHID",
                principalSchema: "MST",
                principalTable: "SubDistrict",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactPhone_MasterCenter_PhoneTypeID",
                schema: "CTM",
                table: "ContactPhone",
                column: "PhoneTypeID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Visitor_MasterCenter_VehicleCenterID",
                schema: "CTM",
                table: "Visitor",
                column: "VehicleCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Visitor_MasterCenter_VisitMasterCenterID",
                schema: "CTM",
                table: "Visitor",
                column: "VisitMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact_District_CitizenDistrictENID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_District_CitizenDistrictTHID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Province_CitizenProvinceENID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Province_CitizenProvinceTHID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_SubDistrict_CitizenSubDistrictENID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_SubDistrict_CitizenSubDistrictTHID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_MasterCenter_GenderMasterCenterID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_District_HomeDistrictENID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_District_HomeDistrictTHID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Province_HomeProvinceENID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Province_HomeProvinceTHID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_SubDistrict_HomeSubDistrictENID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_SubDistrict_HomeSubDistrictTHID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_MasterCenter_TitleENMasterCenterID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_MasterCenter_TitleTHMasterCenterID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_District_WorkDistrictENID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_District_WorkDistrictTHID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Province_WorkProvinceENID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_Province_WorkProvinceTHID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_SubDistrict_WorkSubDistrictENID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_SubDistrict_WorkSubDistrictTHID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactAddress_District_DistrictENID",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactAddress_District_DistrictTHID",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactAddress_Province_ProvinceENID",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactAddress_Province_ProvinceTHID",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactAddress_SubDistrict_SubDistrictENID",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactAddress_SubDistrict_SubDistrictTHID",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactPhone_MasterCenter_PhoneTypeID",
                schema: "CTM",
                table: "ContactPhone");

            migrationBuilder.DropForeignKey(
                name: "FK_Visitor_MasterCenter_VehicleCenterID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropForeignKey(
                name: "FK_Visitor_MasterCenter_VisitMasterCenterID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropIndex(
                name: "IX_Visitor_VehicleCenterID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropIndex(
                name: "IX_Visitor_VisitMasterCenterID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropIndex(
                name: "IX_ContactPhone_PhoneTypeID",
                schema: "CTM",
                table: "ContactPhone");

            migrationBuilder.DropIndex(
                name: "IX_ContactAddress_DistrictENID",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.DropIndex(
                name: "IX_ContactAddress_DistrictTHID",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.DropIndex(
                name: "IX_ContactAddress_ProvinceENID",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.DropIndex(
                name: "IX_ContactAddress_ProvinceTHID",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.DropIndex(
                name: "IX_ContactAddress_SubDistrictENID",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.DropIndex(
                name: "IX_ContactAddress_SubDistrictTHID",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.DropIndex(
                name: "IX_Contact_CitizenDistrictENID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Contact_CitizenDistrictTHID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Contact_CitizenProvinceENID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Contact_CitizenProvinceTHID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Contact_CitizenSubDistrictENID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Contact_CitizenSubDistrictTHID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Contact_GenderMasterCenterID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Contact_HomeDistrictENID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Contact_HomeDistrictTHID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Contact_HomeProvinceENID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Contact_HomeProvinceTHID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Contact_HomeSubDistrictENID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Contact_HomeSubDistrictTHID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Contact_TitleENMasterCenterID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Contact_TitleTHMasterCenterID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Contact_WorkDistrictENID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Contact_WorkDistrictTHID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Contact_WorkProvinceENID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Contact_WorkProvinceTHID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Contact_WorkSubDistrictENID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Contact_WorkSubDistrictTHID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "ContactSupervisor",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "FileAttachmentID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "ReceiveNumber",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "VehicleCenterID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "VisitMasterCenterID",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "WalkStatus",
                schema: "CTM",
                table: "Visitor");

            migrationBuilder.DropColumn(
                name: "PhoneTypeID",
                schema: "CTM",
                table: "ContactPhone");

            migrationBuilder.DropColumn(
                name: "DistrictENID",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.DropColumn(
                name: "DistrictTHID",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.DropColumn(
                name: "ProvinceENID",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.DropColumn(
                name: "ProvinceTHID",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.DropColumn(
                name: "SubDistrictENID",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.DropColumn(
                name: "SubDistrictTHID",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.DropColumn(
                name: "CitizenDistrictENID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "CitizenDistrictTHID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "CitizenProvinceENID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "CitizenProvinceTHID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "CitizenSubDistrictENID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "CitizenSubDistrictTHID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "GenderMasterCenterID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "HomeDistrictENID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "HomeDistrictTHID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "HomeProvinceENID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "HomeProvinceTHID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "HomeSubDistrictENID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "HomeSubDistrictTHID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "TitleENMasterCenterID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "TitleTHMasterCenterID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "WorkDistrictENID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "WorkDistrictTHID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "WorkProvinceENID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "WorkProvinceTHID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "WorkSubDistrictENID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "WorkSubDistrictTHID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.AddColumn<string>(
                name: "PhoneType",
                schema: "CTM",
                table: "ContactPhone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DistrictEN",
                schema: "CTM",
                table: "ContactAddress",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DistrictTH",
                schema: "CTM",
                table: "ContactAddress",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProvinceEN",
                schema: "CTM",
                table: "ContactAddress",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProvinceTH",
                schema: "CTM",
                table: "ContactAddress",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubDistrictEN",
                schema: "CTM",
                table: "ContactAddress",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubDistrictTH",
                schema: "CTM",
                table: "ContactAddress",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CitizenDistrictEN",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CitizenDistrictTH",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CitizenProvinceEN",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CitizenProvinceTH",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CitizenSubDistrictEN",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CitizenSubDistrictTH",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HomeDistrictEN",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HomeDistrictTH",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HomeProvinceEN",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HomeProvinceTH",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HomeSubDistrictEN",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HomeSubDistrictTH",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleEN",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleTH",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkDistrictEN",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkDistrictTH",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkProvinceEN",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkProvinceTH",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkSubDistrictEN",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkSubDistrictTH",
                schema: "CTM",
                table: "Contact",
                nullable: true);
        }
    }
}
