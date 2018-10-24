using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyTvSeries.Domain.Migrations.Migrations
{
    public partial class AddImageToSeries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "PosterContent",
                table: "Series",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PosterName",
                table: "Series",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PosterContent",
                table: "Series");

            migrationBuilder.DropColumn(
                name: "PosterName",
                table: "Series");
        }
    }
}
