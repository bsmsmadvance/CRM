using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class ChangeTagtoPRJ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UnitAuthorTag",
                schema: "MST");

            migrationBuilder.DropTable(
                name: "UnitTag",
                schema: "MST");

            migrationBuilder.CreateTable(
                name: "UnitOtherUnitInfoTag",
                schema: "PRJ",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitOtherUnitInfoTag", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "OtherUnitInfoTag",
                schema: "PRJ",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    TagID = table.Column<Guid>(nullable: false),
                    UnitID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtherUnitInfoTag", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OtherUnitInfoTag_UnitOtherUnitInfoTag_TagID",
                        column: x => x.TagID,
                        principalSchema: "PRJ",
                        principalTable: "UnitOtherUnitInfoTag",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OtherUnitInfoTag_Unit_UnitID",
                        column: x => x.UnitID,
                        principalSchema: "PRJ",
                        principalTable: "Unit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OtherUnitInfoTag_TagID",
                schema: "PRJ",
                table: "OtherUnitInfoTag",
                column: "TagID");

            migrationBuilder.CreateIndex(
                name: "IX_OtherUnitInfoTag_UnitID",
                schema: "PRJ",
                table: "OtherUnitInfoTag",
                column: "UnitID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OtherUnitInfoTag",
                schema: "PRJ");

            migrationBuilder.DropTable(
                name: "UnitOtherUnitInfoTag",
                schema: "PRJ");

            migrationBuilder.CreateTable(
                name: "UnitTag",
                schema: "MST",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitTag", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UnitAuthorTag",
                schema: "MST",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    TagID = table.Column<Guid>(nullable: false),
                    UnitID = table.Column<Guid>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitAuthorTag", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UnitAuthorTag_UnitTag_TagID",
                        column: x => x.TagID,
                        principalSchema: "MST",
                        principalTable: "UnitTag",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UnitAuthorTag_Unit_UnitID",
                        column: x => x.UnitID,
                        principalSchema: "PRJ",
                        principalTable: "Unit",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UnitAuthorTag_TagID",
                schema: "MST",
                table: "UnitAuthorTag",
                column: "TagID");

            migrationBuilder.CreateIndex(
                name: "IX_UnitAuthorTag_UnitID",
                schema: "MST",
                table: "UnitAuthorTag",
                column: "UnitID");
        }
    }
}
