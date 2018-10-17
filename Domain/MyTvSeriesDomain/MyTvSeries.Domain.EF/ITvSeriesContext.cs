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
        DbSet<EpisodeRuntime> EpisodesRuntimes { get; set; }
        Task<int> SaveChangesAsync();
    }
}
