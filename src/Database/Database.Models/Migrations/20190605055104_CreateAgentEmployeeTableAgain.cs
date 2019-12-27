using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class CreateAgentEmployeeTableAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AgentEmployeeID",
                schema: "SAL",
                table: "Booking",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AgentEmployee",
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
                    table.PrimaryKey("PK_AgentEmployee", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Booking_AgentEmployeeID",
                schema: "SAL",
                table: "Booking",
                column: "AgentEmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_AgentEmployee_AgentEmployeeID",
                schema: "SAL",
                table: "Booking",
                column: "AgentEmployeeID",
                principalSchema: "MST",
                principalTable: "AgentEmployee",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_AgentEmployee_AgentEmployeeID",
                schema: "SAL",
                table: "Booking");

            migrationBuilder.DropTable(
                name: "AgentEmployee",
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
    }
}
