using Microsoft.EntityFrameworkCore.Migrations;

namespace iread_notifications_ms.Migrations
{
    public partial class AddTopic1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TopicNotification_Notifications_Id",
                table: "TopicNotification");

            migrationBuilder.DropForeignKey(
                name: "FK_TopicNotification_Topics_TopicId",
                table: "TopicNotification");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TopicNotification",
                table: "TopicNotification");

            migrationBuilder.RenameTable(
                name: "TopicNotification",
                newName: "TopicNotifications");

            migrationBuilder.RenameIndex(
                name: "IX_TopicNotification_TopicId",
                table: "TopicNotifications",
                newName: "IX_TopicNotifications_TopicId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TopicNotifications",
                table: "TopicNotifications",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TopicNotifications_Notifications_Id",
                table: "TopicNotifications",
                column: "Id",
                principalTable: "Notifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TopicNotifications_Topics_TopicId",
                table: "TopicNotifications",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TopicNotifications_Notifications_Id",
                table: "TopicNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_TopicNotifications_Topics_TopicId",
                table: "TopicNotifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TopicNotifications",
                table: "TopicNotifications");

            migrationBuilder.RenameTable(
                name: "TopicNotifications",
                newName: "TopicNotification");

            migrationBuilder.RenameIndex(
                name: "IX_TopicNotifications_TopicId",
                table: "TopicNotification",
                newName: "IX_TopicNotification_TopicId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TopicNotification",
                table: "TopicNotification",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TopicNotification_Notifications_Id",
                table: "TopicNotification",
                column: "Id",
                principalTable: "Notifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TopicNotification_Topics_TopicId",
                table: "TopicNotification",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
