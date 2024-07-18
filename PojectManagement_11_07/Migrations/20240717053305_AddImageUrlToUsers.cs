using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManagement_11_07.Migrations
{
    public partial class AddImageUrlToUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Users",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "E:/LTWebNC_Ki3/BTL/PojectManagement_11_07/PojectManagement_11_07/wwwroot/image/default-avater.png");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Users");
        }
    }
}
