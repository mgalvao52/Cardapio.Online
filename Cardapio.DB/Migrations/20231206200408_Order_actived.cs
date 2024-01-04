using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cardapio.DB.Migrations
{
    public partial class Order_actived : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Actived",
                table: "Order",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Actived",
                table: "Order");
        }
    }
}
