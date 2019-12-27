using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class AddAgentToAgentEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AgentID",
                schema: "MST",
                table: "AgentEmployee",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AgentEmployee_AgentID",
                schema: "MST",
                table: "AgentEmployee",
                column: "AgentID");

            migrationBuilder.AddForeignKey(
                name: "FK_AgentEmployee_Agent_AgentID",
                schema: "MST",
                table: "AgentEmployee",
                column: "AgentID",
                principalSchema: "MST",
                principalTable: "Agent",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AgentEmployee_Agent_AgentID",
                schema: "MST",
                table: "AgentEmployee");

            migrationBuilder.DropIndex(
                name: "IX_AgentEmployee_AgentID",
                schema: "MST",
                table: "AgentEmployee");

            migrationBuilder.DropColumn(
                name: "AgentID",
                schema: "MST",
                table: "AgentEmployee");
        }
    }
}
