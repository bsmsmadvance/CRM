using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class CreateTitleDeedDetailHistoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "PRJ",
                table: "TitledeedDetail");

            migrationBuilder.CreateTable(
                name: "TitledeedDetailHistory",
                schema: "PRJ",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ProjectID = table.Column<Guid>(nullable: true),
                    UnitID = table.Column<Guid>(nullable: true),
                    TitledeedNo = table.Column<string>(maxLength: 50, nullable: true),
                    TitledeedArea = table.Column<double>(nullable: true),
                    AddressID = table.Column<Guid>(nullable: true),
                    LandOfficeID = table.Column<Guid>(nullable: true),
                    LandNo = table.Column<string>(maxLength: 100, nullable: true),
                    HouseNo = table.Column<string>(maxLength: 100, nullable: true),
                    YearGotHouseNo = table.Column<int>(nullable: true),
                    UsedArea = table.Column<double>(nullable: true),
                    ParkingArea = table.Column<double>(nullable: true),
                    FenceArea = table.Column<double>(nullable: true),
                    FenceIronArea = table.Column<double>(nullable: true),
                    BalconyArea = table.Column<double>(nullable: true),
                    AirArea = table.Column<double>(nullable: true),
                    BookNo = table.Column<string>(maxLength: 100, nullable: true),
                    PageNo = table.Column<string>(maxLength: 100, nullable: true),
                    EstimatePrice = table.Column<decimal>(type: "Money", nullable: true),
                    Remark = table.Column<string>(maxLength: 5000, nullable: true),
                    TitleDeedAddress = table.Column<string>(maxLength: 1000, nullable: true),
                    IsSameAddressAsTitledeed = table.Column<bool>(nullable: true),
                    HousePostalCode = table.Column<string>(maxLength: 10, nullable: true),
                    HouseProvinceID = table.Column<Guid>(nullable: true),
                    HouseDistrictID = table.Column<Guid>(nullable: true),
                    HouseSubDistrictID = table.Column<Guid>(nullable: true),
                    HouseMoo = table.Column<string>(maxLength: 1000, nullable: true),
                    HouseSoiTH = table.Column<string>(maxLength: 1000, nullable: true),
                    HouseSoiEN = table.Column<string>(maxLength: 1000, nullable: true),
                    HouseRoadTH = table.Column<string>(maxLength: 1000, nullable: true),
                    HouseRoadEN = table.Column<string>(maxLength: 1000, nullable: true),
                    LandSurveyArea = table.Column<double>(nullable: true),
                    LandPortionNo = table.Column<string>(maxLength: 50, nullable: true),
                    LandStatusMasterCenterID = table.Column<Guid>(nullable: true),
                    LandStatusDate = table.Column<DateTime>(nullable: true),
                    LandStatusNote = table.Column<string>(maxLength: 1000, nullable: true),
                    TitledeedDetailID = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TitledeedDetailHistory", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TitledeedDetailHistory_Address_AddressID",
                        column: x => x.AddressID,
                        principalSchema: "PRJ",
                        principalTable: "Address",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TitledeedDetailHistory_District_HouseDistrictID",
                        column: x => x.HouseDistrictID,
                        principalSchema: "MST",
                        principalTable: "District",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TitledeedDetailHistory_Province_HouseProvinceID",
                        column: x => x.HouseProvinceID,
                        principalSchema: "MST",
                        principalTable: "Province",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TitledeedDetailHistory_SubDistrict_HouseSubDistrictID",
                        column: x => x.HouseSubDistrictID,
                        principalSchema: "MST",
                        principalTable: "SubDistrict",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TitledeedDetailHistory_LandOffice_LandOfficeID",
                        column: x => x.LandOfficeID,
                        principalSchema: "MST",
                        principalTable: "LandOffice",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TitledeedDetailHistory_MasterCenter_LandStatusMasterCenterID",
                        column: x => x.LandStatusMasterCenterID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TitledeedDetailHistory_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "PRJ",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TitledeedDetailHistory_TitledeedDetail_TitledeedDetailID",
                        column: x => x.TitledeedDetailID,
                        principalSchema: "PRJ",
                        principalTable: "TitledeedDetail",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TitledeedDetailHistory_Unit_UnitID",
                        column: x => x.UnitID,
                        principalSchema: "PRJ",
                        principalTable: "Unit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TitledeedDetailHistory_AddressID",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_TitledeedDetailHistory_HouseDistrictID",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                column: "HouseDistrictID");

            migrationBuilder.CreateIndex(
                name: "IX_TitledeedDetailHistory_HouseProvinceID",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                column: "HouseProvinceID");

            migrationBuilder.CreateIndex(
                name: "IX_TitledeedDetailHistory_HouseSubDistrictID",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                column: "HouseSubDistrictID");

            migrationBuilder.CreateIndex(
                name: "IX_TitledeedDetailHistory_LandOfficeID",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                column: "LandOfficeID");

            migrationBuilder.CreateIndex(
                name: "IX_TitledeedDetailHistory_LandStatusMasterCenterID",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                column: "LandStatusMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_TitledeedDetailHistory_ProjectID",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_TitledeedDetailHistory_TitledeedDetailID",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                column: "TitledeedDetailID");

            migrationBuilder.CreateIndex(
                name: "IX_TitledeedDetailHistory_UnitID",
                schema: "PRJ",
                table: "TitledeedDetailHistory",
                column: "UnitID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TitledeedDetailHistory",
                schema: "PRJ");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "PRJ",
                table: "TitledeedDetail",
                nullable: false,
                defaultValue: false);
        }
    }
}
