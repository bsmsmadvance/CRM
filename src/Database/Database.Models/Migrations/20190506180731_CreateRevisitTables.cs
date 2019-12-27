using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class CreateRevisitTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RevisitActivity",
                schema: "CTM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    OpportunityID = table.Column<Guid>(nullable: false),
                    RevisitActivityTypeMasterCenterID = table.Column<Guid>(nullable: true),
                    ConvenientTimeMasterCenterID = table.Column<Guid>(nullable: true),
                    ActualDate = table.Column<DateTime>(nullable: true),
                    AppointmentDate = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(maxLength: 5000, nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RevisitActivity", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RevisitActivity_MasterCenter_ConvenientTimeMasterCenterID",
                        column: x => x.ConvenientTimeMasterCenterID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RevisitActivity_Opportunity_OpportunityID",
                        column: x => x.OpportunityID,
                        principalSchema: "CTM",
                        principalTable: "Opportunity",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RevisitActivity_MasterCenter_RevisitActivityTypeMasterCenterID",
                        column: x => x.RevisitActivityTypeMasterCenterID,
                        principalSchema: "MST",
                        principalTable: "MasterCenter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RevisitActivityStatus",
                schema: "CTM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    Description = table.Column<string>(maxLength: 100, nullable: true),
                    Order = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RevisitActivityStatus", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RevisitActivityResult",
                schema: "CTM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    RevisitAcitivityID = table.Column<Guid>(nullable: true),
                    StatusID = table.Column<Guid>(nullable: true),
                    OtherReasons = table.Column<string>(maxLength: 5000, nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RevisitActivityResult", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RevisitActivityResult_RevisitActivity_RevisitAcitivityID",
                        column: x => x.RevisitAcitivityID,
                        principalSchema: "CTM",
                        principalTable: "RevisitActivity",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RevisitActivityResult_RevisitActivityStatus_StatusID",
                        column: x => x.StatusID,
                        principalSchema: "CTM",
                        principalTable: "RevisitActivityStatus",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RevisitActivity_ConvenientTimeMasterCenterID",
                schema: "CTM",
                table: "RevisitActivity",
                column: "ConvenientTimeMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_RevisitActivity_OpportunityID",
                schema: "CTM",
                table: "RevisitActivity",
                column: "OpportunityID");

            migrationBuilder.CreateIndex(
                name: "IX_RevisitActivity_RevisitActivityTypeMasterCenterID",
                schema: "CTM",
                table: "RevisitActivity",
                column: "RevisitActivityTypeMasterCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_RevisitActivityResult_RevisitAcitivityID",
                schema: "CTM",
                table: "RevisitActivityResult",
                column: "RevisitAcitivityID");

            migrationBuilder.CreateIndex(
                name: "IX_RevisitActivityResult_StatusID",
                schema: "CTM",
                table: "RevisitActivityResult",
                column: "StatusID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RevisitActivityResult",
                schema: "CTM");

            migrationBuilder.DropTable(
                name: "RevisitActivity",
                schema: "CTM");

            migrationBuilder.DropTable(
                name: "RevisitActivityStatus",
                schema: "CTM");
        }
    }
}
