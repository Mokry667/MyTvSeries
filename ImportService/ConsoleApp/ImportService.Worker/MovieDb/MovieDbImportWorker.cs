﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImportService.TheMovieDb.Api;
using ImportService.TheMovieDb.Converter;
using ImportService.TheTvDb.Api;
using ImportService.TheTvDb.Api.Json.Entities;
using ImportService.TheTvDb.Converter;
using ImportService.Worker.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MyTvSeries.Domain.Ef;
using MyTvSeries.Domain.Entities;

namespace ImportService.Worker.MovieDb
{
    public class MovieDbImportWorker : IMovieDbImportWorker
    {
        #region Properties

        private readonly ITvSeriesContext _tvSeriesContext;
        private readonly ILogger<IMovieDbImportWorker> _logger;
        private readonly IConfiguration _configuration;

        private readonly IMovieDbApi _movieDbApi;
        private readonly IMovieDbDomainConverter _movieDbDomainConverter;
        private readonly IMovieDbImportServiceDbHelper _movieDbImportServiceDbHelper;

        private readonly ITvDbApi _tvDbApi;
        private readonly ITvDbDomainConverter _tvDbDomainConverter;

        private readonly IdSource _idSource;
        private readonly int _popularPages;

        private bool _isInitialized;

        private IList<long> _importSeriesIds;

        private IList<long> _importPersonsIds;

        // seriesId, seasonNumber
        private List<SeasonWithSeries> _importSeasonsIds;

        #endregion

        #region Ctors

        public MovieDbImportWorker(IMovieDbApi movieDbApi, IMovieDbDomainConverter movieDbDomainConverter, 
            ITvDbApi tvDbApi, ITvDbDomainConverter tvDbDomainConverter, ITvSeriesContext tvSeriesContext,
            IMovieDbImportServiceDbHelper movieDbImportServiceDbHelper, ILogger<IMovieDbImportWorker> logger, IConfiguration configuration)
        {
            _movieDbApi = movieDbApi;
            _movieDbDomainConverter = movieDbDomainConverter;
            _tvDbApi = tvDbApi;
            _tvDbDomainConverter = tvDbDomainConverter;
            _tvSeriesContext = tvSeriesContext;
            _movieDbImportServiceDbHelper = movieDbImportServiceDbHelper;
            _logger = logger;
            _configuration = configuration;
            _idSource = (IdSource)Enum.Parse(typeof(IdSource), _configuration.GetSection("ImportWorker").GetSection("MovieDb").GetSection("IdSource").Value, true);
            _popularPages = 1;
            _isInitialized = false;
        }

        #endregion

        #region Public methods

        public async Task Initialize()
        {
            await _movieDbApi.SetUpImageApi();

            _importSeriesIds = new List<long>();
            _importPersonsIds = new List<long>();
            _importSeasonsIds = new List<SeasonWithSeries>();

            switch (_idSource)
            {
                case IdSource.Db:
                    await InitializeFromDb();
                    break;
                case IdSource.Config:
                    InitializeFromConfig();
                    break;
                case IdSource.Popular:
                    await InitializePopular();
                    break;
            }

            _isInitialized = true;
        }

        public async Task Start()
        {
            if (_isInitialized)
            {
                await GetSeasonDetailsToImport();
                _logger.LogInformation("Start Import Series Details");
                await ImportSeriesDetails();
                _logger.LogInformation("Finished Import Series Details");

                _logger.LogInformation("Start Import Person Details");
                await ImportPersonsDetails();
                _logger.LogInformation("Finished Import Person Details");

                await GetSeasonDetailsToImport();

                if (_importSeasonsIds != null)
                {
                    _logger.LogInformation("Start Import Season Details");
                    await ImportSeasonDetails();
                    _logger.LogInformation("Finished Import Season Details");
                }

                _logger.LogInformation("Start Import Series Credits");
                await ImportSeriesCredits();
                _logger.LogInformation("Finished Import Series Credits");

                _logger.LogInformation("Start Import Series External Ids");
                await ImportSeriesExternalIds();
                _logger.LogInformation("Finished Import Series External Ids");

                _logger.LogInformation("Start Import Series Brodcast Time and Runtime");
                await ImportSeriesDetailsFromTvDb();
                _logger.LogInformation("Finished Import Series Brodcast Time and Runtime");
            }
            else
                _logger.LogError("Import worker is not initialized");
        }

