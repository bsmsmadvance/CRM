using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class CreateWBSAndLeadSyncJobTablesAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeadSyncJob",
                schema: "CTM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Progress = table.Column<double>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Params = table.Column<string>(nullable: true),
                    ResponseMessage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadSyncJob", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SAPWBSProSyncJob",
                schema: "PRJ",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Progress = table.Column<double>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Params = table.Column<string>(nullable: true),
                    ResponseMessage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SAPWBSProSyncJob", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeadSyncJob",
                schema: "CTM");

            migrationBuilder.DropTable(
                name: "SAPWBSProSyncJob",
                schema: "PRJ");
        }
    }
}
