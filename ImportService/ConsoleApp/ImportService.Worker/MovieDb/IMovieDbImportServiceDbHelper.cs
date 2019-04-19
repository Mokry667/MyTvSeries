using System.Threading.Tasks;
using MyTvSeries.Domain.Entities;
using MyTvSeries.Domain.Entities.Base;

namespace ImportService.Worker.MovieDb
{
    public interface IMovieDbImportServiceDbHelper
    {
        Task AddOrUpdate(ImportEntity entityFromDb, ImportEntity entityFromImport);
        Task AddOrUpdateWithNotification(ImportEntity entityFromDb, ImportEntity entityFromImport);
        Task UpdateSeriesExternalIds(Series seriesFromDb, Series seriesFromImport);
        Task UpdateSeriesRuntimeAndBroadcast(Series seriesFromDb, Series seriesFromImport);
    }
}
