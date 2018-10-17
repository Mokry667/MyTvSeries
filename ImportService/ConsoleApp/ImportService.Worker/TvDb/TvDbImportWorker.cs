using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImportService.TheTvDb.Api;
using ImportService.TheTvDb.Converter;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MyTvSeries.Domain.Ef;
using MyTvSeries.Domain.Entities;

namespace ImportService.Worker.TvDb
{
    public class TvDbImportWorker : ITvDbImportWorker
    {
        #region Properties

        private readonly ITvDbApi _tvDbApi;
        private readonly ITvDbDomainConverter _tvDbDomainConverter;
        private readonly ITvSeriesContext _tvSeriesContext;
        private readonly ILogger<ITvDbImportWorker> _logger;
        private readonly IConfiguration _configuration;

        private readonly IdSource _idSource;

        private bool _isInitialized;
        private readonly int _maxRefreshRetries;

        private IList<long> _importSeriesIds;
        private IList<long> _importActorsIds;

        #endregion

        #region Ctors

        public TvDbImportWorker(ITvDbApi tvDbApi, ITvDbDomainConverter tvDbDomainConverter, ITvSeriesContext tvSeriesContext,
            ILogger<ITvDbImportWorker> logger, IConfiguration configuration)
        {
            _tvDbApi = tvDbApi;
            _tvDbApi.GetJwtToken();
            _tvDbDomainConverter = tvDbDomainConverter;
            _tvSeriesContext = tvSeriesContext;
            _logger = logger;
            _configuration = configuration;
            _maxRefreshRetries = Convert.ToInt32(_configuration.GetSection("ImportWorker").GetSection("TvDb").GetSection("MaxRefreshRetries").Value);
            _idSource = (IdSource) Enum.Parse(typeof(IdSource), _configuration.GetSection("ImportWorker").GetSection("TvDb").GetSection("IdSource").Value, true);
            _isInitialized = false;
        }

        #endregion

        #region Public methods

        public async Task Initialize()
        {
            _importSeriesIds = new List<long>();
            _importActorsIds = new List<long>();
            switch (_idSource)
            {
                case IdSource.Db:
                    await InitializeFromDb();
                    break;
                case IdSource.Config:
                    InitializeFromConfig();
                    break;
            }
            _isInitialized = true;
        }

        public async Task Start()
        {
            if (_isInitialized)
                await ImportSeries();
            else
                _logger.LogError("Import worker is not initialized");
        }

        #endregion

        #region Private methods

        private async Task InitializeFromDb()
        {
            _importSeriesIds = await _tvSeriesContext
                .Series
                .Where(x => x.IsImportEnabled)
                .OrderByDescending(x => x.TvDbId)
                .Select(x => x.TvDbId.Value)
                .ToListAsync();

            _importActorsIds = await _tvSeriesContext
                .Persons
                .Where(x => x.IsImportEnabled)
                .OrderByDescending(x => x.TvDbId)
                .Select(x => x.TvDbId.Value)
                .ToListAsync();
        }

        private void InitializeFromConfig()
        {
            var seriesMinIndex = Convert.ToInt32(_configuration.GetSection("ImportWorker").GetSection("TvDb").GetSection("SeriesIdMinIndex").Value);
            var seriesMaxIndex = Convert.ToInt32(_configuration.GetSection("ImportWorker").GetSection("TvDb").GetSection("SeriesIdMaxIndex").Value);
            for(long i = seriesMinIndex; i <= seriesMaxIndex; i++)
                _importSeriesIds.Add(i);

            var actorsMinIndex = Convert.ToInt32(_configuration.GetSection("ImportWorker").GetSection("TvDb").GetSection("ActorsIdMinIndex").Value);
            var actorsMaxIndex = Convert.ToInt32(_configuration.GetSection("ImportWorker").GetSection("TvDb").GetSection("ActorsIdMaxIndex").Value);
            for (long i = actorsMinIndex; i <= actorsMaxIndex; i++)
                _importActorsIds.Add(i);
        }

