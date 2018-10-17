using System.Threading.Tasks;
using MyTvSeries.Domain.Entities;

namespace ImportService.Worker.MovieDb
{
    public interface IMovieDbImportServiceDbHelper
    {
        Task AddOrUpdateSeries(Series seriesFromDb, Series seriesFromImport);
        Task AddOrUpdatePerson(Person personFromDb, Person personFromImport);
        Task AddOrUpdateSeason(Season seasonFromDb, Season seasonFromImport);
        Task AddOrUpdateEpisode(Episode episodeFromDb, Episode episodeFromImport);
        Task AddOrUpdateCharacter(Character characterFromDb, Character characterFromImport);
        Task AddOrUpdateCrew(Crew crewFromDb, Crew crewFromImport);
    }
}
