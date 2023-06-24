using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgustinDonalisioProyectoPNT1.Data.Migrations
{
    public partial class unaMigra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "Cellars");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "Cellars",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
