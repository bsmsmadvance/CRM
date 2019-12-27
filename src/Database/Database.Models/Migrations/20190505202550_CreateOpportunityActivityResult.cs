using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class CreateOpportunityActivityResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OpportunityActivityTrack",
                schema: "CTM");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                schema: "CTM",
                table: "OpportunityActivityStatus",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                schema: "CTM",
                table: "OpportunityActivityStatus",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StatusType",
                schema: "CTM",
                table: "OpportunityActivityStatus",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "OpportunityActivityResult",
                schema: "CTM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    OpportunityAcitivityID = table.Column<Guid>(nullable: true),
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
                    table.PrimaryKey("PK_OpportunityActivityResult", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OpportunityActivityResult_OpportunityActivity_OpportunityAcitivityID",
                        column: x => x.OpportunityAcitivityID,
                        principalSchema: "CTM",
                        principalTable: "OpportunityActivity",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OpportunityActivityResult_OpportunityActivityStatus_StatusID",
                        column: x => x.StatusID,
                        principalSchema: "CTM",
                        principalTable: "OpportunityActivityStatus",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OpportunityActivityResult_OpportunityAcitivityID",
                schema: "CTM",
                table: "OpportunityActivityResult",
                column: "OpportunityAcitivityID");

            migrationBuilder.CreateIndex(
                name: "IX_OpportunityActivityResult_StatusID",
                schema: "CTM",
                table: "OpportunityActivityResult",
                column: "StatusID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OpportunityActivityResult",
                schema: "CTM");

            migrationBuilder.DropColumn(
                name: "Code",
                schema: "CTM",
                table: "OpportunityActivityStatus");

            migrationBuilder.DropColumn(
                name: "Order",
                schema: "CTM",
                table: "OpportunityActivityStatus");

            migrationBuilder.DropColumn(
                name: "StatusType",
                schema: "CTM",
                table: "OpportunityActivityStatus");

            migrationBuilder.CreateTable(
                name: "OpportunityActivityTrack",
                schema: "CTM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OpportunityAcitivityID = table.Column<Guid>(nullable: true),
                    OtherReasons = table.Column<string>(maxLength: 1000, nullable: true),
                    StatusID = table.Column<Guid>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true)
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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OpportunityActivityTrack_OpportunityActivityStatus_StatusID",
                        column: x => x.StatusID,
                        principalSchema: "CTM",
                        principalTable: "OpportunityActivityStatus",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

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
        }
    }
}
