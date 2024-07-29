using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManagement_11_07.Migrations
{
    public partial class AddStatusTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StatusTask",
                table: "Tasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Đang thực hiện");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusTask",
                table: "Tasks");
        }
    }
}