        #endregion

        #region Private methods

        private async Task InitializePopular()
        {
            for (int i = 1; i <= _popularPages; i++)
            {
                var popularJson = await _movieDbApi.GetPopular(i);
                foreach (var result in popularJson.Results)
                {
                    _importSeriesIds.Add(result.Id);
                }
            }
            foreach (var seriesId in _importSeriesIds)
            {
                var creditsJson = await _movieDbApi.GetCredits(seriesId);
                if (creditsJson != null)
                {
                    foreach (var cast in creditsJson.CastJson)
                    {
                        if (cast.Id != null && !_importPersonsIds.Contains(cast.Id.Value))
                        {
                            _importPersonsIds.Add(cast.Id.Value);
                        }
                    }
                    foreach (var crew in creditsJson.CrewJson)
                    {
                        if (crew.Id != null && !_importPersonsIds.Contains(crew.Id.Value))
                        {
                            _importPersonsIds.Add(crew.Id.Value);
                        }
                    }
                }
            }
        }

        private async Task InitializeFromDb()
        {
            _importSeriesIds = await _tvSeriesContext
                .Series
                .Where(x => x.IsImportEnabled)
                .OrderByDescending(x => x.MovieDbId)
                .Select(x => x.MovieDbId.Value)
                .ToListAsync();

            _importPersonsIds = await _tvSeriesContext
                .Persons
                .Where(x => x.IsImportEnabled)
                .OrderByDescending(x => x.MovieDbId)
                .Select(x => x.MovieDbId.Value)
                .ToListAsync();
        }

        private void InitializeFromConfig()
        {
            var seriesMinIndex = Convert.ToInt32(_configuration.GetSection("ImportWorker").GetSection("MovieDb").GetSection("SeriesIdMinIndex").Value);
            var seriesMaxIndex = Convert.ToInt32(_configuration.GetSection("ImportWorker").GetSection("MovieDb").GetSection("SeriesIdMaxIndex").Value);

            var personsMinIndex = Convert.ToInt32(_configuration.GetSection("ImportWorker").GetSection("MovieDb").GetSection("PersonsIdMinIndex").Value);
            var personsMaxIndex = Convert.ToInt32(_configuration.GetSection("ImportWorker").GetSection("MovieDb").GetSection("PersonsIdMaxIndex").Value);

            for (long i = seriesMinIndex; i <= seriesMaxIndex; i++)
                _importSeriesIds.Add(i);

            for (long i = personsMinIndex; i <= personsMaxIndex; i++)
                _importPersonsIds.Add(i);

            // Cannot take it from config
            _importSeasonsIds = null;
        }

        private async Task GetSeasonDetailsToImport()
        {
            var seriesIdsFromDb = await _tvSeriesContext
                .Series
                .Where(x => x.IsImportEnabled)
                .OrderByDescending(x => x.Id)
                .Select(x => x.Id)
                .ToListAsync();

            foreach (var seriesId in seriesIdsFromDb)
            {
                var seasons = await _tvSeriesContext
                    .Seasons
                    .Where(x => x.SeriesId == seriesId)
                    .ToListAsync();

                // TODO optimize this? Do I really need to fetch every series separately?
                var movieDbSeriesId = await _tvSeriesContext
                    .Series
                    .Where(x => x.Id == seriesId)
                    .Select(x => x.MovieDbId)
                    .FirstOrDefaultAsync();

                foreach (var season in seasons)
                {
                    if (season.SeasonNumber != null && season.MovieDbId != null)
                    {
                        if (movieDbSeriesId != null)
                        {
                            var seasonWithSeries = new SeasonWithSeries
                            {
                                SeasonId = season.Id,
                                MovieDbSeriesId = movieDbSeriesId.Value,
                                SeriesId = seriesId,
                                SeasonNumber = season.SeasonNumber.Value
                            };
                            _importSeasonsIds.Add(seasonWithSeries);
                        }
                    }
                }
            }
        }

