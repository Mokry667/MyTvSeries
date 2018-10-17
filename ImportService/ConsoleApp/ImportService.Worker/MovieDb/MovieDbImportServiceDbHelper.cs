using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MyTvSeries.Domain.Ef;
using MyTvSeries.Domain.Entities;

namespace ImportService.Worker.MovieDb
{
    public class MovieDbImportServiceDbHelper : IMovieDbImportServiceDbHelper
    {
        #region Properties

        private readonly ITvSeriesContext _tvSeriesContext;
        private readonly IMovieDbMapper _movieDbMapper;
        private readonly ILogger<IMovieDbImportServiceDbHelper> _logger;

        private readonly long _systemId;

        #endregion

        #region Ctors

        public MovieDbImportServiceDbHelper(ITvSeriesContext tvSeriesContext, IMovieDbMapper movieDbMapper, 
            ILogger<IMovieDbImportServiceDbHelper> logger)
        {
            _tvSeriesContext = tvSeriesContext;
            _movieDbMapper = movieDbMapper;
            _logger = logger;
            // TODO change this in the future
            _systemId = 1;
        }

        #endregion

        #region Public methods

        public async Task AddOrUpdateSeries(Series seriesFromDb, Series seriesFromImport)
        {
            if (seriesFromDb != null)
            {
                seriesFromDb = _movieDbMapper.MapSeriesFromImportToSeriesFromDb(seriesFromDb, seriesFromImport);
                seriesFromDb = SetAuditValuesForUpdateSeries(seriesFromDb);
                _tvSeriesContext.Series.Update(seriesFromDb);
                await _tvSeriesContext.SaveChangesAsync();
                _logger.LogInformation("Updated series with id [{0}]", seriesFromDb.Id);
            }
            else
            {
                seriesFromImport = SetAuditValuesForAddSeries(seriesFromImport);
                await _tvSeriesContext.Series.AddAsync(seriesFromImport);
                await _tvSeriesContext.SaveChangesAsync();
                _logger.LogInformation("Added series with id [{0}]", seriesFromImport.Id);
            }
        }

        public async Task AddOrUpdatePerson(Person personFromDb, Person personFromImport)
        {
            if (personFromDb != null)
            {
                personFromDb = _movieDbMapper.MapPersonFromImportToPersonFromDb(personFromDb, personFromImport);
                personFromDb = SetAuditValuesForUpdatePerson(personFromDb);
                _tvSeriesContext.Persons.Update(personFromDb);
                await _tvSeriesContext.SaveChangesAsync();
                _logger.LogInformation("Updated person with id [{0}]", personFromDb.Id);
            }
            else
            {
                personFromImport = SetAuditValuesForAddPerson(personFromImport);
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
                seasonFromDb = SetAuditValuesForUpdateSeason(seasonFromDb);
                _tvSeriesContext.Seasons.Update(seasonFromDb);
                await _tvSeriesContext.SaveChangesAsync();
                _logger.LogInformation("Updated season with id [{0}] for series with id [{1}]", seasonFromDb.Id, seasonFromDb.SeriesId);
            }
            else
            {
                seasonFromImport = SetAuditValuesForAddSeason(seasonFromImport);
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
                episodeFromDb = SetAuditValuesForUpdateEpisode(episodeFromDb);
                _tvSeriesContext.Episodes.Update(episodeFromDb);
                await _tvSeriesContext.SaveChangesAsync();
                _logger.LogInformation("Updated episode with id [{0}] for season with id [{1}]", episodeFromDb.Id, episodeFromDb.SeasonId);
            }
            else
            {
                episodeFromImport = SetAuditValuesForAddEpisode(episodeFromImport);
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
                characterFromDb = SetAuditValuesForUpdateCharacter(characterFromDb);
                _tvSeriesContext.Characters.Update(characterFromDb);
                await _tvSeriesContext.SaveChangesAsync();
                _logger.LogInformation("Updated character with id [{0}] for person with id [{1}]", characterFromDb.Id, characterFromDb.PersonId);
            }
            else
            {
                characterFromImport = SetAuditValuesForAddCharacter(characterFromImport);
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
                crewFromDb = SetAuditValuesForUpdateCharacter(crewFromDb);
                _tvSeriesContext.Crews.Update(crewFromDb);
                await _tvSeriesContext.SaveChangesAsync();
                _logger.LogInformation("Updated crew with id [{0}] for person with id [{1}]", crewFromDb.Id, crewFromDb.PersonId);
            }
            else
            {
                crewFromImport = SetAuditValuesForAddCharacter(crewFromImport);
                await _tvSeriesContext.Crews.AddAsync(crewFromImport);
                await _tvSeriesContext.SaveChangesAsync();
                _logger.LogInformation("Added crew with id [{0}] for person with id [{1}]", crewFromImport.Id, crewFromImport.PersonId);
            }
        }

