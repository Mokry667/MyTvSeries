using Microsoft.Extensions.Logging;
using MyTvSeries.Domain.Entities;

namespace ImportService.Worker.MovieDb
{
    public class MovieDbMapper : IMovieDbMapper
    {
        #region Properties

        private readonly ILogger<IMovieDbMapper> _logger;

        #endregion

        #region Ctors

        public MovieDbMapper(ILogger<IMovieDbMapper> logger)
        {
            _logger = logger;
        }

        #endregion

        #region Public methods

        public Series MapSeriesFromImportToSeriesFromDb(Series seriesFromDb, Series seriesFromImport)
        {
            seriesFromDb.Name = seriesFromImport.Name;
            seriesFromDb.Overview = seriesFromImport.Overview;
            seriesFromDb.Status = seriesFromImport.Status;
            seriesFromDb.AiredFrom = seriesFromImport.AiredFrom;
            seriesFromDb.AiredTo = seriesFromImport.AiredTo;
            seriesFromDb.NumberOfSeasons = seriesFromImport.NumberOfSeasons;
            seriesFromDb.NumberOfEpisodes = seriesFromImport.NumberOfEpisodes;

            // TODO maybe only in initial import (?)
            seriesFromDb.PosterName = seriesFromImport.PosterName;
            seriesFromDb.PosterContent = seriesFromImport.PosterContent;

            return seriesFromDb;
        }

        public Person MapPersonFromImportToPersonFromDb(Person personFromDb, Person personFromImport)
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

            return personFromDb;
        }

        public Season MapSeasonFromImportToSeasonFromDb(Season seasonFromDb, Season seasonFromImport)
        {
            seasonFromDb.Name = seasonFromImport.Name;
            seasonFromDb.Overview = seasonFromImport.Overview;
            seasonFromDb.SeasonNumber = seasonFromImport.SeasonNumber;
            seasonFromDb.AiredFrom = seasonFromImport.AiredFrom;
            seasonFromDb.NumberOfEpisodes = seasonFromImport.NumberOfEpisodes;

            return seasonFromDb;
        }

        public Episode MapEpisodeFromImportToEpisodeFromDb(Episode episodeFromDb, Episode episodeFromImport)
        {
            episodeFromDb.Name = episodeFromImport.Name;
            episodeFromDb.Overview = episodeFromImport.Overview;
            episodeFromDb.Aired = episodeFromImport.Aired;
            episodeFromDb.SeasonNumber = episodeFromImport.SeasonNumber;
            episodeFromDb.SeasonEpisodeNumber = episodeFromImport.SeasonEpisodeNumber;
            episodeFromDb.AbsoluteEpisodeNumber =
                episodeFromImport.SeasonNumber * episodeFromImport.SeasonEpisodeNumber;

            return episodeFromDb;
        }

        public Character MapCharacterFromImportToCharacterFromDb(Character characterFromDb, Character characterFromImport)
        {
            characterFromDb.Name = characterFromImport.Name;

            return characterFromDb;
        }

        public Crew MapCrewFromImportToCrewFromDb(Crew crewFromDb, Crew crewFromImport)
        {
            crewFromDb.Department = crewFromImport.Department;
            crewFromDb.Job = crewFromImport.Job;

            return crewFromDb;
        }

        #endregion

        #region Private methods



        #endregion
    }
}