        private async Task ImportSeriesDetails()
        {
            for (var index = 0; index < _importSeriesIds.Count; index++)
            {
                var seriesId = _importSeriesIds.ElementAt(index);

                var seriesJson = await _movieDbApi.GetSeriesDetails(seriesId);

                if (seriesJson != null)
                {
                    var seriesFromImport = await _movieDbDomainConverter.ConvertToSeries(seriesJson);

                    var seriesFromDb = await _tvSeriesContext
                        .Series
                        .Where(x => x.MovieDbId == seriesId)
                        .FirstOrDefaultAsync();

                    var poster = await _movieDbApi.GetImage(seriesFromImport.PosterName);

                    seriesFromImport.PosterContent = poster;

                    await _movieDbImportServiceDbHelper.AddOrUpdate(seriesFromDb, seriesFromImport);

                    // Import seasons too because it is in seriesJson already
                    // get series id for season

                    var seriesDbId = await _tvSeriesContext
                        .Series
                        .Where(x => x.MovieDbId == seriesId)
                        .Select(x => x.Id)
                        .FirstOrDefaultAsync();

                    var seasons = _movieDbDomainConverter.ConvertToSeason(seriesJson, seriesDbId);

                    foreach (var seasonFromImport in seasons)
                    {
                        var seasonFromDb = await _tvSeriesContext
                            .Seasons
                            .Where(x => x.MovieDbId == seasonFromImport.MovieDbId)
                            .FirstOrDefaultAsync();

                        await _movieDbImportServiceDbHelper.AddOrUpdate(seasonFromDb, seasonFromImport);
                    }
                }
                else
                {
                    _logger.LogInformation("Series with MovieDbId [{0}] not found", seriesId);
                }
            }
        }

        private async Task ImportPersonsDetails()
        {
            for (var index = 0; index < _importPersonsIds.Count; index++)
            {
                var personId = _importPersonsIds.ElementAt(index);

                var personJson = await _movieDbApi.GetPersonDetails(personId);

                if (personJson != null)
                {
                    var personFromImport = _movieDbDomainConverter.ConvertToPerson(personJson);

                    var poster = await _movieDbApi.GetImage(personFromImport.PosterName);

                    personFromImport.PosterContent = poster;

                    var personFromDb = await _tvSeriesContext
                        .Persons
                        .Where(x => x.MovieDbId == personId)
                        .FirstOrDefaultAsync();


                    await _movieDbImportServiceDbHelper.AddOrUpdate(personFromDb, personFromImport);
                }
                else
                {
                    _logger.LogInformation("Person with MovieDbId [{0}] not found", personId);
                }
            }

        }

        // It is actually importing all episodes - seasons are already imported with series 
        private async Task ImportSeasonDetails()
        {
            for (var index = 0; index < _importSeasonsIds.Count; index++)
            {
                var seasonWithSeries = _importSeasonsIds.ElementAt(index);

                var seasonDetailsJson = await _movieDbApi.GetSeason(seasonWithSeries.MovieDbSeriesId, seasonWithSeries.SeasonNumber);

                if (seasonDetailsJson != null)
                {
                    var episodes = _movieDbDomainConverter.ConvertToEpisode(seasonDetailsJson, seasonWithSeries.SeasonId);

                    foreach (var episodeFromImport in episodes)
                    {
                        var episodeFromDb = await _tvSeriesContext
                                .Episodes
                                .Where(x => x.MovieDbId == episodeFromImport.MovieDbId)
                                .FirstOrDefaultAsync();

                        await _movieDbImportServiceDbHelper.AddOrUpdate(episodeFromDb, episodeFromImport);
                    }
                }
                else
                {
                    _logger.LogInformation("Season with SeriesId [{0}] and SeasonId [{1}] not found", seasonWithSeries.SeriesId, seasonWithSeries.SeasonId);
                }
            }
        }

