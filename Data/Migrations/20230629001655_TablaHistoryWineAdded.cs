using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgustinDonalisioProyectoPNT1.Data.Migrations
{
    public partial class TablaHistoryWineAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HistoryWines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CellarName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WineName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WineBrand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WineType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WineYear = table.Column<int>(type: "int", nullable: false),
                    Consumed = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryWines", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoryWines");
        }
    }
}
