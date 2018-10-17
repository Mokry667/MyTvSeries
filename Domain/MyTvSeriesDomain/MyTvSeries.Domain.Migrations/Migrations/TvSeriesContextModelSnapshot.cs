﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyTvSeries.Domain.Ef;

namespace MyTvSeries.Domain.Migrations.Migrations
{
    [DbContext(typeof(TvSeriesContext))]
    partial class TvSeriesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MyTvSeries.Domain.Entities.Character", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<long>("CreatedBy");

                    b.Property<bool>("IsImportEnabled");

                    b.Property<DateTime?>("LastChangedAt");

                    b.Property<long?>("LastChangedBy");

                    b.Property<string>("Name");

                    b.Property<long>("PersonId");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("MyTvSeries.Domain.Entities.Country", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<long>("CreatedBy");

                    b.Property<DateTime?>("LastChangedAt");

                    b.Property<long?>("LastChangedBy");

                    b.Property<string>("Name");

                    b.Property<string>("ShortName");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("MyTvSeries.Domain.Entities.Crew", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<long>("CreatedBy");

                    b.Property<string>("Department");

                    b.Property<bool>("IsImportEnabled");

                    b.Property<string>("Job");

                    b.Property<DateTime?>("LastChangedAt");

                    b.Property<long?>("LastChangedBy");

                    b.Property<long>("PersonId");

                    b.Property<long>("SeriesId");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.HasIndex("SeriesId");

                    b.ToTable("Crews");
                });

