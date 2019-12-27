using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class UpdateMasterProjectTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Budget",
                schema: "PRJ");

            migrationBuilder.DropTable(
                name: "License",
                schema: "PRJ");

            migrationBuilder.AlterColumn<int>(
                name: "TypeOfProject",
                schema: "PRJ",
                table: "Project",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "BOConfiguration",
                schema: "MST",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Vat = table.Column<double>(nullable: false),
                    BOIAmount = table.Column<double>(nullable: false),
                    IncomeTaxPercent = table.Column<double>(nullable: false),
                    BusinessTaxPercent = table.Column<double>(nullable: false),
                    LocalTaxPercent = table.Column<double>(nullable: false),
                    UnitTransferFee = table.Column<double>(nullable: false),
                    AdjustAccount = table.Column<double>(nullable: false),
                    TaxAccount = table.Column<double>(nullable: false),
                    DepreciationYear = table.Column<double>(nullable: false),
                    VoidRefund = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BOConfiguration", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PriceItemKey",
                schema: "MST",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Key = table.Column<string>(nullable: true),
                    PriceType = table.Column<int>(nullable: false),
                    Tax = table.Column<double>(nullable: false),
                    ACCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceItemKey", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BudgetMinPrice",
                schema: "PRJ",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ProjectID = table.Column<Guid>(nullable: false),
                    BudgetType = table.Column<string>(nullable: true),
                    Year = table.Column<string>(nullable: true),
                    Quarter = table.Column<string>(nullable: true),
                    TotalAmount = table.Column<decimal>(type: "Money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetMinPrice", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BudgetMinPrice_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "PRJ",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MinPrice",
                schema: "PRJ",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ProjectID = table.Column<Guid>(nullable: true),
                    UnitNo = table.Column<string>(nullable: true),
                    Cost = table.Column<decimal>(type: "Money", nullable: false),
                    CostType = table.Column<string>(nullable: true),
                    ROIMinprice = table.Column<decimal>(type: "Money", nullable: false),
                    ApprovedMinPrice = table.Column<decimal>(type: "Money", nullable: false),
                    DocType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MinPrice", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MinPrice_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "PRJ",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PriceListItemTemplate",
                schema: "PRJ",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    TypeOfProject = table.Column<int>(nullable: false),
                    PriceListID = table.Column<Guid>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    Key = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PriceType = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceListItemTemplate", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PriceListItemTemplate_PriceList_PriceListID",
                        column: x => x.PriceListID,
                        principalSchema: "PRJ",
                        principalTable: "PriceList",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WaterElectricMeterPrice",
                schema: "PRJ",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModelID = table.Column<Guid>(nullable: false),
                    ProjectID = table.Column<Guid>(nullable: true),
                    WaterMeterPrice = table.Column<decimal>(type: "Money", nullable: false),
                    ElectricMeterPrice = table.Column<decimal>(type: "Money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WaterElectricMeterPrice", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WaterElectricMeterPrice_Model_ModelID",
                        column: x => x.ModelID,
                        principalSchema: "PRJ",
                        principalTable: "Model",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WaterElectricMeterPrice_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "PRJ",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BudgetMinPrice_ProjectID",
                schema: "PRJ",
                table: "BudgetMinPrice",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_MinPrice_ProjectID",
                schema: "PRJ",
                table: "MinPrice",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_PriceListItemTemplate_PriceListID",
                schema: "PRJ",
                table: "PriceListItemTemplate",
                column: "PriceListID");

            migrationBuilder.CreateIndex(
                name: "IX_WaterElectricMeterPrice_ModelID",
                schema: "PRJ",
                table: "WaterElectricMeterPrice",
                column: "ModelID");

            migrationBuilder.CreateIndex(
                name: "IX_WaterElectricMeterPrice_ProjectID",
                schema: "PRJ",
                table: "WaterElectricMeterPrice",
                column: "ProjectID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BOConfiguration",
                schema: "MST");

            migrationBuilder.DropTable(
                name: "PriceItemKey",
                schema: "MST");

            migrationBuilder.DropTable(
                name: "BudgetMinPrice",
                schema: "PRJ");

            migrationBuilder.DropTable(
                name: "MinPrice",
                schema: "PRJ");

            migrationBuilder.DropTable(
                name: "PriceListItemTemplate",
                schema: "PRJ");

            migrationBuilder.DropTable(
                name: "WaterElectricMeterPrice",
                schema: "PRJ");

            migrationBuilder.AlterColumn<string>(
                name: "TypeOfProject",
                schema: "PRJ",
                table: "Project",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateTable(
                name: "Budget",
                schema: "PRJ",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    AcceptMinPrice = table.Column<decimal>(type: "Money", nullable: false),
                    ChanoteArea = table.Column<double>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    DocType = table.Column<string>(nullable: true),
                    HouseNo = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastedCost = table.Column<decimal>(type: "Money", nullable: false),
                    ProjectID = table.Column<Guid>(nullable: false),
                    RoiMinPrice = table.Column<decimal>(type: "Money", nullable: false),
                    SaleArea = table.Column<double>(nullable: false),
                    SalePrice = table.Column<decimal>(type: "Money", nullable: false),
                    UnitNo = table.Column<string>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true)
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
                name: "License",
                schema: "PRJ",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    APCentralMonth = table.Column<int>(nullable: false),
                    APCentralValue = table.Column<decimal>(type: "Money", nullable: false),
                    AttorneyFreePosition = table.Column<string>(nullable: true),
                    AttorneyNameEN1 = table.Column<string>(nullable: true),
                    AttorneyNameEN2 = table.Column<string>(nullable: true),
                    AttorneyNameFree = table.Column<string>(nullable: true),
                    AttorneyNameTH1 = table.Column<string>(nullable: true),
                    AttorneyNameTH2 = table.Column<string>(nullable: true),
                    BuildingInsurance = table.Column<decimal>(type: "Money", nullable: false),
                    CentralMonth = table.Column<int>(nullable: false),
                    CentralValue = table.Column<decimal>(type: "Money", nullable: false),
                    ChangeNameFee = table.Column<decimal>(type: "Money", nullable: false),
                    CondoFundRate = table.Column<decimal>(type: "Money", nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    DelayTransfer = table.Column<decimal>(nullable: false),
                    EIAApproved = table.Column<bool>(nullable: false),
                    EIAApprovedDate = table.Column<DateTime>(nullable: true),
                    EndPublicDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LegalNameEN = table.Column<string>(nullable: true),
                    LegalNameTH = table.Column<string>(nullable: true),
                    OwnerShipDate = table.Column<DateTime>(nullable: true),
                    ParkingSpace = table.Column<int>(nullable: false),
                    PowerAttorneyDate = table.Column<DateTime>(nullable: true),
                    ProjectID = table.Column<Guid>(nullable: false),
                    RoomTransferFee = table.Column<decimal>(type: "Money", nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    VisitFine = table.Column<decimal>(type: "Money", nullable: false),
                    VisitFineDay = table.Column<int>(nullable: false),
                    WitnessEN1 = table.Column<string>(nullable: true),
                    WitnessEN2 = table.Column<string>(nullable: true),
                    WitnessTH1 = table.Column<string>(nullable: true),
                    WitnessTH2 = table.Column<string>(nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_Budget_ProjectID",
                schema: "PRJ",
                table: "Budget",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_License_ProjectID",
                schema: "PRJ",
                table: "License",
                column: "ProjectID");
        }
    }
}
