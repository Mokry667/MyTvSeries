using System.Collections.Generic;
using System.Threading.Tasks;
using ImportService.TheTvDb.Api.Json.Authentication;
using ImportService.TheTvDb.Api.Json.Entities;

namespace ImportService.TheTvDb.Api
{
    public interface ITvDbApi
    {
        bool IsTokenFresh();
        Task RefreshJwtToken();
        Task<JwtTokenJson> GetJwtToken();
        Task<SeriesJson> GetSeries(long seriesId);
        Task<IEnumerable<SeriesActorJson>> GetSeriesActors(long seriesId);
        Task<object> GetSeriesEpisodes(long seriesId);
        Task<object> GetSeriesEpisodes(long seriesId, int seasonNumber);
        Task<object> GetSeriesEpisode(long seriesId, int episodeNumber);
        Task<object> GetEpisode(long episodeId);
        Task<object> GetUpdated(string fromTime, string toTime);
    }
}
