using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class DropAgentEmployeeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_AgentEmplyee_AgentEmployeeID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropTable(
                name: "AgentEmplyee",
                schema: "MST");

            migrationBuilder.DropIndex(
                name: "IX_Booking_AgentEmployeeID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "AgentEmployeeID",
                schema: "SAL",
                table: "Booking");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AgentEmployeeID",
                schema: "SAL",
                table: "Booking",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AgentEmplyee",
                schema: "MST",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    FirstName = table.Column<string>(maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: true),
                    TelNo = table.Column<string>(maxLength: 100, nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentEmplyee", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Booking_AgentEmployeeID",
                schema: "SAL",
                table: "Booking",
                column: "AgentEmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_AgentEmplyee_AgentEmployeeID",
                schema: "SAL",
                table: "Booking",
                column: "AgentEmployeeID",
                principalSchema: "MST",
                principalTable: "AgentEmplyee",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
