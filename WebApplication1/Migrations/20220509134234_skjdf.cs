using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    public partial class skjdf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Users_Username",
                table: "Chats");

            migrationBuilder.DropIndex(
                name: "IX_Chats_Username",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Chats");

            migrationBuilder.RenameColumn(
                name: "User",
                table: "Ratings",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "Last",
                table: "Chats",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "LastDate",
                table: "Chats",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Last",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "LastDate",
                table: "Chats");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Ratings",
                newName: "User");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Chats",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_Username",
                table: "Chats",
                column: "Username");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Users_Username",
                table: "Chats",
                column: "Username",
                principalTable: "Users",
                principalColumn: "Username");
        }
    }
}
