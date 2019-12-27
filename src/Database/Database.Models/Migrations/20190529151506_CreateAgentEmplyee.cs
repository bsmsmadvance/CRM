using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class CreateAgentEmplyee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Agency",
                schema: "MST");

            migrationBuilder.CreateTable(
                name: "Agent",
                schema: "MST",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    NameTH = table.Column<string>(maxLength: 100, nullable: true),
                    NameEN = table.Column<string>(maxLength: 100, nullable: true),
                    Address = table.Column<string>(maxLength: 5000, nullable: true),
                    Building = table.Column<string>(maxLength: 1000, nullable: true),
                    Soi = table.Column<string>(maxLength: 1000, nullable: true),
                    Road = table.Column<string>(maxLength: 1000, nullable: true),
                    PostalCode = table.Column<string>(maxLength: 50, nullable: true),
                    ProvinceID = table.Column<Guid>(nullable: true),
                    DistrictID = table.Column<Guid>(nullable: true),
                    SubDistrictID = table.Column<Guid>(nullable: true),
                    TelNo = table.Column<string>(maxLength: 100, nullable: true),
                    FaxNo = table.Column<string>(maxLength: 100, nullable: true),
                    Website = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agent", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Agent_District_DistrictID",
                        column: x => x.DistrictID,
                        principalSchema: "MST",
                        principalTable: "District",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Agent_Province_ProvinceID",
                        column: x => x.ProvinceID,
                        principalSchema: "MST",
                        principalTable: "Province",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Agent_SubDistrict_SubDistrictID",
                        column: x => x.SubDistrictID,
                        principalSchema: "MST",
                        principalTable: "SubDistrict",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AgentEmplyee",
                schema: "MST",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 100, nullable: true),
                    LastName = table.Column<string>(maxLength: 100, nullable: true),
                    TelNo = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentEmplyee", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agent_DistrictID",
                schema: "MST",
                table: "Agent",
                column: "DistrictID");

            migrationBuilder.CreateIndex(
                name: "IX_Agent_ProvinceID",
                schema: "MST",
                table: "Agent",
                column: "ProvinceID");

            migrationBuilder.CreateIndex(
                name: "IX_Agent_SubDistrictID",
                schema: "MST",
                table: "Agent",
                column: "SubDistrictID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Agent",
                schema: "MST");

            migrationBuilder.DropTable(
                name: "AgentEmplyee",
                schema: "MST");

            migrationBuilder.CreateTable(
                name: "Agency",
                schema: "MST",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    NameEN = table.Column<string>(maxLength: 100, nullable: true),
                    NameTH = table.Column<string>(maxLength: 100, nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agency", x => x.ID);
                });
        }
    }
}
