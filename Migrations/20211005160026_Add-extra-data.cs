using Microsoft.EntityFrameworkCore.Migrations;

namespace iread_notifications_ms.Migrations
{
    public partial class Addextradata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExtraData",
                table: "TopicNotifications",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtraData",
                table: "SingleNotifications",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExtraData",
                table: "TopicNotifications");

            migrationBuilder.DropColumn(
                name: "ExtraData",
                table: "SingleNotifications");
        }
    }
}
