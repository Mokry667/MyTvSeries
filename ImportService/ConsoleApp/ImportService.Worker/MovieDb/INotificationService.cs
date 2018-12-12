using MyTvSeries.Domain.Entities;
using System.Threading.Tasks;

namespace ImportService.Worker.MovieDb
{
    public interface INotificationService
    {
        Task CreateSeriesNotificationsForUsers(Season season);
        Task CreatePersonNotificationsForUsers(Character character);
    }
}