        #endregion

        #region Private methods

        // TODO make it more generic?
        // TODO remove import is enabled?

        private Series SetAuditValuesForAddSeries(Series series)
        {
            series.CreatedAt = DateTime.UtcNow;
            series.CreatedBy = _systemId;
            series.IsImportEnabled = true;
            return series;
        }

        private Series SetAuditValuesForUpdateSeries(Series series)
        {
            series.LastChangedAt = DateTime.UtcNow;
            series.LastChangedBy = _systemId;
            series.IsImportEnabled = true;
            return series;
        }

        private Person SetAuditValuesForAddPerson(Person person)
        {
            person.CreatedAt = DateTime.UtcNow;
            person.CreatedBy = _systemId;
            person.IsImportEnabled = true;
            return person;
        }

        private Person SetAuditValuesForUpdatePerson(Person person)
        {
            person.LastChangedAt = DateTime.UtcNow;
            person.LastChangedBy = _systemId;
            person.IsImportEnabled = true;
            return person;
        }

        private Season SetAuditValuesForAddSeason(Season season)
        {
            season.CreatedAt = DateTime.UtcNow;
            season.CreatedBy = _systemId;
            season.IsImportEnabled = true;
            return season;
        }

        private Season SetAuditValuesForUpdateSeason(Season season)
        {
            season.LastChangedAt = DateTime.UtcNow;
            season.LastChangedBy = _systemId;
            season.IsImportEnabled = true;
            return season;
        }

        private Episode SetAuditValuesForAddEpisode(Episode episode)
        {
            episode.CreatedAt = DateTime.UtcNow;
            episode.CreatedBy = _systemId;
            episode.IsImportEnabled = true;
            return episode;
        }

        private Episode SetAuditValuesForUpdateEpisode(Episode episode)
        {
            episode.LastChangedAt = DateTime.UtcNow;
            episode.LastChangedBy = _systemId;
            episode.IsImportEnabled = true;
            return episode;
        }

        private Character SetAuditValuesForAddCharacter(Character character)
        {
            character.CreatedAt = DateTime.UtcNow;
            character.CreatedBy = _systemId;
            character.IsImportEnabled = true;
            return character;
        }

        private Character SetAuditValuesForUpdateCharacter(Character character)
        {
            character.LastChangedAt = DateTime.UtcNow;
            character.LastChangedBy = _systemId;
            character.IsImportEnabled = true;
            return character;
        }

        private Crew SetAuditValuesForAddCharacter(Crew crew)
        {
            crew.CreatedAt = DateTime.UtcNow;
            crew.CreatedBy = _systemId;
            crew.IsImportEnabled = true;
            return crew;
        }

        private Crew SetAuditValuesForUpdateCharacter(Crew crew)
        {
            crew.LastChangedAt = DateTime.UtcNow;
            crew.LastChangedBy = _systemId;
            crew.IsImportEnabled = true;
            return crew;
        }

        #endregion
    }
}
