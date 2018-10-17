using System.Threading.Tasks;
using MyTvSeries.Domain.Entities;

namespace ImportService.TheTvDb.Converter
{
    public interface ITvDbDomainDbHelper
    {
        Task<Genre> GetGenre(string name);
    }
}
