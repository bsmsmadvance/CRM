using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class CreateSmsNotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SmsNotification",
                schema: "NTF",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    PhoneNumbers = table.Column<string>(maxLength: 5000, nullable: true),
                    Message = table.Column<string>(maxLength: 5000, nullable: true),
                    Status = table.Column<int>(nullable: false),
                    TemplateName = table.Column<string>(maxLength: 100, nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmsNotification", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SmsNotification",
                schema: "NTF");
        }
    }
}