        private async Task ImportSeries()
        {
            var refreshRetries = 0;

            // get and set token
            await _tvDbApi.RefreshJwtToken();

            for (var index = 0; index < _importSeriesIds.Count - 1; index++) 
            {
                var seriesId = _importSeriesIds.ElementAt(index);

                var seriesJson = await _tvDbApi.GetSeries(seriesId);

                if (seriesJson != null)
                {
                    var series = await _tvDbDomainConverter.ConvertToSeries(seriesJson);

                    // often series does not contain name, skip such record
                    // TODO move to validator
                    if (string.IsNullOrEmpty(series.Name))
                    {
                        _logger.LogInformation("Skip Series with id [{0}] - empty name", seriesId);
                        continue;
                    }

                    // save to DB;
                    var seriesFromDb = await _tvSeriesContext
                        .Series
                        .Where(x => x.TvDbId == seriesId)
                        .FirstOrDefaultAsync();


                    // TODO think about default value
                    series.IsImportEnabled = true;

                    // series already in db
                    if (seriesFromDb != null)
                    {
                        // TODO move mapper?
                        MapSeriesFromImportToSeriesFromDb(ref seriesFromDb, series);

                        seriesFromDb.LastChangedAt = DateTime.UtcNow;
                        // TODO system user id
                        seriesFromDb.LastChangedBy = 1;
                        _tvSeriesContext.Series.Update(seriesFromDb);
                        _logger.LogInformation("Updated series with id [{0}]", seriesId);
                    }
                    else
                    {
                        series.CreatedAt = DateTime.UtcNow;
                        series.CreatedBy = 1;
                        await _tvSeriesContext.Series.AddAsync(series);
                        _logger.LogInformation("Added series with id [{0}]", seriesId);
                    }

                    await _tvSeriesContext.SaveChangesAsync();
                }

                if (seriesJson != null) continue;

                if (!_tvDbApi.IsTokenFresh())
                {
                    if (refreshRetries == _maxRefreshRetries)
                    {
                        _logger.LogError("Maximum number of refresh retries [{0}] was reached. Last processed series with id [{1}]. Worker will be shut down", 
                            _maxRefreshRetries, seriesId);
                        return;
                    }

                    await _tvDbApi.RefreshJwtToken();
                    _logger.LogWarning("Unauthorized request. Token was refreshed. Retrying to get series with id [{0}]", seriesId);
                    index--;
                    refreshRetries++;
                    continue;
                }
                _logger.LogInformation("Could not found error-free series with id [{0}]", seriesId);
            }
        }

        private async Task ImportSeriesActors()
        {
            var refreshRetries = 0;

            // get and set token
            await _tvDbApi.RefreshJwtToken();

            for (var index = 0; index < _importSeriesIds.Count - 1; index++)
            {
                var seriesId = _importSeriesIds.ElementAt(index);

                var actorsJson = await _tvDbApi.GetSeriesActors(seriesId);

                foreach (var actorJson in actorsJson)
                {
                    var person = _tvDbDomainConverter.ConvertToPerson(actorJson);

                    var personId = Convert.ToInt64(actorJson.Id);
                    var personFromDb = await _tvSeriesContext
                        .Persons
                        .Where(x => x.TvDbId == personId)
                        .FirstOrDefaultAsync();

                    // TODO move to validator
/*                    if (string.IsNullOrEmpty(person.FirstName))
                    {
                        _logger.LogInformation("Skip Person with id [{0}] - empty first name", personId);
                        continue;
                    }*/

/*                    if (string.IsNullOrEmpty(person.LastName))
                    {
                        _logger.LogInformation("Skip Person with id [{0}] - empty last name", personId);
                    }*/

                    if (personFromDb != null)
                    {
                        // TODO move mapper?
                        MapActorFromImportToPersonFromDb(ref personFromDb, person);
                        personFromDb.LastChangedAt = DateTime.UtcNow;
                        // TODO system user id
                        personFromDb.LastChangedBy = 1;
                        _tvSeriesContext.Persons.Update(personFromDb);
                        _logger.LogInformation("Updated person with id [{0}]", personId);
                    }
                    else
                    {
                        person.CreatedAt = DateTime.UtcNow;
                        person.CreatedBy = 1;
                        await _tvSeriesContext.Persons.AddAsync(person);
                        _logger.LogInformation("Added person with id [{0}]", personId);
                    }

                    await _tvSeriesContext.SaveChangesAsync();
                }
            }

        }


        // TODO check the changes in db and import and log them ???
        private void MapSeriesFromImportToSeriesFromDb(ref Series seriesFromDb, Series seriesFromImport)
        {
            seriesFromDb.Name = seriesFromImport.Name;
            seriesFromDb.Status = seriesFromImport.Status;
            seriesFromDb.AiredFrom = seriesFromImport.AiredFrom;
            seriesFromDb.AiredTo = seriesFromImport.AiredTo;
            //seriesFromDb.EpisodeRuntime = seriesFromImport.EpisodeRuntime;
            seriesFromDb.SeriesGenres = seriesFromImport.SeriesGenres;
            seriesFromDb.Overview = seriesFromImport.Overview;
            seriesFromDb.AirDayOfWeek = seriesFromImport.AirDayOfWeek;
            seriesFromDb.AirTime = seriesFromImport.AirTime;
        }

        private void MapActorFromImportToPersonFromDb(ref Person personFromDb, Person actorFromImport)
        {
            // TODO Fix
            //personFromDb.FirstName = actorFromImport.FirstName;
            //personFromDb.MiddleName = actorFromImport.MiddleName;
            //personFromDb.LastName = actorFromImport.LastName;
        }

        #endregion

    }
}
