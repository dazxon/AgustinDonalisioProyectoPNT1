﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgustinDonalisioProyectoPNT1.Data.Migrations
{
    public partial class RefactorWineModel2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Wines",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Year",
                table: "Wines");
        }
    }
}
