using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Photogram.Migrations
{
    public partial class addfollowerstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsersId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Photos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UsersId",
                table: "Users",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_UsersId",
                table: "Users",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_UsersId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UsersId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Photos");
        }
    }
}
