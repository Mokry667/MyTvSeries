using System;
using Dynamitey;
using Microsoft.Extensions.Logging;
using MyTvSeries.Domain.Entities;
using MyTvSeries.Domain.Entities.Base;
using MyTvSeries.Framework;

namespace ImportService.Worker.MovieDb
{
    public class MovieDbMapper : IMovieDbMapper
    {
        #region Properties

        private readonly ILogger<IMovieDbMapper> _logger;
        private readonly ITypeCaster _typeCaster;

        #endregion

        #region Ctors

        public MovieDbMapper(ILogger<IMovieDbMapper> logger, ITypeCaster typeCaster)
        {
            _logger = logger;
            _typeCaster = typeCaster;
        }

        #endregion

        #region Public methods

        public void MapFromImportToDb(BaseEntity entityFromDb, BaseEntity entityFromImport)
        {
            var entityFromDbDowncast = _typeCaster.CastToDerivedType(entityFromDb);
            var entityFromImportDowncast = _typeCaster.CastToDerivedType(entityFromImport);
            MapProperties(entityFromDbDowncast, entityFromImportDowncast);
        }

        public Series MapSeriesExternalIdsFromImportToSeriesFromDb(Series seriesFromDb, Series seriesFromImport)
        {
            seriesFromDb.TvDbId = seriesFromImport.TvDbId;
            seriesFromDb.ImdbId = seriesFromImport.ImdbId;

            return seriesFromDb;
        }

        public Series MapSeriesBroadcastAndRuntimeFromImportToSeriesFromDb(Series seriesFromDb, Series seriesFromImport)
        {
            seriesFromDb.AirTime = seriesFromImport.AirTime;
            seriesFromDb.AirDayOfWeek = seriesFromImport.AirDayOfWeek;

            seriesFromDb.EpisodeRuntime = seriesFromImport.EpisodeRuntime;

            return seriesFromDb;
        }

        #endregion

        #region Private methods

        private void MapProperties(Series seriesFromDb, Series seriesFromImport)
        {
            seriesFromDb.Name = seriesFromImport.Name;
            seriesFromDb.OriginalName = seriesFromImport.OriginalName;
            seriesFromDb.Overview = seriesFromImport.Overview;
            seriesFromDb.Status = seriesFromImport.Status;
            seriesFromDb.AiredFrom = seriesFromImport.AiredFrom;
            seriesFromDb.AiredTo = seriesFromImport.AiredTo;
            seriesFromDb.NumberOfSeasons = seriesFromImport.NumberOfSeasons;
            seriesFromDb.NumberOfEpisodes = seriesFromImport.NumberOfEpisodes;

            // TODO maybe only in initial import (?)
            seriesFromDb.PosterName = seriesFromImport.PosterName;
            seriesFromDb.PosterContent = seriesFromImport.PosterContent;
        }

        private void MapProperties(Person personFromDb, Person personFromImport)
        {
            personFromDb.Name = personFromImport.Name;
            personFromDb.Gender = personFromImport.Gender;
            personFromDb.Biography = personFromImport.Biography;
            personFromDb.Birthday = personFromImport.Birthday;
            personFromDb.Deathday = personFromImport.Deathday;
            personFromDb.PlaceOfBirth = personFromImport.PlaceOfBirth;

            // TODO maybe only in initial import (?)
            personFromDb.PosterName = personFromImport.PosterName;
            personFromDb.PosterContent = personFromImport.PosterContent;
        }

        private void MapProperties(Season seasonFromDb, Season seasonFromImport)
        {
            seasonFromDb.Name = seasonFromImport.Name;
            seasonFromDb.Overview = seasonFromImport.Overview;
            seasonFromDb.SeasonNumber = seasonFromImport.SeasonNumber;
            seasonFromDb.AiredFrom = seasonFromImport.AiredFrom;
            seasonFromDb.NumberOfEpisodes = seasonFromImport.NumberOfEpisodes;
        }

        private void MapProperties(Episode episodeFromDb, Episode episodeFromImport)
        {
            episodeFromDb.Name = episodeFromImport.Name;
            episodeFromDb.Overview = episodeFromImport.Overview;
            episodeFromDb.Aired = episodeFromImport.Aired;
            episodeFromDb.SeasonNumber = episodeFromImport.SeasonNumber;
            episodeFromDb.SeasonEpisodeNumber = episodeFromImport.SeasonEpisodeNumber;
            episodeFromDb.AbsoluteEpisodeNumber = episodeFromImport.SeasonNumber * episodeFromImport.SeasonEpisodeNumber;
        }

        private void MapProperties(Character characterFromDb, Character characterFromImport)
        {
            characterFromDb.Name = characterFromImport.Name;
        }

        private void MapProperties(Crew crewFromDb, Crew crewFromImport)
        {
            crewFromDb.Department = crewFromImport.Department;
            crewFromDb.Job = crewFromImport.Job;
        }

        #endregion
    }
}
