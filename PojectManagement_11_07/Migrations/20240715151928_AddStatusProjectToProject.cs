using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectManagement_11_07.Migrations
{
    public partial class AddStatusProjectToProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StatusProject",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Đang thực hiện");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusProject",
                table: "Projects");
        }
    }
}
