using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class CreateOwnerTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AgreementOwner_Contact_ContactID",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropTable(
                name: "BookingCustomer",
                schema: "SAL");

            migrationBuilder.DropIndex(
                name: "IX_AgreementOwner_ContactID",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "ContactID",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.RenameColumn(
                name: "OwnerType",
                schema: "SAL",
                table: "AgreementOwner",
                newName: "ContactNo");

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                schema: "SAL",
                table: "AgreementOwner",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CitizenExpireDate",
                schema: "SAL",
                table: "AgreementOwner",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CitizenIdentityNo",
                schema: "SAL",
                table: "AgreementOwner",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactFirstName",
                schema: "SAL",
                table: "AgreementOwner",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactLastname",
                schema: "SAL",
                table: "AgreementOwner",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ContactTitleENMasterCenterID",
                schema: "SAL",
                table: "AgreementOwner",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ContactTitleTHMasterCenterID",
                schema: "SAL",
                table: "AgreementOwner",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ContactTypeMasterCenterID",
                schema: "SAL",
                table: "AgreementOwner",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FatherName",
                schema: "SAL",
                table: "AgreementOwner",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FatherNational",
                schema: "SAL",
                table: "AgreementOwner",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FatherOtherNational",
                schema: "SAL",
                table: "AgreementOwner",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstNameEN",
                schema: "SAL",
                table: "AgreementOwner",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstNameTH",
                schema: "SAL",
                table: "AgreementOwner",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FromContactID",
                schema: "SAL",
                table: "AgreementOwner",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GenderMasterCenterID",
                schema: "SAL",
                table: "AgreementOwner",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsMainOwner",
                schema: "SAL",
                table: "AgreementOwner",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsThaiNationality",
                schema: "SAL",
                table: "AgreementOwner",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVIP",
                schema: "SAL",
                table: "AgreementOwner",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastNameEN",
                schema: "SAL",
                table: "AgreementOwner",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastNameTH",
                schema: "SAL",
                table: "AgreementOwner",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LineID",
                schema: "SAL",
                table: "AgreementOwner",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MarriageName",
                schema: "SAL",
                table: "AgreementOwner",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MarriageNational",
                schema: "SAL",
                table: "AgreementOwner",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MarriageOtherNational",
                schema: "SAL",
                table: "AgreementOwner",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MiddleNameEN",
                schema: "SAL",
                table: "AgreementOwner",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MiddleNameTH",
                schema: "SAL",
                table: "AgreementOwner",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MotherName",
                schema: "SAL",
                table: "AgreementOwner",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MotherNational",
                schema: "SAL",
                table: "AgreementOwner",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MotherOtherNational",
                schema: "SAL",
                table: "AgreementOwner",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "NationalMasterCenterID",
                schema: "SAL",
                table: "AgreementOwner",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nickname",
                schema: "SAL",
                table: "AgreementOwner",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                schema: "SAL",
                table: "AgreementOwner",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumberExt",
                schema: "SAL",
                table: "AgreementOwner",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaxID",
                schema: "SAL",
                table: "AgreementOwner",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleExtEN",
                schema: "SAL",
                table: "AgreementOwner",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleExtTH",
                schema: "SAL",
                table: "AgreementOwner",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WeChatID",
                schema: "SAL",
                table: "AgreementOwner",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WhatsAppID",
                schema: "SAL",
                table: "AgreementOwner",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AgreementOwnerAddress",
                schema: "SAL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsFromMigration = table.Column<bool>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false),
                    AgreementOwnerID = table.Column<Guid>(nullable: false),
                    FromContactAddressID = table.Column<Guid>(nullable: true),
                    ContactAddressTypeMasterCenterID = table.Column<Guid>(nullable: true),
                    HouseNoTH = table.Column<string>(maxLength: 100, nullable: true),
                    MooTH = table.Column<string>(maxLength: 100, nullable: true),
                    VillageTH = table.Column<string>(maxLength: 1000, nullable: true),
                    SoiTH = table.Column<string>(maxLength: 100, nullable: true),
                    RoadTH = table.Column<string>(maxLength: 100, nullable: true),
                    HouseNoEN = table.Column<string>(maxLength: 100, nullable: true),
                    MooEN = table.Column<string>(maxLength: 100, nullable: true),
                    VillageEN = table.Column<string>(maxLength: 1000, nullable: true),
                    SoiEN = table.Column<string>(maxLength: 100, nullable: true),
                    RoadEN = table.Column<string>(maxLength: 100, nullable: true),
                    PostalCode = table.Column<string>(maxLength: 50, nullable: true),
                    CountryID = table.Column<Guid>(nullable: true),
                    ProvinceID = table.Column<Guid>(nullable: true),
                    DistrictID = table.Column<Guid>(nullable: true),
                    SubDistrictID = table.Column<Guid>(nullable: true),
                    ForeignProvince = table.Column<string>(maxLength: 100, nullable: true),
                    ForeignDistrict = table.Column<string>(maxLength: 100, nullable: true),
                    ForeignSubDistrict = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgreementOwnerAddress", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AgreementOwnerAddress_AgreementOwner_AgreementOwnerID",
                        column: x => x.AgreementOwnerID,
                        principalSchema: "SAL",
                        principalTable: "AgreementOwner",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgreementOwnerAddress_MasterCenter_ContactAddressTypeMasterCenterID",
                        column: x => x.ContactAddressTypeMasterCenterID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AgreementOwnerAddress_Country_CountryID",
                        column: x => x.CountryID,
                        principalSchema: "MST",
                        principalTable: "Country",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AgreementOwnerAddress_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AgreementOwnerAddress_District_DistrictID",
                        column: x => x.DistrictID,
                        principalSchema: "MST",
                        principalTable: "District",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AgreementOwnerAddress_ContactAddress_FromContactAddressID",
                        column: x => x.FromContactAddressID,
                        principalSchema: "CTM",
                        principalTable: "ContactAddress",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AgreementOwnerAddress_Province_ProvinceID",
                        column: x => x.ProvinceID,
                        principalSchema: "MST",
                        principalTable: "Province",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AgreementOwnerAddress_SubDistrict_SubDistrictID",
                        column: x => x.SubDistrictID,
                        principalSchema: "MST",
                        principalTable: "SubDistrict",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AgreementOwnerAddress_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AgreementOwnerEmail",
                schema: "SAL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsFromMigration = table.Column<bool>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false),
                    AgreementOwnerID = table.Column<Guid>(nullable: false),
                    FromContactEmailID = table.Column<Guid>(nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: true),
                    IsMain = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgreementOwnerEmail", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AgreementOwnerEmail_AgreementOwner_AgreementOwnerID",
                        column: x => x.AgreementOwnerID,
                        principalSchema: "SAL",
                        principalTable: "AgreementOwner",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgreementOwnerEmail_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AgreementOwnerEmail_ContactEmail_FromContactEmailID",
                        column: x => x.FromContactEmailID,
                        principalSchema: "CTM",
                        principalTable: "ContactEmail",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AgreementOwnerEmail_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AgreementOwnerPhone",
                schema: "SAL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsFromMigration = table.Column<bool>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false),
                    AgreementOwnerID = table.Column<Guid>(nullable: false),
                    FromContactPhoneID = table.Column<Guid>(nullable: true),
                    PhoneTypeMasterCenterID = table.Column<Guid>(nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 100, nullable: true),
                    PhoneNumberExt = table.Column<string>(maxLength: 100, nullable: true),
                    CountryCode = table.Column<string>(maxLength: 50, nullable: true),
                    IsMain = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgreementOwnerPhone", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AgreementOwnerPhone_AgreementOwner_AgreementOwnerID",
                        column: x => x.AgreementOwnerID,
                        principalSchema: "SAL",
                        principalTable: "AgreementOwner",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgreementOwnerPhone_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AgreementOwnerPhone_ContactPhone_FromContactPhoneID",
                        column: x => x.FromContactPhoneID,
                        principalSchema: "CTM",
                        principalTable: "ContactPhone",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AgreementOwnerPhone_MasterCenter_PhoneTypeMasterCenterID",
                        column: x => x.PhoneTypeMasterCenterID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AgreementOwnerPhone_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookingOwner",
                schema: "SAL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsFromMigration = table.Column<bool>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    BookingID = table.Column<Guid>(nullable: true),
                    FromContactID = table.Column<Guid>(nullable: true),
                    IsMainOwner = table.Column<bool>(nullable: false),
                    ContactNo = table.Column<string>(nullable: true),
                    ContactTypeMasterCenterID = table.Column<Guid>(nullable: true),
                    ContactTitleTHMasterCenterID = table.Column<Guid>(nullable: true),
                    TitleExtTH = table.Column<string>(maxLength: 100, nullable: true),
                    FirstNameTH = table.Column<string>(maxLength: 100, nullable: true),
                    MiddleNameTH = table.Column<string>(maxLength: 100, nullable: true),
                    LastNameTH = table.Column<string>(maxLength: 100, nullable: true),
                    Nickname = table.Column<string>(maxLength: 100, nullable: true),
                    ContactTitleENMasterCenterID = table.Column<Guid>(nullable: true),
                    TitleExtEN = table.Column<string>(maxLength: 100, nullable: true),
                    FirstNameEN = table.Column<string>(maxLength: 100, nullable: true),
                    MiddleNameEN = table.Column<string>(maxLength: 100, nullable: true),
                    LastNameEN = table.Column<string>(maxLength: 100, nullable: true),
                    CitizenIdentityNo = table.Column<string>(maxLength: 50, nullable: true),
                    CitizenExpireDate = table.Column<DateTime>(nullable: true),
                    NationalMasterCenterID = table.Column<Guid>(nullable: true),
                    GenderMasterCenterID = table.Column<Guid>(nullable: true),
                    TaxID = table.Column<string>(maxLength: 100, nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 50, nullable: true),
                    PhoneNumberExt = table.Column<string>(maxLength: 50, nullable: true),
                    ContactFirstName = table.Column<string>(maxLength: 100, nullable: true),
                    ContactLastname = table.Column<string>(maxLength: 100, nullable: true),
                    WeChatID = table.Column<string>(maxLength: 100, nullable: true),
                    WhatsAppID = table.Column<string>(maxLength: 100, nullable: true),
                    LineID = table.Column<string>(maxLength: 100, nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: true),
                    MarriageName = table.Column<string>(maxLength: 100, nullable: true),
                    MarriageNational = table.Column<string>(maxLength: 100, nullable: true),
                    MarriageOtherNational = table.Column<string>(maxLength: 100, nullable: true),
                    FatherName = table.Column<string>(maxLength: 100, nullable: true),
                    FatherNational = table.Column<string>(maxLength: 100, nullable: true),
                    FatherOtherNational = table.Column<string>(maxLength: 100, nullable: true),
                    MotherName = table.Column<string>(maxLength: 100, nullable: true),
                    MotherNational = table.Column<string>(maxLength: 100, nullable: true),
                    MotherOtherNational = table.Column<string>(maxLength: 100, nullable: true),
                    IsVIP = table.Column<bool>(nullable: false),
                    IsThaiNationality = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingOwner", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BookingOwner_Booking_BookingID",
                        column: x => x.BookingID,
                        principalSchema: "SAL",
                        principalTable: "Booking",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingOwner_MasterCenter_ContactTitleENMasterCenterID",
                        column: x => x.ContactTitleENMasterCenterID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingOwner_MasterCenter_ContactTitleTHMasterCenterID",
                        column: x => x.ContactTitleTHMasterCenterID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingOwner_MasterCenter_ContactTypeMasterCenterID",
                        column: x => x.ContactTypeMasterCenterID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingOwner_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingOwner_Contact_FromContactID",
                        column: x => x.FromContactID,
                        principalSchema: "CTM",
                        principalTable: "Contact",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingOwner_MasterCenter_GenderMasterCenterID",
                        column: x => x.GenderMasterCenterID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingOwner_MasterCenter_NationalMasterCenterID",
                        column: x => x.NationalMasterCenterID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingOwner_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookingOwnerAddress",
                schema: "SAL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsFromMigration = table.Column<bool>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false),
                    BookingOwnerID = table.Column<Guid>(nullable: false),
                    FromContactAddressID = table.Column<Guid>(nullable: true),
                    ContactAddressTypeMasterCenterID = table.Column<Guid>(nullable: true),
                    HouseNoTH = table.Column<string>(maxLength: 100, nullable: true),
                    MooTH = table.Column<string>(maxLength: 100, nullable: true),
                    VillageTH = table.Column<string>(maxLength: 1000, nullable: true),
                    SoiTH = table.Column<string>(maxLength: 100, nullable: true),
                    RoadTH = table.Column<string>(maxLength: 100, nullable: true),
                    HouseNoEN = table.Column<string>(maxLength: 100, nullable: true),
                    MooEN = table.Column<string>(maxLength: 100, nullable: true),
                    VillageEN = table.Column<string>(maxLength: 1000, nullable: true),
                    SoiEN = table.Column<string>(maxLength: 100, nullable: true),
                    RoadEN = table.Column<string>(maxLength: 100, nullable: true),
                    PostalCode = table.Column<string>(maxLength: 50, nullable: true),
                    CountryID = table.Column<Guid>(nullable: true),
                    ProvinceID = table.Column<Guid>(nullable: true),
                    DistrictID = table.Column<Guid>(nullable: true),
                    SubDistrictID = table.Column<Guid>(nullable: true),
                    ForeignProvince = table.Column<string>(maxLength: 100, nullable: true),
                    ForeignDistrict = table.Column<string>(maxLength: 100, nullable: true),
                    ForeignSubDistrict = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingOwnerAddress", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BookingOwnerAddress_BookingOwner_BookingOwnerID",
                        column: x => x.BookingOwnerID,
                        principalSchema: "SAL",
                        principalTable: "BookingOwner",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingOwnerAddress_MasterCenter_ContactAddressTypeMasterCenterID",
                        column: x => x.ContactAddressTypeMasterCenterID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingOwnerAddress_Country_CountryID",
                        column: x => x.CountryID,
                        principalSchema: "MST",
                        principalTable: "Country",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingOwnerAddress_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingOwnerAddress_District_DistrictID",
                        column: x => x.DistrictID,
                        principalSchema: "MST",
                        principalTable: "District",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingOwnerAddress_ContactAddress_FromContactAddressID",
                        column: x => x.FromContactAddressID,
                        principalSchema: "CTM",
                        principalTable: "ContactAddress",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingOwnerAddress_Province_ProvinceID",
                        column: x => x.ProvinceID,
                        principalSchema: "MST",
                        principalTable: "Province",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingOwnerAddress_SubDistrict_SubDistrictID",
                        column: x => x.SubDistrictID,
                        principalSchema: "MST",
                        principalTable: "SubDistrict",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingOwnerAddress_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookingOwnerEmail",
                schema: "SAL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsFromMigration = table.Column<bool>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false),
                    BookingOwnerID = table.Column<Guid>(nullable: false),
                    FromContactEmailID = table.Column<Guid>(nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: true),
                    IsMain = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingOwnerEmail", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BookingOwnerEmail_BookingOwner_BookingOwnerID",
                        column: x => x.BookingOwnerID,
                        principalSchema: "SAL",
                        principalTable: "BookingOwner",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingOwnerEmail_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingOwnerEmail_ContactEmail_FromContactEmailID",
                        column: x => x.FromContactEmailID,
                        principalSchema: "CTM",
                        principalTable: "ContactEmail",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingOwnerEmail_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookingOwnerPhone",
                schema: "SAL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsFromMigration = table.Column<bool>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false),
                    BookingOwnerID = table.Column<Guid>(nullable: false),
                    FromContactPhoneID = table.Column<Guid>(nullable: true),
                    PhoneTypeMasterCenterID = table.Column<Guid>(nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 100, nullable: true),
                    PhoneNumberExt = table.Column<string>(maxLength: 100, nullable: true),
                    CountryCode = table.Column<string>(maxLength: 50, nullable: true),
                    IsMain = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingOwnerPhone", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BookingOwnerPhone_BookingOwner_BookingOwnerID",
                        column: x => x.BookingOwnerID,
                        principalSchema: "SAL",
                        principalTable: "BookingOwner",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingOwnerPhone_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingOwnerPhone_ContactPhone_FromContactPhoneID",
                        column: x => x.FromContactPhoneID,
                        principalSchema: "CTM",
                        principalTable: "ContactPhone",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingOwnerPhone_MasterCenter_PhoneTypeMasterCenterID",
                        column: x => x.PhoneTypeMasterCenterID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingOwnerPhone_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgreementOwner_ContactTitleENMasterCenterID",
                schema: "SAL",
                table: "AgreementOwner",
                column: "ContactTitleENMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_AgreementOwner_ContactTitleTHMasterCenterID",
                schema: "SAL",
                table: "AgreementOwner",
                column: "ContactTitleTHMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_AgreementOwner_ContactTypeMasterCenterID",
                schema: "SAL",
                table: "AgreementOwner",
                column: "ContactTypeMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_AgreementOwner_FromContactID",
                schema: "SAL",
                table: "AgreementOwner",
                column: "FromContactID");

            migrationBuilder.CreateIndex(
                name: "IX_AgreementOwner_GenderMasterCenterID",
                schema: "SAL",
                table: "AgreementOwner",
                column: "GenderMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_AgreementOwner_NationalMasterCenterID",
                schema: "SAL",
                table: "AgreementOwner",
                column: "NationalMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_AgreementOwnerAddress_AgreementOwnerID",
                schema: "SAL",
                table: "AgreementOwnerAddress",
                column: "AgreementOwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_AgreementOwnerAddress_ContactAddressTypeMasterCenterID",
                schema: "SAL",
                table: "AgreementOwnerAddress",
                column: "ContactAddressTypeMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_AgreementOwnerAddress_CountryID",
                schema: "SAL",
                table: "AgreementOwnerAddress",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_AgreementOwnerAddress_CreatedByUserID",
                schema: "SAL",
                table: "AgreementOwnerAddress",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_AgreementOwnerAddress_DistrictID",
                schema: "SAL",
                table: "AgreementOwnerAddress",
                column: "DistrictID");

            migrationBuilder.CreateIndex(
                name: "IX_AgreementOwnerAddress_FromContactAddressID",
                schema: "SAL",
                table: "AgreementOwnerAddress",
                column: "FromContactAddressID");

            migrationBuilder.CreateIndex(
                name: "IX_AgreementOwnerAddress_ProvinceID",
                schema: "SAL",
                table: "AgreementOwnerAddress",
                column: "ProvinceID");

            migrationBuilder.CreateIndex(
                name: "IX_AgreementOwnerAddress_SubDistrictID",
                schema: "SAL",
                table: "AgreementOwnerAddress",
                column: "SubDistrictID");

            migrationBuilder.CreateIndex(
                name: "IX_AgreementOwnerAddress_UpdatedByUserID",
                schema: "SAL",
                table: "AgreementOwnerAddress",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_AgreementOwnerEmail_AgreementOwnerID",
                schema: "SAL",
                table: "AgreementOwnerEmail",
                column: "AgreementOwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_AgreementOwnerEmail_CreatedByUserID",
                schema: "SAL",
                table: "AgreementOwnerEmail",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_AgreementOwnerEmail_FromContactEmailID",
                schema: "SAL",
                table: "AgreementOwnerEmail",
                column: "FromContactEmailID");

            migrationBuilder.CreateIndex(
                name: "IX_AgreementOwnerEmail_UpdatedByUserID",
                schema: "SAL",
                table: "AgreementOwnerEmail",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_AgreementOwnerPhone_AgreementOwnerID",
                schema: "SAL",
                table: "AgreementOwnerPhone",
                column: "AgreementOwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_AgreementOwnerPhone_CreatedByUserID",
                schema: "SAL",
                table: "AgreementOwnerPhone",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_AgreementOwnerPhone_FromContactPhoneID",
                schema: "SAL",
                table: "AgreementOwnerPhone",
                column: "FromContactPhoneID");

            migrationBuilder.CreateIndex(
                name: "IX_AgreementOwnerPhone_PhoneTypeMasterCenterID",
                schema: "SAL",
                table: "AgreementOwnerPhone",
                column: "PhoneTypeMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_AgreementOwnerPhone_UpdatedByUserID",
                schema: "SAL",
                table: "AgreementOwnerPhone",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingOwner_BookingID",
                schema: "SAL",
                table: "BookingOwner",
                column: "BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingOwner_ContactTitleENMasterCenterID",
                schema: "SAL",
                table: "BookingOwner",
                column: "ContactTitleENMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingOwner_ContactTitleTHMasterCenterID",
                schema: "SAL",
                table: "BookingOwner",
                column: "ContactTitleTHMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingOwner_ContactTypeMasterCenterID",
                schema: "SAL",
                table: "BookingOwner",
                column: "ContactTypeMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingOwner_CreatedByUserID",
                schema: "SAL",
                table: "BookingOwner",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingOwner_FromContactID",
                schema: "SAL",
                table: "BookingOwner",
                column: "FromContactID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingOwner_GenderMasterCenterID",
                schema: "SAL",
                table: "BookingOwner",
                column: "GenderMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingOwner_NationalMasterCenterID",
                schema: "SAL",
                table: "BookingOwner",
                column: "NationalMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingOwner_UpdatedByUserID",
                schema: "SAL",
                table: "BookingOwner",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingOwnerAddress_BookingOwnerID",
                schema: "SAL",
                table: "BookingOwnerAddress",
                column: "BookingOwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingOwnerAddress_ContactAddressTypeMasterCenterID",
                schema: "SAL",
                table: "BookingOwnerAddress",
                column: "ContactAddressTypeMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingOwnerAddress_CountryID",
                schema: "SAL",
                table: "BookingOwnerAddress",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingOwnerAddress_CreatedByUserID",
                schema: "SAL",
                table: "BookingOwnerAddress",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingOwnerAddress_DistrictID",
                schema: "SAL",
                table: "BookingOwnerAddress",
                column: "DistrictID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingOwnerAddress_FromContactAddressID",
                schema: "SAL",
                table: "BookingOwnerAddress",
                column: "FromContactAddressID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingOwnerAddress_ProvinceID",
                schema: "SAL",
                table: "BookingOwnerAddress",
                column: "ProvinceID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingOwnerAddress_SubDistrictID",
                schema: "SAL",
                table: "BookingOwnerAddress",
                column: "SubDistrictID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingOwnerAddress_UpdatedByUserID",
                schema: "SAL",
                table: "BookingOwnerAddress",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingOwnerEmail_BookingOwnerID",
                schema: "SAL",
                table: "BookingOwnerEmail",
                column: "BookingOwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingOwnerEmail_CreatedByUserID",
                schema: "SAL",
                table: "BookingOwnerEmail",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingOwnerEmail_FromContactEmailID",
                schema: "SAL",
                table: "BookingOwnerEmail",
                column: "FromContactEmailID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingOwnerEmail_UpdatedByUserID",
                schema: "SAL",
                table: "BookingOwnerEmail",
                column: "UpdatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingOwnerPhone_BookingOwnerID",
                schema: "SAL",
                table: "BookingOwnerPhone",
                column: "BookingOwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingOwnerPhone_CreatedByUserID",
                schema: "SAL",
                table: "BookingOwnerPhone",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingOwnerPhone_FromContactPhoneID",
                schema: "SAL",
                table: "BookingOwnerPhone",
                column: "FromContactPhoneID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingOwnerPhone_PhoneTypeMasterCenterID",
                schema: "SAL",
                table: "BookingOwnerPhone",
                column: "PhoneTypeMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingOwnerPhone_UpdatedByUserID",
                schema: "SAL",
                table: "BookingOwnerPhone",
                column: "UpdatedByUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_AgreementOwner_MasterCenter_ContactTitleENMasterCenterID",
                schema: "SAL",
                table: "AgreementOwner",
                column: "ContactTitleENMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AgreementOwner_MasterCenter_ContactTitleTHMasterCenterID",
                schema: "SAL",
                table: "AgreementOwner",
                column: "ContactTitleTHMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AgreementOwner_MasterCenter_ContactTypeMasterCenterID",
                schema: "SAL",
                table: "AgreementOwner",
                column: "ContactTypeMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AgreementOwner_Contact_FromContactID",
                schema: "SAL",
                table: "AgreementOwner",
                column: "FromContactID",
                principalSchema: "CTM",
                principalTable: "Contact",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AgreementOwner_MasterCenter_GenderMasterCenterID",
                schema: "SAL",
                table: "AgreementOwner",
                column: "GenderMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AgreementOwner_MasterCenter_NationalMasterCenterID",
                schema: "SAL",
                table: "AgreementOwner",
                column: "NationalMasterCenterID",
                principalSchema: "MST",
                principalTable: "MasterCenter",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AgreementOwner_MasterCenter_ContactTitleENMasterCenterID",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_AgreementOwner_MasterCenter_ContactTitleTHMasterCenterID",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_AgreementOwner_MasterCenter_ContactTypeMasterCenterID",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_AgreementOwner_Contact_FromContactID",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_AgreementOwner_MasterCenter_GenderMasterCenterID",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_AgreementOwner_MasterCenter_NationalMasterCenterID",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropTable(
                name: "AgreementOwnerAddress",
                schema: "SAL");

            migrationBuilder.DropTable(
                name: "AgreementOwnerEmail",
                schema: "SAL");

            migrationBuilder.DropTable(
                name: "AgreementOwnerPhone",
                schema: "SAL");

            migrationBuilder.DropTable(
                name: "BookingOwnerAddress",
                schema: "SAL");

            migrationBuilder.DropTable(
                name: "BookingOwnerEmail",
                schema: "SAL");

            migrationBuilder.DropTable(
                name: "BookingOwnerPhone",
                schema: "SAL");

            migrationBuilder.DropTable(
                name: "BookingOwner",
                schema: "SAL");

            migrationBuilder.DropIndex(
                name: "IX_AgreementOwner_ContactTitleENMasterCenterID",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropIndex(
                name: "IX_AgreementOwner_ContactTitleTHMasterCenterID",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropIndex(
                name: "IX_AgreementOwner_ContactTypeMasterCenterID",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropIndex(
                name: "IX_AgreementOwner_FromContactID",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropIndex(
                name: "IX_AgreementOwner_GenderMasterCenterID",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropIndex(
                name: "IX_AgreementOwner_NationalMasterCenterID",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "CitizenExpireDate",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "CitizenIdentityNo",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "ContactFirstName",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "ContactLastname",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "ContactTitleENMasterCenterID",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "ContactTitleTHMasterCenterID",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "ContactTypeMasterCenterID",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "FatherName",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "FatherNational",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "FatherOtherNational",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "FirstNameEN",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "FirstNameTH",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "FromContactID",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "GenderMasterCenterID",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "IsMainOwner",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "IsThaiNationality",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "IsVIP",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "LastNameEN",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "LastNameTH",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "LineID",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "MarriageName",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "MarriageNational",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "MarriageOtherNational",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "MiddleNameEN",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "MiddleNameTH",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "MotherName",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "MotherNational",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "MotherOtherNational",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "NationalMasterCenterID",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "Nickname",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "PhoneNumberExt",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "TaxID",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "TitleExtEN",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "TitleExtTH",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "WeChatID",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.DropColumn(
                name: "WhatsAppID",
                schema: "SAL",
                table: "AgreementOwner");

            migrationBuilder.RenameColumn(
                name: "ContactNo",
                schema: "SAL",
                table: "AgreementOwner",
                newName: "OwnerType");

            migrationBuilder.AddColumn<Guid>(
                name: "ContactID",
                schema: "SAL",
                table: "AgreementOwner",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "BookingCustomer",
                schema: "SAL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    BirthDate = table.Column<DateTime>(nullable: true),
                    BookingID = table.Column<Guid>(nullable: true),
                    CitizenExpireDate = table.Column<DateTime>(nullable: true),
                    CitizenIdentityNo = table.Column<string>(maxLength: 50, nullable: true),
                    ContactFirstName = table.Column<string>(maxLength: 100, nullable: true),
                    ContactLastname = table.Column<string>(maxLength: 100, nullable: true),
                    ContactNo = table.Column<string>(nullable: true),
                    ContactTitleENMasterCenterID = table.Column<Guid>(nullable: true),
                    ContactTitleTHMasterCenterID = table.Column<Guid>(nullable: true),
                    ContactTypeMasterCenterID = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    FatherName = table.Column<string>(maxLength: 100, nullable: true),
                    FatherNational = table.Column<string>(maxLength: 100, nullable: true),
                    FatherOtherNational = table.Column<string>(maxLength: 100, nullable: true),
                    FirstNameEN = table.Column<string>(maxLength: 100, nullable: true),
                    FirstNameTH = table.Column<string>(maxLength: 100, nullable: true),
                    FromContactID = table.Column<Guid>(nullable: true),
                    GenderMasterCenterID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsFromMigration = table.Column<bool>(nullable: false),
                    IsMainCustomer = table.Column<bool>(nullable: false),
                    IsThaiNationality = table.Column<bool>(nullable: false),
                    IsUsed = table.Column<bool>(nullable: false),
                    IsVIP = table.Column<bool>(nullable: false),
                    LastNameEN = table.Column<string>(maxLength: 100, nullable: true),
                    LastNameTH = table.Column<string>(maxLength: 100, nullable: true),
                    LastOpportunityID = table.Column<Guid>(nullable: true),
                    LineID = table.Column<string>(maxLength: 100, nullable: true),
                    MarriageName = table.Column<string>(maxLength: 100, nullable: true),
                    MarriageNational = table.Column<string>(maxLength: 100, nullable: true),
                    MarriageOtherNational = table.Column<string>(maxLength: 100, nullable: true),
                    MiddleNameEN = table.Column<string>(maxLength: 100, nullable: true),
                    MiddleNameTH = table.Column<string>(maxLength: 100, nullable: true),
                    MotherName = table.Column<string>(maxLength: 100, nullable: true),
                    MotherNational = table.Column<string>(maxLength: 100, nullable: true),
                    MotherOtherNational = table.Column<string>(maxLength: 100, nullable: true),
                    NationalMasterCenterID = table.Column<Guid>(nullable: true),
                    Nickname = table.Column<string>(maxLength: 100, nullable: true),
                    OpportunityCount = table.Column<int>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 50, nullable: true),
                    PhoneNumberExt = table.Column<string>(maxLength: 50, nullable: true),
                    TaxID = table.Column<string>(maxLength: 100, nullable: true),
                    TitleExtEN = table.Column<string>(maxLength: 100, nullable: true),
                    TitleExtTH = table.Column<string>(maxLength: 100, nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true),
                    WeChatID = table.Column<string>(maxLength: 100, nullable: true),
                    WhatsAppID = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingCustomer", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BookingCustomer_Booking_BookingID",
                        column: x => x.BookingID,
                        principalSchema: "SAL",
                        principalTable: "Booking",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingCustomer_MasterCenter_ContactTitleENMasterCenterID",
                        column: x => x.ContactTitleENMasterCenterID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingCustomer_MasterCenter_ContactTitleTHMasterCenterID",
                        column: x => x.ContactTitleTHMasterCenterID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingCustomer_MasterCenter_ContactTypeMasterCenterID",
                        column: x => x.ContactTypeMasterCenterID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingCustomer_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingCustomer_Contact_FromContactID",
                        column: x => x.FromContactID,
                        principalSchema: "CTM",
                        principalTable: "Contact",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingCustomer_MasterCenter_GenderMasterCenterID",
                        column: x => x.GenderMasterCenterID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingCustomer_Opportunity_LastOpportunityID",
                        column: x => x.LastOpportunityID,
                        principalSchema: "CTM",
                        principalTable: "Opportunity",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingCustomer_MasterCenter_NationalMasterCenterID",
                        column: x => x.NationalMasterCenterID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingCustomer_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgreementOwner_ContactID",
                schema: "SAL",
                table: "AgreementOwner",
                column: "ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingCustomer_BookingID",
                schema: "SAL",
                table: "BookingCustomer",
                column: "BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingCustomer_ContactTitleENMasterCenterID",
                schema: "SAL",
                table: "BookingCustomer",
                column: "ContactTitleENMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingCustomer_ContactTitleTHMasterCenterID",
                schema: "SAL",
                table: "BookingCustomer",
                column: "ContactTitleTHMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingCustomer_ContactTypeMasterCenterID",
                schema: "SAL",
                table: "BookingCustomer",
                column: "ContactTypeMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingCustomer_CreatedByUserID",
                schema: "SAL",
                table: "BookingCustomer",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingCustomer_FromContactID",
                schema: "SAL",
                table: "BookingCustomer",
                column: "FromContactID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingCustomer_GenderMasterCenterID",
                schema: "SAL",
                table: "BookingCustomer",
                column: "GenderMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingCustomer_LastOpportunityID",
                schema: "SAL",
                table: "BookingCustomer",
                column: "LastOpportunityID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingCustomer_NationalMasterCenterID",
                schema: "SAL",
                table: "BookingCustomer",
                column: "NationalMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingCustomer_UpdatedByUserID",
                schema: "SAL",
                table: "BookingCustomer",
                column: "UpdatedByUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_AgreementOwner_Contact_ContactID",
                schema: "SAL",
                table: "AgreementOwner",
                column: "ContactID",
                principalSchema: "CTM",
                principalTable: "Contact",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
