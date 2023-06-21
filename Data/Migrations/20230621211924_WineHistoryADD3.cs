using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgustinDonalisioProyectoPNT1.Data.Migrations
{
    public partial class WineHistoryADD3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Wine",
                table: "Wine");

            migrationBuilder.RenameTable(
                name: "Wine",
                newName: "Wines");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wines",
                table: "Wines",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Wines",
                table: "Wines");

            migrationBuilder.RenameTable(
                name: "Wines",
                newName: "Wine");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wine",
                table: "Wine",
                column: "Id");
        }
    }
}
