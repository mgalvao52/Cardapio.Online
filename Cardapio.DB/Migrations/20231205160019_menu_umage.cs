using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cardapio.DB.Migrations
{
    public partial class menu_umage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UrlImage",
                table: "MenuItem",
                newName: "Image");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "MenuItem",
                newName: "UrlImage");
        }
    }
}
