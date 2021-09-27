using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace iread_notifications_ms.Migrations
{
    public partial class AddTopic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Topic",
                table: "TopicNotification");

            migrationBuilder.AddColumn<int>(
                name: "TopicId",
                table: "TopicNotification",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TopicNotification_TopicId",
                table: "TopicNotification",
                column: "TopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_TopicNotification_Topics_TopicId",
                table: "TopicNotification",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TopicNotification_Topics_TopicId",
                table: "TopicNotification");

            migrationBuilder.DropTable(
                name: "Topics");

            migrationBuilder.DropIndex(
                name: "IX_TopicNotification_TopicId",
                table: "TopicNotification");

            migrationBuilder.DropColumn(
                name: "TopicId",
                table: "TopicNotification");

            migrationBuilder.AddColumn<string>(
                name: "Topic",
                table: "TopicNotification",
                type: "text",
                nullable: true);
        }
    }
}
