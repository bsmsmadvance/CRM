using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class UpdateContactAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "FK_Lead_MasterCenter_LeadStatusMasterCenterID",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropIndex(
                name: "IX_ContactAddress_DistrictENID",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.DropColumn(
                name: "CountryEN",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.DropColumn(
                name: "DistrictENID",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.RenameColumn(
                name: "Social",
                schema: "CTM",
                table: "Lead",
                newName: "LeadStatus");

            migrationBuilder.RenameColumn(
                name: "LeadStatusMasterCenterID",
                schema: "CTM",
                table: "Lead",
                newName: "SocialMasterCenterID");

            migrationBuilder.RenameIndex(
                name: "IX_Lead_LeadStatusMasterCenterID",
                schema: "CTM",
                table: "Lead",
                newName: "IX_Lead_SocialMasterCenterID");

            migrationBuilder.RenameColumn(
                name: "SubDistrictTHID",
                schema: "CTM",
                table: "ContactAddress",
                newName: "SubDistrictID");

            migrationBuilder.RenameColumn(
                name: "SubDistrictENID",
                schema: "CTM",
                table: "ContactAddress",
                newName: "ProvinceID");

            migrationBuilder.RenameColumn(
                name: "ProvinceTHID",
                schema: "CTM",
                table: "ContactAddress",
                newName: "DistrictID");

            migrationBuilder.RenameColumn(
                name: "ProvinceENID",
                schema: "CTM",
                table: "ContactAddress",
                newName: "CountryID");

            migrationBuilder.RenameColumn(
                name: "PostalCodeTH",
                schema: "CTM",
                table: "ContactAddress",
                newName: "ForeignSubDistrict");

            migrationBuilder.RenameColumn(
                name: "PostalCodeEN",
                schema: "CTM",
                table: "ContactAddress",
                newName: "ForeignProvince");

            migrationBuilder.RenameColumn(
                name: "DistrictTHID",
                schema: "CTM",
                table: "ContactAddress",
                newName: "ContactTypeMasterCenterID");

            migrationBuilder.RenameColumn(
                name: "CountryTH",
                schema: "CTM",
                table: "ContactAddress",
                newName: "ForeignDistrict");

            migrationBuilder.RenameIndex(
                name: "IX_ContactAddress_SubDistrictTHID",
                schema: "CTM",
                table: "ContactAddress",
                newName: "IX_ContactAddress_SubDistrictID");

            migrationBuilder.RenameIndex(
                name: "IX_ContactAddress_SubDistrictENID",
                schema: "CTM",
                table: "ContactAddress",
                newName: "IX_ContactAddress_ProvinceID");

            migrationBuilder.RenameIndex(
                name: "IX_ContactAddress_ProvinceTHID",
                schema: "CTM",
                table: "ContactAddress",
                newName: "IX_ContactAddress_DistrictID");

            migrationBuilder.RenameIndex(
                name: "IX_ContactAddress_ProvinceENID",
                schema: "CTM",
                table: "ContactAddress",
                newName: "IX_ContactAddress_CountryID");

            migrationBuilder.RenameIndex(
                name: "IX_ContactAddress_DistrictTHID",
                schema: "CTM",
                table: "ContactAddress",
                newName: "IX_ContactAddress_ContactTypeMasterCenterID");

            migrationBuilder.AddColumn<string>(
                name: "Compaign",
                schema: "CTM",
                table: "Lead",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Country",
                schema: "MST",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    NameTH = table.Column<string>(nullable: true),
                    NameEN = table.Column<string>(nullable: true),
                    IsShow = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.ID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ContactAddress_MasterCenter_ContactTypeMasterCenterID",
                schema: "CTM",
                table: "ContactAddress",
                column: "ContactTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactAddress_Country_CountryID",
                schema: "CTM",
                table: "ContactAddress",
                column: "CountryID",
                principalSchema: "MST",
                principalTable: "Country",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactAddress_District_DistrictID",
                schema: "CTM",
                table: "ContactAddress",
                column: "DistrictID",
                principalSchema: "MST",
                principalTable: "District",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactAddress_Province_ProvinceID",
                schema: "CTM",
                table: "ContactAddress",
                column: "ProvinceID",
                principalSchema: "MST",
                principalTable: "Province",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactAddress_SubDistrict_SubDistrictID",
                schema: "CTM",
                table: "ContactAddress",
                column: "SubDistrictID",
                principalSchema: "MST",
                principalTable: "SubDistrict",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lead_MasterCenter_SocialMasterCenterID",
                schema: "CTM",
                table: "Lead",
                column: "SocialMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactAddress_MasterCenter_ContactTypeMasterCenterID",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactAddress_Country_CountryID",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactAddress_District_DistrictID",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactAddress_Province_ProvinceID",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactAddress_SubDistrict_SubDistrictID",
                schema: "CTM",
                table: "ContactAddress");

            migrationBuilder.DropForeignKey(
                name: "FK_Lead_MasterCenter_SocialMasterCenterID",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropTable(
                name: "Country",
                schema: "MST");

            migrationBuilder.DropColumn(
                name: "Compaign",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.RenameColumn(
                name: "SocialMasterCenterID",
                schema: "CTM",
                table: "Lead",
                newName: "LeadStatusMasterCenterID");

            migrationBuilder.RenameColumn(
                name: "LeadStatus",
                schema: "CTM",
                table: "Lead",
                newName: "Social");

            migrationBuilder.RenameIndex(
                name: "IX_Lead_SocialMasterCenterID",
                schema: "CTM",
                table: "Lead",
                newName: "IX_Lead_LeadStatusMasterCenterID");

            migrationBuilder.RenameColumn(
                name: "SubDistrictID",
                schema: "CTM",
                table: "ContactAddress",
                newName: "SubDistrictTHID");

            migrationBuilder.RenameColumn(
                name: "ProvinceID",
                schema: "CTM",
                table: "ContactAddress",
                newName: "SubDistrictENID");

            migrationBuilder.RenameColumn(
                name: "ForeignSubDistrict",
                schema: "CTM",
                table: "ContactAddress",
                newName: "PostalCodeTH");

            migrationBuilder.RenameColumn(
                name: "ForeignProvince",
                schema: "CTM",
                table: "ContactAddress",
                newName: "PostalCodeEN");

            migrationBuilder.RenameColumn(
                name: "ForeignDistrict",
                schema: "CTM",
                table: "ContactAddress",
                newName: "CountryTH");

            migrationBuilder.RenameColumn(
                name: "DistrictID",
                schema: "CTM",
                table: "ContactAddress",
                newName: "ProvinceTHID");

            migrationBuilder.RenameColumn(
                name: "CountryID",
                schema: "CTM",
                table: "ContactAddress",
                newName: "ProvinceENID");

            migrationBuilder.RenameColumn(
                name: "ContactTypeMasterCenterID",
                schema: "CTM",
                table: "ContactAddress",
                newName: "DistrictTHID");

            migrationBuilder.RenameIndex(
                name: "IX_ContactAddress_SubDistrictID",
                schema: "CTM",
                table: "ContactAddress",
                newName: "IX_ContactAddress_SubDistrictTHID");

            migrationBuilder.RenameIndex(
                name: "IX_ContactAddress_ProvinceID",
                schema: "CTM",
                table: "ContactAddress",
                newName: "IX_ContactAddress_SubDistrictENID");

            migrationBuilder.RenameIndex(
                name: "IX_ContactAddress_DistrictID",
                schema: "CTM",
                table: "ContactAddress",
                newName: "IX_ContactAddress_ProvinceTHID");

            migrationBuilder.RenameIndex(
                name: "IX_ContactAddress_CountryID",
                schema: "CTM",
                table: "ContactAddress",
                newName: "IX_ContactAddress_ProvinceENID");

            migrationBuilder.RenameIndex(
                name: "IX_ContactAddress_ContactTypeMasterCenterID",
                schema: "CTM",
                table: "ContactAddress",
                newName: "IX_ContactAddress_DistrictTHID");

            migrationBuilder.AddColumn<string>(
                name: "CountryEN",
                schema: "CTM",
                table: "ContactAddress",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DistrictENID",
                schema: "CTM",
                table: "ContactAddress",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContactAddress_DistrictENID",
                schema: "CTM",
                table: "ContactAddress",
                column: "DistrictENID");

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
                name: "FK_Lead_MasterCenter_LeadStatusMasterCenterID",
                schema: "CTM",
                table: "Lead",
                column: "LeadStatusMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
