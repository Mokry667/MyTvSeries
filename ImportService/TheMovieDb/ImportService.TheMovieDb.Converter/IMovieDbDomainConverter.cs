using System.Collections.Generic;
using System.Threading.Tasks;
using ImportService.TheMovieDb.Api.Json.Entities;
using MyTvSeries.Domain.Entities;

namespace ImportService.TheMovieDb.Converter
{
    public interface IMovieDbDomainConverter
    {
        Task<Series> ConvertToSeries(SeriesDetailsJson seriesJson);
        Person ConvertToPerson(PersonDetailsJson personJson);
        List<Season> ConvertToSeason(SeriesDetailsJson seriesJson, long seriesId);
        List<Episode> ConvertToEpisode(SeasonDetailsJson seasonDetailsJson, long seasonId);
        Character ConvertToCharacter(CastJson characterJson, long seriesId, long personId);
        Crew ConvertToCrew(CrewJson crewJson, long seriesId, long personId);
        Series ConvertToSeriesWithExternalIds(SeriesExternalIdsJson seriesJson);
    }
}
