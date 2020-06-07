using Microsoft.EntityFrameworkCore.Migrations;

namespace getOrderWeb.Data.Migrations
{
    public partial class m2_modify_order_model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountBuy",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UnitPriceBuy",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "Family",
                table: "Customers");

            migrationBuilder.AddColumn<int>(
                name: "AmountSell",
                table: "Orders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ShopOwnerId",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitPriceSell",
                table: "OrderDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShopOwnerId",
                table: "Orders",
                column: "ShopOwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_ShopOwnerId",
                table: "Orders",
                column: "ShopOwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_ShopOwnerId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ShopOwnerId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "AmountSell",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShopOwnerId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UnitPriceSell",
                table: "OrderDetails");

            migrationBuilder.AddColumn<int>(
                name: "AmountBuy",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UnitPriceBuy",
                table: "OrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Family",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
