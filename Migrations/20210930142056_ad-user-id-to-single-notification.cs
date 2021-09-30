using Microsoft.EntityFrameworkCore.Migrations;

namespace iread_notifications_ms.Migrations
{
    public partial class aduseridtosinglenotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SingleNotifications_Users_UserId",
                table: "SingleNotifications");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "SingleNotifications",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SingleNotifications_Users_UserId",
                table: "SingleNotifications",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SingleNotifications_Users_UserId",
                table: "SingleNotifications");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "SingleNotifications",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_SingleNotifications_Users_UserId",
                table: "SingleNotifications",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