        // import characters and crew member
        private async Task ImportSeriesCredits()
        {
            for (var index = 0; index < _importSeriesIds.Count; index++)
            {
                var seriesId = _importSeriesIds.ElementAt(index);

                var creditsJson = await _movieDbApi.GetCredits(seriesId);

                if (creditsJson != null)
                {
                    // import characters
                    foreach (var characterJson in creditsJson.CastJson)
                    {
                        var personFromDb = await _tvSeriesContext
                            .Persons
                            .Where(x => x.MovieDbId == characterJson.Id)
                            .FirstOrDefaultAsync();

                        var seriesDbId = await _tvSeriesContext
                            .Series
                            .Where(x => x.MovieDbId == seriesId)
                            .Select(x => x.Id)
                            .FirstOrDefaultAsync();

                        if (personFromDb != null)
                        {
                            var characterFromImport = _movieDbDomainConverter.ConvertToCharacter(characterJson, seriesDbId,
                                personFromDb.Id);

                            // could be mutliple characters with same person id
                            var charactersFromDb = await _tvSeriesContext
                                .Characters
                                .Where(x => x.PersonId == personFromDb.Id)
                                .ToListAsync();

                            charactersFromDb = charactersFromDb
                                .Where(x => x.SeriesCharacters.Any(y => y.SeriesId == seriesId)).ToList();

                            Character characterFromDb = null;

                            if (charactersFromDb.Count == 1)
                            {
                                characterFromDb = charactersFromDb.FirstOrDefault();
                            }
                            else
                            {
                                _logger.LogInformation("Found more than one character with PersonId [{0}] and seriesId [{1}]", personFromDb.Id, seriesId);
                            }

                            await _movieDbImportServiceDbHelper.AddOrUpdate(characterFromDb, characterFromImport);
                        }
                        else
                        {
                            _logger.LogInformation("Character with MovieDbId [{0}] not found in db", characterJson.Id);
                        }
                    }

                    // import crew
                    foreach (var crewJson in creditsJson.CrewJson)
                    {
                        var personFromDb = await _tvSeriesContext
                            .Persons
                            .Where(x => x.MovieDbId == crewJson.Id)
                            .FirstOrDefaultAsync();

                        var seriesDbId = await _tvSeriesContext
                            .Series
                            .Where(x => x.MovieDbId == seriesId)
                            .Select(x => x.Id)
                            .FirstOrDefaultAsync();

                        if (personFromDb != null)
                        {
                            var crewFromImport = _movieDbDomainConverter.ConvertToCrew(crewJson, seriesDbId,
                                personFromDb.Id);

                            var crewFromDb = await _tvSeriesContext
                                .Crews
                                .Where(x => x.PersonId == personFromDb.Id)
                                .FirstOrDefaultAsync();

                            await _movieDbImportServiceDbHelper.AddOrUpdate(crewFromDb, crewFromImport);
                        }
                        else
                        {
                            _logger.LogInformation("Crew with MovieDbId [{0}] not found in db", crewJson.Id);
                        }
                    }
                }
                else
                {
                    _logger.LogInformation("SeriesId [{0}] with crew not found in db", seriesId);
                }
            }
        }

        private async Task ImportSeriesExternalIds()
        {
            for (var index = 0; index < _importSeriesIds.Count; index++)
            {
                var seriesId = _importSeriesIds.ElementAt(index);

                var seriesExternalIdsJson = await _movieDbApi.GetSeriesExternalIds(seriesId);

                if (seriesExternalIdsJson != null)
                {
                    var seriesFromImport = _movieDbDomainConverter.ConvertToSeriesWithExternalIds(seriesExternalIdsJson);

                    var seriesFromDb = await _tvSeriesContext
                        .Series
                        .Where(x => x.MovieDbId == seriesId)
                        .FirstOrDefaultAsync();

                    await _movieDbImportServiceDbHelper.UpdateSeriesExternalIds(seriesFromDb, seriesFromImport);

                }
                else
                {
                    _logger.LogInformation("Series with MovieDbId [{0}] not found", seriesId);
                }
            }
        }

        private async Task ImportSeriesDetailsFromTvDb()
        {
            // get and set token
            await _tvDbApi.RefreshJwtToken();

            for (var index = 0; index < _importSeriesIds.Count; index++)
            {
                var seriesId = _importSeriesIds.ElementAt(index);

                var tvDbId = await _tvSeriesContext
                    .Series
                    .Where(x => x.MovieDbId == seriesId)
                    .Select(x => x.TvDbId)
                    .FirstOrDefaultAsync();

                SeriesJson seriesJson = null;

                if (tvDbId != null)
                {
                    seriesJson = await _tvDbApi.GetSeries(tvDbId.Value);
                }

                if (seriesJson != null)
                {
                    var seriesFromImport = _tvDbDomainConverter.ConvertToSeriesRuntimeAndBroadcast(seriesJson);

                    var seriesFromDb = await _tvSeriesContext
                        .Series
                        .Where(x => x.MovieDbId == seriesId)
                        .FirstOrDefaultAsync();

                    await _movieDbImportServiceDbHelper.UpdateSeriesRuntimeAndBroadcast(seriesFromDb, seriesFromImport);

                }
                else
                {
                    _logger.LogInformation("Series with TvDbId [{0}] not found", tvDbId);
                }
            }
        }

        #endregion
    }
}
