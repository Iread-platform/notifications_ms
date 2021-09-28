using Microsoft.EntityFrameworkCore.Migrations;

namespace iread_notifications_ms.Migrations
{
    public partial class TopicUsersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TopicUsers",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(767)", nullable: false),
                    TopicId = table.Column<int>(type: "int", nullable: false),
                    DevicesToken = table.Column<string>(type: "varchar(767)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicUsers", x => new { x.UserId, x.TopicId });
                    table.ForeignKey(
                        name: "FK_TopicUsers_Devices_DevicesToken",
                        column: x => x.DevicesToken,
                        principalTable: "Devices",
                        principalColumn: "Token",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TopicUsers_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TopicUsers_DevicesToken",
                table: "TopicUsers",
                column: "DevicesToken");

            migrationBuilder.CreateIndex(
                name: "IX_TopicUsers_TopicId",
                table: "TopicUsers",
                column: "TopicId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TopicUsers");
        }
    }
}
