using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MyTvSeries.Domain.Ef;
using MyTvSeries.Domain.Entities;
using MyTvSeries.Domain.Entities.Base;

namespace ImportService.Worker.MovieDb
{
    public class MovieDbImportServiceDbHelper : IMovieDbImportServiceDbHelper
    {
        #region Properties

        private readonly ITvSeriesContext _tvSeriesContext;
        private readonly IMovieDbMapper _movieDbMapper;
        private readonly ILogger<IMovieDbImportServiceDbHelper> _logger;
        private readonly INotificationService _notificationService;

        private readonly string _systemGuid;

        #endregion

        #region Ctors

        public MovieDbImportServiceDbHelper(ITvSeriesContext tvSeriesContext, IMovieDbMapper movieDbMapper, 
            ILogger<IMovieDbImportServiceDbHelper> logger, IConfiguration configuration, INotificationService notificationService)
        {
            _tvSeriesContext = tvSeriesContext;
            _movieDbMapper = movieDbMapper;
            _logger = logger;
            _systemGuid = configuration.GetSection("System").GetSection("SystemGuid").Value;
            _notificationService = notificationService;
        }

        #endregion

        #region Public methods

        public async Task AddOrUpdate(ImportEntity entityFromDb, ImportEntity entityFromImport)
        {
            if (entityFromDb != null)
            {
                _movieDbMapper.MapFromImportToDb(entityFromDb, entityFromImport);
                entityFromDb.UpdateAuditValuesForUpdatedEntity(_systemGuid);
                _tvSeriesContext.Update(entityFromDb);
                await _tvSeriesContext.SaveChangesAsync();
                _logger.LogInformation($"Update {entityFromDb.GetType()} with id {entityFromDb.Id}");
            }
            else
            {
                entityFromImport.UpdateAuditValuesForNewEntity(_systemGuid);
                entityFromImport.EnableImport();
                await _tvSeriesContext.AddAsync(entityFromImport);
                await _tvSeriesContext.SaveChangesAsync();
                _logger.LogInformation($"Added {entityFromImport.GetType()} with id {entityFromImport.Id}");
            }
        }

