using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImportService.TheMovieDb.Api.Json.Entities;
using MyTvSeries.Domain.Entities;
using MyTvSeries.Domain.Enums;
using MyTvSeries.Domain.ManyToMany;

namespace ImportService.TheMovieDb.Converter
{
    public class MovieDbDomainConverter : IMovieDbDomainConverter
    {
        #region Properties

        private readonly IMovieDbDomainDbHelper _movieDbDomainDbHelper;

        #endregion

        #region Ctors

        public MovieDbDomainConverter(IMovieDbDomainDbHelper movieDbDomainDbHelper)
        {
            _movieDbDomainDbHelper = movieDbDomainDbHelper;
        }

        #endregion

        #region Public methods

        public async Task<Series> ConvertToSeries(SeriesDetailsJson seriesJson)
        {
            DateTime? firstAirDateValue = null;
            if (seriesJson.FirstAirDate.HasValue)
                firstAirDateValue = seriesJson.FirstAirDate.Value.DateTime;

            DateTime? lastAirDateValue = null;
            if (seriesJson.LastAirDate.HasValue)
                lastAirDateValue = seriesJson.LastAirDate.Value.DateTime;


            var series = new Series
            {
                MovieDbId = seriesJson.Id,
                Name = seriesJson.Name,
                Overview = seriesJson.Overview,
                Status = ConvertToStatus(seriesJson.Status),
                // TODO check if this date conversion is correct
                AiredFrom = firstAirDateValue,
                AiredTo = lastAirDateValue,
                NumberOfSeasons = Convert.ToInt32(seriesJson.NumberOfSeasons),
                NumberOfEpisodes = Convert.ToInt32(seriesJson.NumberOfEpisodes),
                PosterName = seriesJson.PosterPath
                // TODO episode runtime can have mutliple values
                // TODO many to many mappings
            };

            await ConvertToGenres(series, seriesJson.GenresJson);

            ConvertToRuntimes(series, seriesJson.EpisodeRunTime);

            return series;
        }

        public Person ConvertToPerson(PersonDetailsJson personJson)
        {
            DateTime? birthdayValue = null;
            if (personJson.Birthday.HasValue)
                birthdayValue = personJson.Birthday.Value.DateTime;

            DateTime? deathdayValue = null;
            if (personJson.Deathday.HasValue)
                deathdayValue = personJson.Deathday.Value.DateTime;

            // TODO take known for department from API ?

            var person = new Person
            {
                MovieDbId = personJson.Id,
                Name = personJson.Name,
                Gender = ConvertToGender(personJson.Gender),
                Biography = personJson.Biography,
                Birthday = birthdayValue,
                Deathday = deathdayValue,
                PlaceOfBirth = personJson.PlaceOfBirth,
            };

            return person;
        }

        public List<Season> ConvertToSeason(SeriesDetailsJson seriesJson, long seriesId)
        {
            var seasons = new List<Season>();

            foreach (var seasonJson in seriesJson.SeasonsJson)
            {
                DateTime? airedFromDateValue = null;
                if (seasonJson.AirDate.HasValue)
                    airedFromDateValue = seasonJson.AirDate.Value.DateTime;

                var season = new Season
                {
                    MovieDbId = seasonJson.Id,
                    SeriesId = seriesId,
                    Name = seasonJson.Name,
                    Overview = seasonJson.Overview,
                    SeasonNumber = seasonJson.SeasonNumber,
                    AiredFrom = airedFromDateValue,
                    NumberOfEpisodes = seasonJson.EpisodeCount
                };

                seasons.Add(season);
            }

            return seasons;
        }

        public List<Episode> ConvertToEpisode(SeasonDetailsJson seasonDetailsJson, long seasonId)
        {
            var episodes = new List<Episode>();

            foreach (var episodeJson in seasonDetailsJson.EpisodesJson)
            {
                DateTime? airedDateValue = null;
                if (seasonDetailsJson.AirDate.HasValue)
                    airedDateValue = seasonDetailsJson.AirDate.Value.DateTime;

                var episode = new Episode
                {
                    MovieDbId = episodeJson.Id,
                    SeasonId = seasonId,
                    Name = episodeJson.Name,
                    Overview = episodeJson.Overview,
                    Aired = airedDateValue,
                    SeasonEpisodeNumber = episodeJson.EpisodeNumber,
                    SeasonNumber = episodeJson.SeasonNumber,
                    AbsoluteEpisodeNumber = episodeJson.SeasonNumber * episodeJson.EpisodeNumber
                };

                episodes.Add(episode);
            }

            return episodes;
        }

        public Character ConvertToCharacter(CastJson characterJson, long seriesId, long personId)
        {
            var character = new Character
            {
                PersonId = personId,
                Name = characterJson.Character
            };

            character = ConvertToSeriesCharacters(character, seriesId);

            return character;
        }

        public Crew ConvertToCrew(CrewJson crewJson, long seriesId, long personId)
        {
            var crew = new Crew
            {
                PersonId = personId,
                Department = crewJson.Department,
                Job = crewJson.Job,
                SeriesId = seriesId
            };

            return crew;
        }

        #endregion

        #region Private methods

        private SeriesStatus ConvertToStatus(string status)
        {
            switch (status)
            {
                // TODO handle all cases
                case "Ended":
                    return SeriesStatus.Finished;
                default:
                    return SeriesStatus.Unknown;
            }
        }

        private async Task ConvertToGenres(Series series, GenreJson[] genresJson)
        {
            series.SeriesGenres = new List<SeriesGenres>();
            foreach (var genre in genresJson)
            {

                var genreFromDb = await _movieDbDomainDbHelper.GetOrAddGenre(genre);
                var seriesGenres = new SeriesGenres
                {
                    Series = series,
                    Genre = genreFromDb
                };

                // search for duplicates
                if (series.SeriesGenres.Any(x => x.Series == seriesGenres.Series && x.Genre == seriesGenres.Genre))
                {
                    // TODO LOG THIS SHIT
                    continue;
                }

                series.SeriesGenres.Add(seriesGenres);
            }
        }

        private Gender ConvertToGender(long? gender)
        {
            switch (gender)
            {
                case 1:
                    return Gender.Female;
                case 2:
                    return Gender.Male;
                default:
                    return Gender.Unknown;
            }
        }

        private Character ConvertToSeriesCharacters(Character character, long seriesId)
        {
            character.SeriesCharacters = new List<SeriesCharacters>();
            var seriesCharacters = new SeriesCharacters
            {
                Character = character,
                SeriesId = seriesId
            };

            // search for duplicates
            if (character.SeriesCharacters.Any(x => x.Character == seriesCharacters.Character 
            && x.SeriesId == seriesCharacters.SeriesId))
            {
                // TODO LOG THIS SHIT
                return character;
            }

            character.SeriesCharacters.Add(seriesCharacters);

            return character;
        }

        private void ConvertToRuntimes(Series series, long[] episodeRuntimes)
        {
            series.EpisodeRuntimes = new List<EpisodeRuntime>();
            foreach (var runtime in episodeRuntimes)
            {
                var episodeRuntime = new EpisodeRuntime
                {
                    Series = series,
                    RuntimeInMinutes = runtime
                };

                if (series.EpisodeRuntimes.Any(x =>
                    x.Series == episodeRuntime.Series && x.RuntimeInMinutes == episodeRuntime.RuntimeInMinutes))
                {
                    //TODO LOG THIS SHIT
                    continue;
                }

                series.EpisodeRuntimes.Add(episodeRuntime);
            }
        }

        #endregion

    }
}
