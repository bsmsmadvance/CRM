using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class InitDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ACC");

            migrationBuilder.EnsureSchema(
                name: "CMS");

            migrationBuilder.EnsureSchema(
                name: "CTM");

            migrationBuilder.EnsureSchema(
                name: "DMT");

            migrationBuilder.EnsureSchema(
                name: "FIN");

            migrationBuilder.EnsureSchema(
                name: "LET");

            migrationBuilder.EnsureSchema(
                name: "MST");

            migrationBuilder.EnsureSchema(
                name: "NTF");

            migrationBuilder.EnsureSchema(
                name: "OST");

            migrationBuilder.EnsureSchema(
                name: "PRJ");

            migrationBuilder.EnsureSchema(
                name: "PRM");

            migrationBuilder.EnsureSchema(
                name: "SAL");

            migrationBuilder.EnsureSchema(
                name: "USR");

            migrationBuilder.EnsureSchema(
                name: "WFL");

            migrationBuilder.CreateTable(
                name: "CalendarLock",
                schema: "ACC",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    LockDate = table.Column<DateTime>(nullable: false),
                    IsLocked = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarLock", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "GLDetail",
                schema: "ACC",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    PostID = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    OperationDate = table.Column<DateTime>(nullable: true),
                    Detail = table.Column<string>(nullable: true),
                    BankAccount = table.Column<string>(nullable: true),
                    DebitCreditAmount = table.Column<decimal>(type: "Money", nullable: false),
                    PostDate = table.Column<DateTime>(nullable: true),
                    PostBy = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GLDetail", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "GLExport",
                schema: "ACC",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    FileName = table.Column<string>(nullable: true),
                    BatchID = table.Column<string>(nullable: true),
                    ExportDate = table.Column<DateTime>(nullable: true),
                    ExportBy = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GLExport", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PostGLAccount",
                schema: "ACC",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    GLType = table.Column<string>(nullable: true),
                    DocCode = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    GLAccountID = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostGLAccount", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PostGLHouseType",
                schema: "ACC",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    GLAccountID = table.Column<string>(nullable: true),
                    IncomeAccountName = table.Column<string>(nullable: true),
                    IncomeAccountNo = table.Column<string>(nullable: true),
                    HouseType = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostGLHouseType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "GeneralSetting",
                schema: "CMS",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ActiveDate = table.Column<DateTime>(nullable: true),
                    LCCenterGuaranteeAmount = table.Column<decimal>(type: "Money", nullable: false),
                    LCCenterAfterSale = table.Column<decimal>(nullable: false),
                    LCCenterAfterSaleAmount = table.Column<decimal>(type: "Money", nullable: false),
                    LCCenterAfterTransfer = table.Column<decimal>(nullable: false),
                    LCCenterAfterTransferAmount = table.Column<decimal>(type: "Money", nullable: false),
                    AfterLaunchAmount = table.Column<decimal>(type: "Money", nullable: false),
                    LaunchStartDate = table.Column<DateTime>(nullable: true),
                    LaunchEndDate = table.Column<DateTime>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralSetting", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RateOnTop",
                schema: "CMS",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ActiveDate = table.Column<DateTime>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    AmountUnit = table.Column<string>(nullable: true),
                    StartRange = table.Column<decimal>(type: "Money", nullable: false),
                    EndRange = table.Column<decimal>(type: "Money", nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RateOnTop", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RateSettingSale",
                schema: "CMS",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ActiveDate = table.Column<DateTime>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    AmountUnit = table.Column<string>(nullable: true),
                    StartRange = table.Column<decimal>(type: "Money", nullable: false),
                    EndRange = table.Column<decimal>(type: "Money", nullable: false),
                    RangeUnit = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RateSettingSale", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RateSettingSaleFix",
                schema: "CMS",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ActiveDate = table.Column<DateTime>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    AmountUnit = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RateSettingSaleFix", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RateSettingTransfer",
                schema: "CMS",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ActiveDate = table.Column<DateTime>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    AmountUnit = table.Column<string>(nullable: true),
                    StartRange = table.Column<decimal>(type: "Money", nullable: false),
                    EndRange = table.Column<decimal>(type: "Money", nullable: false),
                    RangeUnit = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RateSettingTransfer", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                schema: "CTM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ContactType = table.Column<string>(nullable: true),
                    ContactStatus = table.Column<string>(nullable: true),
                    TitleTH = table.Column<string>(nullable: true),
                    TitleExtTH = table.Column<string>(nullable: true),
                    FirstNameTH = table.Column<string>(nullable: true),
                    MiddleNameTH = table.Column<string>(nullable: true),
                    LastNameTH = table.Column<string>(nullable: true),
                    Nickname = table.Column<string>(nullable: true),
                    TitleEN = table.Column<string>(nullable: true),
                    TitleExtEN = table.Column<string>(nullable: true),
                    FirstNameEN = table.Column<string>(nullable: true),
                    MiddleNameEN = table.Column<string>(nullable: true),
                    LastNameEN = table.Column<string>(nullable: true),
                    National = table.Column<string>(nullable: true),
                    CitizenIdentityNo = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    CompanyNameTH = table.Column<string>(nullable: true),
                    CompanyNameEN = table.Column<string>(nullable: true),
                    TaxID = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberExt = table.Column<string>(nullable: true),
                    ContactFirstName = table.Column<string>(nullable: true),
                    ContactLastname = table.Column<string>(nullable: true),
                    Wechat = table.Column<string>(nullable: true),
                    WhatsApp = table.Column<string>(nullable: true),
                    LineID = table.Column<string>(nullable: true),
                    CitizenHouseNoTH = table.Column<string>(nullable: true),
                    CitizenMooTH = table.Column<string>(nullable: true),
                    CitizenVillageTH = table.Column<string>(nullable: true),
                    CitizenSoiTH = table.Column<string>(nullable: true),
                    CitizenRoadTH = table.Column<string>(nullable: true),
                    CitizenCountryTH = table.Column<string>(nullable: true),
                    CitizenProvinceTH = table.Column<string>(nullable: true),
                    CitizenDistrictTH = table.Column<string>(nullable: true),
                    CitizenSubDistrictTH = table.Column<string>(nullable: true),
                    CitizenPostalCodeTH = table.Column<string>(nullable: true),
                    CitizenHouseNoEN = table.Column<string>(nullable: true),
                    CitizenMooEN = table.Column<string>(nullable: true),
                    CitizenVillageEN = table.Column<string>(nullable: true),
                    CitizenSoiEN = table.Column<string>(nullable: true),
                    CitizenRoadEN = table.Column<string>(nullable: true),
                    CitizenCountryEN = table.Column<string>(nullable: true),
                    CitizenProvinceEN = table.Column<string>(nullable: true),
                    CitizenDistrictEN = table.Column<string>(nullable: true),
                    CitizenSubDistrictEN = table.Column<string>(nullable: true),
                    CitizenPostalCodeEN = table.Column<string>(nullable: true),
                    WorkHouseNoTH = table.Column<string>(nullable: true),
                    WorkMooTH = table.Column<string>(nullable: true),
                    WorkVillageTH = table.Column<string>(nullable: true),
                    WorkSoiTH = table.Column<string>(nullable: true),
                    WorkRoadTH = table.Column<string>(nullable: true),
                    WorkCountryTH = table.Column<string>(nullable: true),
                    WorkProvinceTH = table.Column<string>(nullable: true),
                    WorkDistrictTH = table.Column<string>(nullable: true),
                    WorkSubDistrictTH = table.Column<string>(nullable: true),
                    WorkPostalCodeTH = table.Column<string>(nullable: true),
                    WorkHouseNoEN = table.Column<string>(nullable: true),
                    WorkMooEN = table.Column<string>(nullable: true),
                    WorkVillageEN = table.Column<string>(nullable: true),
                    WorkSoiEN = table.Column<string>(nullable: true),
                    WorkRoadEN = table.Column<string>(nullable: true),
                    WorkCountryEN = table.Column<string>(nullable: true),
                    WorkProvinceEN = table.Column<string>(nullable: true),
                    WorkDistrictEN = table.Column<string>(nullable: true),
                    WorkSubDistrictEN = table.Column<string>(nullable: true),
                    WorkPostalCodeEN = table.Column<string>(nullable: true),
                    HomeHouseNoTH = table.Column<string>(nullable: true),
                    HomeMooTH = table.Column<string>(nullable: true),
                    HomeVillageTH = table.Column<string>(nullable: true),
                    HomeSoiTH = table.Column<string>(nullable: true),
                    HomeRoadTH = table.Column<string>(nullable: true),
                    HomeCountryTH = table.Column<string>(nullable: true),
                    HomeProvinceTH = table.Column<string>(nullable: true),
                    HomeDistrictTH = table.Column<string>(nullable: true),
                    HomeSubDistrictTH = table.Column<string>(nullable: true),
                    HomePostalCodeTH = table.Column<string>(nullable: true),
                    HomeHouseNoEN = table.Column<string>(nullable: true),
                    HomeMooEN = table.Column<string>(nullable: true),
                    HomeVillageEN = table.Column<string>(nullable: true),
                    HomeSoiEN = table.Column<string>(nullable: true),
                    HomeRoadEN = table.Column<string>(nullable: true),
                    HomeCountryEN = table.Column<string>(nullable: true),
                    HomeProvinceEN = table.Column<string>(nullable: true),
                    HomeDistrictEN = table.Column<string>(nullable: true),
                    HomeSubDistrictEN = table.Column<string>(nullable: true),
                    HomePostalCodeEN = table.Column<string>(nullable: true),
                    MarriageName = table.Column<string>(nullable: true),
                    MarriageNational = table.Column<string>(nullable: true),
                    MarriageOtherNational = table.Column<string>(nullable: true),
                    FatherName = table.Column<string>(nullable: true),
                    FatherNational = table.Column<string>(nullable: true),
                    FatherOtherNational = table.Column<string>(nullable: true),
                    MotherName = table.Column<string>(nullable: true),
                    MotherNational = table.Column<string>(nullable: true),
                    MotherOtherNational = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LeadActivityStatus",
                schema: "CTM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadActivityStatus", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "OpportunityActivityStatus",
                schema: "CTM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpportunityActivityStatus", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "JobTransaction",
                schema: "DMT",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTransaction", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EDCFee",
                schema: "FIN",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CardType = table.Column<string>(nullable: true),
                    CardProvider = table.Column<string>(nullable: true),
                    CustomerCardFrom = table.Column<string>(nullable: true),
                    CardPaymentType = table.Column<string>(nullable: true),
                    Fee = table.Column<decimal>(type: "Money", nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EDCFee", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Bank",
                schema: "MST",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    NameTH = table.Column<string>(nullable: true),
                    NameEN = table.Column<string>(nullable: true),
                    Alias = table.Column<string>(nullable: true),
                    IsCreditCard = table.Column<bool>(nullable: false),
                    IsNonBank = table.Column<bool>(nullable: false),
                    IsCoorperative = table.Column<bool>(nullable: false),
                    IsFreeMortgage = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bank", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                schema: "MST",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    NameTH = table.Column<string>(nullable: true),
                    NameEN = table.Column<string>(nullable: true),
                    TaxID = table.Column<string>(nullable: true),
                    AddressTH = table.Column<string>(nullable: true),
                    AddressEN = table.Column<string>(nullable: true),
                    BuildingTH = table.Column<string>(nullable: true),
                    BuildingEN = table.Column<string>(nullable: true),
                    SoiTH = table.Column<string>(nullable: true),
                    SoiEN = table.Column<string>(nullable: true),
                    RoadTH = table.Column<string>(nullable: true),
                    RoadEN = table.Column<string>(nullable: true),
                    SubDistrictTH = table.Column<string>(nullable: true),
                    SubDistrictEN = table.Column<string>(nullable: true),
                    DistrictTH = table.Column<string>(nullable: true),
                    DistrictEN = table.Column<string>(nullable: true),
                    ProvinceTH = table.Column<string>(nullable: true),
                    ProvinceEN = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    Telephone = table.Column<string>(nullable: true),
                    Fax = table.Column<string>(nullable: true),
                    Website = table.Column<string>(nullable: true),
                    SAPCompanyID = table.Column<string>(nullable: true),
                    NameTHOld = table.Column<string>(nullable: true),
                    NameENOld = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Counter",
                schema: "MST",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Counter", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LandOffice",
                schema: "MST",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    NameTH = table.Column<string>(nullable: true),
                    NameEN = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LandOffice", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MasterCenterGroup",
                schema: "MST",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Key = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterCenterGroup", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MenuArea",
                schema: "MST",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuArea", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Province",
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
                    table.PrimaryKey("PK_Province", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SBU",
                schema: "MST",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SBU", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EmailNotification",
                schema: "NTF",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Subject = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    Receivers = table.Column<string>(nullable: true),
                    CCReceivers = table.Column<string>(nullable: true),
                    BCCReceivers = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Retry = table.Column<int>(nullable: false),
                    ErrorMessage = table.Column<string>(nullable: true),
                    TemplateName = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailNotification", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NotificationTemplate",
                schema: "NTF",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    WebMessage = table.Column<string>(nullable: true),
                    WebAction = table.Column<string>(nullable: true),
                    WebParams = table.Column<string>(nullable: true),
                    EmailSubject = table.Column<string>(nullable: true),
                    EmailMessage = table.Column<string>(nullable: true),
                    MobileSubject = table.Column<string>(nullable: true),
                    MobileMessage = table.Column<string>(nullable: true),
                    MobileAction = table.Column<string>(nullable: true),
                    MobileParams = table.Column<string>(nullable: true),
                    IsWebOpen = table.Column<bool>(nullable: false),
                    IsEmailOpen = table.Column<bool>(nullable: false),
                    IsMobileOpen = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTemplate", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ContactStoryGroup",
                schema: "OST",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactStoryGroup", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ContactStoryType",
                schema: "OST",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Ref1 = table.Column<string>(nullable: true),
                    Ref2 = table.Column<string>(nullable: true),
                    Ref3 = table.Column<string>(nullable: true),
                    Ref4 = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactStoryType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UnitStoryGroup",
                schema: "OST",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitStoryGroup", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UnitStoryType",
                schema: "OST",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Ref1 = table.Column<string>(nullable: true),
                    Ref2 = table.Column<string>(nullable: true),
                    Ref3 = table.Column<string>(nullable: true),
                    Ref4 = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitStoryType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LowRiseFee",
                schema: "PRJ",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ProjectID = table.Column<Guid>(nullable: false),
                    UnitNo = table.Column<string>(nullable: true),
                    EstimatePriceArea = table.Column<decimal>(type: "Money", nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LowRiseFee", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                schema: "PRJ",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ProjectNo = table.Column<string>(nullable: true),
                    SapCode = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    ProjectNameTH = table.Column<string>(nullable: true),
                    ProjectNameEN = table.Column<string>(nullable: true),
                    ProjectShortName = table.Column<string>(nullable: true),
                    TypeOfProject = table.Column<string>(nullable: true),
                    ProjectType = table.Column<string>(nullable: true),
                    ProjectPrice = table.Column<decimal>(type: "Money", nullable: false),
                    BdID = table.Column<string>(nullable: true),
                    Group = table.Column<string>(nullable: true),
                    SbuID = table.Column<string>(nullable: true),
                    Brand = table.Column<string>(nullable: true),
                    Company = table.Column<string>(nullable: true),
                    CostCenterCode = table.Column<string>(nullable: true),
                    ProfitCenterCode = table.Column<string>(nullable: true),
                    ProjectStartDate = table.Column<DateTime>(nullable: true),
                    ProjectEndDate = table.Column<DateTime>(nullable: true),
                    FloatingEndDate = table.Column<DateTime>(nullable: true),
                    MortgageBankID = table.Column<string>(nullable: true),
                    MortgageAmount = table.Column<decimal>(type: "Money", nullable: false),
                    TotalUnit = table.Column<double>(nullable: false),
                    Rai = table.Column<double>(nullable: false),
                    Ngan = table.Column<double>(nullable: false),
                    SqaureWa = table.Column<double>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PromotionCardItem",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    BankName = table.Column<string>(nullable: true),
                    ProductName = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(type: "Money", nullable: false),
                    PriceUnit = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionCardItem", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PromotionItem",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    AgreementNo = table.Column<string>(nullable: true),
                    ItemNo = table.Column<string>(nullable: true),
                    Plant = table.Column<string>(nullable: true),
                    ProductName = table.Column<string>(nullable: true),
                    MaterialCode = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(type: "Money", nullable: false),
                    PriceUnit = table.Column<string>(nullable: true),
                    ExpireDate = table.Column<DateTime>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionItem", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PromotionMaterial",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CompanyCode = table.Column<string>(nullable: true),
                    Plant = table.Column<string>(nullable: true),
                    ProductNameTH = table.Column<string>(nullable: true),
                    ProductNameEN = table.Column<string>(nullable: true),
                    ProductBrand = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(type: "Money", nullable: false),
                    PriceUnit = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionMaterial", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AuthorizeRule",
                schema: "USR",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ParentRuleID = table.Column<Guid>(nullable: true),
                    HasAuthorize = table.Column<bool>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorizeRule", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AuthorizeRule_AuthorizeRule_ParentRuleID",
                        column: x => x.ParentRuleID,
                        principalSchema: "USR",
                        principalTable: "AuthorizeRule",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleGroup",
                schema: "USR",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleGroup", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TaskType",
                schema: "USR",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Ref1 = table.Column<string>(nullable: true),
                    Ref2 = table.Column<string>(nullable: true),
                    Ref3 = table.Column<string>(nullable: true),
                    Ref4 = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "USR",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    EmployeeNo = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    ProfilePicture = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    LastLoginTime = table.Column<DateTime>(nullable: true),
                    LastActivityTime = table.Column<DateTime>(nullable: true),
                    PhoneNo = table.Column<string>(nullable: true),
                    LineId = table.Column<string>(nullable: true),
                    ReportToUserID = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "WorkflowType",
                schema: "WFL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Ref1 = table.Column<string>(nullable: true),
                    Ref2 = table.Column<string>(nullable: true),
                    Ref3 = table.Column<string>(nullable: true),
                    Ref4 = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ContactAddress",
                schema: "CTM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ContactID = table.Column<Guid>(nullable: false),
                    HouseNoTH = table.Column<string>(nullable: true),
                    MooTH = table.Column<string>(nullable: true),
                    VillageTH = table.Column<string>(nullable: true),
                    SoiTH = table.Column<string>(nullable: true),
                    RoadTH = table.Column<string>(nullable: true),
                    CountryTH = table.Column<string>(nullable: true),
                    ProvinceTH = table.Column<string>(nullable: true),
                    DistrictTH = table.Column<string>(nullable: true),
                    SubDistrictTH = table.Column<string>(nullable: true),
                    PostalCodeTH = table.Column<string>(nullable: true),
                    HouseNoEN = table.Column<string>(nullable: true),
                    MooEN = table.Column<string>(nullable: true),
                    VillageEN = table.Column<string>(nullable: true),
                    SoiEN = table.Column<string>(nullable: true),
                    RoadEN = table.Column<string>(nullable: true),
                    CountryEN = table.Column<string>(nullable: true),
                    ProvinceEN = table.Column<string>(nullable: true),
                    DistrictEN = table.Column<string>(nullable: true),
                    SubDistrictEN = table.Column<string>(nullable: true),
                    PostalCodeEN = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactAddress", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ContactAddress_Contact_ContactID",
                        column: x => x.ContactID,
                        principalSchema: "CTM",
                        principalTable: "Contact",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactEmail",
                schema: "CTM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ContactID = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    IsMain = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactEmail", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ContactEmail_Contact_ContactID",
                        column: x => x.ContactID,
                        principalSchema: "CTM",
                        principalTable: "Contact",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactPhone",
                schema: "CTM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ContactID = table.Column<Guid>(nullable: false),
                    PhoneType = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberExt = table.Column<string>(nullable: true),
                    IsMain = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactPhone", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ContactPhone_Contact_ContactID",
                        column: x => x.ContactID,
                        principalSchema: "CTM",
                        principalTable: "Contact",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lead",
                schema: "CTM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ContactID = table.Column<Guid>(nullable: true),
                    Fullname = table.Column<string>(nullable: true),
                    Lastname = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    HomePhoneNumber = table.Column<string>(nullable: true),
                    VisitZone = table.Column<string>(nullable: true),
                    Social = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    LeadStatus = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lead", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Lead_Contact_ContactID",
                        column: x => x.ContactID,
                        principalSchema: "CTM",
                        principalTable: "Contact",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BillPayment",
                schema: "FIN",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    BatchID = table.Column<string>(nullable: true),
                    MST_BankID = table.Column<Guid>(nullable: true),
                    PayDate = table.Column<DateTime>(nullable: false),
                    ImportTime = table.Column<DateTime>(nullable: false),
                    TotalAmount = table.Column<decimal>(type: "Money", nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillPayment", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BillPayment_Bank_MST_BankID",
                        column: x => x.MST_BankID,
                        principalSchema: "MST",
                        principalTable: "Bank",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DirectCreditDetail",
                schema: "FIN",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    MST_BankID = table.Column<Guid>(nullable: true),
                    CreditCardNo = table.Column<string>(nullable: true),
                    CreditCardExpireMonth = table.Column<int>(nullable: false),
                    CreditCardExpireYear = table.Column<int>(nullable: false),
                    CreditCardOwner = table.Column<string>(nullable: true),
                    CitizenIdentityNo = table.Column<string>(nullable: true),
                    Memo = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectCreditDetail", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DirectCreditDetail_Bank_MST_BankID",
                        column: x => x.MST_BankID,
                        principalSchema: "MST",
                        principalTable: "Bank",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BankBranch",
                schema: "MST",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    BankID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Building = table.Column<string>(nullable: true),
                    Soi = table.Column<string>(nullable: true),
                    Road = table.Column<string>(nullable: true),
                    SubDistrict = table.Column<string>(nullable: true),
                    District = table.Column<string>(nullable: true),
                    Province = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    Telephone = table.Column<string>(nullable: true),
                    Fax = table.Column<string>(nullable: true),
                    IsCreditBank = table.Column<bool>(nullable: false),
                    IsDirectDebit = table.Column<bool>(nullable: false),
                    IsDirectCredit = table.Column<bool>(nullable: false),
                    AreaCode = table.Column<string>(nullable: true),
                    OldBankID = table.Column<string>(nullable: true),
                    OldBranchID = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankBranch", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BankBranch_Bank_BankID",
                        column: x => x.BankID,
                        principalSchema: "MST",
                        principalTable: "Bank",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MortgageWithBank",
                schema: "SAL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    MST_BankID = table.Column<Guid>(nullable: true),
                    LoanSubmitDate = table.Column<DateTime>(nullable: true),
                    LoanAmount = table.Column<decimal>(type: "Money", nullable: false),
                    ApprovedLoadAPAmount = table.Column<decimal>(type: "Money", nullable: false),
                    Interest = table.Column<decimal>(type: "Money", nullable: false),
                    InterestOnFire = table.Column<decimal>(type: "Money", nullable: false),
                    FirstDeductAmount = table.Column<decimal>(type: "Money", nullable: false),
                    ReturnCustomerAmount = table.Column<decimal>(type: "Money", nullable: false),
                    ApprovedAmount = table.Column<decimal>(type: "Money", nullable: false),
                    LoanStatus = table.Column<string>(nullable: true),
                    ReceiveDate = table.Column<DateTime>(nullable: true),
                    IsChosenBankStatus = table.Column<bool>(nullable: false),
                    ChosenBankReason = table.Column<string>(nullable: true),
                    ChoseBankOtherReason = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MortgageWithBank", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MortgageWithBank_Bank_MST_BankID",
                        column: x => x.MST_BankID,
                        principalSchema: "MST",
                        principalTable: "Bank",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DirectCreditDebitExport",
                schema: "FIN",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    BatchID = table.Column<string>(nullable: true),
                    DirectFormType = table.Column<int>(nullable: false),
                    MST_CompanyID = table.Column<Guid>(nullable: true),
                    MST_BankID = table.Column<Guid>(nullable: true),
                    DirectPeriod = table.Column<string>(nullable: true),
                    Month = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    DirectPayDate = table.Column<DateTime>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectCreditDebitExport", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DirectCreditDebitExport_Bank_MST_BankID",
                        column: x => x.MST_BankID,
                        principalSchema: "MST",
                        principalTable: "Bank",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DirectCreditDebitExport_Company_MST_CompanyID",
                        column: x => x.MST_CompanyID,
                        principalSchema: "MST",
                        principalTable: "Company",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MasterCenter",
                schema: "MST",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    MasterCenterGroupID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Key = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterCenter", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MasterCenter_MasterCenterGroup_MasterCenterGroupID",
                        column: x => x.MasterCenterGroupID,
                        principalSchema: "MST",
                        principalTable: "MasterCenterGroup",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                schema: "MST",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ParentMenuID = table.Column<Guid>(nullable: true),
                    MenuAreaID = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Menu_MenuArea_MenuAreaID",
                        column: x => x.MenuAreaID,
                        principalSchema: "MST",
                        principalTable: "MenuArea",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Menu_Menu_ParentMenuID",
                        column: x => x.ParentMenuID,
                        principalSchema: "MST",
                        principalTable: "Menu",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "District",
                schema: "MST",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ProvinceID = table.Column<Guid>(nullable: false),
                    NameTH = table.Column<string>(nullable: true),
                    NameEN = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_District", x => x.ID);
                    table.ForeignKey(
                        name: "FK_District_Province_ProvinceID",
                        column: x => x.ProvinceID,
                        principalSchema: "MST",
                        principalTable: "Province",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Brand",
                schema: "MST",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    SBUID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Brand_SBU_SBUID",
                        column: x => x.SBUID,
                        principalSchema: "MST",
                        principalTable: "SBU",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactStory",
                schema: "OST",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CTM_ContactID = table.Column<Guid>(nullable: false),
                    ProjectNo = table.Column<string>(nullable: true),
                    UnitNo = table.Column<string>(nullable: true),
                    Ref1 = table.Column<string>(nullable: true),
                    Ref2 = table.Column<string>(nullable: true),
                    Ref3 = table.Column<string>(nullable: true),
                    Ref4 = table.Column<string>(nullable: true),
                    ContactStoryTypeID = table.Column<Guid>(nullable: false),
                    ContactStoryGroupID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactStory", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ContactStory_Contact_CTM_ContactID",
                        column: x => x.CTM_ContactID,
                        principalSchema: "CTM",
                        principalTable: "Contact",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContactStory_ContactStoryGroup_ContactStoryGroupID",
                        column: x => x.ContactStoryGroupID,
                        principalSchema: "OST",
                        principalTable: "ContactStoryGroup",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContactStory_ContactStoryType_ContactStoryTypeID",
                        column: x => x.ContactStoryTypeID,
                        principalSchema: "OST",
                        principalTable: "ContactStoryType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Opportunity",
                schema: "CTM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ContactID = table.Column<Guid>(nullable: false),
                    PRJ_ProjectID = table.Column<Guid>(nullable: true),
                    ArriveDate = table.Column<DateTime>(nullable: true),
                    EstimateSalesOpportunity = table.Column<string>(nullable: true),
                    SalesOpportunity = table.Column<string>(nullable: true),
                    InterestedProduct1 = table.Column<string>(nullable: true),
                    InterestedProduct2 = table.Column<string>(nullable: true),
                    InterestedProduct3 = table.Column<string>(nullable: true),
                    StatusQuestionaire = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opportunity", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Opportunity_Contact_ContactID",
                        column: x => x.ContactID,
                        principalSchema: "CTM",
                        principalTable: "Contact",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Opportunity_Project_PRJ_ProjectID",
                        column: x => x.PRJ_ProjectID,
                        principalSchema: "PRJ",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Visitor",
                schema: "CTM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ContactID = table.Column<Guid>(nullable: true),
                    PRJ_ProjectID = table.Column<Guid>(nullable: true),
                    VisitDateIn = table.Column<DateTime>(nullable: true),
                    VisitDateOut = table.Column<DateTime>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visitor", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Visitor_Contact_ContactID",
                        column: x => x.ContactID,
                        principalSchema: "CTM",
                        principalTable: "Contact",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Visitor_Project_PRJ_ProjectID",
                        column: x => x.PRJ_ProjectID,
                        principalSchema: "PRJ",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerWallet",
                schema: "FIN",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CTM_ContactID = table.Column<Guid>(nullable: false),
                    PRJ_ProjectID = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(type: "Money", nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerWallet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CustomerWallet_Contact_CTM_ContactID",
                        column: x => x.CTM_ContactID,
                        principalSchema: "CTM",
                        principalTable: "Contact",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerWallet_Project_PRJ_ProjectID",
                        column: x => x.PRJ_ProjectID,
                        principalSchema: "PRJ",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EDC",
                schema: "FIN",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    MST_BankID = table.Column<Guid>(nullable: true),
                    PRJ_ProjectID = table.Column<Guid>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    EDCType = table.Column<string>(nullable: true),
                    TelNo = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EDC", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EDC_Bank_MST_BankID",
                        column: x => x.MST_BankID,
                        principalSchema: "MST",
                        principalTable: "Bank",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EDC_Project_PRJ_ProjectID",
                        column: x => x.PRJ_ProjectID,
                        principalSchema: "PRJ",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                schema: "PRJ",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ProjectID = table.Column<Guid>(nullable: false),
                    CategoryType = table.Column<string>(nullable: true),
                    isMainAddress = table.Column<bool>(nullable: false),
                    AddressNameTH = table.Column<string>(nullable: true),
                    AddressNameEN = table.Column<string>(nullable: true),
                    TitleDeedNo = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    Province = table.Column<string>(nullable: true),
                    District = table.Column<string>(nullable: true),
                    SubDistrict = table.Column<string>(nullable: true),
                    VillageNo = table.Column<string>(nullable: true),
                    LaneTH = table.Column<string>(nullable: true),
                    LaneEN = table.Column<string>(nullable: true),
                    RoadTH = table.Column<string>(nullable: true),
                    RoadEN = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Address_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "PRJ",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Budget",
                schema: "PRJ",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ProjectID = table.Column<Guid>(nullable: false),
                    UnitNo = table.Column<string>(nullable: true),
                    HouseNo = table.Column<string>(nullable: true),
                    SaleArea = table.Column<double>(nullable: false),
                    ChanoteArea = table.Column<double>(nullable: false),
                    LastedCost = table.Column<decimal>(type: "Money", nullable: false),
                    RoiMinPrice = table.Column<decimal>(type: "Money", nullable: false),
                    SalePrice = table.Column<decimal>(type: "Money", nullable: false),
                    AcceptMinPrice = table.Column<decimal>(type: "Money", nullable: false),
                    DocType = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budget", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Budget_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "PRJ",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BudgetPromotion",
                schema: "PRJ",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ProjectID = table.Column<Guid>(nullable: false),
                    UnitNo = table.Column<string>(nullable: true),
                    HouseNo = table.Column<string>(nullable: true),
                    HouseType = table.Column<string>(nullable: true),
                    WBSCRM = table.Column<string>(nullable: true),
                    WBSSAP = table.Column<string>(nullable: true),
                    PromotionPrice = table.Column<decimal>(type: "Money", nullable: false),
                    PromotionTransferPrice = table.Column<decimal>(type: "Money", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "Money", nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetPromotion", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BudgetPromotion_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "PRJ",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FloorPlanImage",
                schema: "PRJ",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ProjectID = table.Column<Guid>(nullable: false),
                    Filename = table.Column<string>(nullable: true),
                    ContentType = table.Column<string>(nullable: true),
                    PathFile = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FloorPlanImage", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FloorPlanImage_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "PRJ",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "License",
                schema: "PRJ",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ProjectID = table.Column<Guid>(nullable: false),
                    AttorneyNameTH1 = table.Column<string>(nullable: true),
                    AttorneyNameTH2 = table.Column<string>(nullable: true),
                    AttorneyNameEN1 = table.Column<string>(nullable: true),
                    AttorneyNameEN2 = table.Column<string>(nullable: true),
                    WitnessTH1 = table.Column<string>(nullable: true),
                    WitnessTH2 = table.Column<string>(nullable: true),
                    WitnessEN1 = table.Column<string>(nullable: true),
                    WitnessEN2 = table.Column<string>(nullable: true),
                    AttorneyNameFree = table.Column<string>(nullable: true),
                    AttorneyFreePosition = table.Column<string>(nullable: true),
                    LegalNameTH = table.Column<string>(nullable: true),
                    LegalNameEN = table.Column<string>(nullable: true),
                    CondoFundRate = table.Column<decimal>(type: "Money", nullable: false),
                    BuildingInsurance = table.Column<decimal>(type: "Money", nullable: false),
                    CentralValue = table.Column<decimal>(type: "Money", nullable: false),
                    APCentralValue = table.Column<decimal>(type: "Money", nullable: false),
                    CentralMonth = table.Column<int>(nullable: false),
                    APCentralMonth = table.Column<int>(nullable: false),
                    RoomTransferFee = table.Column<decimal>(type: "Money", nullable: false),
                    ChangeNameFee = table.Column<decimal>(type: "Money", nullable: false),
                    VisitFine = table.Column<decimal>(type: "Money", nullable: false),
                    VisitFineDay = table.Column<int>(nullable: false),
                    DelayTransfer = table.Column<decimal>(nullable: false),
                    ParkingSpace = table.Column<int>(nullable: false),
                    PowerAttorneyDate = table.Column<DateTime>(nullable: true),
                    EndPublicDate = table.Column<DateTime>(nullable: true),
                    OwnerShipDate = table.Column<DateTime>(nullable: true),
                    EIAApproved = table.Column<bool>(nullable: false),
                    EIAApprovedDate = table.Column<DateTime>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_License", x => x.ID);
                    table.ForeignKey(
                        name: "FK_License_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "PRJ",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LowRiseFenceFee",
                schema: "PRJ",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ProjectID = table.Column<Guid>(nullable: false),
                    LandOffice = table.Column<string>(nullable: true),
                    HouseType = table.Column<string>(nullable: true),
                    ConcreteRate = table.Column<decimal>(type: "Money", nullable: false),
                    IronRate = table.Column<decimal>(type: "Money", nullable: false),
                    DepreciationPerYear = table.Column<decimal>(type: "Money", nullable: false),
                    CalculateFence = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LowRiseFenceFee", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LowRiseFenceFee_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "PRJ",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Model",
                schema: "PRJ",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ProjectID = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ShortName = table.Column<string>(nullable: true),
                    Style = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    ModelDesc = table.Column<string>(nullable: true),
                    Frontage = table.Column<double>(nullable: false),
                    PowerMeter = table.Column<double>(nullable: false),
                    PowerMeterPrice = table.Column<decimal>(type: "Money", nullable: false),
                    WaterPowerMeter = table.Column<double>(nullable: false),
                    WaterPowerMeterPrice = table.Column<decimal>(type: "Money", nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Model", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Model_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "PRJ",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Permit",
                schema: "PRJ",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ProjectID = table.Column<Guid>(nullable: false),
                    PermitName = table.Column<string>(nullable: true),
                    PermitNo = table.Column<string>(nullable: true),
                    PermitDate = table.Column<DateTime>(nullable: true),
                    ExpireDate = table.Column<DateTime>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permit", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Permit_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "PRJ",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoomPlanImage",
                schema: "PRJ",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ProjectID = table.Column<Guid>(nullable: false),
                    Filename = table.Column<string>(nullable: true),
                    ContentType = table.Column<string>(nullable: true),
                    PathFile = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomPlanImage", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RoomPlanImage_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "PRJ",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoundFee",
                schema: "PRJ",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ProjectID = table.Column<Guid>(nullable: false),
                    LandOffice = table.Column<string>(nullable: true),
                    OtherFee = table.Column<decimal>(type: "Money", nullable: false),
                    RoundTransferFee = table.Column<string>(nullable: true),
                    RoundBusinessFee = table.Column<string>(nullable: true),
                    RoundLocalFee = table.Column<string>(nullable: true),
                    RoundIncomeFee = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoundFee", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RoundFee_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "PRJ",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TitledeedDetail",
                schema: "PRJ",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ProjectID = table.Column<Guid>(nullable: false),
                    TitledeedNo = table.Column<string>(nullable: true),
                    UnitNumber = table.Column<string>(nullable: true),
                    Titledeed = table.Column<double>(nullable: false),
                    Location = table.Column<string>(nullable: true),
                    DepartmentOfLand = table.Column<string>(nullable: true),
                    HouseNo = table.Column<string>(nullable: true),
                    YearGotHouseNo = table.Column<int>(nullable: false),
                    UsedArea = table.Column<double>(nullable: false),
                    ParkingArea = table.Column<double>(nullable: false),
                    FenceArea = table.Column<double>(nullable: false),
                    FenceIronArea = table.Column<double>(nullable: false),
                    BalconyArea = table.Column<double>(nullable: false),
                    AirArea = table.Column<double>(nullable: false),
                    BookNo = table.Column<string>(nullable: true),
                    PageNo = table.Column<string>(nullable: true),
                    EstimatePrice = table.Column<decimal>(type: "Money", nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    IsSameLocation = table.Column<bool>(nullable: false),
                    PostalCode = table.Column<string>(nullable: true),
                    AddressProvince = table.Column<string>(nullable: true),
                    AddressDistrict = table.Column<string>(nullable: true),
                    AddressSubDistrict = table.Column<string>(nullable: true),
                    AddressVillageNo = table.Column<string>(nullable: true),
                    AddressLaneTH = table.Column<string>(nullable: true),
                    AddressLaneEN = table.Column<string>(nullable: true),
                    AddressRoadTH = table.Column<string>(nullable: true),
                    AddressRoadEN = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TitledeedDetail", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TitledeedDetail_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "PRJ",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tower",
                schema: "PRJ",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ProjectID = table.Column<Guid>(nullable: true),
                    TowerCode = table.Column<string>(nullable: true),
                    TowerNoTH = table.Column<string>(nullable: true),
                    TowerNoEN = table.Column<string>(nullable: true),
                    TitledeedNo = table.Column<string>(nullable: true),
                    TitledeedName = table.Column<string>(nullable: true),
                    TowerDescription = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tower", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Tower_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "PRJ",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WaiveQC",
                schema: "PRJ",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ProjectID = table.Column<Guid>(nullable: false),
                    UnitNo = table.Column<string>(nullable: true),
                    ActualTransferDate = table.Column<DateTime>(nullable: true),
                    WaiveQCeDate = table.Column<DateTime>(nullable: true),
                    EndMajoreDate = table.Column<DateTime>(nullable: true),
                    EndFulleDate = table.Column<DateTime>(nullable: true),
                    WaiveSigneDate = table.Column<DateTime>(nullable: true),
                    ArriveDate = table.Column<DateTime>(nullable: true),
                    UnitStatus = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WaiveQC", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WaiveQC_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "PRJ",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Promotion",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    PromotionType = table.Column<string>(nullable: true),
                    PromotionNo = table.Column<string>(nullable: true),
                    PromotionName = table.Column<string>(nullable: true),
                    PRJ_ProjectID = table.Column<Guid>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: true),
                    DiscountTransfer = table.Column<decimal>(type: "Money", nullable: false),
                    UsageStatus = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotion", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Promotion_Project_PRJ_ProjectID",
                        column: x => x.PRJ_ProjectID,
                        principalSchema: "PRJ",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromotionPreSale",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    PromotionCode = table.Column<string>(nullable: true),
                    PromotionName = table.Column<string>(nullable: true),
                    PRJ_ProjectID = table.Column<Guid>(nullable: false),
                    CompanyCode = table.Column<string>(nullable: true),
                    Plant = table.Column<string>(nullable: true),
                    UsageStatus = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionPreSale", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PromotionPreSale_Project_PRJ_ProjectID",
                        column: x => x.PRJ_ProjectID,
                        principalSchema: "PRJ",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "USR",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    RoleGroupID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Role_RoleGroup_RoleGroupID",
                        column: x => x.RoleGroupID,
                        principalSchema: "USR",
                        principalTable: "RoleGroup",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CalendarLockHistory",
                schema: "ACC",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CalendarLockID = table.Column<Guid>(nullable: false),
                    UserID = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarLockHistory", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CalendarLockHistory_CalendarLock_CalendarLockID",
                        column: x => x.CalendarLockID,
                        principalSchema: "ACC",
                        principalTable: "CalendarLock",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CalendarLockHistory_User_UserID",
                        column: x => x.UserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CalculateOther",
                schema: "CMS",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    EffectiveDate = table.Column<DateTime>(nullable: true),
                    PRJ_ProjectID = table.Column<Guid>(nullable: true),
                    USR_LCUserID = table.Column<Guid>(nullable: true),
                    Amount = table.Column<decimal>(type: "Money", nullable: false),
                    DeductDate = table.Column<DateTime>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculateOther", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CalculateOther_Project_PRJ_ProjectID",
                        column: x => x.PRJ_ProjectID,
                        principalSchema: "PRJ",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalculateOther_User_USR_LCUserID",
                        column: x => x.USR_LCUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MobileInstallation",
                schema: "NTF",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    USR_UserID = table.Column<Guid>(nullable: false),
                    InstallationID = table.Column<string>(nullable: true),
                    DeviceType = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MobileInstallation", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MobileInstallation_User_USR_UserID",
                        column: x => x.USR_UserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MobileNotification",
                schema: "NTF",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    USR_UserID = table.Column<Guid>(nullable: false),
                    Subject = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    Action = table.Column<string>(nullable: true),
                    Params = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    DeviceIds = table.Column<string>(nullable: true),
                    ErrorMessages = table.Column<string>(nullable: true),
                    TemplateName = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MobileNotification", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MobileNotification_User_USR_UserID",
                        column: x => x.USR_UserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WebNotification",
                schema: "NTF",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    USR_UserID = table.Column<Guid>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    Action = table.Column<string>(nullable: true),
                    Params = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    TemplateName = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebNotification", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WebNotification_User_USR_UserID",
                        column: x => x.USR_UserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Task",
                schema: "USR",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Topic = table.Column<string>(nullable: true),
                    Detail = table.Column<string>(nullable: true),
                    DueDate = table.Column<DateTime>(nullable: true),
                    Ref1 = table.Column<string>(nullable: true),
                    Ref2 = table.Column<string>(nullable: true),
                    Ref3 = table.Column<string>(nullable: true),
                    Ref4 = table.Column<string>(nullable: true),
                    UserID = table.Column<Guid>(nullable: true),
                    FromUserID = table.Column<Guid>(nullable: true),
                    TaskTypeID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Task_User_FromUserID",
                        column: x => x.FromUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Task_TaskType_TaskTypeID",
                        column: x => x.TaskTypeID,
                        principalSchema: "USR",
                        principalTable: "TaskType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Task_User_UserID",
                        column: x => x.UserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserDefaultProject",
                schema: "USR",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    UserID = table.Column<Guid>(nullable: false),
                    PRJ_ProjectID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDefaultProject", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserDefaultProject_Project_PRJ_ProjectID",
                        column: x => x.PRJ_ProjectID,
                        principalSchema: "PRJ",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserDefaultProject_User_UserID",
                        column: x => x.UserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Workflow",
                schema: "WFL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    WorkflowTypeID = table.Column<Guid>(nullable: false),
                    TemplateName = table.Column<string>(nullable: true),
                    Result = table.Column<bool>(nullable: true),
                    Memo = table.Column<string>(nullable: true),
                    Ref1 = table.Column<string>(nullable: true),
                    Ref2 = table.Column<string>(nullable: true),
                    Ref3 = table.Column<string>(nullable: true),
                    Ref4 = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workflow", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Workflow_WorkflowType_WorkflowTypeID",
                        column: x => x.WorkflowTypeID,
                        principalSchema: "WFL",
                        principalTable: "WorkflowType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkflowTemplate",
                schema: "WFL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    WorkflowTypeID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowTemplate", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WorkflowTemplate_WorkflowType_WorkflowTypeID",
                        column: x => x.WorkflowTypeID,
                        principalSchema: "WFL",
                        principalTable: "WorkflowType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactAddressProject",
                schema: "CTM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ContactAddressID = table.Column<Guid>(nullable: true),
                    PRJ_ProjectID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactAddressProject", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ContactAddressProject_ContactAddress_ContactAddressID",
                        column: x => x.ContactAddressID,
                        principalSchema: "CTM",
                        principalTable: "ContactAddress",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactAddressProject_Project_PRJ_ProjectID",
                        column: x => x.PRJ_ProjectID,
                        principalSchema: "PRJ",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LeadActivity",
                schema: "CTM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    LeadID = table.Column<Guid>(nullable: false),
                    ActivityType = table.Column<string>(nullable: true),
                    ActivityDate = table.Column<DateTime>(nullable: true),
                    ActualDate = table.Column<DateTime>(nullable: true),
                    ConvenientTime = table.Column<DateTime>(nullable: true),
                    AppointmentDate = table.Column<DateTime>(nullable: true),
                    StatusID = table.Column<Guid>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadActivity", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LeadActivity_Lead_LeadID",
                        column: x => x.LeadID,
                        principalSchema: "CTM",
                        principalTable: "Lead",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeadActivity_LeadActivityStatus_StatusID",
                        column: x => x.StatusID,
                        principalSchema: "CTM",
                        principalTable: "LeadActivityStatus",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BillPaymentTransaction",
                schema: "FIN",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    BillPaymentID = table.Column<Guid>(nullable: false),
                    PayDate = table.Column<DateTime>(nullable: false),
                    BankRef1 = table.Column<string>(nullable: true),
                    BankRef2 = table.Column<string>(nullable: true),
                    BankRef3 = table.Column<string>(nullable: true),
                    BillPaymentType = table.Column<string>(nullable: true),
                    PayType = table.Column<string>(nullable: true),
                    UnitPriceItemKey = table.Column<string>(nullable: true),
                    PayAmount = table.Column<decimal>(type: "Money", nullable: false),
                    Status = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillPaymentTransaction", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BillPaymentTransaction_BillPayment_BillPaymentID",
                        column: x => x.BillPaymentID,
                        principalSchema: "FIN",
                        principalTable: "BillPayment",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DirectDebitDetail",
                schema: "FIN",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    MST_BankID = table.Column<Guid>(nullable: true),
                    MST_ProvinceID = table.Column<Guid>(nullable: true),
                    MST_BankBranchID = table.Column<Guid>(nullable: true),
                    Memo = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectDebitDetail", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DirectDebitDetail_BankBranch_MST_BankBranchID",
                        column: x => x.MST_BankBranchID,
                        principalSchema: "MST",
                        principalTable: "BankBranch",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DirectDebitDetail_Bank_MST_BankID",
                        column: x => x.MST_BankID,
                        principalSchema: "MST",
                        principalTable: "Bank",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DirectDebitDetail_Province_MST_ProvinceID",
                        column: x => x.MST_ProvinceID,
                        principalSchema: "MST",
                        principalTable: "Province",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteMenu",
                schema: "USR",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    UserID = table.Column<Guid>(nullable: false),
                    MST_MenuID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteMenu", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FavoriteMenu_Menu_MST_MenuID",
                        column: x => x.MST_MenuID,
                        principalSchema: "MST",
                        principalTable: "Menu",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoriteMenu_User_UserID",
                        column: x => x.UserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BankAccount",
                schema: "ACC",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    MST_CompanyID = table.Column<Guid>(nullable: true),
                    MST_BankID = table.Column<Guid>(nullable: true),
                    MST_DistrictID = table.Column<Guid>(nullable: true),
                    MST_BankBranchID = table.Column<Guid>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    BankAccountNo = table.Column<string>(nullable: true),
                    GLAccountID = table.Column<string>(nullable: true),
                    isBankTransfer = table.Column<bool>(nullable: false),
                    isDirectDebit = table.Column<bool>(nullable: false),
                    isDirectCredit = table.Column<bool>(nullable: false),
                    isDepositAccount = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccount", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BankAccount_BankBranch_MST_BankBranchID",
                        column: x => x.MST_BankBranchID,
                        principalSchema: "MST",
                        principalTable: "BankBranch",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BankAccount_Bank_MST_BankID",
                        column: x => x.MST_BankID,
                        principalSchema: "MST",
                        principalTable: "Bank",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BankAccount_Company_MST_CompanyID",
                        column: x => x.MST_CompanyID,
                        principalSchema: "MST",
                        principalTable: "Company",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BankAccount_District_MST_DistrictID",
                        column: x => x.MST_DistrictID,
                        principalSchema: "MST",
                        principalTable: "District",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostGLDepositAccount",
                schema: "ACC",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    MST_CompanyID = table.Column<Guid>(nullable: true),
                    MST_BankID = table.Column<Guid>(nullable: true),
                    MST_DistrictID = table.Column<Guid>(nullable: true),
                    MST_BankBranchID = table.Column<Guid>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    BankAccountNo = table.Column<string>(nullable: true),
                    GLAccountID = table.Column<string>(nullable: true),
                    isBankTransfer = table.Column<bool>(nullable: false),
                    isDirectDebit = table.Column<bool>(nullable: false),
                    isDirectCredit = table.Column<bool>(nullable: false),
                    isDepositAccount = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostGLDepositAccount", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PostGLDepositAccount_BankBranch_MST_BankBranchID",
                        column: x => x.MST_BankBranchID,
                        principalSchema: "MST",
                        principalTable: "BankBranch",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostGLDepositAccount_Bank_MST_BankID",
                        column: x => x.MST_BankID,
                        principalSchema: "MST",
                        principalTable: "Bank",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostGLDepositAccount_Company_MST_CompanyID",
                        column: x => x.MST_CompanyID,
                        principalSchema: "MST",
                        principalTable: "Company",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostGLDepositAccount_District_MST_DistrictID",
                        column: x => x.MST_DistrictID,
                        principalSchema: "MST",
                        principalTable: "District",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubDistrict",
                schema: "MST",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    DistrictID = table.Column<Guid>(nullable: false),
                    LandOfficeID = table.Column<Guid>(nullable: true),
                    NameTH = table.Column<string>(nullable: true),
                    NameEN = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubDistrict", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SubDistrict_District_DistrictID",
                        column: x => x.DistrictID,
                        principalSchema: "MST",
                        principalTable: "District",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubDistrict_LandOffice_LandOfficeID",
                        column: x => x.LandOfficeID,
                        principalSchema: "MST",
                        principalTable: "LandOffice",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OpportunityActivity",
                schema: "CTM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    OpportunityID = table.Column<Guid>(nullable: false),
                    ActivityType = table.Column<string>(nullable: true),
                    ActivityDate = table.Column<DateTime>(nullable: true),
                    ActualDate = table.Column<DateTime>(nullable: true),
                    ConvenientTime = table.Column<DateTime>(nullable: true),
                    AppointmentDate = table.Column<DateTime>(nullable: true),
                    StopTrack = table.Column<bool>(nullable: false),
                    StopTrackReason = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpportunityActivity", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OpportunityActivity_Opportunity_OpportunityID",
                        column: x => x.OpportunityID,
                        principalSchema: "CTM",
                        principalTable: "Opportunity",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TitledeedReceive",
                schema: "SAL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    PRJ_TitledeedDetailID = table.Column<Guid>(nullable: false),
                    LCProceedDate = table.Column<DateTime>(nullable: true),
                    FIProceedDate = table.Column<DateTime>(nullable: true),
                    LCStatus = table.Column<string>(nullable: true),
                    FIStatus = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TitledeedReceive", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TitledeedReceive_TitledeedDetail_PRJ_TitledeedDetailID",
                        column: x => x.PRJ_TitledeedDetailID,
                        principalSchema: "PRJ",
                        principalTable: "TitledeedDetail",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Floor",
                schema: "PRJ",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ProjectID = table.Column<Guid>(nullable: false),
                    TowerID = table.Column<Guid>(nullable: false),
                    FloorNameTH = table.Column<string>(nullable: true),
                    FloorNameEN = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    FloorFilename = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Floor", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Floor_Tower_TowerID",
                        column: x => x.TowerID,
                        principalSchema: "PRJ",
                        principalTable: "Tower",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromotionCard",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    PromotionID = table.Column<Guid>(nullable: false),
                    PromotionCardItemID = table.Column<Guid>(nullable: false),
                    ProductNameTH = table.Column<string>(nullable: true),
                    ProductNameEN = table.Column<string>(nullable: true),
                    RecieveContact = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionCard", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PromotionCard_PromotionCardItem_PromotionCardItemID",
                        column: x => x.PromotionCardItemID,
                        principalSchema: "PRM",
                        principalTable: "PromotionCardItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PromotionCard_Promotion_PromotionID",
                        column: x => x.PromotionID,
                        principalSchema: "PRM",
                        principalTable: "Promotion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromotionDetail",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    PromotionID = table.Column<Guid>(nullable: false),
                    PromotionItemID = table.Column<Guid>(nullable: false),
                    PromotionType = table.Column<string>(nullable: true),
                    ProductNameTH = table.Column<string>(nullable: true),
                    ProductNameEN = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    UnitTH = table.Column<string>(nullable: true),
                    UnitEN = table.Column<string>(nullable: true),
                    RecieveDate = table.Column<int>(nullable: false),
                    TotalPrice = table.Column<decimal>(type: "Money", nullable: false),
                    RecieveContact = table.Column<string>(nullable: true),
                    HousePlan = table.Column<string>(nullable: true),
                    ExpireDate = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionDetail", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PromotionDetail_Promotion_PromotionID",
                        column: x => x.PromotionID,
                        principalSchema: "PRM",
                        principalTable: "Promotion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PromotionDetail_PromotionItem_PromotionItemID",
                        column: x => x.PromotionItemID,
                        principalSchema: "PRM",
                        principalTable: "PromotionItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromotionPreSaleDetail",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    PromotionPreSaleID = table.Column<Guid>(nullable: false),
                    PromotionItemID = table.Column<Guid>(nullable: false),
                    PromotionType = table.Column<string>(nullable: true),
                    ProductNameTH = table.Column<string>(nullable: true),
                    ProductNameEN = table.Column<string>(nullable: true),
                    Quantity = table.Column<decimal>(nullable: false),
                    UnitTH = table.Column<string>(nullable: true),
                    UnitEN = table.Column<string>(nullable: true),
                    RecieveDate = table.Column<int>(nullable: false),
                    TotalPrice = table.Column<decimal>(type: "Money", nullable: false),
                    RecieveContact = table.Column<string>(nullable: true),
                    HousePlan = table.Column<string>(nullable: true),
                    ExpireDate = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionPreSaleDetail", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PromotionPreSaleDetail_PromotionItem_PromotionItemID",
                        column: x => x.PromotionItemID,
                        principalSchema: "PRM",
                        principalTable: "PromotionItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PromotionPreSaleDetail_PromotionPreSale_PromotionPreSaleID",
                        column: x => x.PromotionPreSaleID,
                        principalSchema: "PRM",
                        principalTable: "PromotionPreSale",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuthorizeRuleByRole",
                schema: "USR",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    AuthorizeRuleID = table.Column<Guid>(nullable: false),
                    RoleID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorizeRuleByRole", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AuthorizeRuleByRole_AuthorizeRule_AuthorizeRuleID",
                        column: x => x.AuthorizeRuleID,
                        principalSchema: "USR",
                        principalTable: "AuthorizeRule",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorizeRuleByRole_Role_RoleID",
                        column: x => x.RoleID,
                        principalSchema: "USR",
                        principalTable: "Role",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                schema: "USR",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    UserID = table.Column<Guid>(nullable: false),
                    RoleID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleID",
                        column: x => x.RoleID,
                        principalSchema: "USR",
                        principalTable: "Role",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserID",
                        column: x => x.UserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkflowStep",
                schema: "WFL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    WorkflowID = table.Column<Guid>(nullable: false),
                    ApproveCondition = table.Column<int>(nullable: false),
                    Result = table.Column<bool>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowStep", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WorkflowStep_Workflow_WorkflowID",
                        column: x => x.WorkflowID,
                        principalSchema: "WFL",
                        principalTable: "Workflow",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkflowStepTemplate",
                schema: "WFL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    WorkflowTemplateID = table.Column<Guid>(nullable: false),
                    ApproveCondition = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowStepTemplate", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WorkflowStepTemplate_WorkflowTemplate_WorkflowTemplateID",
                        column: x => x.WorkflowTemplateID,
                        principalSchema: "WFL",
                        principalTable: "WorkflowTemplate",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostGLChartOfAccount",
                schema: "ACC",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    GLAccountID = table.Column<string>(nullable: true),
                    AccountType = table.Column<string>(nullable: true),
                    MST_CompanyID = table.Column<Guid>(nullable: true),
                    MST_Bank = table.Column<Guid>(nullable: true),
                    BankAccountID = table.Column<Guid>(nullable: true),
                    AccountName = table.Column<string>(nullable: true),
                    AccountTypeGroup = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostGLChartOfAccount", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PostGLChartOfAccount_BankAccount_BankAccountID",
                        column: x => x.BankAccountID,
                        principalSchema: "ACC",
                        principalTable: "BankAccount",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostGLChartOfAccount_Bank_MST_Bank",
                        column: x => x.MST_Bank,
                        principalSchema: "MST",
                        principalTable: "Bank",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostGLChartOfAccount_Company_MST_CompanyID",
                        column: x => x.MST_CompanyID,
                        principalSchema: "MST",
                        principalTable: "Company",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Deposit",
                schema: "FIN",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    DepositNo = table.Column<string>(nullable: true),
                    DepositDate = table.Column<DateTime>(nullable: false),
                    ACC_BankAccount = table.Column<Guid>(nullable: true),
                    TotalAmount = table.Column<decimal>(type: "Money", nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deposit", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Deposit_BankAccount_ACC_BankAccount",
                        column: x => x.ACC_BankAccount,
                        principalSchema: "ACC",
                        principalTable: "BankAccount",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UnknownPayment",
                schema: "FIN",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    BankDepositNo = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(type: "Money", nullable: false),
                    ACC_BankAccountID = table.Column<Guid>(nullable: true),
                    ReceiveDate = table.Column<DateTime>(nullable: false),
                    TransferDate = table.Column<DateTime>(nullable: false),
                    AttachFile = table.Column<string>(nullable: true),
                    MST_AttachFileFromBankID = table.Column<Guid>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    CancelMemo = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnknownPayment", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UnknownPayment_BankAccount_ACC_BankAccountID",
                        column: x => x.ACC_BankAccountID,
                        principalSchema: "ACC",
                        principalTable: "BankAccount",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UnknownPayment_Bank_MST_AttachFileFromBankID",
                        column: x => x.MST_AttachFileFromBankID,
                        principalSchema: "MST",
                        principalTable: "Bank",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OpportunityActivityTrack",
                schema: "CTM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    OpportunityAcitivityID = table.Column<Guid>(nullable: false),
                    StatusID = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpportunityActivityTrack", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OpportunityActivityTrack_OpportunityActivity_OpportunityAcitivityID",
                        column: x => x.OpportunityAcitivityID,
                        principalSchema: "CTM",
                        principalTable: "OpportunityActivity",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OpportunityActivityTrack_OpportunityActivityStatus_StatusID",
                        column: x => x.StatusID,
                        principalSchema: "CTM",
                        principalTable: "OpportunityActivityStatus",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TitledeedReceiveHistory",
                schema: "SAL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    TitledeedReceiveID = table.Column<Guid>(nullable: false),
                    USR_ActorUserID = table.Column<Guid>(nullable: false),
                    ProceedDate = table.Column<DateTime>(nullable: false),
                    PreviousStatus = table.Column<string>(nullable: true),
                    ChangedStatus = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TitledeedReceiveHistory", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TitledeedReceiveHistory_TitledeedReceive_TitledeedReceiveID",
                        column: x => x.TitledeedReceiveID,
                        principalSchema: "SAL",
                        principalTable: "TitledeedReceive",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TitledeedReceiveHistory_User_USR_ActorUserID",
                        column: x => x.USR_ActorUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Unit",
                schema: "PRJ",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ProjectID = table.Column<Guid>(nullable: true),
                    UnitNo = table.Column<string>(nullable: true),
                    HouseNo = table.Column<string>(nullable: true),
                    YearGotHouseNo = table.Column<int>(nullable: false),
                    SapwbsObject = table.Column<string>(nullable: true),
                    SapwbsNo = table.Column<string>(nullable: true),
                    HouseCode = table.Column<string>(nullable: true),
                    HouseName = table.Column<string>(nullable: true),
                    UnitType = table.Column<string>(nullable: true),
                    UnitStatus = table.Column<string>(nullable: true),
                    SaleArea = table.Column<double>(nullable: false),
                    UnitArea = table.Column<double>(nullable: false),
                    TowerID = table.Column<Guid>(nullable: false),
                    FloorID = table.Column<Guid>(nullable: false),
                    Location = table.Column<string>(nullable: true),
                    Reamark = table.Column<string>(nullable: true),
                    NumberOfPrivilege = table.Column<double>(nullable: false),
                    NumberOfParkingFix = table.Column<double>(nullable: false),
                    NumberOfParkingUnFix = table.Column<double>(nullable: false),
                    FloorFilename = table.Column<string>(nullable: true),
                    RoomFilename = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Unit_Floor_FloorID",
                        column: x => x.FloorID,
                        principalSchema: "PRJ",
                        principalTable: "Floor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Unit_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "PRJ",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PromotionSubDetail",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    PromotionDetailID = table.Column<Guid>(nullable: false),
                    PromotionItemID = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionSubDetail", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PromotionSubDetail_PromotionDetail_PromotionDetailID",
                        column: x => x.PromotionDetailID,
                        principalSchema: "PRM",
                        principalTable: "PromotionDetail",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PromotionSubDetail_PromotionItem_PromotionItemID",
                        column: x => x.PromotionItemID,
                        principalSchema: "PRM",
                        principalTable: "PromotionItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkflowApprover",
                schema: "WFL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    WorkflowStepID = table.Column<Guid>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    USR_RoleID = table.Column<Guid>(nullable: true),
                    USR_ApproverID = table.Column<Guid>(nullable: true),
                    Result = table.Column<bool>(nullable: true),
                    Memo = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowApprover", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WorkflowApprover_User_USR_ApproverID",
                        column: x => x.USR_ApproverID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkflowApprover_Role_USR_RoleID",
                        column: x => x.USR_RoleID,
                        principalSchema: "USR",
                        principalTable: "Role",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkflowApprover_WorkflowStep_WorkflowStepID",
                        column: x => x.WorkflowStepID,
                        principalSchema: "WFL",
                        principalTable: "WorkflowStep",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkflowApproverTemplate",
                schema: "WFL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    WorkflowStepTemplateID = table.Column<Guid>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    USR_RoleID = table.Column<Guid>(nullable: true),
                    USR_ApproverID = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowApproverTemplate", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WorkflowApproverTemplate_User_USR_ApproverID",
                        column: x => x.USR_ApproverID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkflowApproverTemplate_Role_USR_RoleID",
                        column: x => x.USR_RoleID,
                        principalSchema: "USR",
                        principalTable: "Role",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkflowApproverTemplate_WorkflowStepTemplate_WorkflowStepTemplateID",
                        column: x => x.WorkflowStepTemplateID,
                        principalSchema: "WFL",
                        principalTable: "WorkflowStepTemplate",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CalculatePerMonth",
                schema: "CMS",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    PRJ_UnitID = table.Column<Guid>(nullable: true),
                    USR_LCClosedDealUserID = table.Column<Guid>(nullable: true),
                    USR_LCAtProjectID = table.Column<Guid>(nullable: true),
                    LCC = table.Column<string>(nullable: true),
                    Rate = table.Column<decimal>(nullable: false),
                    NewRate = table.Column<decimal>(nullable: false),
                    ContractValue = table.Column<decimal>(type: "Money", nullable: false),
                    TransferDate = table.Column<DateTime>(nullable: true),
                    SigningContractCommission = table.Column<decimal>(type: "Money", nullable: false),
                    TransferClosedDealCommission = table.Column<decimal>(type: "Money", nullable: false),
                    TransferAtProjectCommission = table.Column<decimal>(type: "Money", nullable: false),
                    TransferTotalCommission = table.Column<decimal>(type: "Money", nullable: false),
                    NewLaunchClosedDeal = table.Column<decimal>(type: "Money", nullable: false),
                    NewLaunchAtProject = table.Column<decimal>(type: "Money", nullable: false),
                    NewLaunchTotalCommission = table.Column<decimal>(type: "Money", nullable: false),
                    SaleCommissionLCCenter = table.Column<decimal>(type: "Money", nullable: false),
                    CommissionForThisMonth = table.Column<decimal>(type: "Money", nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculatePerMonth", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CalculatePerMonth_Unit_PRJ_UnitID",
                        column: x => x.PRJ_UnitID,
                        principalSchema: "PRJ",
                        principalTable: "Unit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalculatePerMonth_User_USR_LCAtProjectID",
                        column: x => x.USR_LCAtProjectID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalculatePerMonth_User_USR_LCClosedDealUserID",
                        column: x => x.USR_LCClosedDealUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CalculateSale",
                schema: "CMS",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    PRJ_UnitID = table.Column<Guid>(nullable: true),
                    USR_LCClosedDealID = table.Column<Guid>(nullable: true),
                    USR_LCAtProjectID = table.Column<Guid>(nullable: true),
                    LCCenter = table.Column<string>(nullable: true),
                    Rate = table.Column<decimal>(nullable: false),
                    ContractValue = table.Column<decimal>(type: "Money", nullable: false),
                    ContractApprovedDate = table.Column<DateTime>(nullable: true),
                    ClosedDealCommission = table.Column<decimal>(type: "Money", nullable: false),
                    AtProjectCommission = table.Column<decimal>(type: "Money", nullable: false),
                    TotalCommission = table.Column<decimal>(type: "Money", nullable: false),
                    SaleCommissionLCCenter = table.Column<decimal>(type: "Money", nullable: false),
                    CommissionForThisMonth = table.Column<decimal>(type: "Money", nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculateSale", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CalculateSale_Unit_PRJ_UnitID",
                        column: x => x.PRJ_UnitID,
                        principalSchema: "PRJ",
                        principalTable: "Unit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalculateSale_User_USR_LCAtProjectID",
                        column: x => x.USR_LCAtProjectID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalculateSale_User_USR_LCClosedDealID",
                        column: x => x.USR_LCClosedDealID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CalculateTransfer",
                schema: "CMS",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    PRJ_UnitID = table.Column<Guid>(nullable: true),
                    USR_LCCTransferID = table.Column<Guid>(nullable: true),
                    LCCenterTransfer = table.Column<string>(nullable: true),
                    Rate = table.Column<decimal>(nullable: false),
                    ActualContractTransfer = table.Column<decimal>(type: "Money", nullable: false),
                    TransferDate = table.Column<DateTime>(nullable: true),
                    TransferCommission = table.Column<decimal>(type: "Money", nullable: false),
                    LCCenterTransferCommission = table.Column<decimal>(type: "Money", nullable: false),
                    CommissionForThisMonth = table.Column<decimal>(type: "Money", nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculateTransfer", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CalculateTransfer_Unit_PRJ_UnitID",
                        column: x => x.PRJ_UnitID,
                        principalSchema: "PRJ",
                        principalTable: "Unit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalculateTransfer_User_USR_LCCTransferID",
                        column: x => x.USR_LCCTransferID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UnitStory",
                schema: "OST",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    UnitID = table.Column<Guid>(nullable: false),
                    Ref1 = table.Column<string>(nullable: true),
                    Ref2 = table.Column<string>(nullable: true),
                    Ref3 = table.Column<string>(nullable: true),
                    Ref4 = table.Column<string>(nullable: true),
                    UnitStoryTypeID = table.Column<Guid>(nullable: false),
                    UnitStoryGroupID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitStory", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UnitStory_Unit_UnitID",
                        column: x => x.UnitID,
                        principalSchema: "PRJ",
                        principalTable: "Unit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UnitStory_UnitStoryGroup_UnitStoryGroupID",
                        column: x => x.UnitStoryGroupID,
                        principalSchema: "OST",
                        principalTable: "UnitStoryGroup",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UnitStory_UnitStoryType_UnitStoryTypeID",
                        column: x => x.UnitStoryTypeID,
                        principalSchema: "OST",
                        principalTable: "UnitStoryType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HighRiseFee",
                schema: "PRJ",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ProjectID = table.Column<Guid>(nullable: false),
                    TowerID = table.Column<Guid>(nullable: false),
                    FloorID = table.Column<Guid>(nullable: false),
                    UnitID = table.Column<Guid>(nullable: false),
                    CalculateArea = table.Column<string>(nullable: true),
                    EstimatePriceArea = table.Column<decimal>(type: "Money", nullable: false),
                    EstimatePriceUsageArea = table.Column<decimal>(type: "Money", nullable: false),
                    EstimatePriceBalconyArea = table.Column<decimal>(type: "Money", nullable: false),
                    EstimatePriceAirArea = table.Column<decimal>(type: "Money", nullable: false),
                    EstimatePricePoolArea = table.Column<decimal>(type: "Money", nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HighRiseFee", x => x.ID);
                    table.ForeignKey(
                        name: "FK_HighRiseFee_Unit_UnitID",
                        column: x => x.UnitID,
                        principalSchema: "PRJ",
                        principalTable: "Unit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LowRiseBuildingPriceFee",
                schema: "PRJ",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ProjectID = table.Column<Guid>(nullable: false),
                    ModelID = table.Column<Guid>(nullable: true),
                    UnitID = table.Column<Guid>(nullable: true),
                    Price = table.Column<decimal>(type: "Money", nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LowRiseBuildingPriceFee", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LowRiseBuildingPriceFee_Model_ModelID",
                        column: x => x.ModelID,
                        principalSchema: "PRJ",
                        principalTable: "Model",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LowRiseBuildingPriceFee_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "PRJ",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LowRiseBuildingPriceFee_Unit_UnitID",
                        column: x => x.UnitID,
                        principalSchema: "PRJ",
                        principalTable: "Unit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PriceList",
                schema: "PRJ",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    UnitID = table.Column<Guid>(nullable: false),
                    ActiveDate = table.Column<DateTime>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceList", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PriceList_Unit_UnitID",
                        column: x => x.UnitID,
                        principalSchema: "PRJ",
                        principalTable: "Unit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Quotation",
                schema: "SAL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    IssueDate = table.Column<DateTime>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    PRJ_UnitID = table.Column<Guid>(nullable: false),
                    ContractSignDate = table.Column<DateTime>(nullable: true),
                    TransferOwnershipDate = table.Column<DateTime>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quotation", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Quotation_Unit_PRJ_UnitID",
                        column: x => x.PRJ_UnitID,
                        principalSchema: "PRJ",
                        principalTable: "Unit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuotationCompare",
                schema: "SAL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    PRJ_UnitID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotationCompare", x => x.ID);
                    table.ForeignKey(
                        name: "FK_QuotationCompare_Unit_PRJ_UnitID",
                        column: x => x.PRJ_UnitID,
                        principalSchema: "PRJ",
                        principalTable: "Unit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PriceListItem",
                schema: "PRJ",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    PriceListID = table.Column<Guid>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    Key = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(type: "Money", nullable: false),
                    IsToBePay = table.Column<bool>(nullable: false),
                    Installment = table.Column<int>(nullable: true),
                    InstallmentPeriodKey = table.Column<string>(nullable: true),
                    InstallmentPeriod = table.Column<int>(nullable: true),
                    IsSpecialInstallmentPeriod = table.Column<bool>(nullable: false),
                    PriceType = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceListItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PriceListItem_PriceList_PriceListID",
                        column: x => x.PriceListID,
                        principalSchema: "PRJ",
                        principalTable: "PriceList",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuotationBookingPromotion",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    SAL_QuotationID = table.Column<Guid>(nullable: false),
                    PromotionID = table.Column<Guid>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    TotalPrice = table.Column<decimal>(type: "Money", nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotationBookingPromotion", x => x.ID);
                    table.ForeignKey(
                        name: "FK_QuotationBookingPromotion_Promotion_PromotionID",
                        column: x => x.PromotionID,
                        principalSchema: "PRM",
                        principalTable: "Promotion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuotationBookingPromotion_Quotation_SAL_QuotationID",
                        column: x => x.SAL_QuotationID,
                        principalSchema: "SAL",
                        principalTable: "Quotation",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuotationTransferPromotion",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    SAL_QuotationID = table.Column<Guid>(nullable: false),
                    QuotationNo = table.Column<string>(nullable: true),
                    TransferType = table.Column<string>(nullable: true),
                    DiscountContact = table.Column<decimal>(type: "Money", nullable: false),
                    DiscountTransfer = table.Column<decimal>(type: "Money", nullable: false),
                    DiscountFGF = table.Column<decimal>(type: "Money", nullable: false),
                    TotalValue = table.Column<decimal>(type: "Money", nullable: false),
                    Advisor = table.Column<string>(nullable: true),
                    Budget = table.Column<decimal>(type: "Money", nullable: false),
                    TransferDate = table.Column<DateTime>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotationTransferPromotion", x => x.ID);
                    table.ForeignKey(
                        name: "FK_QuotationTransferPromotion_Quotation_SAL_QuotationID",
                        column: x => x.SAL_QuotationID,
                        principalSchema: "SAL",
                        principalTable: "Quotation",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuotationUnitPriceItem",
                schema: "SAL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    QuotationID = table.Column<Guid>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    Key = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(type: "Money", nullable: false),
                    IsToBePay = table.Column<bool>(nullable: false),
                    Installment = table.Column<int>(nullable: true),
                    InstallmentPeriodKey = table.Column<string>(nullable: true),
                    InstallmentPeriod = table.Column<int>(nullable: true),
                    IsSpecialInstallmentPeriod = table.Column<bool>(nullable: false),
                    PriceType = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotationUnitPriceItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_QuotationUnitPriceItem_Quotation_QuotationID",
                        column: x => x.QuotationID,
                        principalSchema: "SAL",
                        principalTable: "Quotation",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuotationBookingPromotionItem",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    QuotationBookingPromotionID = table.Column<Guid>(nullable: false),
                    PromotionItemID = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(type: "Money", nullable: false),
                    PriceUnit = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotationBookingPromotionItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_QuotationBookingPromotionItem_QuotationBookingPromotion_QuotationBookingPromotionID",
                        column: x => x.QuotationBookingPromotionID,
                        principalSchema: "PRM",
                        principalTable: "QuotationBookingPromotion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuotationPromotionExpense",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    QuotationBookingPromotionID = table.Column<Guid>(nullable: false),
                    ResponsibleBy = table.Column<int>(nullable: false),
                    UnitPriceItemKey = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotationPromotionExpense", x => x.ID);
                    table.ForeignKey(
                        name: "FK_QuotationPromotionExpense_QuotationBookingPromotion_QuotationBookingPromotionID",
                        column: x => x.QuotationBookingPromotionID,
                        principalSchema: "PRM",
                        principalTable: "QuotationBookingPromotion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuotationTransferPromotionItem",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    QuotationTransferPromotionID = table.Column<Guid>(nullable: false),
                    PromotionItemID = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(type: "Money", nullable: false),
                    PriceUnit = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotationTransferPromotionItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_QuotationTransferPromotionItem_QuotationTransferPromotion_QuotationTransferPromotionID",
                        column: x => x.QuotationTransferPromotionID,
                        principalSchema: "PRM",
                        principalTable: "QuotationTransferPromotion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentBankTransfer",
                schema: "FIN",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    PaymentMethodID = table.Column<Guid>(nullable: false),
                    ACC_BankAccountID = table.Column<Guid>(nullable: true),
                    UnknownPaymentID = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentBankTransfer", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PaymentBankTransfer_BankAccount_ACC_BankAccountID",
                        column: x => x.ACC_BankAccountID,
                        principalSchema: "ACC",
                        principalTable: "BankAccount",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentBankTransfer_UnknownPayment_UnknownPaymentID",
                        column: x => x.UnknownPaymentID,
                        principalSchema: "FIN",
                        principalTable: "UnknownPayment",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentQRCode",
                schema: "FIN",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    PaymentMethodID = table.Column<Guid>(nullable: false),
                    ACC_BankAccountID = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentQRCode", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PaymentQRCode_BankAccount_ACC_BankAccountID",
                        column: x => x.ACC_BankAccountID,
                        principalSchema: "ACC",
                        principalTable: "BankAccount",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Receipt",
                schema: "FIN",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ReceiptNo = table.Column<string>(nullable: true),
                    PaymentID = table.Column<Guid>(nullable: true),
                    CTM_ContactID = table.Column<Guid>(nullable: true),
                    MST_CompanyID = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipt", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Receipt_Contact_CTM_ContactID",
                        column: x => x.CTM_ContactID,
                        principalSchema: "CTM",
                        principalTable: "Contact",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Receipt_Company_MST_CompanyID",
                        column: x => x.MST_CompanyID,
                        principalSchema: "MST",
                        principalTable: "Company",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptSendEmailHistory",
                schema: "FIN",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ReceiptID = table.Column<Guid>(nullable: false),
                    CTM_SendToContactID = table.Column<Guid>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptSendEmailHistory", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ReceiptSendEmailHistory_Contact_CTM_SendToContactID",
                        column: x => x.CTM_SendToContactID,
                        principalSchema: "CTM",
                        principalTable: "Contact",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiptSendEmailHistory_Receipt_ReceiptID",
                        column: x => x.ReceiptID,
                        principalSchema: "FIN",
                        principalTable: "Receipt",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptSendPrintingHistory",
                schema: "FIN",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ReceiptID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptSendPrintingHistory", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ReceiptSendPrintingHistory_Receipt_ReceiptID",
                        column: x => x.ReceiptID,
                        principalSchema: "FIN",
                        principalTable: "Receipt",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptTemp",
                schema: "FIN",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ReceiptTempNo = table.Column<string>(nullable: true),
                    PaymentID = table.Column<Guid>(nullable: true),
                    CTM_ContactID = table.Column<Guid>(nullable: true),
                    MST_CompanyID = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptTemp", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ReceiptTemp_Contact_CTM_ContactID",
                        column: x => x.CTM_ContactID,
                        principalSchema: "CTM",
                        principalTable: "Contact",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiptTemp_Company_MST_CompanyID",
                        column: x => x.MST_CompanyID,
                        principalSchema: "MST",
                        principalTable: "Company",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Agreement",
                schema: "SAL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CTM_ContactID = table.Column<Guid>(nullable: true),
                    PRJ_UnitID = table.Column<Guid>(nullable: true),
                    BookingID = table.Column<Guid>(nullable: true),
                    SignAgreementDate = table.Column<DateTime>(nullable: true),
                    AgreementStatus = table.Column<string>(nullable: true),
                    AgreementNo = table.Column<string>(nullable: true),
                    ApproveStatus = table.Column<string>(nullable: true),
                    ActiveUnitPriceID = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agreement", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Agreement_Contact_CTM_ContactID",
                        column: x => x.CTM_ContactID,
                        principalSchema: "CTM",
                        principalTable: "Contact",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Agreement_Unit_PRJ_UnitID",
                        column: x => x.PRJ_UnitID,
                        principalSchema: "PRJ",
                        principalTable: "Unit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DownPaymentLetter",
                schema: "LET",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    SAL_AgreementID = table.Column<Guid>(nullable: false),
                    AgreementNo = table.Column<string>(nullable: true),
                    RemainDownPeriodCount = table.Column<int>(nullable: false),
                    TotalRemainAmount = table.Column<decimal>(type: "Money", nullable: false),
                    RemainDownPeriod = table.Column<int>(nullable: false),
                    LetterType = table.Column<string>(nullable: true),
                    LetterStatus = table.Column<string>(nullable: true),
                    ResponseDate = table.Column<DateTime>(nullable: true),
                    Memo = table.Column<string>(nullable: true),
                    PostTrackingNo = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DownPaymentLetter", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DownPaymentLetter_Agreement_SAL_AgreementID",
                        column: x => x.SAL_AgreementID,
                        principalSchema: "SAL",
                        principalTable: "Agreement",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransferLetter",
                schema: "LET",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    SAL_AgreementID = table.Column<Guid>(nullable: false),
                    AgreementNo = table.Column<string>(nullable: true),
                    AppointmentTransferDate = table.Column<DateTime>(nullable: true),
                    TransferStatus = table.Column<string>(nullable: true),
                    LetterType = table.Column<string>(nullable: true),
                    LetterTransferDate = table.Column<DateTime>(nullable: true),
                    PostTrackingNo = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferLetter", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TransferLetter_Agreement_SAL_AgreementID",
                        column: x => x.SAL_AgreementID,
                        principalSchema: "SAL",
                        principalTable: "Agreement",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AgreementDownPeriod",
                schema: "SAL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    SAL_AgreementID = table.Column<Guid>(nullable: false),
                    Detail = table.Column<string>(nullable: true),
                    DownNo = table.Column<string>(nullable: true),
                    ScheduleDate = table.Column<DateTime>(nullable: true),
                    PayDate = table.Column<DateTime>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgreementDownPeriod", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AgreementDownPeriod_Agreement_SAL_AgreementID",
                        column: x => x.SAL_AgreementID,
                        principalSchema: "SAL",
                        principalTable: "Agreement",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AgreementOwner",
                schema: "SAL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    SAL_AgreementID = table.Column<Guid>(nullable: false),
                    OwnerType = table.Column<string>(nullable: true),
                    CTM_ContactID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgreementOwner", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AgreementOwner_Contact_CTM_ContactID",
                        column: x => x.CTM_ContactID,
                        principalSchema: "CTM",
                        principalTable: "Contact",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgreementOwner_Agreement_SAL_AgreementID",
                        column: x => x.SAL_AgreementID,
                        principalSchema: "SAL",
                        principalTable: "Agreement",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transfer",
                schema: "SAL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    TransferNo = table.Column<string>(nullable: true),
                    SAL_AgreementID = table.Column<Guid>(nullable: true),
                    USR_LCID = table.Column<Guid>(nullable: true),
                    PRJ_UnitID = table.Column<Guid>(nullable: true),
                    ScheduleTransferDate = table.Column<DateTime>(nullable: true),
                    ActualTransferDate = table.Column<DateTime>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transfer", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Transfer_Unit_PRJ_UnitID",
                        column: x => x.PRJ_UnitID,
                        principalSchema: "PRJ",
                        principalTable: "Unit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transfer_Agreement_SAL_AgreementID",
                        column: x => x.SAL_AgreementID,
                        principalSchema: "SAL",
                        principalTable: "Agreement",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transfer_User_USR_LCID",
                        column: x => x.USR_LCID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransferUnit",
                schema: "SAL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    SAL_AgreementID = table.Column<Guid>(nullable: true),
                    PRJ_OldUnitID = table.Column<Guid>(nullable: true),
                    PRJ_NewUnitID = table.Column<Guid>(nullable: true),
                    ApproveDate = table.Column<DateTime>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferUnit", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TransferUnit_Unit_PRJ_NewUnitID",
                        column: x => x.PRJ_NewUnitID,
                        principalSchema: "PRJ",
                        principalTable: "Unit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferUnit_Unit_PRJ_OldUnitID",
                        column: x => x.PRJ_OldUnitID,
                        principalSchema: "PRJ",
                        principalTable: "Unit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferUnit_Agreement_SAL_AgreementID",
                        column: x => x.SAL_AgreementID,
                        principalSchema: "SAL",
                        principalTable: "Agreement",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransferCash",
                schema: "SAL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    PayTo = table.Column<int>(nullable: false),
                    PayDate = table.Column<string>(nullable: true),
                    SAL_TransferID = table.Column<Guid>(nullable: false),
                    MST_BankID = table.Column<Guid>(nullable: true),
                    MST_BankBranchID = table.Column<Guid>(nullable: true),
                    TransferAmount = table.Column<decimal>(type: "Money", nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferCash", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TransferCash_BankBranch_MST_BankBranchID",
                        column: x => x.MST_BankBranchID,
                        principalSchema: "MST",
                        principalTable: "BankBranch",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferCash_Bank_MST_BankID",
                        column: x => x.MST_BankID,
                        principalSchema: "MST",
                        principalTable: "Bank",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferCash_Transfer_SAL_TransferID",
                        column: x => x.SAL_TransferID,
                        principalSchema: "SAL",
                        principalTable: "Transfer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransferCheque",
                schema: "SAL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    PayTo = table.Column<int>(nullable: false),
                    ChequeNo = table.Column<string>(nullable: true),
                    PayDate = table.Column<string>(nullable: true),
                    SAL_TransferID = table.Column<Guid>(nullable: false),
                    MST_BankID = table.Column<Guid>(nullable: true),
                    MST_BankBranchID = table.Column<Guid>(nullable: true),
                    TransferAmount = table.Column<decimal>(type: "Money", nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferCheque", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TransferCheque_BankBranch_MST_BankBranchID",
                        column: x => x.MST_BankBranchID,
                        principalSchema: "MST",
                        principalTable: "BankBranch",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferCheque_Bank_MST_BankID",
                        column: x => x.MST_BankID,
                        principalSchema: "MST",
                        principalTable: "Bank",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferCheque_Transfer_SAL_TransferID",
                        column: x => x.SAL_TransferID,
                        principalSchema: "SAL",
                        principalTable: "Transfer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransferDocument",
                schema: "SAL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    SAL_TransferID = table.Column<Guid>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    IsRejected = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferDocument", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TransferDocument_Transfer_SAL_TransferID",
                        column: x => x.SAL_TransferID,
                        principalSchema: "SAL",
                        principalTable: "Transfer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransferOwner",
                schema: "SAL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    SAL_TransferID = table.Column<Guid>(nullable: false),
                    CTM_ContactID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferOwner", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TransferOwner_Contact_CTM_ContactID",
                        column: x => x.CTM_ContactID,
                        principalSchema: "CTM",
                        principalTable: "Contact",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransferOwner_Transfer_SAL_TransferID",
                        column: x => x.SAL_TransferID,
                        principalSchema: "SAL",
                        principalTable: "Transfer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Booking",
                schema: "SAL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    BookingNo = table.Column<string>(nullable: true),
                    BookingStatus = table.Column<string>(nullable: true),
                    BookingType = table.Column<string>(nullable: true),
                    PRJ_UnitID = table.Column<Guid>(nullable: false),
                    CTM_ContactID = table.Column<Guid>(nullable: false),
                    SignBookedDate = table.Column<DateTime>(nullable: true),
                    SignContractDate = table.Column<DateTime>(nullable: true),
                    BookingPaymentStatus = table.Column<string>(nullable: true),
                    ApproveDate = table.Column<DateTime>(nullable: true),
                    USR_SaleID = table.Column<Guid>(nullable: true),
                    USR_AgencyID = table.Column<Guid>(nullable: true),
                    USR_SaleAtProjectID = table.Column<Guid>(nullable: true),
                    ActiveUnitPriceID = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Booking_Contact_CTM_ContactID",
                        column: x => x.CTM_ContactID,
                        principalSchema: "CTM",
                        principalTable: "Contact",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Booking_Unit_PRJ_UnitID",
                        column: x => x.PRJ_UnitID,
                        principalSchema: "PRJ",
                        principalTable: "Unit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Booking_User_USR_AgencyID",
                        column: x => x.USR_AgencyID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Booking_User_USR_SaleAtProjectID",
                        column: x => x.USR_SaleAtProjectID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Booking_User_USR_SaleID",
                        column: x => x.USR_SaleID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DirectCreditDebitApprovalForm",
                schema: "FIN",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    SAL_BookingID = table.Column<Guid>(nullable: false),
                    FormType = table.Column<int>(nullable: false),
                    ApprovalStatus = table.Column<string>(nullable: true),
                    DirectPeriod = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectCreditDebitApprovalForm", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DirectCreditDebitApprovalForm_Booking_SAL_BookingID",
                        column: x => x.SAL_BookingID,
                        principalSchema: "SAL",
                        principalTable: "Booking",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                schema: "FIN",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    SAL_BookingID = table.Column<Guid>(nullable: false),
                    Memo = table.Column<string>(nullable: true),
                    AttachFile = table.Column<string>(nullable: true),
                    ReceiveDate = table.Column<DateTime>(nullable: false),
                    TotalAmount = table.Column<decimal>(type: "Money", nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Payment_Booking_SAL_BookingID",
                        column: x => x.SAL_BookingID,
                        principalSchema: "SAL",
                        principalTable: "Booking",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingPromotion",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    SAL_BookingID = table.Column<Guid>(nullable: false),
                    PromotionID = table.Column<Guid>(nullable: false),
                    TotalPrice = table.Column<decimal>(type: "Money", nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingPromotion", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BookingPromotion_Promotion_PromotionID",
                        column: x => x.PromotionID,
                        principalSchema: "PRM",
                        principalTable: "Promotion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingPromotion_Booking_SAL_BookingID",
                        column: x => x.SAL_BookingID,
                        principalSchema: "SAL",
                        principalTable: "Booking",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransferPromotion",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    SAL_BookingID = table.Column<Guid>(nullable: false),
                    BookingNo = table.Column<string>(nullable: true),
                    TransferType = table.Column<string>(nullable: true),
                    DiscountContact = table.Column<decimal>(type: "Money", nullable: false),
                    DiscountTransfer = table.Column<decimal>(type: "Money", nullable: false),
                    DiscountFGF = table.Column<decimal>(type: "Money", nullable: false),
                    TotalValue = table.Column<decimal>(type: "Money", nullable: false),
                    Advisor = table.Column<string>(nullable: true),
                    Budget = table.Column<decimal>(type: "Money", nullable: false),
                    TransferDate = table.Column<DateTime>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferPromotion", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TransferPromotion_Booking_SAL_BookingID",
                        column: x => x.SAL_BookingID,
                        principalSchema: "SAL",
                        principalTable: "Booking",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingOwner",
                schema: "SAL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    SAL_BookingID = table.Column<Guid>(nullable: true),
                    CTM_ContactID = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingOwner", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BookingOwner_Contact_CTM_ContactID",
                        column: x => x.CTM_ContactID,
                        principalSchema: "CTM",
                        principalTable: "Contact",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingOwner_Booking_SAL_BookingID",
                        column: x => x.SAL_BookingID,
                        principalSchema: "SAL",
                        principalTable: "Booking",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuotationUnitPrice",
                schema: "SAL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    BookingID = table.Column<Guid>(nullable: false),
                    ActiveDate = table.Column<DateTime>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotationUnitPrice", x => x.ID);
                    table.ForeignKey(
                        name: "FK_QuotationUnitPrice_Booking_BookingID",
                        column: x => x.BookingID,
                        principalSchema: "SAL",
                        principalTable: "Booking",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UnitPrice",
                schema: "SAL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    BookingID = table.Column<Guid>(nullable: false),
                    ActiveDate = table.Column<DateTime>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitPrice", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UnitPrice_Booking_BookingID",
                        column: x => x.BookingID,
                        principalSchema: "SAL",
                        principalTable: "Booking",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DirectCreditDebitUnitPriceItem",
                schema: "FIN",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    DirectCreditDebitFormID = table.Column<Guid>(nullable: false),
                    UnitPriceItemKey = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectCreditDebitUnitPriceItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DirectCreditDebitUnitPriceItem_DirectCreditDebitApprovalForm_DirectCreditDebitFormID",
                        column: x => x.DirectCreditDebitFormID,
                        principalSchema: "FIN",
                        principalTable: "DirectCreditDebitApprovalForm",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentItem",
                schema: "FIN",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    PaymentID = table.Column<Guid>(nullable: false),
                    PayAmount = table.Column<decimal>(type: "Money", nullable: false),
                    ItemAmout = table.Column<decimal>(type: "Money", nullable: false),
                    RemainAmount = table.Column<decimal>(type: "Money", nullable: false),
                    UnitPriceItemKey = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PaymentItem_Payment_PaymentID",
                        column: x => x.PaymentID,
                        principalSchema: "FIN",
                        principalTable: "Payment",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethod",
                schema: "FIN",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    PaymentID = table.Column<Guid>(nullable: false),
                    PaymentMethodType = table.Column<int>(nullable: false),
                    PayAmount = table.Column<decimal>(type: "Money", nullable: false),
                    DepositID = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethod", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PaymentMethod_Deposit_DepositID",
                        column: x => x.DepositID,
                        principalSchema: "FIN",
                        principalTable: "Deposit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentMethod_Payment_PaymentID",
                        column: x => x.PaymentID,
                        principalSchema: "FIN",
                        principalTable: "Payment",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingPromotionExpense",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    BookingPromotionID = table.Column<Guid>(nullable: false),
                    ResponsibleBy = table.Column<int>(nullable: false),
                    UnitPriceItemKey = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingPromotionExpense", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BookingPromotionExpense_BookingPromotion_BookingPromotionID",
                        column: x => x.BookingPromotionID,
                        principalSchema: "PRM",
                        principalTable: "BookingPromotion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingPromotionItem",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    BookingPromotionID = table.Column<Guid>(nullable: false),
                    PromotionItemID = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(type: "Money", nullable: false),
                    PriceUnit = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingPromotionItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BookingPromotionItem_BookingPromotion_BookingPromotionID",
                        column: x => x.BookingPromotionID,
                        principalSchema: "PRM",
                        principalTable: "BookingPromotion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromotionDelivery",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    TransferPromotionID = table.Column<Guid>(nullable: false),
                    BookingNo = table.Column<string>(nullable: true),
                    ReceiveDate = table.Column<DateTime>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionDelivery", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PromotionDelivery_TransferPromotion_TransferPromotionID",
                        column: x => x.TransferPromotionID,
                        principalSchema: "PRM",
                        principalTable: "TransferPromotion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromotionReceive",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    TransferPromotionID = table.Column<Guid>(nullable: false),
                    BookingNo = table.Column<string>(nullable: true),
                    ReceiveDate = table.Column<DateTime>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionReceive", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PromotionReceive_TransferPromotion_TransferPromotionID",
                        column: x => x.TransferPromotionID,
                        principalSchema: "PRM",
                        principalTable: "TransferPromotion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransferPromotionExpense",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    TransferPromotionID = table.Column<Guid>(nullable: false),
                    ResponsibleBy = table.Column<int>(nullable: false),
                    BookingPriceItemKey = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferPromotionExpense", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TransferPromotionExpense_TransferPromotion_TransferPromotionID",
                        column: x => x.TransferPromotionID,
                        principalSchema: "PRM",
                        principalTable: "TransferPromotion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransferPromotionItem",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    TransferPromotionID = table.Column<Guid>(nullable: false),
                    PromotionItemID = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(type: "Money", nullable: false),
                    PriceUnit = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferPromotionItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TransferPromotionItem_TransferPromotion_TransferPromotionID",
                        column: x => x.TransferPromotionID,
                        principalSchema: "PRM",
                        principalTable: "TransferPromotion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UnitPriceItem",
                schema: "SAL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    UnitPriceID = table.Column<Guid>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    Key = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(type: "Money", nullable: false),
                    IsToBePay = table.Column<bool>(nullable: false),
                    Installment = table.Column<int>(nullable: true),
                    InstallmentPeriodKey = table.Column<string>(nullable: true),
                    InstallmentPeriod = table.Column<int>(nullable: true),
                    IsSpecialInstallmentPeriod = table.Column<bool>(nullable: false),
                    PriceType = table.Column<int>(nullable: false),
                    PayDate = table.Column<DateTime>(nullable: true),
                    DueDate = table.Column<DateTime>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitPriceItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UnitPriceItem_UnitPrice_UnitPriceID",
                        column: x => x.UnitPriceID,
                        principalSchema: "SAL",
                        principalTable: "UnitPrice",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DirectCreditDebitTransaction",
                schema: "FIN",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    TransactionNo = table.Column<string>(nullable: true),
                    DirectCreditDebitUnitPriceItemID = table.Column<Guid>(nullable: false),
                    DueDate = table.Column<DateTime>(nullable: false),
                    PayDate = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(type: "Money", nullable: false),
                    PayAmount = table.Column<decimal>(type: "Money", nullable: false),
                    Result = table.Column<bool>(nullable: false),
                    Memo = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectCreditDebitTransaction", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DirectCreditDebitTransaction_DirectCreditDebitUnitPriceItem_DirectCreditDebitUnitPriceItemID",
                        column: x => x.DirectCreditDebitUnitPriceItemID,
                        principalSchema: "FIN",
                        principalTable: "DirectCreditDebitUnitPriceItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerWalletTransaction",
                schema: "FIN",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CustomerWalletID = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(type: "Money", nullable: false),
                    RemainAmount = table.Column<decimal>(type: "Money", nullable: false),
                    PaymentTypeItemID = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerWalletTransaction", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CustomerWalletTransaction_CustomerWallet_CustomerWalletID",
                        column: x => x.CustomerWalletID,
                        principalSchema: "FIN",
                        principalTable: "CustomerWallet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerWalletTransaction_PaymentMethod_PaymentTypeItemID",
                        column: x => x.PaymentTypeItemID,
                        principalSchema: "FIN",
                        principalTable: "PaymentMethod",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentBillPayment",
                schema: "FIN",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    PaymentMethodID = table.Column<Guid>(nullable: true),
                    BillPaymentTransactionID = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentBillPayment", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PaymentBillPayment_BillPaymentTransaction_BillPaymentTransactionID",
                        column: x => x.BillPaymentTransactionID,
                        principalSchema: "FIN",
                        principalTable: "BillPaymentTransaction",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentBillPayment_PaymentMethod_PaymentMethodID",
                        column: x => x.PaymentMethodID,
                        principalSchema: "FIN",
                        principalTable: "PaymentMethod",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentCashierCheque",
                schema: "FIN",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    PaymentMethodID = table.Column<Guid>(nullable: false),
                    ChequeDate = table.Column<DateTime>(nullable: false),
                    ChequeNo = table.Column<string>(nullable: true),
                    MST_PayToCompanyID = table.Column<Guid>(nullable: true),
                    MST_BankID = table.Column<Guid>(nullable: true),
                    MST_BankBranchID = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentCashierCheque", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PaymentCashierCheque_BankBranch_MST_BankBranchID",
                        column: x => x.MST_BankBranchID,
                        principalSchema: "MST",
                        principalTable: "BankBranch",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentCashierCheque_Bank_MST_BankID",
                        column: x => x.MST_BankID,
                        principalSchema: "MST",
                        principalTable: "Bank",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentCashierCheque_Company_MST_PayToCompanyID",
                        column: x => x.MST_PayToCompanyID,
                        principalSchema: "MST",
                        principalTable: "Company",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentCashierCheque_PaymentMethod_PaymentMethodID",
                        column: x => x.PaymentMethodID,
                        principalSchema: "FIN",
                        principalTable: "PaymentMethod",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentCreditCard",
                schema: "FIN",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    PaymentMethodID = table.Column<Guid>(nullable: false),
                    IsForeignCreditCard = table.Column<bool>(nullable: false),
                    Fee = table.Column<decimal>(type: "Money", nullable: false),
                    CardNo = table.Column<string>(nullable: true),
                    CardPaymentType = table.Column<string>(nullable: true),
                    CardProvider = table.Column<string>(nullable: true),
                    MST_BankID = table.Column<Guid>(nullable: true),
                    EDCID = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentCreditCard", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PaymentCreditCard_EDC_EDCID",
                        column: x => x.EDCID,
                        principalSchema: "FIN",
                        principalTable: "EDC",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentCreditCard_Bank_MST_BankID",
                        column: x => x.MST_BankID,
                        principalSchema: "MST",
                        principalTable: "Bank",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentCreditCard_PaymentMethod_PaymentMethodID",
                        column: x => x.PaymentMethodID,
                        principalSchema: "FIN",
                        principalTable: "PaymentMethod",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentForeignBankTransfer",
                schema: "FIN",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    PaymentMethodID = table.Column<Guid>(nullable: false),
                    Fee = table.Column<decimal>(type: "Money", nullable: false),
                    MST_BankID = table.Column<Guid>(nullable: true),
                    ForeignBank = table.Column<string>(nullable: true),
                    SourceCurrency = table.Column<string>(nullable: true),
                    ForeignTransferType = table.Column<string>(nullable: true),
                    IR = table.Column<string>(nullable: true),
                    TransferorName = table.Column<string>(nullable: true),
                    IsRequestFET = table.Column<bool>(nullable: false),
                    IsNotifyForEdit = table.Column<bool>(nullable: false),
                    NotifyMemo = table.Column<string>(nullable: true),
                    UnknownPaymentID = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentForeignBankTransfer", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PaymentForeignBankTransfer_Bank_MST_BankID",
                        column: x => x.MST_BankID,
                        principalSchema: "MST",
                        principalTable: "Bank",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentForeignBankTransfer_PaymentMethod_PaymentMethodID",
                        column: x => x.PaymentMethodID,
                        principalSchema: "FIN",
                        principalTable: "PaymentMethod",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaymentForeignBankTransfer_UnknownPayment_UnknownPaymentID",
                        column: x => x.UnknownPaymentID,
                        principalSchema: "FIN",
                        principalTable: "UnknownPayment",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethodToItem",
                schema: "FIN",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    PaymentMethodID = table.Column<Guid>(nullable: true),
                    PaymentItemID = table.Column<Guid>(nullable: true),
                    PayAmount = table.Column<decimal>(type: "Money", nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethodToItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PaymentMethodToItem_PaymentItem_PaymentItemID",
                        column: x => x.PaymentItemID,
                        principalSchema: "FIN",
                        principalTable: "PaymentItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentMethodToItem_PaymentMethod_PaymentMethodID",
                        column: x => x.PaymentMethodID,
                        principalSchema: "FIN",
                        principalTable: "PaymentMethod",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentPersonalCheque",
                schema: "FIN",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    PaymentMethodID = table.Column<Guid>(nullable: false),
                    ChequeDate = table.Column<DateTime>(nullable: false),
                    ChequeNo = table.Column<string>(nullable: true),
                    MST_PayToCompanyID = table.Column<Guid>(nullable: true),
                    MST_BankID = table.Column<Guid>(nullable: true),
                    MST_BankBranchID = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentPersonalCheque", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PaymentPersonalCheque_BankBranch_MST_BankBranchID",
                        column: x => x.MST_BankBranchID,
                        principalSchema: "MST",
                        principalTable: "BankBranch",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentPersonalCheque_Bank_MST_BankID",
                        column: x => x.MST_BankID,
                        principalSchema: "MST",
                        principalTable: "Bank",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentPersonalCheque_Company_MST_PayToCompanyID",
                        column: x => x.MST_PayToCompanyID,
                        principalSchema: "MST",
                        principalTable: "Company",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentPersonalCheque_PaymentMethod_PaymentMethodID",
                        column: x => x.PaymentMethodID,
                        principalSchema: "FIN",
                        principalTable: "PaymentMethod",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromotionDeliveryItem",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    PromotionDeliveryID = table.Column<Guid>(nullable: false),
                    PromotionItemID = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    EstimateRecieveDate = table.Column<DateTime>(nullable: true),
                    SerialNo = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionDeliveryItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PromotionDeliveryItem_PromotionDelivery_PromotionDeliveryID",
                        column: x => x.PromotionDeliveryID,
                        principalSchema: "PRM",
                        principalTable: "PromotionDelivery",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromotionReceiveItem",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    PromotionReceiveID = table.Column<Guid>(nullable: false),
                    PromotionItemID = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    EstimateRecieveDate = table.Column<DateTime>(nullable: true),
                    PRNo = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromotionReceiveItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PromotionReceiveItem_PromotionReceive_PromotionReceiveID",
                        column: x => x.PromotionReceiveID,
                        principalSchema: "PRM",
                        principalTable: "PromotionReceive",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentDirectCreditDebit",
                schema: "FIN",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    PaymentMethodID = table.Column<Guid>(nullable: true),
                    DirectCreditDebitTransactionID = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentDirectCreditDebit", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PaymentDirectCreditDebit_DirectCreditDebitTransaction_DirectCreditDebitTransactionID",
                        column: x => x.DirectCreditDebitTransactionID,
                        principalSchema: "FIN",
                        principalTable: "DirectCreditDebitTransaction",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentDirectCreditDebit_PaymentMethod_PaymentMethodID",
                        column: x => x.PaymentMethodID,
                        principalSchema: "FIN",
                        principalTable: "PaymentMethod",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentCustomerWallet",
                schema: "FIN",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    PaymentMethodID = table.Column<Guid>(nullable: true),
                    CustomerWalletTransactionID = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentCustomerWallet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PaymentCustomerWallet_CustomerWalletTransaction_CustomerWalletTransactionID",
                        column: x => x.CustomerWalletTransactionID,
                        principalSchema: "FIN",
                        principalTable: "CustomerWalletTransaction",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentCustomerWallet_PaymentMethod_PaymentMethodID",
                        column: x => x.PaymentMethodID,
                        principalSchema: "FIN",
                        principalTable: "PaymentMethod",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankAccount_MST_BankBranchID",
                schema: "ACC",
                table: "BankAccount",
                column: "MST_BankBranchID");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccount_MST_BankID",
                schema: "ACC",
                table: "BankAccount",
                column: "MST_BankID");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccount_MST_CompanyID",
                schema: "ACC",
                table: "BankAccount",
                column: "MST_CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccount_MST_DistrictID",
                schema: "ACC",
                table: "BankAccount",
                column: "MST_DistrictID");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarLockHistory_CalendarLockID",
                schema: "ACC",
                table: "CalendarLockHistory",
                column: "CalendarLockID");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarLockHistory_UserID",
                schema: "ACC",
                table: "CalendarLockHistory",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_PostGLChartOfAccount_BankAccountID",
                schema: "ACC",
                table: "PostGLChartOfAccount",
                column: "BankAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_PostGLChartOfAccount_MST_Bank",
                schema: "ACC",
                table: "PostGLChartOfAccount",
                column: "MST_Bank");

            migrationBuilder.CreateIndex(
                name: "IX_PostGLChartOfAccount_MST_CompanyID",
                schema: "ACC",
                table: "PostGLChartOfAccount",
                column: "MST_CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_PostGLDepositAccount_MST_BankBranchID",
                schema: "ACC",
                table: "PostGLDepositAccount",
                column: "MST_BankBranchID");

            migrationBuilder.CreateIndex(
                name: "IX_PostGLDepositAccount_MST_BankID",
                schema: "ACC",
                table: "PostGLDepositAccount",
                column: "MST_BankID");

            migrationBuilder.CreateIndex(
                name: "IX_PostGLDepositAccount_MST_CompanyID",
                schema: "ACC",
                table: "PostGLDepositAccount",
                column: "MST_CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_PostGLDepositAccount_MST_DistrictID",
                schema: "ACC",
                table: "PostGLDepositAccount",
                column: "MST_DistrictID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculateOther_PRJ_ProjectID",
                schema: "CMS",
                table: "CalculateOther",
                column: "PRJ_ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculateOther_USR_LCUserID",
                schema: "CMS",
                table: "CalculateOther",
                column: "USR_LCUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculatePerMonth_PRJ_UnitID",
                schema: "CMS",
                table: "CalculatePerMonth",
                column: "PRJ_UnitID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculatePerMonth_USR_LCAtProjectID",
                schema: "CMS",
                table: "CalculatePerMonth",
                column: "USR_LCAtProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculatePerMonth_USR_LCClosedDealUserID",
                schema: "CMS",
                table: "CalculatePerMonth",
                column: "USR_LCClosedDealUserID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculateSale_PRJ_UnitID",
                schema: "CMS",
                table: "CalculateSale",
                column: "PRJ_UnitID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculateSale_USR_LCAtProjectID",
                schema: "CMS",
                table: "CalculateSale",
                column: "USR_LCAtProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculateSale_USR_LCClosedDealID",
                schema: "CMS",
                table: "CalculateSale",
                column: "USR_LCClosedDealID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculateTransfer_PRJ_UnitID",
                schema: "CMS",
                table: "CalculateTransfer",
                column: "PRJ_UnitID");

            migrationBuilder.CreateIndex(
                name: "IX_CalculateTransfer_USR_LCCTransferID",
                schema: "CMS",
                table: "CalculateTransfer",
                column: "USR_LCCTransferID");

            migrationBuilder.CreateIndex(
                name: "IX_ContactAddress_ContactID",
                schema: "CTM",
                table: "ContactAddress",
                column: "ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_ContactAddressProject_ContactAddressID",
                schema: "CTM",
                table: "ContactAddressProject",
                column: "ContactAddressID");

            migrationBuilder.CreateIndex(
                name: "IX_ContactAddressProject_PRJ_ProjectID",
                schema: "CTM",
                table: "ContactAddressProject",
                column: "PRJ_ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_ContactEmail_ContactID",
                schema: "CTM",
                table: "ContactEmail",
                column: "ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_ContactPhone_ContactID",
                schema: "CTM",
                table: "ContactPhone",
                column: "ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_Lead_ContactID",
                schema: "CTM",
                table: "Lead",
                column: "ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_LeadActivity_LeadID",
                schema: "CTM",
                table: "LeadActivity",
                column: "LeadID");

            migrationBuilder.CreateIndex(
                name: "IX_LeadActivity_StatusID",
                schema: "CTM",
                table: "LeadActivity",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Opportunity_ContactID",
                schema: "CTM",
                table: "Opportunity",
                column: "ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_Opportunity_PRJ_ProjectID",
                schema: "CTM",
                table: "Opportunity",
                column: "PRJ_ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_OpportunityActivity_OpportunityID",
                schema: "CTM",
                table: "OpportunityActivity",
                column: "OpportunityID");

            migrationBuilder.CreateIndex(
                name: "IX_OpportunityActivityTrack_OpportunityAcitivityID",
                schema: "CTM",
                table: "OpportunityActivityTrack",
                column: "OpportunityAcitivityID");

            migrationBuilder.CreateIndex(
                name: "IX_OpportunityActivityTrack_StatusID",
                schema: "CTM",
                table: "OpportunityActivityTrack",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Visitor_ContactID",
                schema: "CTM",
                table: "Visitor",
                column: "ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_Visitor_PRJ_ProjectID",
                schema: "CTM",
                table: "Visitor",
                column: "PRJ_ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_BillPayment_MST_BankID",
                schema: "FIN",
                table: "BillPayment",
                column: "MST_BankID");

            migrationBuilder.CreateIndex(
                name: "IX_BillPaymentTransaction_BillPaymentID",
                schema: "FIN",
                table: "BillPaymentTransaction",
                column: "BillPaymentID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerWallet_CTM_ContactID",
                schema: "FIN",
                table: "CustomerWallet",
                column: "CTM_ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerWallet_PRJ_ProjectID",
                schema: "FIN",
                table: "CustomerWallet",
                column: "PRJ_ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerWalletTransaction_CustomerWalletID",
                schema: "FIN",
                table: "CustomerWalletTransaction",
                column: "CustomerWalletID");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerWalletTransaction_PaymentTypeItemID",
                schema: "FIN",
                table: "CustomerWalletTransaction",
                column: "PaymentTypeItemID");

            migrationBuilder.CreateIndex(
                name: "IX_Deposit_ACC_BankAccount",
                schema: "FIN",
                table: "Deposit",
                column: "ACC_BankAccount");

            migrationBuilder.CreateIndex(
                name: "IX_DirectCreditDebitApprovalForm_SAL_BookingID",
                schema: "FIN",
                table: "DirectCreditDebitApprovalForm",
                column: "SAL_BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_DirectCreditDebitExport_MST_BankID",
                schema: "FIN",
                table: "DirectCreditDebitExport",
                column: "MST_BankID");

            migrationBuilder.CreateIndex(
                name: "IX_DirectCreditDebitExport_MST_CompanyID",
                schema: "FIN",
                table: "DirectCreditDebitExport",
                column: "MST_CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_DirectCreditDebitTransaction_DirectCreditDebitUnitPriceItemID",
                schema: "FIN",
                table: "DirectCreditDebitTransaction",
                column: "DirectCreditDebitUnitPriceItemID");

            migrationBuilder.CreateIndex(
                name: "IX_DirectCreditDebitUnitPriceItem_DirectCreditDebitFormID",
                schema: "FIN",
                table: "DirectCreditDebitUnitPriceItem",
                column: "DirectCreditDebitFormID");

            migrationBuilder.CreateIndex(
                name: "IX_DirectCreditDetail_MST_BankID",
                schema: "FIN",
                table: "DirectCreditDetail",
                column: "MST_BankID");

            migrationBuilder.CreateIndex(
                name: "IX_DirectDebitDetail_MST_BankBranchID",
                schema: "FIN",
                table: "DirectDebitDetail",
                column: "MST_BankBranchID");

            migrationBuilder.CreateIndex(
                name: "IX_DirectDebitDetail_MST_BankID",
                schema: "FIN",
                table: "DirectDebitDetail",
                column: "MST_BankID");

            migrationBuilder.CreateIndex(
                name: "IX_DirectDebitDetail_MST_ProvinceID",
                schema: "FIN",
                table: "DirectDebitDetail",
                column: "MST_ProvinceID");

            migrationBuilder.CreateIndex(
                name: "IX_EDC_MST_BankID",
                schema: "FIN",
                table: "EDC",
                column: "MST_BankID");

            migrationBuilder.CreateIndex(
                name: "IX_EDC_PRJ_ProjectID",
                schema: "FIN",
                table: "EDC",
                column: "PRJ_ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_SAL_BookingID",
                schema: "FIN",
                table: "Payment",
                column: "SAL_BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentBankTransfer_ACC_BankAccountID",
                schema: "FIN",
                table: "PaymentBankTransfer",
                column: "ACC_BankAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentBankTransfer_PaymentMethodID",
                schema: "FIN",
                table: "PaymentBankTransfer",
                column: "PaymentMethodID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentBankTransfer_UnknownPaymentID",
                schema: "FIN",
                table: "PaymentBankTransfer",
                column: "UnknownPaymentID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentBillPayment_BillPaymentTransactionID",
                schema: "FIN",
                table: "PaymentBillPayment",
                column: "BillPaymentTransactionID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentBillPayment_PaymentMethodID",
                schema: "FIN",
                table: "PaymentBillPayment",
                column: "PaymentMethodID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentCashierCheque_MST_BankBranchID",
                schema: "FIN",
                table: "PaymentCashierCheque",
                column: "MST_BankBranchID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentCashierCheque_MST_BankID",
                schema: "FIN",
                table: "PaymentCashierCheque",
                column: "MST_BankID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentCashierCheque_MST_PayToCompanyID",
                schema: "FIN",
                table: "PaymentCashierCheque",
                column: "MST_PayToCompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentCashierCheque_PaymentMethodID",
                schema: "FIN",
                table: "PaymentCashierCheque",
                column: "PaymentMethodID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentCreditCard_EDCID",
                schema: "FIN",
                table: "PaymentCreditCard",
                column: "EDCID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentCreditCard_MST_BankID",
                schema: "FIN",
                table: "PaymentCreditCard",
                column: "MST_BankID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentCreditCard_PaymentMethodID",
                schema: "FIN",
                table: "PaymentCreditCard",
                column: "PaymentMethodID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentCustomerWallet_CustomerWalletTransactionID",
                schema: "FIN",
                table: "PaymentCustomerWallet",
                column: "CustomerWalletTransactionID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentCustomerWallet_PaymentMethodID",
                schema: "FIN",
                table: "PaymentCustomerWallet",
                column: "PaymentMethodID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentDirectCreditDebit_DirectCreditDebitTransactionID",
                schema: "FIN",
                table: "PaymentDirectCreditDebit",
                column: "DirectCreditDebitTransactionID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentDirectCreditDebit_PaymentMethodID",
                schema: "FIN",
                table: "PaymentDirectCreditDebit",
                column: "PaymentMethodID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentForeignBankTransfer_MST_BankID",
                schema: "FIN",
                table: "PaymentForeignBankTransfer",
                column: "MST_BankID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentForeignBankTransfer_PaymentMethodID",
                schema: "FIN",
                table: "PaymentForeignBankTransfer",
                column: "PaymentMethodID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentForeignBankTransfer_UnknownPaymentID",
                schema: "FIN",
                table: "PaymentForeignBankTransfer",
                column: "UnknownPaymentID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentItem_PaymentID",
                schema: "FIN",
                table: "PaymentItem",
                column: "PaymentID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethod_DepositID",
                schema: "FIN",
                table: "PaymentMethod",
                column: "DepositID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethod_PaymentID",
                schema: "FIN",
                table: "PaymentMethod",
                column: "PaymentID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethodToItem_PaymentItemID",
                schema: "FIN",
                table: "PaymentMethodToItem",
                column: "PaymentItemID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethodToItem_PaymentMethodID",
                schema: "FIN",
                table: "PaymentMethodToItem",
                column: "PaymentMethodID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentPersonalCheque_MST_BankBranchID",
                schema: "FIN",
                table: "PaymentPersonalCheque",
                column: "MST_BankBranchID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentPersonalCheque_MST_BankID",
                schema: "FIN",
                table: "PaymentPersonalCheque",
                column: "MST_BankID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentPersonalCheque_MST_PayToCompanyID",
                schema: "FIN",
                table: "PaymentPersonalCheque",
                column: "MST_PayToCompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentPersonalCheque_PaymentMethodID",
                schema: "FIN",
                table: "PaymentPersonalCheque",
                column: "PaymentMethodID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentQRCode_ACC_BankAccountID",
                schema: "FIN",
                table: "PaymentQRCode",
                column: "ACC_BankAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentQRCode_PaymentMethodID",
                schema: "FIN",
                table: "PaymentQRCode",
                column: "PaymentMethodID");

            migrationBuilder.CreateIndex(
                name: "IX_Receipt_CTM_ContactID",
                schema: "FIN",
                table: "Receipt",
                column: "CTM_ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_Receipt_MST_CompanyID",
                schema: "FIN",
                table: "Receipt",
                column: "MST_CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Receipt_PaymentID",
                schema: "FIN",
                table: "Receipt",
                column: "PaymentID");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptSendEmailHistory_CTM_SendToContactID",
                schema: "FIN",
                table: "ReceiptSendEmailHistory",
                column: "CTM_SendToContactID");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptSendEmailHistory_ReceiptID",
                schema: "FIN",
                table: "ReceiptSendEmailHistory",
                column: "ReceiptID");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptSendPrintingHistory_ReceiptID",
                schema: "FIN",
                table: "ReceiptSendPrintingHistory",
                column: "ReceiptID");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptTemp_CTM_ContactID",
                schema: "FIN",
                table: "ReceiptTemp",
                column: "CTM_ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptTemp_MST_CompanyID",
                schema: "FIN",
                table: "ReceiptTemp",
                column: "MST_CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptTemp_PaymentID",
                schema: "FIN",
                table: "ReceiptTemp",
                column: "PaymentID");

            migrationBuilder.CreateIndex(
                name: "IX_UnknownPayment_ACC_BankAccountID",
                schema: "FIN",
                table: "UnknownPayment",
                column: "ACC_BankAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_UnknownPayment_MST_AttachFileFromBankID",
                schema: "FIN",
                table: "UnknownPayment",
                column: "MST_AttachFileFromBankID");

            migrationBuilder.CreateIndex(
                name: "IX_DownPaymentLetter_SAL_AgreementID",
                schema: "LET",
                table: "DownPaymentLetter",
                column: "SAL_AgreementID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferLetter_SAL_AgreementID",
                schema: "LET",
                table: "TransferLetter",
                column: "SAL_AgreementID");

            migrationBuilder.CreateIndex(
                name: "IX_BankBranch_BankID",
                schema: "MST",
                table: "BankBranch",
                column: "BankID");

            migrationBuilder.CreateIndex(
                name: "IX_Brand_SBUID",
                schema: "MST",
                table: "Brand",
                column: "SBUID");

            migrationBuilder.CreateIndex(
                name: "IX_District_ProvinceID",
                schema: "MST",
                table: "District",
                column: "ProvinceID");

            migrationBuilder.CreateIndex(
                name: "IX_MasterCenter_MasterCenterGroupID",
                schema: "MST",
                table: "MasterCenter",
                column: "MasterCenterGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Menu_MenuAreaID",
                schema: "MST",
                table: "Menu",
                column: "MenuAreaID");

            migrationBuilder.CreateIndex(
                name: "IX_Menu_ParentMenuID",
                schema: "MST",
                table: "Menu",
                column: "ParentMenuID");

            migrationBuilder.CreateIndex(
                name: "IX_SubDistrict_DistrictID",
                schema: "MST",
                table: "SubDistrict",
                column: "DistrictID");

            migrationBuilder.CreateIndex(
                name: "IX_SubDistrict_LandOfficeID",
                schema: "MST",
                table: "SubDistrict",
                column: "LandOfficeID");

            migrationBuilder.CreateIndex(
                name: "IX_MobileInstallation_USR_UserID",
                schema: "NTF",
                table: "MobileInstallation",
                column: "USR_UserID");

            migrationBuilder.CreateIndex(
                name: "IX_MobileNotification_USR_UserID",
                schema: "NTF",
                table: "MobileNotification",
                column: "USR_UserID");

            migrationBuilder.CreateIndex(
                name: "IX_WebNotification_USR_UserID",
                schema: "NTF",
                table: "WebNotification",
                column: "USR_UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ContactStory_CTM_ContactID",
                schema: "OST",
                table: "ContactStory",
                column: "CTM_ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_ContactStory_ContactStoryGroupID",
                schema: "OST",
                table: "ContactStory",
                column: "ContactStoryGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_ContactStory_ContactStoryTypeID",
                schema: "OST",
                table: "ContactStory",
                column: "ContactStoryTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_UnitStory_UnitID",
                schema: "OST",
                table: "UnitStory",
                column: "UnitID");

            migrationBuilder.CreateIndex(
                name: "IX_UnitStory_UnitStoryGroupID",
                schema: "OST",
                table: "UnitStory",
                column: "UnitStoryGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_UnitStory_UnitStoryTypeID",
                schema: "OST",
                table: "UnitStory",
                column: "UnitStoryTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Address_ProjectID",
                schema: "PRJ",
                table: "Address",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Budget_ProjectID",
                schema: "PRJ",
                table: "Budget",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetPromotion_ProjectID",
                schema: "PRJ",
                table: "BudgetPromotion",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Floor_TowerID",
                schema: "PRJ",
                table: "Floor",
                column: "TowerID");

            migrationBuilder.CreateIndex(
                name: "IX_FloorPlanImage_ProjectID",
                schema: "PRJ",
                table: "FloorPlanImage",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_HighRiseFee_UnitID",
                schema: "PRJ",
                table: "HighRiseFee",
                column: "UnitID");

            migrationBuilder.CreateIndex(
                name: "IX_License_ProjectID",
                schema: "PRJ",
                table: "License",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_LowRiseBuildingPriceFee_ModelID",
                schema: "PRJ",
                table: "LowRiseBuildingPriceFee",
                column: "ModelID");

            migrationBuilder.CreateIndex(
                name: "IX_LowRiseBuildingPriceFee_ProjectID",
                schema: "PRJ",
                table: "LowRiseBuildingPriceFee",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_LowRiseBuildingPriceFee_UnitID",
                schema: "PRJ",
                table: "LowRiseBuildingPriceFee",
                column: "UnitID");

            migrationBuilder.CreateIndex(
                name: "IX_LowRiseFenceFee_ProjectID",
                schema: "PRJ",
                table: "LowRiseFenceFee",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Model_ProjectID",
                schema: "PRJ",
                table: "Model",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Permit_ProjectID",
                schema: "PRJ",
                table: "Permit",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_PriceList_UnitID",
                schema: "PRJ",
                table: "PriceList",
                column: "UnitID");

            migrationBuilder.CreateIndex(
                name: "IX_PriceListItem_PriceListID",
                schema: "PRJ",
                table: "PriceListItem",
                column: "PriceListID");

            migrationBuilder.CreateIndex(
                name: "IX_RoomPlanImage_ProjectID",
                schema: "PRJ",
                table: "RoomPlanImage",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_RoundFee_ProjectID",
                schema: "PRJ",
                table: "RoundFee",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_TitledeedDetail_ProjectID",
                schema: "PRJ",
                table: "TitledeedDetail",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Tower_ProjectID",
                schema: "PRJ",
                table: "Tower",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Unit_FloorID",
                schema: "PRJ",
                table: "Unit",
                column: "FloorID");

            migrationBuilder.CreateIndex(
                name: "IX_Unit_ProjectID",
                schema: "PRJ",
                table: "Unit",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_WaiveQC_ProjectID",
                schema: "PRJ",
                table: "WaiveQC",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingPromotion_PromotionID",
                schema: "PRM",
                table: "BookingPromotion",
                column: "PromotionID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingPromotion_SAL_BookingID",
                schema: "PRM",
                table: "BookingPromotion",
                column: "SAL_BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingPromotionExpense_BookingPromotionID",
                schema: "PRM",
                table: "BookingPromotionExpense",
                column: "BookingPromotionID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingPromotionItem_BookingPromotionID",
                schema: "PRM",
                table: "BookingPromotionItem",
                column: "BookingPromotionID");

            migrationBuilder.CreateIndex(
                name: "IX_Promotion_PRJ_ProjectID",
                schema: "PRM",
                table: "Promotion",
                column: "PRJ_ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionCard_PromotionCardItemID",
                schema: "PRM",
                table: "PromotionCard",
                column: "PromotionCardItemID");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionCard_PromotionID",
                schema: "PRM",
                table: "PromotionCard",
                column: "PromotionID");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionDelivery_TransferPromotionID",
                schema: "PRM",
                table: "PromotionDelivery",
                column: "TransferPromotionID");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionDeliveryItem_PromotionDeliveryID",
                schema: "PRM",
                table: "PromotionDeliveryItem",
                column: "PromotionDeliveryID");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionDetail_PromotionID",
                schema: "PRM",
                table: "PromotionDetail",
                column: "PromotionID");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionDetail_PromotionItemID",
                schema: "PRM",
                table: "PromotionDetail",
                column: "PromotionItemID");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionPreSale_PRJ_ProjectID",
                schema: "PRM",
                table: "PromotionPreSale",
                column: "PRJ_ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionPreSaleDetail_PromotionItemID",
                schema: "PRM",
                table: "PromotionPreSaleDetail",
                column: "PromotionItemID");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionPreSaleDetail_PromotionPreSaleID",
                schema: "PRM",
                table: "PromotionPreSaleDetail",
                column: "PromotionPreSaleID");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionReceive_TransferPromotionID",
                schema: "PRM",
                table: "PromotionReceive",
                column: "TransferPromotionID");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionReceiveItem_PromotionReceiveID",
                schema: "PRM",
                table: "PromotionReceiveItem",
                column: "PromotionReceiveID");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionSubDetail_PromotionDetailID",
                schema: "PRM",
                table: "PromotionSubDetail",
                column: "PromotionDetailID");

            migrationBuilder.CreateIndex(
                name: "IX_PromotionSubDetail_PromotionItemID",
                schema: "PRM",
                table: "PromotionSubDetail",
                column: "PromotionItemID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationBookingPromotion_PromotionID",
                schema: "PRM",
                table: "QuotationBookingPromotion",
                column: "PromotionID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationBookingPromotion_SAL_QuotationID",
                schema: "PRM",
                table: "QuotationBookingPromotion",
                column: "SAL_QuotationID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationBookingPromotionItem_QuotationBookingPromotionID",
                schema: "PRM",
                table: "QuotationBookingPromotionItem",
                column: "QuotationBookingPromotionID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationPromotionExpense_QuotationBookingPromotionID",
                schema: "PRM",
                table: "QuotationPromotionExpense",
                column: "QuotationBookingPromotionID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationTransferPromotion_SAL_QuotationID",
                schema: "PRM",
                table: "QuotationTransferPromotion",
                column: "SAL_QuotationID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationTransferPromotionItem_QuotationTransferPromotionID",
                schema: "PRM",
                table: "QuotationTransferPromotionItem",
                column: "QuotationTransferPromotionID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferPromotion_SAL_BookingID",
                schema: "PRM",
                table: "TransferPromotion",
                column: "SAL_BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferPromotionExpense_TransferPromotionID",
                schema: "PRM",
                table: "TransferPromotionExpense",
                column: "TransferPromotionID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferPromotionItem_TransferPromotionID",
                schema: "PRM",
                table: "TransferPromotionItem",
                column: "TransferPromotionID");

            migrationBuilder.CreateIndex(
                name: "IX_Agreement_ActiveUnitPriceID",
                schema: "SAL",
                table: "Agreement",
                column: "ActiveUnitPriceID");

            migrationBuilder.CreateIndex(
                name: "IX_Agreement_BookingID",
                schema: "SAL",
                table: "Agreement",
                column: "BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_Agreement_CTM_ContactID",
                schema: "SAL",
                table: "Agreement",
                column: "CTM_ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_Agreement_PRJ_UnitID",
                schema: "SAL",
                table: "Agreement",
                column: "PRJ_UnitID");

            migrationBuilder.CreateIndex(
                name: "IX_AgreementDownPeriod_SAL_AgreementID",
                schema: "SAL",
                table: "AgreementDownPeriod",
                column: "SAL_AgreementID");

            migrationBuilder.CreateIndex(
                name: "IX_AgreementOwner_CTM_ContactID",
                schema: "SAL",
                table: "AgreementOwner",
                column: "CTM_ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_AgreementOwner_SAL_AgreementID",
                schema: "SAL",
                table: "AgreementOwner",
                column: "SAL_AgreementID");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_ActiveUnitPriceID",
                schema: "SAL",
                table: "Booking",
                column: "ActiveUnitPriceID");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_CTM_ContactID",
                schema: "SAL",
                table: "Booking",
                column: "CTM_ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_PRJ_UnitID",
                schema: "SAL",
                table: "Booking",
                column: "PRJ_UnitID");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_USR_AgencyID",
                schema: "SAL",
                table: "Booking",
                column: "USR_AgencyID");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_USR_SaleAtProjectID",
                schema: "SAL",
                table: "Booking",
                column: "USR_SaleAtProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_USR_SaleID",
                schema: "SAL",
                table: "Booking",
                column: "USR_SaleID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingOwner_CTM_ContactID",
                schema: "SAL",
                table: "BookingOwner",
                column: "CTM_ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingOwner_SAL_BookingID",
                schema: "SAL",
                table: "BookingOwner",
                column: "SAL_BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_MortgageWithBank_MST_BankID",
                schema: "SAL",
                table: "MortgageWithBank",
                column: "MST_BankID");

            migrationBuilder.CreateIndex(
                name: "IX_Quotation_PRJ_UnitID",
                schema: "SAL",
                table: "Quotation",
                column: "PRJ_UnitID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationCompare_PRJ_UnitID",
                schema: "SAL",
                table: "QuotationCompare",
                column: "PRJ_UnitID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationUnitPrice_BookingID",
                schema: "SAL",
                table: "QuotationUnitPrice",
                column: "BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationUnitPriceItem_QuotationID",
                schema: "SAL",
                table: "QuotationUnitPriceItem",
                column: "QuotationID");

            migrationBuilder.CreateIndex(
                name: "IX_TitledeedReceive_PRJ_TitledeedDetailID",
                schema: "SAL",
                table: "TitledeedReceive",
                column: "PRJ_TitledeedDetailID");

            migrationBuilder.CreateIndex(
                name: "IX_TitledeedReceiveHistory_TitledeedReceiveID",
                schema: "SAL",
                table: "TitledeedReceiveHistory",
                column: "TitledeedReceiveID");

            migrationBuilder.CreateIndex(
                name: "IX_TitledeedReceiveHistory_USR_ActorUserID",
                schema: "SAL",
                table: "TitledeedReceiveHistory",
                column: "USR_ActorUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Transfer_PRJ_UnitID",
                schema: "SAL",
                table: "Transfer",
                column: "PRJ_UnitID");

            migrationBuilder.CreateIndex(
                name: "IX_Transfer_SAL_AgreementID",
                schema: "SAL",
                table: "Transfer",
                column: "SAL_AgreementID");

            migrationBuilder.CreateIndex(
                name: "IX_Transfer_USR_LCID",
                schema: "SAL",
                table: "Transfer",
                column: "USR_LCID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferCash_MST_BankBranchID",
                schema: "SAL",
                table: "TransferCash",
                column: "MST_BankBranchID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferCash_MST_BankID",
                schema: "SAL",
                table: "TransferCash",
                column: "MST_BankID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferCash_SAL_TransferID",
                schema: "SAL",
                table: "TransferCash",
                column: "SAL_TransferID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferCheque_MST_BankBranchID",
                schema: "SAL",
                table: "TransferCheque",
                column: "MST_BankBranchID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferCheque_MST_BankID",
                schema: "SAL",
                table: "TransferCheque",
                column: "MST_BankID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferCheque_SAL_TransferID",
                schema: "SAL",
                table: "TransferCheque",
                column: "SAL_TransferID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferDocument_SAL_TransferID",
                schema: "SAL",
                table: "TransferDocument",
                column: "SAL_TransferID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferOwner_CTM_ContactID",
                schema: "SAL",
                table: "TransferOwner",
                column: "CTM_ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferOwner_SAL_TransferID",
                schema: "SAL",
                table: "TransferOwner",
                column: "SAL_TransferID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferUnit_PRJ_NewUnitID",
                schema: "SAL",
                table: "TransferUnit",
                column: "PRJ_NewUnitID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferUnit_PRJ_OldUnitID",
                schema: "SAL",
                table: "TransferUnit",
                column: "PRJ_OldUnitID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferUnit_SAL_AgreementID",
                schema: "SAL",
                table: "TransferUnit",
                column: "SAL_AgreementID");

            migrationBuilder.CreateIndex(
                name: "IX_UnitPrice_BookingID",
                schema: "SAL",
                table: "UnitPrice",
                column: "BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_UnitPriceItem_UnitPriceID",
                schema: "SAL",
                table: "UnitPriceItem",
                column: "UnitPriceID");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorizeRule_ParentRuleID",
                schema: "USR",
                table: "AuthorizeRule",
                column: "ParentRuleID");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorizeRuleByRole_AuthorizeRuleID",
                schema: "USR",
                table: "AuthorizeRuleByRole",
                column: "AuthorizeRuleID");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorizeRuleByRole_RoleID",
                schema: "USR",
                table: "AuthorizeRuleByRole",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteMenu_MST_MenuID",
                schema: "USR",
                table: "FavoriteMenu",
                column: "MST_MenuID");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteMenu_UserID",
                schema: "USR",
                table: "FavoriteMenu",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Role_RoleGroupID",
                schema: "USR",
                table: "Role",
                column: "RoleGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Task_FromUserID",
                schema: "USR",
                table: "Task",
                column: "FromUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Task_TaskTypeID",
                schema: "USR",
                table: "Task",
                column: "TaskTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Task_UserID",
                schema: "USR",
                table: "Task",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserDefaultProject_PRJ_ProjectID",
                schema: "USR",
                table: "UserDefaultProject",
                column: "PRJ_ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_UserDefaultProject_UserID",
                schema: "USR",
                table: "UserDefaultProject",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleID",
                schema: "USR",
                table: "UserRole",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserID",
                schema: "USR",
                table: "UserRole",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Workflow_WorkflowTypeID",
                schema: "WFL",
                table: "Workflow",
                column: "WorkflowTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowApprover_USR_ApproverID",
                schema: "WFL",
                table: "WorkflowApprover",
                column: "USR_ApproverID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowApprover_USR_RoleID",
                schema: "WFL",
                table: "WorkflowApprover",
                column: "USR_RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowApprover_WorkflowStepID",
                schema: "WFL",
                table: "WorkflowApprover",
                column: "WorkflowStepID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowApproverTemplate_USR_ApproverID",
                schema: "WFL",
                table: "WorkflowApproverTemplate",
                column: "USR_ApproverID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowApproverTemplate_USR_RoleID",
                schema: "WFL",
                table: "WorkflowApproverTemplate",
                column: "USR_RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowApproverTemplate_WorkflowStepTemplateID",
                schema: "WFL",
                table: "WorkflowApproverTemplate",
                column: "WorkflowStepTemplateID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowStep_WorkflowID",
                schema: "WFL",
                table: "WorkflowStep",
                column: "WorkflowID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowStepTemplate_WorkflowTemplateID",
                schema: "WFL",
                table: "WorkflowStepTemplate",
                column: "WorkflowTemplateID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowTemplate_WorkflowTypeID",
                schema: "WFL",
                table: "WorkflowTemplate",
                column: "WorkflowTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentBankTransfer_PaymentMethod_PaymentMethodID",
                schema: "FIN",
                table: "PaymentBankTransfer",
                column: "PaymentMethodID",
                principalSchema: "FIN",
                principalTable: "PaymentMethod",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentQRCode_PaymentMethod_PaymentMethodID",
                schema: "FIN",
                table: "PaymentQRCode",
                column: "PaymentMethodID",
                principalSchema: "FIN",
                principalTable: "PaymentMethod",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipt_Payment_PaymentID",
                schema: "FIN",
                table: "Receipt",
                column: "PaymentID",
                principalSchema: "FIN",
                principalTable: "Payment",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiptTemp_Payment_PaymentID",
                schema: "FIN",
                table: "ReceiptTemp",
                column: "PaymentID",
                principalSchema: "FIN",
                principalTable: "Payment",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Agreement_Booking_BookingID",
                schema: "SAL",
                table: "Agreement",
                column: "BookingID",
                principalSchema: "SAL",
                principalTable: "Booking",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Agreement_UnitPrice_ActiveUnitPriceID",
                schema: "SAL",
                table: "Agreement",
                column: "ActiveUnitPriceID",
                principalSchema: "SAL",
                principalTable: "UnitPrice",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_UnitPrice_ActiveUnitPriceID",
                schema: "SAL",
                table: "Booking",
                column: "ActiveUnitPriceID",
                principalSchema: "SAL",
                principalTable: "UnitPrice",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_User_USR_AgencyID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_User_USR_SaleAtProjectID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_User_USR_SaleID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Tower_Project_ProjectID",
                schema: "PRJ",
                table: "Tower");

            migrationBuilder.DropForeignKey(
                name: "FK_Unit_Project_ProjectID",
                schema: "PRJ",
                table: "Unit");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Unit_PRJ_UnitID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Contact_CTM_ContactID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitPrice_Booking_BookingID",
                schema: "SAL",
                table: "UnitPrice");

            migrationBuilder.DropTable(
                name: "CalendarLockHistory",
                schema: "ACC");

            migrationBuilder.DropTable(
                name: "GLDetail",
                schema: "ACC");

            migrationBuilder.DropTable(
                name: "GLExport",
                schema: "ACC");

            migrationBuilder.DropTable(
                name: "PostGLAccount",
                schema: "ACC");

            migrationBuilder.DropTable(
                name: "PostGLChartOfAccount",
                schema: "ACC");

            migrationBuilder.DropTable(
                name: "PostGLDepositAccount",
                schema: "ACC");

            migrationBuilder.DropTable(
                name: "PostGLHouseType",
                schema: "ACC");

            migrationBuilder.DropTable(
                name: "CalculateOther",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "CalculatePerMonth",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "CalculateSale",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "CalculateTransfer",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "GeneralSetting",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "RateOnTop",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "RateSettingSale",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "RateSettingSaleFix",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "RateSettingTransfer",
                schema: "CMS");

            migrationBuilder.DropTable(
                name: "ContactAddressProject",
                schema: "CTM");

            migrationBuilder.DropTable(
                name: "ContactEmail",
                schema: "CTM");

            migrationBuilder.DropTable(
                name: "ContactPhone",
                schema: "CTM");

            migrationBuilder.DropTable(
                name: "LeadActivity",
                schema: "CTM");

            migrationBuilder.DropTable(
                name: "OpportunityActivityTrack",
                schema: "CTM");

            migrationBuilder.DropTable(
                name: "Visitor",
                schema: "CTM");

            migrationBuilder.DropTable(
                name: "JobTransaction",
                schema: "DMT");

            migrationBuilder.DropTable(
                name: "DirectCreditDebitExport",
                schema: "FIN");

            migrationBuilder.DropTable(
                name: "DirectCreditDetail",
                schema: "FIN");

            migrationBuilder.DropTable(
                name: "DirectDebitDetail",
                schema: "FIN");

            migrationBuilder.DropTable(
                name: "EDCFee",
                schema: "FIN");

            migrationBuilder.DropTable(
                name: "PaymentBankTransfer",
                schema: "FIN");

            migrationBuilder.DropTable(
                name: "PaymentBillPayment",
                schema: "FIN");

            migrationBuilder.DropTable(
                name: "PaymentCashierCheque",
                schema: "FIN");

            migrationBuilder.DropTable(
                name: "PaymentCreditCard",
                schema: "FIN");

            migrationBuilder.DropTable(
                name: "PaymentCustomerWallet",
                schema: "FIN");

            migrationBuilder.DropTable(
                name: "PaymentDirectCreditDebit",
                schema: "FIN");

            migrationBuilder.DropTable(
                name: "PaymentForeignBankTransfer",
                schema: "FIN");

            migrationBuilder.DropTable(
                name: "PaymentMethodToItem",
                schema: "FIN");

            migrationBuilder.DropTable(
                name: "PaymentPersonalCheque",
                schema: "FIN");

            migrationBuilder.DropTable(
                name: "PaymentQRCode",
                schema: "FIN");

            migrationBuilder.DropTable(
                name: "ReceiptSendEmailHistory",
                schema: "FIN");

            migrationBuilder.DropTable(
                name: "ReceiptSendPrintingHistory",
                schema: "FIN");

            migrationBuilder.DropTable(
                name: "ReceiptTemp",
                schema: "FIN");

            migrationBuilder.DropTable(
                name: "DownPaymentLetter",
                schema: "LET");

            migrationBuilder.DropTable(
                name: "TransferLetter",
                schema: "LET");

            migrationBuilder.DropTable(
                name: "Brand",
                schema: "MST");

            migrationBuilder.DropTable(
                name: "Counter",
                schema: "MST");

            migrationBuilder.DropTable(
                name: "MasterCenter",
                schema: "MST");

            migrationBuilder.DropTable(
                name: "SubDistrict",
                schema: "MST");

            migrationBuilder.DropTable(
                name: "EmailNotification",
                schema: "NTF");

            migrationBuilder.DropTable(
                name: "MobileInstallation",
                schema: "NTF");

            migrationBuilder.DropTable(
                name: "MobileNotification",
                schema: "NTF");

            migrationBuilder.DropTable(
                name: "NotificationTemplate",
                schema: "NTF");

            migrationBuilder.DropTable(
                name: "WebNotification",
                schema: "NTF");

            migrationBuilder.DropTable(
                name: "ContactStory",
                schema: "OST");

            migrationBuilder.DropTable(
                name: "UnitStory",
                schema: "OST");

            migrationBuilder.DropTable(
                name: "Address",
                schema: "PRJ");

            migrationBuilder.DropTable(
                name: "Budget",
                schema: "PRJ");

            migrationBuilder.DropTable(
                name: "BudgetPromotion",
                schema: "PRJ");

            migrationBuilder.DropTable(
                name: "FloorPlanImage",
                schema: "PRJ");

            migrationBuilder.DropTable(
                name: "HighRiseFee",
                schema: "PRJ");

            migrationBuilder.DropTable(
                name: "License",
                schema: "PRJ");

            migrationBuilder.DropTable(
                name: "LowRiseBuildingPriceFee",
                schema: "PRJ");

            migrationBuilder.DropTable(
                name: "LowRiseFee",
                schema: "PRJ");

            migrationBuilder.DropTable(
                name: "LowRiseFenceFee",
                schema: "PRJ");

            migrationBuilder.DropTable(
                name: "Permit",
                schema: "PRJ");

            migrationBuilder.DropTable(
                name: "PriceListItem",
                schema: "PRJ");

            migrationBuilder.DropTable(
                name: "RoomPlanImage",
                schema: "PRJ");

            migrationBuilder.DropTable(
                name: "RoundFee",
                schema: "PRJ");

            migrationBuilder.DropTable(
                name: "WaiveQC",
                schema: "PRJ");

            migrationBuilder.DropTable(
                name: "BookingPromotionExpense",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "BookingPromotionItem",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "PromotionCard",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "PromotionDeliveryItem",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "PromotionMaterial",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "PromotionPreSaleDetail",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "PromotionReceiveItem",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "PromotionSubDetail",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "QuotationBookingPromotionItem",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "QuotationPromotionExpense",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "QuotationTransferPromotionItem",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "TransferPromotionExpense",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "TransferPromotionItem",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "AgreementDownPeriod",
                schema: "SAL");

            migrationBuilder.DropTable(
                name: "AgreementOwner",
                schema: "SAL");

            migrationBuilder.DropTable(
                name: "BookingOwner",
                schema: "SAL");

            migrationBuilder.DropTable(
                name: "MortgageWithBank",
                schema: "SAL");

            migrationBuilder.DropTable(
                name: "QuotationCompare",
                schema: "SAL");

            migrationBuilder.DropTable(
                name: "QuotationUnitPrice",
                schema: "SAL");

            migrationBuilder.DropTable(
                name: "QuotationUnitPriceItem",
                schema: "SAL");

            migrationBuilder.DropTable(
                name: "TitledeedReceiveHistory",
                schema: "SAL");

            migrationBuilder.DropTable(
                name: "TransferCash",
                schema: "SAL");

            migrationBuilder.DropTable(
                name: "TransferCheque",
                schema: "SAL");

            migrationBuilder.DropTable(
                name: "TransferDocument",
                schema: "SAL");

            migrationBuilder.DropTable(
                name: "TransferOwner",
                schema: "SAL");

            migrationBuilder.DropTable(
                name: "TransferUnit",
                schema: "SAL");

            migrationBuilder.DropTable(
                name: "UnitPriceItem",
                schema: "SAL");

            migrationBuilder.DropTable(
                name: "AuthorizeRuleByRole",
                schema: "USR");

            migrationBuilder.DropTable(
                name: "FavoriteMenu",
                schema: "USR");

            migrationBuilder.DropTable(
                name: "Task",
                schema: "USR");

            migrationBuilder.DropTable(
                name: "UserDefaultProject",
                schema: "USR");

            migrationBuilder.DropTable(
                name: "UserRole",
                schema: "USR");

            migrationBuilder.DropTable(
                name: "WorkflowApprover",
                schema: "WFL");

            migrationBuilder.DropTable(
                name: "WorkflowApproverTemplate",
                schema: "WFL");

            migrationBuilder.DropTable(
                name: "CalendarLock",
                schema: "ACC");

            migrationBuilder.DropTable(
                name: "ContactAddress",
                schema: "CTM");

            migrationBuilder.DropTable(
                name: "Lead",
                schema: "CTM");

            migrationBuilder.DropTable(
                name: "LeadActivityStatus",
                schema: "CTM");

            migrationBuilder.DropTable(
                name: "OpportunityActivity",
                schema: "CTM");

            migrationBuilder.DropTable(
                name: "OpportunityActivityStatus",
                schema: "CTM");

            migrationBuilder.DropTable(
                name: "BillPaymentTransaction",
                schema: "FIN");

            migrationBuilder.DropTable(
                name: "EDC",
                schema: "FIN");

            migrationBuilder.DropTable(
                name: "CustomerWalletTransaction",
                schema: "FIN");

            migrationBuilder.DropTable(
                name: "DirectCreditDebitTransaction",
                schema: "FIN");

            migrationBuilder.DropTable(
                name: "UnknownPayment",
                schema: "FIN");

            migrationBuilder.DropTable(
                name: "PaymentItem",
                schema: "FIN");

            migrationBuilder.DropTable(
                name: "Receipt",
                schema: "FIN");

            migrationBuilder.DropTable(
                name: "SBU",
                schema: "MST");

            migrationBuilder.DropTable(
                name: "MasterCenterGroup",
                schema: "MST");

            migrationBuilder.DropTable(
                name: "LandOffice",
                schema: "MST");

            migrationBuilder.DropTable(
                name: "ContactStoryGroup",
                schema: "OST");

            migrationBuilder.DropTable(
                name: "ContactStoryType",
                schema: "OST");

            migrationBuilder.DropTable(
                name: "UnitStoryGroup",
                schema: "OST");

            migrationBuilder.DropTable(
                name: "UnitStoryType",
                schema: "OST");

            migrationBuilder.DropTable(
                name: "Model",
                schema: "PRJ");

            migrationBuilder.DropTable(
                name: "PriceList",
                schema: "PRJ");

            migrationBuilder.DropTable(
                name: "BookingPromotion",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "PromotionCardItem",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "PromotionDelivery",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "PromotionPreSale",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "PromotionReceive",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "PromotionDetail",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "QuotationBookingPromotion",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "QuotationTransferPromotion",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "TitledeedReceive",
                schema: "SAL");

            migrationBuilder.DropTable(
                name: "Transfer",
                schema: "SAL");

            migrationBuilder.DropTable(
                name: "AuthorizeRule",
                schema: "USR");

            migrationBuilder.DropTable(
                name: "Menu",
                schema: "MST");

            migrationBuilder.DropTable(
                name: "TaskType",
                schema: "USR");

            migrationBuilder.DropTable(
                name: "WorkflowStep",
                schema: "WFL");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "USR");

            migrationBuilder.DropTable(
                name: "WorkflowStepTemplate",
                schema: "WFL");

            migrationBuilder.DropTable(
                name: "Opportunity",
                schema: "CTM");

            migrationBuilder.DropTable(
                name: "BillPayment",
                schema: "FIN");

            migrationBuilder.DropTable(
                name: "CustomerWallet",
                schema: "FIN");

            migrationBuilder.DropTable(
                name: "PaymentMethod",
                schema: "FIN");

            migrationBuilder.DropTable(
                name: "DirectCreditDebitUnitPriceItem",
                schema: "FIN");

            migrationBuilder.DropTable(
                name: "TransferPromotion",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "PromotionItem",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "Promotion",
                schema: "PRM");

            migrationBuilder.DropTable(
                name: "Quotation",
                schema: "SAL");

            migrationBuilder.DropTable(
                name: "TitledeedDetail",
                schema: "PRJ");

            migrationBuilder.DropTable(
                name: "Agreement",
                schema: "SAL");

            migrationBuilder.DropTable(
                name: "MenuArea",
                schema: "MST");

            migrationBuilder.DropTable(
                name: "Workflow",
                schema: "WFL");

            migrationBuilder.DropTable(
                name: "RoleGroup",
                schema: "USR");

            migrationBuilder.DropTable(
                name: "WorkflowTemplate",
                schema: "WFL");

            migrationBuilder.DropTable(
                name: "Deposit",
                schema: "FIN");

            migrationBuilder.DropTable(
                name: "Payment",
                schema: "FIN");

            migrationBuilder.DropTable(
                name: "DirectCreditDebitApprovalForm",
                schema: "FIN");

            migrationBuilder.DropTable(
                name: "WorkflowType",
                schema: "WFL");

            migrationBuilder.DropTable(
                name: "BankAccount",
                schema: "ACC");

            migrationBuilder.DropTable(
                name: "BankBranch",
                schema: "MST");

            migrationBuilder.DropTable(
                name: "Company",
                schema: "MST");

            migrationBuilder.DropTable(
                name: "District",
                schema: "MST");

            migrationBuilder.DropTable(
                name: "Bank",
                schema: "MST");

            migrationBuilder.DropTable(
                name: "Province",
                schema: "MST");

            migrationBuilder.DropTable(
                name: "User",
                schema: "USR");

            migrationBuilder.DropTable(
                name: "Project",
                schema: "PRJ");

            migrationBuilder.DropTable(
                name: "Unit",
                schema: "PRJ");

            migrationBuilder.DropTable(
                name: "Floor",
                schema: "PRJ");

            migrationBuilder.DropTable(
                name: "Tower",
                schema: "PRJ");

            migrationBuilder.DropTable(
                name: "Contact",
                schema: "CTM");

            migrationBuilder.DropTable(
                name: "Booking",
                schema: "SAL");

            migrationBuilder.DropTable(
                name: "UnitPrice",
                schema: "SAL");
        }
    }
}
