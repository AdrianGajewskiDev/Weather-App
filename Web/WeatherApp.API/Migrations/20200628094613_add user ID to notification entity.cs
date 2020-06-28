using Microsoft.EntityFrameworkCore.Migrations;

namespace WeatherApp.API.Migrations
{
    public partial class adduserIDtonotificationentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Notifications",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Notifications");
        }
    }
}
