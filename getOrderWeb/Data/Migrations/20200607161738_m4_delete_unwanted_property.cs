using Microsoft.EntityFrameworkCore.Migrations;

namespace getOrderWeb.Data.Migrations
{
    public partial class m4_delete_unwanted_property : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureAddress",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PictureAddress",
                table: "Categories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PictureAddress",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PictureAddress",
                table: "Categories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
