using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class RemoveAddressFromContact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "CitizenCountryEN",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "CitizenCountryTH",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "CitizenDistrictENID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "CitizenDistrictTHID",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "CitizenHouseNoEN",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "CitizenHouseNoTH",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "CitizenMooEN",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "CitizenMooTH",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "CitizenPostalCodeEN",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "CitizenPostalCodeTH",
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
                name: "CitizenRoadEN",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "CitizenRoadTH",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "CitizenSoiEN",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "CitizenSoiTH",
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
                name: "CitizenVillageEN",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "CitizenVillageTH",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "HomeCountryEN",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "HomeCountryTH",
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
                name: "HomeHouseNoEN",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "HomeHouseNoTH",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "HomeMooEN",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "HomeMooTH",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "HomePostalCodeEN",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "HomePostalCodeTH",
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
                name: "HomeRoadEN",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "HomeRoadTH",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "HomeSoiEN",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "HomeSoiTH",
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
                name: "HomeVillageEN",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "HomeVillageTH",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "WorkCountryEN",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "WorkCountryTH",
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
                name: "WorkHouseNoEN",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "WorkHouseNoTH",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "WorkMooEN",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "WorkMooTH",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "WorkPostalCodeEN",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "WorkPostalCodeTH",
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
                name: "WorkRoadEN",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "WorkRoadTH",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "WorkSoiEN",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "WorkSoiTH",
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

            migrationBuilder.DropColumn(
                name: "WorkVillageEN",
                schema: "CTM",
                table: "Contact");

            migrationBuilder.RenameColumn(
                name: "WorkVillageTH",
                schema: "CTM",
                table: "Contact",
                newName: "CitizenExpireDate");

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                schema: "CTM",
                table: "ContactAddress",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostalCode",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.RenameColumn(
                name: "CitizenExpireDate",
                schema: "CTM",
                table: "Contact",
                newName: "WorkVillageTH");

            migrationBuilder.AddColumn<string>(
                name: "CitizenCountryEN",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CitizenCountryTH",
                schema: "CTM",
                table: "Contact",
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

            migrationBuilder.AddColumn<string>(
                name: "CitizenHouseNoEN",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CitizenHouseNoTH",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CitizenMooEN",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CitizenMooTH",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CitizenPostalCodeEN",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CitizenPostalCodeTH",
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

            migrationBuilder.AddColumn<string>(
                name: "CitizenRoadEN",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CitizenRoadTH",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CitizenSoiEN",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CitizenSoiTH",
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

            migrationBuilder.AddColumn<string>(
                name: "CitizenVillageEN",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CitizenVillageTH",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HomeCountryEN",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HomeCountryTH",
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

            migrationBuilder.AddColumn<string>(
                name: "HomeHouseNoEN",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HomeHouseNoTH",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HomeMooEN",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HomeMooTH",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HomePostalCodeEN",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HomePostalCodeTH",
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

            migrationBuilder.AddColumn<string>(
                name: "HomeRoadEN",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HomeRoadTH",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HomeSoiEN",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HomeSoiTH",
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

            migrationBuilder.AddColumn<string>(
                name: "HomeVillageEN",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HomeVillageTH",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkCountryEN",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkCountryTH",
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

            migrationBuilder.AddColumn<string>(
                name: "WorkHouseNoEN",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkHouseNoTH",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkMooEN",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkMooTH",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkPostalCodeEN",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkPostalCodeTH",
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

            migrationBuilder.AddColumn<string>(
                name: "WorkRoadEN",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkRoadTH",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkSoiEN",
                schema: "CTM",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkSoiTH",
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

            migrationBuilder.AddColumn<string>(
                name: "WorkVillageEN",
                schema: "CTM",
                table: "Contact",
                nullable: true);

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
        }
    }
}
