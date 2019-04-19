using MyTvSeries.Domain.Entities;
using MyTvSeries.Domain.Entities.Base;

namespace ImportService.Worker.MovieDb
{
    public interface IMovieDbMapper
    {
        void MapFromImportToDb(BaseEntity entityFromDb, BaseEntity entityFromImport);
        Series MapSeriesExternalIdsFromImportToSeriesFromDb(Series seriesFromDb, Series seriesFromImport);
        Series MapSeriesBroadcastAndRuntimeFromImportToSeriesFromDb(Series seriesFromDb, Series seriesFromImport);
    }
}