            modelBuilder.Entity("MyTvSeries.Domain.Entities.Episode", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("AbsoluteEpisodeNumber");

                    b.Property<DateTime?>("Aired");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<long>("CreatedBy");

                    b.Property<bool>("IsImportEnabled");

                    b.Property<DateTime?>("LastChangedAt");

                    b.Property<long?>("LastChangedBy");

                    b.Property<long?>("MovieDbId");

                    b.Property<string>("Name");

                    b.Property<string>("Overview");

                    b.Property<long?>("SeasonEpisodeNumber");

                    b.Property<long>("SeasonId");

                    b.Property<long?>("SeasonNumber");

                    b.Property<long?>("TvDbId");

                    b.HasKey("Id");

                    b.HasIndex("SeasonId");

                    b.ToTable("Episodes");
                });

            modelBuilder.Entity("MyTvSeries.Domain.Entities.EpisodeRuntime", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<long>("CreatedBy");

                    b.Property<DateTime?>("LastChangedAt");

                    b.Property<long?>("LastChangedBy");

                    b.Property<long>("RuntimeInMinutes");

                    b.Property<long>("SeriesId");

                    b.HasKey("Id");

                    b.HasIndex("SeriesId");

                    b.ToTable("EpisodesRuntimes");
                });

            modelBuilder.Entity("MyTvSeries.Domain.Entities.Genre", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<long>("CreatedBy");

                    b.Property<string>("Description");

                    b.Property<DateTime?>("LastChangedAt");

                    b.Property<long?>("LastChangedBy");

                    b.Property<long?>("MovieDbId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("MyTvSeries.Domain.Entities.Keyword", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<long>("CreatedBy");

                    b.Property<string>("Description");

                    b.Property<bool>("IsImportEnabled");

                    b.Property<DateTime?>("LastChangedAt");

                    b.Property<long?>("LastChangedBy");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Keywords");
                });

            modelBuilder.Entity("MyTvSeries.Domain.Entities.Network", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("CountryId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<long>("CreatedBy");

                    b.Property<bool>("IsImportEnabled");

                    b.Property<DateTime?>("LastChangedAt");

                    b.Property<long?>("LastChangedBy");

                    b.Property<string>("Name");

                    b.Property<long?>("TvDbId");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Networks");
                });

            modelBuilder.Entity("MyTvSeries.Domain.Entities.Person", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Biography");

                    b.Property<DateTime?>("Birthday");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<long>("CreatedBy");

                    b.Property<DateTime?>("Deathday");

                    b.Property<int>("Gender");

                    b.Property<string>("ImdbId");

                    b.Property<bool>("IsImportEnabled");

                    b.Property<DateTime?>("LastChangedAt");

                    b.Property<long?>("LastChangedBy");

                    b.Property<long?>("MovieDbId");

                    b.Property<string>("Name");

                    b.Property<string>("PlaceOfBirth");

                    b.Property<long?>("TvDbId");

                    b.HasKey("Id");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("MyTvSeries.Domain.Entities.Season", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("AiredFrom");

                    b.Property<DateTime?>("AiredTo");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<long>("CreatedBy");

                    b.Property<bool>("IsImportEnabled");

                    b.Property<DateTime?>("LastChangedAt");

                    b.Property<long?>("LastChangedBy");

                    b.Property<long?>("MovieDbId");

                    b.Property<string>("Name");

                    b.Property<long?>("NumberOfEpisodes");

                    b.Property<string>("Overview");

                    b.Property<long?>("SeasonNumber");

                    b.Property<long>("SeriesId");

                    b.HasKey("Id");

                    b.HasIndex("SeriesId");

                    b.ToTable("Seasons");
                });

            modelBuilder.Entity("MyTvSeries.Domain.Entities.Series", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AirDayOfWeek");

                    b.Property<TimeSpan>("AirTime");

                    b.Property<DateTime?>("AiredFrom");

                    b.Property<DateTime?>("AiredTo");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<long>("CreatedBy");

                    b.Property<string>("ImdbId");

                    b.Property<bool>("IsImportEnabled");

                    b.Property<DateTime?>("LastChangedAt");

                    b.Property<long?>("LastChangedBy");

                    b.Property<long?>("MovieDbId");

                    b.Property<string>("Name");

                    b.Property<int>("NumberOfEpisodes");

                    b.Property<int>("NumberOfSeasons");

                    b.Property<string>("OriginalName");

                    b.Property<string>("Overview");

                    b.Property<int>("Status");

                    b.Property<decimal>("TotalRuntime")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<long?>("TvDbId");

                    b.Property<decimal>("UserRating")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("Id");

                    b.ToTable("Series");
                });

            modelBuilder.Entity("MyTvSeries.Domain.Entities.Studio", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("CountryId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<long>("CreatedBy");

                    b.Property<string>("Description");

                    b.Property<bool>("IsImportEnabled");

                    b.Property<DateTime?>("LastChangedAt");

                    b.Property<long?>("LastChangedBy");

                    b.Property<string>("Name");

                    b.Property<long?>("TvDbId");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Studios");
                });

            modelBuilder.Entity("MyTvSeries.Domain.ManyToMany.SeriesCharacters", b =>
                {
                    b.Property<long>("SeriesId");

                    b.Property<long>("CharacterId");

                    b.HasKey("SeriesId", "CharacterId");

                    b.HasIndex("CharacterId");

                    b.ToTable("SeriesCharacters");
                });

            modelBuilder.Entity("MyTvSeries.Domain.ManyToMany.SeriesCountries", b =>
                {
                    b.Property<long>("SeriesId");

                    b.Property<long>("CountryId");

                    b.HasKey("SeriesId", "CountryId");

                    b.HasIndex("CountryId");

                    b.ToTable("SeriesCountries");
                });

            modelBuilder.Entity("MyTvSeries.Domain.ManyToMany.SeriesGenres", b =>
                {
                    b.Property<long>("SeriesId");

                    b.Property<long>("GenreId");

                    b.HasKey("SeriesId", "GenreId");

                    b.HasIndex("GenreId");

                    b.ToTable("SeriesGenres");
                });

            modelBuilder.Entity("MyTvSeries.Domain.ManyToMany.SeriesKeywords", b =>
                {
                    b.Property<long>("SeriesId");

                    b.Property<long>("KeywordId");

                    b.HasKey("SeriesId", "KeywordId");

                    b.HasIndex("KeywordId");

                    b.ToTable("SeriesKeywords");
                });

            modelBuilder.Entity("MyTvSeries.Domain.ManyToMany.SeriesNetworks", b =>
                {
                    b.Property<long>("SeriesId");

                    b.Property<long>("NetworkId");

                    b.HasKey("SeriesId", "NetworkId");

                    b.HasIndex("NetworkId");

                    b.ToTable("SeriesNetworks");
                });

            modelBuilder.Entity("MyTvSeries.Domain.ManyToMany.SeriesStudios", b =>
                {
                    b.Property<long>("SeriesId");

                    b.Property<long>("StudioId");

                    b.HasKey("SeriesId", "StudioId");

                    b.HasIndex("StudioId");

                    b.ToTable("SeriesStudios");
                });

            modelBuilder.Entity("MyTvSeries.Domain.Entities.Character", b =>
                {
                    b.HasOne("MyTvSeries.Domain.Entities.Person", "Person")
                        .WithMany("Characters")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MyTvSeries.Domain.Entities.Crew", b =>
                {
                    b.HasOne("MyTvSeries.Domain.Entities.Person", "Person")
                        .WithMany("Crews")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MyTvSeries.Domain.Entities.Series", "Series")
                        .WithMany("Crews")
                        .HasForeignKey("SeriesId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MyTvSeries.Domain.Entities.Episode", b =>
                {
                    b.HasOne("MyTvSeries.Domain.Entities.Season", "Season")
                        .WithMany("Episodes")
                        .HasForeignKey("SeasonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MyTvSeries.Domain.Entities.EpisodeRuntime", b =>
                {
                    b.HasOne("MyTvSeries.Domain.Entities.Series", "Series")
                        .WithMany("EpisodeRuntimes")
                        .HasForeignKey("SeriesId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MyTvSeries.Domain.Entities.Network", b =>
                {
                    b.HasOne("MyTvSeries.Domain.Entities.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId");
                });

            modelBuilder.Entity("MyTvSeries.Domain.Entities.Season", b =>
                {
                    b.HasOne("MyTvSeries.Domain.Entities.Series", "Series")
                        .WithMany()
                        .HasForeignKey("SeriesId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MyTvSeries.Domain.Entities.Studio", b =>
                {
                    b.HasOne("MyTvSeries.Domain.Entities.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId");
                });

            modelBuilder.Entity("MyTvSeries.Domain.ManyToMany.SeriesCharacters", b =>
                {
                    b.HasOne("MyTvSeries.Domain.Entities.Character", "Character")
                        .WithMany("SeriesCharacters")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MyTvSeries.Domain.Entities.Series", "Series")
                        .WithMany("SeriesCharacters")
                        .HasForeignKey("SeriesId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MyTvSeries.Domain.ManyToMany.SeriesCountries", b =>
                {
                    b.HasOne("MyTvSeries.Domain.Entities.Country", "Country")
                        .WithMany("SeriesCountries")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MyTvSeries.Domain.Entities.Series", "Series")
                        .WithMany("SeriesCountries")
                        .HasForeignKey("SeriesId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MyTvSeries.Domain.ManyToMany.SeriesGenres", b =>
                {
                    b.HasOne("MyTvSeries.Domain.Entities.Genre", "Genre")
                        .WithMany("SeriesGenres")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MyTvSeries.Domain.Entities.Series", "Series")
                        .WithMany("SeriesGenres")
                        .HasForeignKey("SeriesId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MyTvSeries.Domain.ManyToMany.SeriesKeywords", b =>
                {
                    b.HasOne("MyTvSeries.Domain.Entities.Keyword", "Keyword")
                        .WithMany("SeriesKeywords")
                        .HasForeignKey("KeywordId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MyTvSeries.Domain.Entities.Series", "Series")
                        .WithMany("SeriesKeywords")
                        .HasForeignKey("SeriesId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MyTvSeries.Domain.ManyToMany.SeriesNetworks", b =>
                {
                    b.HasOne("MyTvSeries.Domain.Entities.Network", "Network")
                        .WithMany("SeriesNetworks")
                        .HasForeignKey("NetworkId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MyTvSeries.Domain.Entities.Series", "Series")
                        .WithMany("SeriesNetworks")
                        .HasForeignKey("SeriesId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MyTvSeries.Domain.ManyToMany.SeriesStudios", b =>
                {
                    b.HasOne("MyTvSeries.Domain.Entities.Series", "Series")
                        .WithMany("SeriesStudios")
                        .HasForeignKey("SeriesId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MyTvSeries.Domain.Entities.Studio", "Studio")
                        .WithMany("SeriesStudios")
                        .HasForeignKey("StudioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
