using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class CreateOpportunityAssign : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OpportunityAssign",
                schema: "CTM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OpportunityID = table.Column<Guid>(nullable: true),
                    OwnerID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpportunityAssign", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OpportunityAssign_Opportunity_OpportunityID",
                        column: x => x.OpportunityID,
                        principalSchema: "CTM",
                        principalTable: "Opportunity",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OpportunityAssign_User_OwnerID",
                        column: x => x.OwnerID,
                        principalSchema: "USR",
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OpportunityAssign_OpportunityID",
                schema: "CTM",
                table: "OpportunityAssign",
                column: "OpportunityID");

            migrationBuilder.CreateIndex(
                name: "IX_OpportunityAssign_OwnerID",
                schema: "CTM",
                table: "OpportunityAssign",
                column: "OwnerID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OpportunityAssign",
                schema: "CTM");
        }
    }
}
