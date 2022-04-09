using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Photogram.Migrations
{
    public partial class modificationphotos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMainPhoto",
                table: "Photos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMainPhoto",
                table: "Photos");
        }
    }
}
