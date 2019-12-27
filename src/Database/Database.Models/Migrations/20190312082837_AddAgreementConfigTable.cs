using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddAgreementConfigTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Permit",
                schema: "PRJ");

            migrationBuilder.CreateTable(
                name: "AgreementConfig",
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
                    table.PrimaryKey("PK_AgreementConfig", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AgreementConfig_Project_ProjectID",
                        column: x => x.ProjectID,
                        principalSchema: "PRJ",
                        principalTable: "Project",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgreementConfig_ProjectID",
                schema: "PRJ",
                table: "AgreementConfig",
                column: "ProjectID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgreementConfig",
                schema: "PRJ");

            migrationBuilder.CreateTable(
                name: "Permit",
                schema: "PRJ",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    ExpireDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PermitDate = table.Column<DateTime>(nullable: true),
                    PermitName = table.Column<string>(nullable: true),
                    PermitNo = table.Column<string>(nullable: true),
                    ProjectID = table.Column<Guid>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_Permit_ProjectID",
                schema: "PRJ",
                table: "Permit",
                column: "ProjectID");
        }
    }
}
