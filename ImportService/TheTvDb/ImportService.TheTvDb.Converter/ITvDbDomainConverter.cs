using System.Threading.Tasks;
using ImportService.TheTvDb.Api.Json.Entities;
using MyTvSeries.Domain.Entities;

namespace ImportService.TheTvDb.Converter
{
    public interface ITvDbDomainConverter
    {
        Task<Series> ConvertToSeries(SeriesJson seriesJson);
        Series ConvertToSeriesRuntimeAndBroadcast(SeriesJson seriesJson);

        Person ConvertToPerson(SeriesActorJson seriesActorJson);

    }
}
