using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Photogram.Migrations
{
    public partial class modifymodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "MainPhotoId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "Photos",
                newName: "UserAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Photos_UsersId",
                table: "Photos",
                newName: "IX_Photos_UserAccountId");

            migrationBuilder.AlterColumn<bool>(
                name: "IsBanned",
                table: "Users",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Users_UserAccountId",
                table: "Photos",
                column: "UserAccountId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Users_UserAccountId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "UserAccountId",
                table: "Photos",
                newName: "UsersId");

            migrationBuilder.RenameIndex(
                name: "IX_Photos_UserAccountId",
                table: "Photos",
                newName: "IX_Photos_UsersId");

            migrationBuilder.AlterColumn<bool>(
                name: "IsBanned",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MainPhotoId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_MainPhotoId",
                table: "Users",
                column: "MainPhotoId");

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
    }
}
