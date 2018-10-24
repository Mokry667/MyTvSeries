using Microsoft.EntityFrameworkCore.Migrations;

namespace MyTvSeries.Domain.Migrations.Migrations
{
    public partial class AddUserVotesToSeries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserVotes",
                table: "Series",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserVotes",
                table: "Series");
        }
    }
}
