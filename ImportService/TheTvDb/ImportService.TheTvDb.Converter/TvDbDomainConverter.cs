using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ImportService.TheTvDb.Api.Json.Entities;
using MyTvSeries.Domain.Entities;
using MyTvSeries.Domain.Enums;
using MyTvSeries.Domain.ManyToMany;

namespace ImportService.TheTvDb.Converter
{
    // TODO move to mapper?
    public class TvDbDomainConverter : ITvDbDomainConverter
    {
        // TODO load existing genres from DB

        private readonly ITvDbDomainDbHelper _tvDbDomainDbHelper;

        public TvDbDomainConverter(ITvDbDomainDbHelper tvDbDomainDbHelper)
        {
            _tvDbDomainDbHelper = tvDbDomainDbHelper;
        }

        public async Task<Series> ConvertToSeries(SeriesJson seriesJson)
        {

            DateTime.TryParseExact(seriesJson.FirstAired, "yyyy-MM-dd", 
                CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime convertedAirFrom);

            // convert air time - 2 possible formats
            bool isParsed = DateTime.TryParseExact(seriesJson.AirsTime, "hh:mm tt",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime convertedAirTimeDateTime);

            TimeSpan convertedAirTime = TimeSpan.Zero;

            if (isParsed)
                convertedAirTime = convertedAirTimeDateTime.TimeOfDay;
            else
            {
                isParsed = DateTime.TryParseExact(seriesJson.AirsTime, "h:mm tt",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime convertedAirTimeDateTime2);
                if(isParsed)
                    convertedAirTime = convertedAirTimeDateTime2.TimeOfDay;
            }

            bool isDayOfWeekParsed = Enum.TryParse(seriesJson.AirsDayOfWeek, out DayOfWeek convertedDayOfWeek);

            bool isRuntimeParsed = Decimal.TryParse(seriesJson.Runtime, out decimal convertedRuntime);

            bool isTvDbIdParsed = Int64.TryParse(seriesJson.Id, out long convertedTvDbId);

            var series = new Series
            {
                Name = seriesJson.SeriesName,
                Overview = seriesJson.Overview,
                Status = ConvertToStatus(seriesJson.Status),
                AiredFrom = convertedAirFrom,
                AirTime = convertedAirTime,
                // TODO change logic to uncomment genres
                //Genres = convertedGenres, 
            };

            if (isTvDbIdParsed)
                series.TvDbId = convertedTvDbId;

            if (isDayOfWeekParsed)
                series.AirDayOfWeek = convertedDayOfWeek;

            if (isRuntimeParsed)
                //series.EpisodeRuntime = convertedRuntime;

            await ConvertToGenres(series, seriesJson.Genres);

            return series;
        }

        public Person ConvertToPerson(SeriesActorJson seriesActorJson)
        {
            bool isTvDbIdParsed = Int64.TryParse(seriesActorJson.Id, out long convertedTvDbId);

            var person = new Person();

            if (isTvDbIdParsed)
                person.TvDbId = convertedTvDbId;

            var names = ParseActorNames(seriesActorJson.Name);

            switch (names.Count)
            {
                //TODO Fix
                case 1:
                    //person.FirstName = names.FirstOrDefault();
                    break;
                case 2:
                    //person.FirstName = names.FirstOrDefault();
                    //person.LastName = names.LastOrDefault();
                    break;
                case 3:
                    //person.FirstName = names.FirstOrDefault();
                    //person.MiddleName = names.ElementAtOrDefault(1);
                    //person.LastName = names.LastOrDefault();
                    break;
                default:
                    // TODO log this shit
                    break;
            }

            return person;
        }


        #region Private methods

        private SeriesStatus ConvertToStatus(string status)
        {
            switch (status)
            {
                // TODO handle all cases
                case "Continuing":
                    return SeriesStatus.Airing;
                case "Ended":
                    return SeriesStatus.Finished;
                default:
                    return SeriesStatus.Unknown;
            }
        }

        private async Task ConvertToGenres(Series series, string[] genres)
        {
            series.SeriesGenres = new List<SeriesGenres>();
            foreach (var genreName in genres)
            {
                var genreFromDb = await _tvDbDomainDbHelper.GetGenre(genreName);
                var seriesGenres = new SeriesGenres
                {
                    Series = series,
                    Genre = genreFromDb
                };
                series.SeriesGenres.Add(seriesGenres);
            }
        }

        private List<string> ParseActorNames(string name)
        {
            var splitedNames = Regex
                .Split(name, @"(?<=[\.\s])")
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToList();

            return splitedNames;
        }

        #endregion
    }
}
