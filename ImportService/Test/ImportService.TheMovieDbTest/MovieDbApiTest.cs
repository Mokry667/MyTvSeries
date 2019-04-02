using System;
using ImportService.TheMovieDb.Api;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Xunit;

namespace ImportService.TheMovieDbTest
{
    public class MovieDbApiTest : IDisposable
    {
        // LOST id
        private readonly long _seriesId = 4607;

        // Terry O'Quinn
        private readonly long _personId = 12646;

        private readonly MovieDbApi _api;

        public MovieDbApiTest()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .AddJsonFile("testAppSettings.json", true, true)
                .AddUserSecrets<MovieDbApiTest>();

            IConfiguration configuration = builder.Build();
            ILoggerFactory loggerFactory = new LoggerFactory();

            var logger = new Logger<IMovieDbApi>(loggerFactory);

            _api = new MovieDbApi(logger, configuration);
        }

        public void Dispose() { }

        [Fact]
        public async void GetSeriesDetailsShouldGetLostSeriesForLostSeriesId()
        {
            var seriesJson = await _api.GetSeriesDetails(_seriesId);
            Assert.Equal("Lost", seriesJson.Name);
        }

        [Fact]
        public async void GetSeriesExternalIdsShouldGetTvDbIdForLostSeriesId()
        {
            var seriesExternalIdsJson = await _api.GetSeriesExternalIds(_seriesId);
            Assert.Equal(73739 , seriesExternalIdsJson.TvdbId);
        }

        [Fact]
        public async void GetGenresShouldGetDramaGenre()
        {
            var genresJson = await _api.GetGenres();
            Assert.Contains(genresJson.GenresJson, x => x.Name == "Drama");
        }

        [Fact]
        public async void GetPersonDetailsShouldGetTerryQuinnForTerryQuinnId()
        {
            var personDetailsJson = await _api.GetPersonDetails(_personId);
            Assert.Equal("Terry O'Quinn", personDetailsJson.Name);
        }

        [Fact]
        public async void GetPersonExternalIdsShouldGetImdbIdForTerryQuinn()
        {
            var personExternalIdsJson = await _api.GetPersonExternalIds(_personId);
            Assert.Equal("nm0642368", personExternalIdsJson.ImdbId);
        }

        [Fact]
        public async void GetSeasonDetailsShouldReturnFirstSeasonOfTheLost()
        {
            var seasonDetailsJson = await _api.GetSeason(_seriesId, 1);
            Assert.Equal(1, seasonDetailsJson.SeasonNumber);
        }

        [Fact]
        public async void GetCreditsShouldReturnCreditsOfTheLost()
        {
            var creditsJson = await _api.GetCredits(_seriesId);
            Assert.Contains(creditsJson.CastJson, x => x.Character == "John Locke");
        }

        [Fact]
        public async void GetImageShouldReturnPosterOfTheLost()
        {
            var seriesJson = await _api.GetSeriesDetails(_seriesId);

            var imagePath = seriesJson.BackdropPath;

            await _api.SetUpImageApi();

            var image = await _api.GetImage(imagePath);

            Assert.True(image.Length > 5000);
        }
    }
}
