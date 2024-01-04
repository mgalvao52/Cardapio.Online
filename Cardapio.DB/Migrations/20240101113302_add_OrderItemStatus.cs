using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cardapio.DB.Migrations
{
    public partial class add_OrderItemStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "OrderItem",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "OrderItem");
        }
    }
}
