using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Models.Migrations
{
    public partial class SetNullablePromotionItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "WhenReceive",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ReceiveDays",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "WhenReceive",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ReceiveDays",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "WhenReceive",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ReceiveDays",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "WhenReceive",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ReceiveDays",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "WhenReceive",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ReceiveDays",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "WhenReceive",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ReceiveDays",
                schema: "PRM",
                table: "MasterTransferPromotionItem",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "WhenReceive",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ReceiveDays",
                schema: "PRM",
                table: "MasterTransferPromotionFreeItem",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "WhenReceive",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ReceiveDays",
                schema: "PRM",
                table: "MasterPreSalePromotionItem",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "WhenReceive",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ReceiveDays",
                schema: "PRM",
                table: "MasterBookingPromotionItem",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "WhenReceive",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ReceiveDays",
                schema: "PRM",
                table: "MasterBookingPromotionFreeItem",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
