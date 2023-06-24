using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgustinDonalisioProyectoPNT1.Data.Migrations
{
    public partial class addddd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
             name: "Cellar");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "Cellars");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Cellars",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");
        }
    }
}
