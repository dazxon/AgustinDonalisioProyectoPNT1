using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgustinDonalisioProyectoPNT1.Data.Migrations
{
    public partial class editedModelCellarWine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdUser",
                table: "CellarWines");

            migrationBuilder.AddColumn<int>(
                name: "IdCellar",
                table: "CellarWines",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdCellar",
                table: "CellarWines");

            migrationBuilder.AddColumn<string>(
                name: "IdUser",
                table: "CellarWines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
