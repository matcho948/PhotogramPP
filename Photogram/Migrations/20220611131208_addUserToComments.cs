using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Photogram.Migrations
{
    public partial class addUserToComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "idUser",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "idUser",
                table: "Comments");
        }
    }
}