        public async Task AddOrUpdateWithNotification(ImportEntity entityFromDb, ImportEntity entityFromImport)
        {
            if (entityFromDb != null)
            {
                _movieDbMapper.MapFromImportToDb(entityFromDb, entityFromImport);
                entityFromDb.UpdateAuditValuesForUpdatedEntity(_systemGuid);
                _tvSeriesContext.Update(entityFromDb);
                await _tvSeriesContext.SaveChangesAsync();
                _logger.LogInformation($"Update {entityFromDb.GetType()} with id {entityFromDb.Id}");
            }
            else
            {
                entityFromImport.UpdateAuditValuesForNewEntity(_systemGuid);
                entityFromImport.EnableImport();
                // TODO AddOrUpdateSeason 
                // await _notificationService.CreateSeriesNotificationsForUsers(seasonFromImport);
                // TODO AddOrUpdateCharacter
                //await _notificationService.CreatePersonNotificationsForUsers(characterFromImport);
                await _tvSeriesContext.AddAsync(entityFromImport);
                await _tvSeriesContext.SaveChangesAsync();
                _logger.LogInformation($"Added {entityFromImport.GetType()} with id {entityFromImport.Id}");
            }
        }



/*        public async Task AddOrUpdateSeries(Series seriesFromDb, Series seriesFromImport)
        {
            if (seriesFromDb != null)
            {
                seriesFromDb = _movieDbMapper.MapSeriesFromImportToSeriesFromDb(seriesFromDb, seriesFromImport);
                seriesFromDb.UpdateAuditValuesForUpdatedEntity(_systemGuid);
                _tvSeriesContext.Series.Update(seriesFromDb);
                await _tvSeriesContext.SaveChangesAsync();
                _logger.LogInformation("Updated series with id [{0}]", seriesFromDb.Id);
            }
            else
            {
                seriesFromImport.UpdateAuditValuesForNewEntity(_systemGuid);
                seriesFromImport.EnableImport();

                await _tvSeriesContext.Series.AddAsync(seriesFromImport);
                await _tvSeriesContext.SaveChangesAsync();
                _logger.LogInformation("Added series with id [{0}]", seriesFromImport.Id);
            }
        }*/

/*        public async Task AddOrUpdatePerson(Person personFromDb, Person personFromImport)
        {
            if (personFromDb != null)
            {
                personFromDb = _movieDbMapper.MapPersonFromImportToPersonFromDb(personFromDb, personFromImport);
                personFromDb.UpdateAuditValuesForUpdatedEntity(_systemGuid);
                _tvSeriesContext.Persons.Update(personFromDb);
                await _tvSeriesContext.SaveChangesAsync();
                _logger.LogInformation("Updated person with id [{0}]", personFromDb.Id);
            }
            else
            {
                personFromImport.UpdateAuditValuesForNewEntity(_systemGuid);
                personFromImport.EnableImport();
                await _tvSeriesContext.Persons.AddAsync(personFromImport);
                await _tvSeriesContext.SaveChangesAsync();
                _logger.LogInformation("Added person with id [{0}]", personFromImport.Id);
            }
        }

        public async Task AddOrUpdateSeason(Season seasonFromDb, Season seasonFromImport)
        {
            if (seasonFromDb != null)
            {
                seasonFromDb = _movieDbMapper.MapSeasonFromImportToSeasonFromDb(seasonFromDb, seasonFromImport);
                seasonFromDb.UpdateAuditValuesForUpdatedEntity(_systemGuid);
                _tvSeriesContext.Seasons.Update(seasonFromDb);
                await _tvSeriesContext.SaveChangesAsync();
                _logger.LogInformation("Updated season with id [{0}] for series with id [{1}]", seasonFromDb.Id, seasonFromDb.SeriesId);
            }
            else
            {
                seasonFromImport.UpdateAuditValuesForNewEntity(_systemGuid);
                seasonFromImport.EnableImport();

                await _notificationService.CreateSeriesNotificationsForUsers(seasonFromImport);

                await _tvSeriesContext.Seasons.AddAsync(seasonFromImport);
                await _tvSeriesContext.SaveChangesAsync();
                _logger.LogInformation("Added season with id [{0}] for series with id [{1}]", seasonFromImport.Id, seasonFromImport.SeriesId);
            }
        }

        public async Task AddOrUpdateEpisode(Episode episodeFromDb, Episode episodeFromImport)
        {
            if (episodeFromDb != null)
            {
                episodeFromDb = _movieDbMapper.MapEpisodeFromImportToEpisodeFromDb(episodeFromDb, episodeFromImport);
                episodeFromDb.UpdateAuditValuesForUpdatedEntity(_systemGuid);
                _tvSeriesContext.Episodes.Update(episodeFromDb);
                await _tvSeriesContext.SaveChangesAsync();
                _logger.LogInformation("Updated episode with id [{0}] for season with id [{1}]", episodeFromDb.Id, episodeFromDb.SeasonId);
            }
            else
            {
                episodeFromImport.UpdateAuditValuesForNewEntity(_systemGuid);
                episodeFromImport.EnableImport();
                await _tvSeriesContext.Episodes.AddAsync(episodeFromImport);
                await _tvSeriesContext.SaveChangesAsync();
                _logger.LogInformation("Added episode with id [{0}] for season with id [{1}]", episodeFromImport.Id, episodeFromImport.SeasonId);
            }
        }

        public async Task AddOrUpdateCharacter(Character characterFromDb, Character characterFromImport)
        {
            if (characterFromDb != null)
            {
                characterFromDb = _movieDbMapper.MapCharacterFromImportToCharacterFromDb(characterFromDb, characterFromImport);
                characterFromDb.UpdateAuditValuesForUpdatedEntity(_systemGuid);
                _tvSeriesContext.Characters.Update(characterFromDb);
                await _tvSeriesContext.SaveChangesAsync();
                _logger.LogInformation("Updated character with id [{0}] for person with id [{1}]", characterFromDb.Id, characterFromDb.PersonId);
            }
            else
            {
                characterFromImport.UpdateAuditValuesForNewEntity(_systemGuid);
                characterFromImport.EnableImport();

                await _notificationService.CreatePersonNotificationsForUsers(characterFromImport);

                await _tvSeriesContext.Characters.AddAsync(characterFromImport);
                await _tvSeriesContext.SaveChangesAsync();
                _logger.LogInformation("Added character with id [{0}] for person with id [{1}]", characterFromImport.Id, characterFromImport.PersonId);
            }
        }

        public async Task AddOrUpdateCrew(Crew crewFromDb, Crew crewFromImport)
        {
            if (crewFromDb != null)
            {
                crewFromDb = _movieDbMapper.MapCrewFromImportToCrewFromDb(crewFromDb, crewFromImport);
                crewFromDb.UpdateAuditValuesForUpdatedEntity(_systemGuid);
                _tvSeriesContext.Crews.Update(crewFromDb);
                await _tvSeriesContext.SaveChangesAsync();
                _logger.LogInformation("Updated crew with id [{0}] for person with id [{1}]", crewFromDb.Id, crewFromDb.PersonId);
            }
            else
            {
                crewFromImport.UpdateAuditValuesForNewEntity(_systemGuid);
                crewFromImport.EnableImport();

                await _tvSeriesContext.Crews.AddAsync(crewFromImport);
                await _tvSeriesContext.SaveChangesAsync();
                _logger.LogInformation("Added crew with id [{0}] for person with id [{1}]", crewFromImport.Id, crewFromImport.PersonId);
            }
        }*/

