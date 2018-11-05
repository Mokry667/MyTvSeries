using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyTvSeries.Domain.Migrations.Migrations
{
    public partial class Add_Poster_To_Person : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "PosterContent",
                table: "Persons",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PosterName",
                table: "Persons",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PosterContent",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "PosterName",
                table: "Persons");
        }
    }
}
