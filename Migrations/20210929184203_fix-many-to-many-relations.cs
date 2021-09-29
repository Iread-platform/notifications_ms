using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace iread_notifications_ms.Migrations
{
    public partial class fixmanytomanyrelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TopicNotifications_Topics_TopicId",
                table: "TopicNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_TopicUsers_Devices_DevicesToken",
                table: "TopicUsers");

            migrationBuilder.DropTable(
                name: "DeviceNotifications");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropIndex(
                name: "IX_TopicUsers_DevicesToken",
                table: "TopicUsers");

            migrationBuilder.DropColumn(
                name: "DevicesToken",
                table: "TopicUsers");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "TopicUsers",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(767)");

            migrationBuilder.AlterColumn<int>(
                name: "TopicId",
                table: "TopicNotifications",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Token = table.Column<string>(type: "varchar(767)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "UsersNotifications",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    NotificationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersNotifications", x => new { x.UserId, x.NotificationId });
                    table.ForeignKey(
                        name: "FK_UsersNotifications_SingleNotifications_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "SingleNotifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersNotifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Token",
                table: "Users",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersNotifications_NotificationId",
                table: "UsersNotifications",
                column: "NotificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_TopicNotifications_Topics_TopicId",
                table: "TopicNotifications",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TopicUsers_Users_UserId",
                table: "TopicUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TopicNotifications_Topics_TopicId",
                table: "TopicNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_TopicUsers_Users_UserId",
                table: "TopicUsers");

            migrationBuilder.DropTable(
                name: "UsersNotifications");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TopicUsers",
                type: "varchar(767)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "DevicesToken",
                table: "TopicUsers",
                type: "varchar(767)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TopicId",
                table: "TopicNotifications",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Token = table.Column<string>(type: "varchar(767)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Token);
                });

            migrationBuilder.CreateTable(
                name: "DeviceNotifications",
                columns: table => new
                {
                    DeviceToken = table.Column<string>(type: "varchar(767)", nullable: false),
                    NotificationId = table.Column<int>(type: "int", nullable: false),
                    NotificationsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceNotifications", x => new { x.DeviceToken, x.NotificationId });
                    table.ForeignKey(
                        name: "FK_DeviceNotifications_Devices_DeviceToken",
                        column: x => x.DeviceToken,
                        principalTable: "Devices",
                        principalColumn: "Token",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceNotifications_SingleNotifications_NotificationsId",
                        column: x => x.NotificationsId,
                        principalTable: "SingleNotifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TopicUsers_DevicesToken",
                table: "TopicUsers",
                column: "DevicesToken");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceNotifications_NotificationsId",
                table: "DeviceNotifications",
                column: "NotificationsId");

            migrationBuilder.AddForeignKey(
                name: "FK_TopicNotifications_Topics_TopicId",
                table: "TopicNotifications",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TopicUsers_Devices_DevicesToken",
                table: "TopicUsers",
                column: "DevicesToken",
                principalTable: "Devices",
                principalColumn: "Token",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
