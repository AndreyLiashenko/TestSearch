using Microsoft.EntityFrameworkCore.Migrations;

namespace TestSearch.Migrations
{
    public partial class Add_TestString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TestString",
                table: "Posts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestString",
                table: "Posts");
        }
    }
}
