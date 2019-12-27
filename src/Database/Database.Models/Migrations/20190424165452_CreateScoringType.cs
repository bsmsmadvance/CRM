using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class CreateScoringType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeadAssign",
                schema: "CTM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LeadID = table.Column<Guid>(nullable: true),
                    OwnerID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadAssign", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LeadAssign_Lead_LeadID",
                        column: x => x.LeadID,
                        principalSchema: "CTM",
                        principalTable: "Lead",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeadAssign_User_OwnerID",
                        column: x => x.OwnerID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LeadScoringType",
                schema: "CTM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Topic = table.Column<string>(maxLength: 1000, nullable: true),
                    Score = table.Column<double>(nullable: false),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadScoringType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LeadScoring",
                schema: "CTM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LeadID = table.Column<Guid>(nullable: true),
                    LeadScoringTypeID = table.Column<Guid>(nullable: true),
                    Score = table.Column<double>(nullable: false),
                    IsGetScore = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadScoring", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LeadScoring_Lead_LeadID",
                        column: x => x.LeadID,
                        principalSchema: "CTM",
                        principalTable: "Lead",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeadScoring_LeadScoringType_LeadScoringTypeID",
                        column: x => x.LeadScoringTypeID,
                        principalSchema: "CTM",
                        principalTable: "LeadScoringType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeadAssign_LeadID",
                schema: "CTM",
                table: "LeadAssign",
                column: "LeadID");

            migrationBuilder.CreateIndex(
                name: "IX_LeadAssign_OwnerID",
                schema: "CTM",
                table: "LeadAssign",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_LeadScoring_LeadID",
                schema: "CTM",
                table: "LeadScoring",
                column: "LeadID");

            migrationBuilder.CreateIndex(
                name: "IX_LeadScoring_LeadScoringTypeID",
                schema: "CTM",
                table: "LeadScoring",
                column: "LeadScoringTypeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeadAssign",
                schema: "CTM");

            migrationBuilder.DropTable(
                name: "LeadScoring",
                schema: "CTM");

            migrationBuilder.DropTable(
                name: "LeadScoringType",
                schema: "CTM");
        }
    }
}
