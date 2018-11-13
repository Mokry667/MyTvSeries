using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyTvSeries.Domain.Migrations.Migrations
{
    public partial class Add_Series_Reviews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SeriesReviews",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SeriesId = table.Column<long>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Likes = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastChangedBy = table.Column<string>(nullable: true),
                    LastChangedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeriesReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeriesReviews_Series_SeriesId",
                        column: x => x.SeriesId,
                        principalTable: "Series",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SeriesReviews_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SeriesReviews_SeriesId",
                table: "SeriesReviews",
                column: "SeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_SeriesReviews_UserId",
                table: "SeriesReviews",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SeriesReviews");
        }
    }
}
