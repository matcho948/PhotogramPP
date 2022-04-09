using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Photogram.Migrations
{
    public partial class updateUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MainPhotoId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsersId",
                table: "Photos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_MainPhotoId",
                table: "Users",
                column: "MainPhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_UsersId",
                table: "Photos",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Users_UsersId",
                table: "Photos",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Photos_MainPhotoId",
                table: "Users",
                column: "MainPhotoId",
                principalTable: "Photos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Users_UsersId",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Photos_MainPhotoId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_MainPhotoId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Photos_UsersId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "MainPhotoId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "Photos");
        }
    }
}
