using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace iread_notifications_ms.Migrations
{
    public partial class removenotificationclass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SingleNotifications_Notifications_Id",
                table: "SingleNotifications");

            migrationBuilder.DropForeignKey(
                name: "FK_TopicNotifications_Notifications_Id",
                table: "TopicNotifications");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.AddColumn<string>(
                name: "Body",
                table: "TopicNotifications",
                type: "text",
                nullable: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "TopicNotifications",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "SendAfter",
                table: "TopicNotifications",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "TopicNotifications",
                type: "text",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "Body",
                table: "SingleNotifications",
                type: "text",
                nullable: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "SingleNotifications",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "SendAfter",
                table: "SingleNotifications",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "SingleNotifications",
                type: "text",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Body",
                table: "TopicNotifications");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "TopicNotifications");

            migrationBuilder.DropColumn(
                name: "SendAfter",
                table: "TopicNotifications");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "TopicNotifications");

            migrationBuilder.DropColumn(
                name: "Body",
                table: "SingleNotifications");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "SingleNotifications");

            migrationBuilder.DropColumn(
                name: "SendAfter",
                table: "SingleNotifications");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "SingleNotifications");

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Body = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    SendAfter = table.Column<TimeSpan>(type: "time", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_SingleNotifications_Notifications_Id",
                table: "SingleNotifications",
                column: "Id",
                principalTable: "Notifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TopicNotifications_Notifications_Id",
                table: "TopicNotifications",
                column: "Id",
                principalTable: "Notifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
