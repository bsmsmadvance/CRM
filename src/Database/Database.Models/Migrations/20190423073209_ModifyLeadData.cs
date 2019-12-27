using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ModifyLeadData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lead_MasterCenter_SocialMasterCenterID",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.RenameColumn(
                name: "SocialMasterCenterID",
                schema: "CTM",
                table: "Lead",
                newName: "OwnerID");

            migrationBuilder.RenameColumn(
                name: "LeadSuperVisor",
                schema: "CTM",
                table: "Lead",
                newName: "VillageTH");

            migrationBuilder.RenameIndex(
                name: "IX_Lead_SocialMasterCenterID",
                schema: "CTM",
                table: "Lead",
                newName: "IX_Lead_OwnerID");

            migrationBuilder.AddColumn<Guid>(
                name: "AdvertisementMasterCenterID",
                schema: "CTM",
                table: "Lead",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CallBack",
                schema: "CTM",
                table: "Lead",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CitizenIdentityNo",
                schema: "CTM",
                table: "Lead",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                schema: "CTM",
                table: "Lead",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "District",
                schema: "CTM",
                table: "Lead",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DistrictOfWorking",
                schema: "CTM",
                table: "Lead",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "CTM",
                table: "Lead",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HouseNoTH",
                schema: "CTM",
                table: "Lead",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LeadVisitDate",
                schema: "CTM",
                table: "Lead",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LeadVisitTime",
                schema: "CTM",
                table: "Lead",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MooTH",
                schema: "CTM",
                table: "Lead",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfContact",
                schema: "CTM",
                table: "Lead",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfPerson",
                schema: "CTM",
                table: "Lead",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfUnit",
                schema: "CTM",
                table: "Lead",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                schema: "CTM",
                table: "Lead",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Province",
                schema: "CTM",
                table: "Lead",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProvinceOfWorking",
                schema: "CTM",
                table: "Lead",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Road",
                schema: "CTM",
                table: "Lead",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoomSize",
                schema: "CTM",
                table: "Lead",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoomType",
                schema: "CTM",
                table: "Lead",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SoiTH",
                schema: "CTM",
                table: "Lead",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubDistrict",
                schema: "CTM",
                table: "Lead",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telephone",
                schema: "CTM",
                table: "Lead",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TelephoneExt",
                schema: "CTM",
                table: "Lead",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UTMCampaign",
                schema: "CTM",
                table: "Lead",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UTMMedium",
                schema: "CTM",
                table: "Lead",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UTMSource",
                schema: "CTM",
                table: "Lead",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lead_AdvertisementMasterCenterID",
                schema: "CTM",
                table: "Lead",
                column: "AdvertisementMasterCenterID");

            migrationBuilder.AddForeignKey(
                name: "FK_Lead_MasterCenter_AdvertisementMasterCenterID",
                schema: "CTM",
                table: "Lead",
                column: "AdvertisementMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lead_User_OwnerID",
                schema: "CTM",
                table: "Lead",
                column: "OwnerID",
                principalSchema: "USR",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lead_MasterCenter_AdvertisementMasterCenterID",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropForeignKey(
                name: "FK_Lead_User_OwnerID",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropIndex(
                name: "IX_Lead_AdvertisementMasterCenterID",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "AdvertisementMasterCenterID",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "CallBack",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "CitizenIdentityNo",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "Country",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "District",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "DistrictOfWorking",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "Email",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "HouseNoTH",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "LeadVisitDate",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "LeadVisitTime",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "MooTH",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "NumberOfContact",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "NumberOfPerson",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "NumberOfUnit",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "Province",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "ProvinceOfWorking",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "Road",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "RoomSize",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "RoomType",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "SoiTH",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "SubDistrict",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "Telephone",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "TelephoneExt",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "UTMCampaign",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "UTMMedium",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.DropColumn(
                name: "UTMSource",
                schema: "CTM",
                table: "Lead");

            migrationBuilder.RenameColumn(
                name: "VillageTH",
                schema: "CTM",
                table: "Lead",
                newName: "LeadSuperVisor");

            migrationBuilder.RenameColumn(
                name: "OwnerID",
                schema: "CTM",
                table: "Lead",
                newName: "SocialMasterCenterID");

            migrationBuilder.RenameIndex(
                name: "IX_Lead_OwnerID",
                schema: "CTM",
                table: "Lead",
                newName: "IX_Lead_SocialMasterCenterID");

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
    }
}
