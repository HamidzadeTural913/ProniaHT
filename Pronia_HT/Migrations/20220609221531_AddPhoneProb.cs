using Microsoft.EntityFrameworkCore.Migrations;

namespace Pronia_HT.Migrations
{
    public partial class AddPhoneProb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Settings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Settings");
        }
    }
}
