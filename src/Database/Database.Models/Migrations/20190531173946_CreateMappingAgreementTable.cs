using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class CreateMappingAgreementTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MappingAgreement",
                schema: "PRM",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(nullable: true),
                    Updated = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OldAgreement = table.Column<string>(maxLength: 100, nullable: true),
                    OldItem = table.Column<string>(maxLength: 100, nullable: true),
                    OldMaterialCode = table.Column<string>(maxLength: 100, nullable: true),
                    NewAgreement = table.Column<string>(maxLength: 100, nullable: true),
                    NewItem = table.Column<string>(maxLength: 100, nullable: true),
                    NewMaterialCode = table.Column<string>(maxLength: 100, nullable: true),
                    Remark = table.Column<string>(maxLength: 5000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MappingAgreement", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MappingAgreement",
                schema: "PRM");
        }
    }
}
