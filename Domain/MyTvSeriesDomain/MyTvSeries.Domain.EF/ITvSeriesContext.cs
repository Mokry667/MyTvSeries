using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyTvSeries.Domain.Entities;

namespace MyTvSeries.Domain.Ef
{
    public interface ITvSeriesContext
    {
        DbSet<Series> Series { get; set; }
        DbSet<Episode> Episodes { get; set; }
        DbSet<Character> Characters { get; set; }
        DbSet<Person> Persons { get; set; }
        DbSet<Season> Seasons { get; set; }
        DbSet<Genre> Genres { get; set; }
        DbSet<Country> Countries { get; set; }
        DbSet<Keyword> Keywords { get; set; }
        DbSet<Network> Networks { get; set; }
        DbSet<Studio> Studios { get; set; }
        DbSet<Crew> Crews { get; set; }
        DbSet<FavoritesPerson> FavoritesPersons { get; set; }
        DbSet<FavoritesSeries> FavoritesSeries { get; set; }
        DbSet<SeriesReview> SeriesReviews { get; set; }
        DbSet<UserEpisode> UserEpisodes { get; set; }
        DbSet<UserReview> UserReviews { get; set; }
        DbSet<SeriesNotification> SeriesNotifications { get; set; }
        DbSet<PersonNotification> PersonNotifications { get; set; }
        Task<int> SaveChangesAsync();
    }
}
