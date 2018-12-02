using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyTvSeries.Domain.Entities;
using MyTvSeries.Domain.Identity;
using MyTvSeries.Domain.ManyToMany;

namespace MyTvSeries.Domain.Ef
{
    public class TvSeriesContext : IdentityDbContext<ApplicationUser>, ITvSeriesContext
    {
        public TvSeriesContext(DbContextOptions<TvSeriesContext> options)
            : base(options)
        {
            //this.Database.Log = x => Debug.Log(x);
        }

        public DbSet<Series> Series { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Keyword> Keywords { get; set; }
        public DbSet<Network> Networks { get; set; }
        public DbSet<Studio> Studios { get; set; }
        public DbSet<Crew> Crews { get; set; }
        public DbSet<UserSeries> UsersSeries { get; set; }
        public DbSet<FavoritesPerson> FavoritesPersons { get; set; }
        public DbSet<FavoritesSeries> FavoritesSeries { get; set; }
        public DbSet<SeriesReview> SeriesReviews { get; set; }
        public DbSet<UserEpisode> UserEpisodes { get; set; }
        public DbSet<UserReview> UserReviews { get; set; }
        public DbSet<SeriesNotification> SeriesNotifications { get; set; }
        public DbSet<PersonNotification> PersonNotifications { get; set; }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Many to many entity framework core workaround
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SeriesGenres>()
                .HasKey(x => new { x.SeriesId, x.GenreId } );

            modelBuilder.Entity<SeriesGenres>()
                .HasOne(x => x.Series)
                .WithMany(x => x.SeriesGenres)
                .HasForeignKey(x => x.SeriesId);

            modelBuilder.Entity<SeriesGenres>()
                .HasOne(x => x.Genre)
                .WithMany(x => x.SeriesGenres)
                .HasForeignKey(x => x.GenreId);


            modelBuilder.Entity<SeriesNetworks>()
                .HasKey(x => new {x.SeriesId, x.NetworkId});

            modelBuilder.Entity<SeriesNetworks>()
                .HasOne(x => x.Series)
                .WithMany(x => x.SeriesNetworks)
                .HasForeignKey(x => x.SeriesId);

            modelBuilder.Entity<SeriesNetworks>()
                .HasOne(x => x.Network)
                .WithMany(x => x.SeriesNetworks)
                .HasForeignKey(x => x.NetworkId);


            modelBuilder.Entity<SeriesCountries>()
                .HasKey(x => new { x.SeriesId, x.CountryId });

            modelBuilder.Entity<SeriesCountries>()
                .HasOne(x => x.Series)
                .WithMany(x => x.SeriesCountries)
                .HasForeignKey(x => x.SeriesId);

            modelBuilder.Entity<SeriesCountries>()
                .HasOne(x => x.Country)
                .WithMany(x => x.SeriesCountries)
                .HasForeignKey(x => x.CountryId);


            modelBuilder.Entity<SeriesKeywords>()
                .HasKey(x => new { x.SeriesId, x.KeywordId });

            modelBuilder.Entity<SeriesKeywords>()
                .HasOne(x => x.Series)
                .WithMany(x => x.SeriesKeywords)
                .HasForeignKey(x => x.SeriesId);

            modelBuilder.Entity<SeriesKeywords>()
                .HasOne(x => x.Keyword)
                .WithMany(x => x.SeriesKeywords)
                .HasForeignKey(x => x.KeywordId);


            modelBuilder.Entity<SeriesStudios>()
                .HasKey(x => new { x.SeriesId, x.StudioId });

            modelBuilder.Entity<SeriesStudios>()
                .HasOne(x => x.Series)
                .WithMany(x => x.SeriesStudios)
                .HasForeignKey(x => x.SeriesId);

            modelBuilder.Entity<SeriesStudios>()
                .HasOne(x => x.Studio)
                .WithMany(x => x.SeriesStudios)
                .HasForeignKey(x => x.StudioId);


            modelBuilder.Entity<SeriesCharacters>()
                .HasKey(x => new { x.SeriesId, x.CharacterId });

            modelBuilder.Entity<SeriesCharacters>()
                .HasOne(x => x.Series)
                .WithMany(x => x.SeriesCharacters)
                .HasForeignKey(x => x.SeriesId);

            modelBuilder.Entity<SeriesCharacters>()
                .HasOne(x => x.Character)
                .WithMany(x => x.SeriesCharacters)
                .HasForeignKey(x => x.CharacterId);

            modelBuilder.Entity<UserSeries>()
                .HasOne(x => x.Series)
                .WithMany(x => x.SeriesUsers)
                .HasForeignKey(x => x.SeriesId);

            modelBuilder.Entity<UserSeries>()
                .HasOne(x => x.User)
                .WithMany(x => x.SeriesUsers)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<FavoritesSeries>()
                .HasOne(x => x.Series)
                .WithMany(x => x.FavoritesSeries)
                .HasForeignKey(x => x.SeriesId);

            modelBuilder.Entity<FavoritesSeries>()
                .HasOne(x => x.User)
                .WithMany(x => x.FavoritesSeries)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<FavoritesPerson>()
                .HasOne(x => x.Person)
                .WithMany(x => x.FavoritesPersons)
                .HasForeignKey(x => x.PersonId);

            modelBuilder.Entity<FavoritesPerson>()
                .HasOne(x => x.User)
                .WithMany(x => x.FavoritesPersons)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<SeriesReview>()
                .HasOne(x => x.Series)
                .WithMany(x => x.SeriesReviews)
                .HasForeignKey(x => x.SeriesId);

            modelBuilder.Entity<SeriesReview>()
                .HasOne(x => x.User)
                .WithMany(x => x.SeriesReviews)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<UserEpisode>()
                .HasOne(x => x.Episode)
                .WithMany(x => x.UserEpisodes)
                .HasForeignKey(x => x.EpisodeId);

            modelBuilder.Entity<UserEpisode>()
                .HasOne(x => x.User)
                .WithMany(x => x.UserEpisodes)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<UserReview>()
                .HasOne(x => x.Review)
                .WithMany(x => x.UserReviews)
                .HasForeignKey(x => x.ReviewId);

            modelBuilder.Entity<UserReview>()
                .HasOne(x => x.User)
                .WithMany(x => x.UserReviews)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<SeriesNotification>()
                .HasOne(x => x.Series)
                .WithMany(x => x.SeriesNotifications)
                .HasForeignKey(x => x.SeriesId);

            modelBuilder.Entity<SeriesNotification>()
                .HasOne(x => x.User)
                .WithMany(x => x.SeriesNotifications)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<PersonNotification>()
                .HasOne(x => x.Person)
                .WithMany(x => x.PersonNotifications)
                .HasForeignKey(x => x.PersonId);

            modelBuilder.Entity<PersonNotification>()
                .HasOne(x => x.User)
                .WithMany(x => x.PersonNotifications)
                .HasForeignKey(x => x.UserId);
        }
    }
}
