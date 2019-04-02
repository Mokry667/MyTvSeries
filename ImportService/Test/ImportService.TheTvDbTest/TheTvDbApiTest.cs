using System;
using System.Linq;
using ImportService.TheTvDb.Api;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Xunit;

namespace ImportService.TheTvDbTest
{
    public class TheTvDbApiTest : IDisposable
    {
        private readonly TvDbApi _api;

        public TheTvDbApiTest()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .AddJsonFile("testAppSettings.json", true, true)
                .AddUserSecrets<TheTvDbApiTest>();

            IConfiguration configuration = builder.Build();
            ILoggerFactory loggerFactory = new LoggerFactory();

            var logger = new Logger<ITvDbApi>(loggerFactory);

            _api = new TvDbApi(logger, configuration);
        }

        public void Dispose() {}

        [Fact]
        public async void ShouldGetSupergirlSeries()
        {
            await _api.RefreshJwtToken();
            var seriesJson = await _api.GetSeries(295759);
            Assert.Equal("Supergirl", seriesJson.SeriesName);
        }

        [Fact]
        public async void ShouldGetAllSupergirlSeriesActors()
        {
            await _api.RefreshJwtToken();
            var seriesActorsJson = await _api.GetSeriesActors(295759);
            Assert.Equal(14, seriesActorsJson.Count());
        }
    }
}