        public async Task UpdateSeriesExternalIds(Series seriesFromDb, Series seriesFromImport)
        {
            if (seriesFromDb.TvDbId != seriesFromImport.TvDbId || seriesFromDb.ImdbId != seriesFromImport.ImdbId)
            {
                seriesFromDb = _movieDbMapper.MapSeriesExternalIdsFromImportToSeriesFromDb(seriesFromDb, seriesFromImport);
                seriesFromDb.UpdateAuditValuesForUpdatedEntity(_systemGuid);
                seriesFromDb.EnableImport();
                _tvSeriesContext.Series.Update(seriesFromDb);
                await _tvSeriesContext.SaveChangesAsync();
                _logger.LogInformation("Updated series externals id with id [{0}]", seriesFromDb.Id);
            }
            else
            {
                _logger.LogInformation("Skip external id update for series with id [{0}]", seriesFromDb.Id);
            }
        }

        public async Task UpdateSeriesRuntimeAndBroadcast(Series seriesFromDb, Series seriesFromImport)
        {
            if (seriesFromDb.AirDayOfWeek != seriesFromImport.AirDayOfWeek || seriesFromDb.AirTime != seriesFromImport.AirTime
                || seriesFromDb.EpisodeRuntime != seriesFromImport.EpisodeRuntime)
            {
                seriesFromDb = _movieDbMapper.MapSeriesBroadcastAndRuntimeFromImportToSeriesFromDb(seriesFromDb, seriesFromImport);
                seriesFromDb.UpdateAuditValuesForNewEntity(_systemGuid);
                seriesFromDb.EnableImport();
                _tvSeriesContext.Series.Update(seriesFromDb);
                await _tvSeriesContext.SaveChangesAsync();
                _logger.LogInformation("Updated series broadcast time and runtime for series with id [{0}]", seriesFromDb.Id);
            }
            else
            {
                _logger.LogInformation("Skip broadcast time and runtime update for series with id [{0}]", seriesFromDb.Id);
            }
        }

        #endregion

        #region Private methods

        #endregion
    }
}
