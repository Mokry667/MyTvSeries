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
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("MyTvSeries.Domain.Entities.Character", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("CreatedBy");

                    b.Property<bool>("IsImportEnabled");

                    b.Property<DateTime?>("LastChangedAt");

                    b.Property<string>("LastChangedBy");

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

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime?>("LastChangedAt");

                    b.Property<string>("LastChangedBy");

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

                    b.Property<string>("CreatedBy");

                    b.Property<string>("Department");

                    b.Property<bool>("IsImportEnabled");

                    b.Property<string>("Job");

                    b.Property<DateTime?>("LastChangedAt");

                    b.Property<string>("LastChangedBy");

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

                    b.Property<string>("CreatedBy");

                    b.Property<bool>("IsImportEnabled");

                    b.Property<DateTime?>("LastChangedAt");

                    b.Property<string>("LastChangedBy");

                    b.Property<long?>("MovieDbId");

                    b.Property<string>("Name");

                    b.Property<string>("Overview");

                    b.Property<long?>("SeasonEpisodeNumber");

                    b.Property<long>("SeasonId");

                    b.Property<long?>("SeasonNumber");

                    b.Property<long?>("TvDbId");

                    b.Property<decimal>("UserRating")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("UserVotes");

                    b.HasKey("Id");

                    b.HasIndex("SeasonId");

                    b.ToTable("Episodes");
                });

            modelBuilder.Entity("MyTvSeries.Domain.Entities.FavoritesPerson", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime?>("LastChangedAt");

                    b.Property<string>("LastChangedBy");

                    b.Property<long?>("PersonId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.HasIndex("UserId");

                    b.ToTable("FavoritesPersons");
                });

            modelBuilder.Entity("MyTvSeries.Domain.Entities.FavoritesSeries", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime?>("LastChangedAt");

                    b.Property<string>("LastChangedBy");

                    b.Property<long?>("SeriesId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("SeriesId");

                    b.HasIndex("UserId");

                    b.ToTable("FavoritesSeries");
                });

            modelBuilder.Entity("MyTvSeries.Domain.Entities.Genre", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("CreatedBy");

                    b.Property<string>("Description");

                    b.Property<DateTime?>("LastChangedAt");

                    b.Property<string>("LastChangedBy");

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

                    b.Property<string>("CreatedBy");

                    b.Property<string>("Description");

                    b.Property<bool>("IsImportEnabled");

                    b.Property<DateTime?>("LastChangedAt");

                    b.Property<string>("LastChangedBy");

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

                    b.Property<string>("CreatedBy");

                    b.Property<bool>("IsImportEnabled");

                    b.Property<DateTime?>("LastChangedAt");

                    b.Property<string>("LastChangedBy");

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

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime?>("Deathday");

                    b.Property<int>("Gender");

                    b.Property<string>("ImdbId");

                    b.Property<bool>("IsImportEnabled");

                    b.Property<DateTime?>("LastChangedAt");

                    b.Property<string>("LastChangedBy");

                    b.Property<long?>("MovieDbId");

                    b.Property<string>("Name");

                    b.Property<string>("PlaceOfBirth");

                    b.Property<byte[]>("PosterContent");

                    b.Property<string>("PosterName");

                    b.Property<long?>("TvDbId");

                    b.HasKey("Id");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("MyTvSeries.Domain.Entities.PersonNotification", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("CreatedBy");

                    b.Property<bool>("IsRead");

                    b.Property<DateTime?>("LastChangedAt");

                    b.Property<string>("LastChangedBy");

                    b.Property<long>("PersonId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.HasIndex("UserId");

                    b.ToTable("PersonNotifications");
                });

            modelBuilder.Entity("MyTvSeries.Domain.Entities.Season", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("AiredFrom");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("CreatedBy");

                    b.Property<bool>("IsImportEnabled");

                    b.Property<DateTime?>("LastChangedAt");

                    b.Property<string>("LastChangedBy");

                    b.Property<long?>("MovieDbId");

                    b.Property<string>("Name");

                    b.Property<int?>("NumberOfEpisodes");

                    b.Property<string>("Overview");

                    b.Property<int?>("SeasonNumber");

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

                    b.Property<string>("CreatedBy");

                    b.Property<int>("EpisodeRuntime");

                    b.Property<string>("ImdbId");

                    b.Property<bool>("IsImportEnabled");

                    b.Property<DateTime?>("LastChangedAt");

                    b.Property<string>("LastChangedBy");

                    b.Property<long?>("MovieDbId");

                    b.Property<string>("Name");

                    b.Property<int>("NumberOfEpisodes");

                    b.Property<int>("NumberOfSeasons");

                    b.Property<string>("OriginalName");

                    b.Property<string>("Overview");

                    b.Property<byte[]>("PosterContent");

                    b.Property<string>("PosterName");

                    b.Property<int>("Status");

                    b.Property<decimal>("TotalRuntime")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<long?>("TvDbId");

                    b.Property<decimal>("UserRating")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("UserVotes");

                    b.HasKey("Id");

                    b.ToTable("Series");
                });

            modelBuilder.Entity("MyTvSeries.Domain.Entities.SeriesNotification", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("CreatedBy");

                    b.Property<bool>("IsRead");

                    b.Property<DateTime?>("LastChangedAt");

                    b.Property<string>("LastChangedBy");

                    b.Property<long>("SeriesId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("SeriesId");

                    b.HasIndex("UserId");

                    b.ToTable("SeriesNotifications");
                });

            modelBuilder.Entity("MyTvSeries.Domain.Entities.SeriesReview", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime?>("LastChangedAt");

                    b.Property<string>("LastChangedBy");

                    b.Property<int>("Likes");

                    b.Property<long?>("SeriesId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("SeriesId");

                    b.HasIndex("UserId");

                    b.ToTable("SeriesReviews");
                });

            modelBuilder.Entity("MyTvSeries.Domain.Entities.Studio", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("CountryId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("CreatedBy");

                    b.Property<string>("Description");

                    b.Property<bool>("IsImportEnabled");

                    b.Property<DateTime?>("LastChangedAt");

                    b.Property<string>("LastChangedBy");

                    b.Property<string>("Name");

                    b.Property<long?>("TvDbId");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Studios");
                });

            modelBuilder.Entity("MyTvSeries.Domain.Entities.UserEpisode", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("CreatedBy");

                    b.Property<long>("EpisodeId");

                    b.Property<DateTime?>("LastChangedAt");

                    b.Property<string>("LastChangedBy");

                    b.Property<int>("Rating");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("EpisodeId");

                    b.HasIndex("UserId");

                    b.ToTable("UserEpisodes");
                });

            modelBuilder.Entity("MyTvSeries.Domain.Entities.UserReview", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("CreatedBy");

                    b.Property<bool>("IsUpvoted");

                    b.Property<DateTime?>("LastChangedAt");

                    b.Property<string>("LastChangedBy");

                    b.Property<long>("ReviewId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ReviewId");

                    b.HasIndex("UserId");

                    b.ToTable("UserReviews");
                });

            modelBuilder.Entity("MyTvSeries.Domain.Entities.UserSeries", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("CreatedBy");

                    b.Property<int>("EpisodesWatched");

                    b.Property<DateTime?>("LastChangedAt");

                    b.Property<string>("LastChangedBy");

                    b.Property<int>("Rating");

                    b.Property<int>("SeasonsWatched");

                    b.Property<long>("SeriesId");

                    b.Property<string>("UserId");

                    b.Property<int>("WatchStatus");

                    b.HasKey("Id");

                    b.HasIndex("SeriesId");

                    b.HasIndex("UserId");

                    b.ToTable("UsersSeries");
                });

            modelBuilder.Entity("MyTvSeries.Domain.Identity.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("MyTvSeries.Domain.Identity.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("MyTvSeries.Domain.Identity.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MyTvSeries.Domain.Identity.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("MyTvSeries.Domain.Identity.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
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

            modelBuilder.Entity("MyTvSeries.Domain.Entities.FavoritesPerson", b =>
                {
                    b.HasOne("MyTvSeries.Domain.Entities.Person", "Person")
                        .WithMany("FavoritesPersons")
                        .HasForeignKey("PersonId");

                    b.HasOne("MyTvSeries.Domain.Identity.ApplicationUser", "User")
                        .WithMany("FavoritesPersons")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("MyTvSeries.Domain.Entities.FavoritesSeries", b =>
                {
                    b.HasOne("MyTvSeries.Domain.Entities.Series", "Series")
                        .WithMany("FavoritesSeries")
                        .HasForeignKey("SeriesId");

                    b.HasOne("MyTvSeries.Domain.Identity.ApplicationUser", "User")
                        .WithMany("FavoritesSeries")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("MyTvSeries.Domain.Entities.Network", b =>
                {
                    b.HasOne("MyTvSeries.Domain.Entities.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId");
                });

            modelBuilder.Entity("MyTvSeries.Domain.Entities.PersonNotification", b =>
                {
                    b.HasOne("MyTvSeries.Domain.Entities.Person", "Person")
                        .WithMany("PersonNotifications")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MyTvSeries.Domain.Identity.ApplicationUser", "User")
                        .WithMany("PersonNotifications")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("MyTvSeries.Domain.Entities.Season", b =>
                {
                    b.HasOne("MyTvSeries.Domain.Entities.Series", "Series")
                        .WithMany("Seasons")
                        .HasForeignKey("SeriesId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MyTvSeries.Domain.Entities.SeriesNotification", b =>
                {
                    b.HasOne("MyTvSeries.Domain.Entities.Series", "Series")
                        .WithMany("SeriesNotifications")
                        .HasForeignKey("SeriesId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MyTvSeries.Domain.Identity.ApplicationUser", "User")
                        .WithMany("SeriesNotifications")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("MyTvSeries.Domain.Entities.SeriesReview", b =>
                {
                    b.HasOne("MyTvSeries.Domain.Entities.Series", "Series")
                        .WithMany("SeriesReviews")
                        .HasForeignKey("SeriesId");

                    b.HasOne("MyTvSeries.Domain.Identity.ApplicationUser", "User")
                        .WithMany("SeriesReviews")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("MyTvSeries.Domain.Entities.Studio", b =>
                {
                    b.HasOne("MyTvSeries.Domain.Entities.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId");
                });

            modelBuilder.Entity("MyTvSeries.Domain.Entities.UserEpisode", b =>
                {
                    b.HasOne("MyTvSeries.Domain.Entities.Episode", "Episode")
                        .WithMany("UserEpisodes")
                        .HasForeignKey("EpisodeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MyTvSeries.Domain.Identity.ApplicationUser", "User")
                        .WithMany("UserEpisodes")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("MyTvSeries.Domain.Entities.UserReview", b =>
                {
                    b.HasOne("MyTvSeries.Domain.Entities.SeriesReview", "Review")
                        .WithMany("UserReviews")
                        .HasForeignKey("ReviewId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MyTvSeries.Domain.Identity.ApplicationUser", "User")
                        .WithMany("UserReviews")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("MyTvSeries.Domain.Entities.UserSeries", b =>
                {
                    b.HasOne("MyTvSeries.Domain.Entities.Series", "Series")
                        .WithMany("SeriesUsers")
                        .HasForeignKey("SeriesId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MyTvSeries.Domain.Identity.ApplicationUser", "User")
                        .WithMany("SeriesUsers")
                        .HasForeignKey("UserId");
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
