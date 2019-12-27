using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class add_tb_TransferFeeResult_serm_kim : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OtherBankBranch",
                schema: "SAL",
                table: "CreditBanking",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ExpireDate",
                schema: "MST",
                table: "BOConfiguration",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "MortgageRate",
                schema: "MST",
                table: "BOConfiguration",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "NewMortgageRate",
                schema: "MST",
                table: "BOConfiguration",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "NewTransferFeeRate",
                schema: "MST",
                table: "BOConfiguration",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TransferFeeResult",
                schema: "SAL",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedByUserID = table.Column<Guid>(nullable: true),
                    UpdatedByUserID = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    RefMigrateID1 = table.Column<string>(maxLength: 100, nullable: true),
                    RefMigrateID2 = table.Column<string>(maxLength: 100, nullable: true),
                    RefMigrateID3 = table.Column<string>(maxLength: 100, nullable: true),
                    LastMigrateDate = table.Column<DateTime>(nullable: true),
                    TransferID = table.Column<Guid>(nullable: false),
                    ProjectID = table.Column<Guid>(nullable: true),
                    UnitID = table.Column<Guid>(nullable: true),
                    ModelID = table.Column<Guid>(nullable: true),
                    TowerID = table.Column<Guid>(nullable: true),
                    FloorID = table.Column<Guid>(nullable: true),
                    LandOfficeID = table.Column<Guid>(nullable: true),
                    SalePrice = table.Column<decimal>(type: "Money", nullable: true),
                    SaleArea = table.Column<double>(nullable: true),
                    UsedArea = table.Column<double>(nullable: true),
                    UsedAreaPrice = table.Column<decimal>(type: "Money", nullable: true),
                    DepreciationPerYear = table.Column<double>(nullable: true),
                    DepreciationYear = table.Column<int>(nullable: true),
                    CompanyIncomeTax = table.Column<decimal>(type: "Money", nullable: true),
                    BusinessTax = table.Column<decimal>(type: "Money", nullable: true),
                    LocalTax = table.Column<decimal>(type: "Money", nullable: true),
                    CondoFundPrice = table.Column<decimal>(type: "Money", nullable: true),
                    PublicFundMonths = table.Column<int>(nullable: true),
                    PublicFundPrice = table.Column<decimal>(type: "Money", nullable: true),
                    ElectricMeterPrice = table.Column<decimal>(type: "Money", nullable: true),
                    WaterMeterPrice = table.Column<decimal>(type: "Money", nullable: true),
                    MortgageRate = table.Column<double>(nullable: true),
                    TransferFeeRate = table.Column<double>(nullable: true),
                    BalconyArea = table.Column<double>(nullable: true),
                    BalconyAreaPrice = table.Column<decimal>(type: "Money", nullable: true),
                    AirArea = table.Column<double>(nullable: true),
                    AirAreaPrice = table.Column<decimal>(type: "Money", nullable: true),
                    NumberOfParkingFix = table.Column<int>(nullable: true),
                    NumberOfParkingUnFix = table.Column<int>(nullable: true),
                    ParkingArea = table.Column<double>(nullable: true),
                    ParkingAreaPrice = table.Column<decimal>(type: "Money", nullable: true),
                    Parkingstatus = table.Column<bool>(nullable: true),
                    ConcreteArea = table.Column<double>(nullable: true),
                    ConcreteAreaPrice = table.Column<decimal>(type: "Money", nullable: true),
                    IronArea = table.Column<double>(nullable: true),
                    IronAreaPrice = table.Column<decimal>(type: "Money", nullable: true),
                    EstimateLandPricePerUnit = table.Column<decimal>(type: "Money", nullable: true),
                    EstimateLandPrice = table.Column<decimal>(type: "Money", nullable: true),
                    EstimateDepreciationPrice = table.Column<decimal>(type: "Money", nullable: true),
                    EstimateBuildingPrice = table.Column<decimal>(type: "Money", nullable: true),
                    EstimateTotalPrice = table.Column<decimal>(type: "Money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferFeeResult", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TransferFeeResult_User_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferFeeResult_Floor_FloorID",
                        column: x => x.FloorID,
                        principalSchema: "PRJ",
                        principalTable: "Floor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferFeeResult_LandOffice_LandOfficeID",
                        column: x => x.LandOfficeID,
                        principalSchema: "MST",
                        principalTable: "LandOffice",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferFeeResult_Model_ModelID",
                        column: x => x.ModelID,
                        principalSchema: "PRJ",
                        principalTable: "Model",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferFeeResult_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "PRJ",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferFeeResult_Tower_TowerID",
                        column: x => x.TowerID,
                        principalSchema: "PRJ",
                        principalTable: "Tower",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferFeeResult_Transfer_TransferID",
                        column: x => x.TransferID,
                        principalSchema: "SAL",
                        principalTable: "Transfer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransferFeeResult_Unit_UnitID",
                        column: x => x.UnitID,
                        principalSchema: "PRJ",
                        principalTable: "Unit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferFeeResult_User_UpdatedByUserID",
                        column: x => x.UpdatedByUserID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransferFeeResult_CreatedByUserID",
                schema: "SAL",
                table: "TransferFeeResult",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferFeeResult_FloorID",
                schema: "SAL",
                table: "TransferFeeResult",
                column: "FloorID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferFeeResult_LandOfficeID",
                schema: "SAL",
                table: "TransferFeeResult",
                column: "LandOfficeID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferFeeResult_ModelID",
                schema: "SAL",
                table: "TransferFeeResult",
                column: "ModelID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferFeeResult_ProjectID",
                schema: "SAL",
                table: "TransferFeeResult",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferFeeResult_TowerID",
                schema: "SAL",
                table: "TransferFeeResult",
                column: "TowerID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferFeeResult_TransferID",
                schema: "SAL",
                table: "TransferFeeResult",
                column: "TransferID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferFeeResult_UnitID",
                schema: "SAL",
                table: "TransferFeeResult",
                column: "UnitID");

            migrationBuilder.CreateIndex(
                name: "IX_TransferFeeResult_UpdatedByUserID",
                schema: "SAL",
                table: "TransferFeeResult",
                column: "UpdatedByUserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransferFeeResult",
                schema: "SAL");

            migrationBuilder.DropColumn(
                name: "OtherBankBranch",
                schema: "SAL",
                table: "CreditBanking");

            migrationBuilder.DropColumn(
                name: "ExpireDate",
                schema: "MST",
                table: "BOConfiguration");

            migrationBuilder.DropColumn(
                name: "MortgageRate",
                schema: "MST",
                table: "BOConfiguration");

            migrationBuilder.DropColumn(
                name: "NewMortgageRate",
                schema: "MST",
                table: "BOConfiguration");

            migrationBuilder.DropColumn(
                name: "NewTransferFeeRate",
                schema: "MST",
                table: "BOConfiguration");
        }
    }
}
