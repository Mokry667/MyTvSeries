/*
using MyTvSeries.TheTvDb;
using MyTvSeries.TheTvDb.Json;
using Xunit;

namespace MyTvSeries.TheTvDbTest
{
    public class TheTvDbApiTest
    {
        [Fact]
        public async void ShouldGetSupergirlSeries()
        {
            var api = new TvDbApi();
            await api.RefreshJwtToken();
            var seriesJson = await api.GetSeries(295759);
            Assert.NotNull(seriesJson);
        }

        [Fact]
        public async void ShouldGetSupergirlSeriesActors()
        {
            var api = new TvDbApi();
            await api.RefreshJwtToken();
            var seriesActorsJson = await api.GetSeriesActors(295759);
            Assert.NotNull(seriesActorsJson);
        }

        [Fact]
        public async void ShouldGetSeriesAndConvertToDbForm()
        {
            var api = new TvDbApi();
            await api.RefreshJwtToken();
            var seriesJson = await api.GetSeries(295759);
            IDomainConverter domainConverter = new DomainConverter();
            var series = domainConverter.ConvertToSeries(seriesJson);
            Assert.NotNull(series);      
        }
    }
}
*/
