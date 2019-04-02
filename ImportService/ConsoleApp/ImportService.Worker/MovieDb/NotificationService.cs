using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MyTvSeries.Domain.Ef;
using MyTvSeries.Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ImportService.Worker.MovieDb
{
    public class NotificationService : INotificationService
    {
        private readonly ITvSeriesContext _context;
        private readonly ILogger<INotificationService> _logger;
        private readonly string _systemGuid;

        public NotificationService(ITvSeriesContext context, ILogger<INotificationService> logger, IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _systemGuid = configuration.GetSection("System").GetSection("SystemGuid").Value;
        }

        public async Task CreateSeriesNotificationsForUsers(Season season)
        {
            var usersWithReadNotifications = await _context.SeriesNotifications
                .Where(x => x.SeriesId == season.SeriesId && x.IsRead)
                .Select(x => x.UserId)
                .ToListAsync();

            var favoriteUsers = await _context
                .FavoritesSeries
                .Where(x => x.SeriesId == season.SeriesId && usersWithReadNotifications.Contains(x.UserId))
                .ToListAsync();

            var userIds = favoriteUsers.Select(x => x.UserId).Distinct();

            foreach(var userId in userIds)
            {
                var notification = CreateSeriesNotification(userId, season.SeriesId);
                await _context.SeriesNotifications.AddAsync(notification);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Created series notification with id [{0}]", userId);
            }
        }

        public async Task CreatePersonNotificationsForUsers(Character character)
        {
            var usersWithReadNotifications = await _context.PersonNotifications
                .Where(x => x.PersonId == character.PersonId && x.IsRead)
                .Select(x => x.UserId)
                .ToListAsync();

            var favoriteUsers = await _context
                .FavoritesPersons
                .Where(x => x.PersonId  == character.PersonId && usersWithReadNotifications.Contains(x.UserId))
                .ToListAsync();

            var userIds = favoriteUsers.Select(x => x.UserId).Distinct();

            foreach (var userId in userIds)
            {
                var notification = CreatePersonNotification(userId, character);
                await _context.PersonNotifications.AddAsync(notification);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Created person notification with id [{0}]", userId);
            }
        }

        private SeriesNotification CreateSeriesNotification(string userId, long seriesId)
        {
            var notification = new SeriesNotification
            {
                SeriesId = seriesId,
                UserId = userId,
                Content = CreateSeriesNotificationMessage(seriesId),
                IsRead = false,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = _systemGuid
            };

            return notification;
        }

        private string CreateSeriesNotificationMessage(long seriesId)
        {
            return $"New season of [{seriesId}] will be airing soon";
        }

        private PersonNotification CreatePersonNotification(string userId, Character character)
        {
            var notification = new PersonNotification
            {
                PersonId = character.PersonId,
                UserId = userId,
                Content = CreatePersonNotificationMessage(character),
                IsRead = false,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = _systemGuid
            };

            return notification;
        }

        private string CreatePersonNotificationMessage(Character character)
        {
            var seriesCharacters = character.SeriesCharacters.FirstOrDefault();
            if (seriesCharacters != null)
            {
                return $"[{character.PersonId}] will be playing as a [{character.Id}] in a [{seriesCharacters.SeriesId}] soon";
            }

            return "Error generating notification message for - Please contact administrator";
        }
    }
}
