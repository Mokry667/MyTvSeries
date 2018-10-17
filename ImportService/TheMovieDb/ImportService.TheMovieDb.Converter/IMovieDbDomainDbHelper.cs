using System.Threading.Tasks;
using ImportService.TheMovieDb.Api.Json.Entities;
using MyTvSeries.Domain.Entities;

namespace ImportService.TheMovieDb.Converter
{
    public interface IMovieDbDomainDbHelper
    {
        Task<Genre> GetOrAddGenre(GenreJson genreJson);
    }
}
