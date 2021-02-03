using Microsoft.EntityFrameworkCore.Migrations;

namespace PrevozPutnikaAPI.Migrations
{
    public partial class v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "zauzeto",
                table: "Sediste",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "zauzeto",
                table: "Sediste");
        }
    }
}
