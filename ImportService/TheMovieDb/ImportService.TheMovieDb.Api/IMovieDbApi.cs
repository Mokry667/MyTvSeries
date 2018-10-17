using System.Threading.Tasks;
using ImportService.TheMovieDb.Api.Json.Entities;

namespace ImportService.TheMovieDb.Api
{
    public interface IMovieDbApi
    {
        Task<SeriesDetailsJson> GetSeriesDetails(long seriesId);

        Task<SeriesExternalIdsJson> GetSeriesExternalIds(long seriesId);
        Task<PersonDetailsJson> GetPersonDetails(long personId);
        Task<PersonDetailsIdsJson> GetPersonExternalIds(long personId);
        Task<GenresListJson> GetGenres();
        Task<SeasonDetailsJson> GetSeason(long seriesId, long seasonNumber);
        Task<SeriesCreditsJson> GetCredits(long seriesId);
    }
}
