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
        public DbSet<EpisodeRuntime> EpisodesRuntimes { get; set; }
        public DbSet<UserSeries> UsersSeries { get; set; }

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
                .HasKey(x => new { x.SeriesId, x.UserId });

            modelBuilder.Entity<UserSeries>()
                .HasOne(x => x.Series)
                .WithMany(x => x.SeriesUsers)
                .HasForeignKey(x => x.SeriesId);

            modelBuilder.Entity<UserSeries>()
                .HasOne(x => x.User)
                .WithMany(x => x.SeriesUsers)
                .HasForeignKey(x => x.UserId);
        }
    }
}
