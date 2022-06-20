using Microsoft.EntityFrameworkCore.Migrations;

namespace Pronia_HT.Migrations
{
    public partial class CreateAnotherSetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnotherSettings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnotherSettings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnotherSettings_Key",
                table: "AnotherSettings",
                column: "Key",
                unique: true,
                filter: "[Key] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnotherSettings");
        }
    }
}
