using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyTvSeries.Domain.Migrations.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    ShortName = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastChangedBy = table.Column<long>(nullable: true),
                    LastChangedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MovieDbId = table.Column<long>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastChangedBy = table.Column<long>(nullable: true),
                    LastChangedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Keywords",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsImportEnabled = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastChangedBy = table.Column<long>(nullable: true),
                    LastChangedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keywords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TvDbId = table.Column<long>(nullable: true),
                    MovieDbId = table.Column<long>(nullable: true),
                    ImdbId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    Biography = table.Column<string>(nullable: true),
                    Birthday = table.Column<DateTime>(nullable: true),
                    Deathday = table.Column<DateTime>(nullable: true),
                    PlaceOfBirth = table.Column<string>(nullable: true),
                    IsImportEnabled = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastChangedBy = table.Column<long>(nullable: true),
                    LastChangedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Series",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TvDbId = table.Column<long>(nullable: true),
                    MovieDbId = table.Column<long>(nullable: true),
                    ImdbId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    OriginalName = table.Column<string>(nullable: true),
                    Overview = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    AiredFrom = table.Column<DateTime>(nullable: true),
                    AiredTo = table.Column<DateTime>(nullable: true),
                    AirDayOfWeek = table.Column<int>(nullable: false),
                    AirTime = table.Column<TimeSpan>(nullable: false),
                    NumberOfSeasons = table.Column<int>(nullable: false),
                    NumberOfEpisodes = table.Column<int>(nullable: false),
                    TotalRuntime = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    UserRating = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    IsImportEnabled = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastChangedBy = table.Column<long>(nullable: true),
                    LastChangedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Series", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Networks",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TvDbId = table.Column<long>(nullable: true),
                    CountryId = table.Column<long>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    IsImportEnabled = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastChangedBy = table.Column<long>(nullable: true),
                    LastChangedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Networks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Networks_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Studios",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TvDbId = table.Column<long>(nullable: true),
                    CountryId = table.Column<long>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsImportEnabled = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastChangedBy = table.Column<long>(nullable: true),
                    LastChangedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Studios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Studios_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PersonId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    IsImportEnabled = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastChangedBy = table.Column<long>(nullable: true),
                    LastChangedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Characters_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Crews",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PersonId = table.Column<long>(nullable: false),
                    SeriesId = table.Column<long>(nullable: false),
                    Department = table.Column<string>(nullable: true),
                    Job = table.Column<string>(nullable: true),
                    IsImportEnabled = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastChangedBy = table.Column<long>(nullable: true),
                    LastChangedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Crews_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Crews_Series_SeriesId",
                        column: x => x.SeriesId,
                        principalTable: "Series",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EpisodesRuntimes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SeriesId = table.Column<long>(nullable: false),
                    RuntimeInMinutes = table.Column<long>(nullable: false),
                    CreatedBy = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastChangedBy = table.Column<long>(nullable: true),
                    LastChangedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EpisodesRuntimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EpisodesRuntimes_Series_SeriesId",
                        column: x => x.SeriesId,
                        principalTable: "Series",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Seasons",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MovieDbId = table.Column<long>(nullable: true),
                    SeriesId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Overview = table.Column<string>(nullable: true),
                    SeasonNumber = table.Column<long>(nullable: true),
                    AiredFrom = table.Column<DateTime>(nullable: true),
                    AiredTo = table.Column<DateTime>(nullable: true),
                    NumberOfEpisodes = table.Column<long>(nullable: true),
                    IsImportEnabled = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastChangedBy = table.Column<long>(nullable: true),
                    LastChangedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seasons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seasons_Series_SeriesId",
                        column: x => x.SeriesId,
                        principalTable: "Series",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeriesCountries",
                columns: table => new
                {
                    SeriesId = table.Column<long>(nullable: false),
                    CountryId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeriesCountries", x => new { x.SeriesId, x.CountryId });
                    table.ForeignKey(
                        name: "FK_SeriesCountries_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeriesCountries_Series_SeriesId",
                        column: x => x.SeriesId,
                        principalTable: "Series",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeriesGenres",
                columns: table => new
                {
                    SeriesId = table.Column<long>(nullable: false),
                    GenreId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeriesGenres", x => new { x.SeriesId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_SeriesGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeriesGenres_Series_SeriesId",
                        column: x => x.SeriesId,
                        principalTable: "Series",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeriesKeywords",
                columns: table => new
                {
                    SeriesId = table.Column<long>(nullable: false),
                    KeywordId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeriesKeywords", x => new { x.SeriesId, x.KeywordId });
                    table.ForeignKey(
                        name: "FK_SeriesKeywords_Keywords_KeywordId",
                        column: x => x.KeywordId,
                        principalTable: "Keywords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeriesKeywords_Series_SeriesId",
                        column: x => x.SeriesId,
                        principalTable: "Series",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeriesNetworks",
                columns: table => new
                {
                    SeriesId = table.Column<long>(nullable: false),
                    NetworkId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeriesNetworks", x => new { x.SeriesId, x.NetworkId });
                    table.ForeignKey(
                        name: "FK_SeriesNetworks_Networks_NetworkId",
                        column: x => x.NetworkId,
                        principalTable: "Networks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeriesNetworks_Series_SeriesId",
                        column: x => x.SeriesId,
                        principalTable: "Series",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeriesStudios",
                columns: table => new
                {
                    SeriesId = table.Column<long>(nullable: false),
                    StudioId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeriesStudios", x => new { x.SeriesId, x.StudioId });
                    table.ForeignKey(
                        name: "FK_SeriesStudios_Series_SeriesId",
                        column: x => x.SeriesId,
                        principalTable: "Series",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeriesStudios_Studios_StudioId",
                        column: x => x.StudioId,
                        principalTable: "Studios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeriesCharacters",
                columns: table => new
                {
                    SeriesId = table.Column<long>(nullable: false),
                    CharacterId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeriesCharacters", x => new { x.SeriesId, x.CharacterId });
                    table.ForeignKey(
                        name: "FK_SeriesCharacters_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeriesCharacters_Series_SeriesId",
                        column: x => x.SeriesId,
                        principalTable: "Series",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Episodes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MovieDbId = table.Column<long>(nullable: true),
                    TvDbId = table.Column<long>(nullable: true),
                    SeasonId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Overview = table.Column<string>(nullable: true),
                    Aired = table.Column<DateTime>(nullable: true),
                    SeasonEpisodeNumber = table.Column<long>(nullable: true),
                    AbsoluteEpisodeNumber = table.Column<long>(nullable: true),
                    SeasonNumber = table.Column<long>(nullable: true),
                    IsImportEnabled = table.Column<bool>(nullable: false),
                    CreatedBy = table.Column<long>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    LastChangedBy = table.Column<long>(nullable: true),
                    LastChangedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Episodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Episodes_Seasons_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Seasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Characters_PersonId",
                table: "Characters",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Crews_PersonId",
                table: "Crews",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Crews_SeriesId",
                table: "Crews",
                column: "SeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_Episodes_SeasonId",
                table: "Episodes",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_EpisodesRuntimes_SeriesId",
                table: "EpisodesRuntimes",
                column: "SeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_Networks_CountryId",
                table: "Networks",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Seasons_SeriesId",
                table: "Seasons",
                column: "SeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_SeriesCharacters_CharacterId",
                table: "SeriesCharacters",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_SeriesCountries_CountryId",
                table: "SeriesCountries",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_SeriesGenres_GenreId",
                table: "SeriesGenres",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_SeriesKeywords_KeywordId",
                table: "SeriesKeywords",
                column: "KeywordId");

            migrationBuilder.CreateIndex(
                name: "IX_SeriesNetworks_NetworkId",
                table: "SeriesNetworks",
                column: "NetworkId");

            migrationBuilder.CreateIndex(
                name: "IX_SeriesStudios_StudioId",
                table: "SeriesStudios",
                column: "StudioId");

            migrationBuilder.CreateIndex(
                name: "IX_Studios_CountryId",
                table: "Studios",
                column: "CountryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Crews");

            migrationBuilder.DropTable(
                name: "Episodes");

            migrationBuilder.DropTable(
                name: "EpisodesRuntimes");

            migrationBuilder.DropTable(
                name: "SeriesCharacters");

            migrationBuilder.DropTable(
                name: "SeriesCountries");

            migrationBuilder.DropTable(
                name: "SeriesGenres");

            migrationBuilder.DropTable(
                name: "SeriesKeywords");

            migrationBuilder.DropTable(
                name: "SeriesNetworks");

            migrationBuilder.DropTable(
                name: "SeriesStudios");

            migrationBuilder.DropTable(
                name: "Seasons");

            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Keywords");

            migrationBuilder.DropTable(
                name: "Networks");

            migrationBuilder.DropTable(
                name: "Studios");

            migrationBuilder.DropTable(
                name: "Series");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
