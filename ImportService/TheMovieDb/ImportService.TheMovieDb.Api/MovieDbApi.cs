using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ImportService.TheMovieDb.Api.Json.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ImportService.TheMovieDb.Api
{
    public class MovieDbApi : IMovieDbApi
    {
        #region Fields

        private readonly ILogger<IMovieDbApi> _logger;

        private readonly HttpClient _httpClient;

        private readonly string _apiSuffix;
        private readonly string _apiVersion;

        private string _imageApiPrefix;

        #endregion

        #region Ctors

        public MovieDbApi(ILogger<IMovieDbApi> logger, IConfiguration configuration)
        {
            _logger = logger;
            var apiKey = configuration["MovieDbApi:ApiKey"];
            var apiPrefix = configuration.GetSection("MovieDbApi").GetSection("ApiPrefix").Value;
            _apiVersion = configuration.GetSection("MovieDbApi").GetSection("ApiVersion").Value;
            var apiLanguage = configuration.GetSection("MovieDbApi").GetSection("ApiLanguage").Value;
            _httpClient = new HttpClient { BaseAddress = new Uri(apiPrefix) };
            _apiSuffix = "?api_key=" + apiKey + "&language=" + apiLanguage;
        }

        #endregion

        public async Task SetUpImageApi()
        {
            var imageConfiguration = await GetConfiguration();

            _imageApiPrefix = imageConfiguration.Images.SecureBaseUrl;
            // poster_size
            _imageApiPrefix = _imageApiPrefix + "/w185/";
        }

        #region Images

        public async Task<ConfigurationJson> GetConfiguration()
        {
            var request = "/configuration";
            request = AddApiVersion(request);
            request = AddApiKey(request);

            var response = await GetResponse(request);
            ConfigurationJson configurationJson = null;
            if (response != null)
            {
                configurationJson = JsonConvert.DeserializeObject<ConfigurationJson>(response.ToString());
            }

            return configurationJson;
        }

        public async Task<byte[]> GetImage(string imagePath)
        {
            var request = _imageApiPrefix + imagePath;

            var response = await GetImageResponse(request);

            return response;
        }

        #endregion

        #region Series

        // tv/{id}/
        public async Task<SeriesDetailsJson> GetSeriesDetails(long seriesId)
        {
            var request = "/tv/" + seriesId;
            request = AddApiVersion(request);
            request = AddApiKey(request);

            var response = await GetResponse(request);

            SeriesDetailsJson seriesDetailsJson = null;
            if (response != null)
            {
                seriesDetailsJson = JsonConvert.DeserializeObject<SeriesDetailsJson>(response.ToString());
            }

            return seriesDetailsJson;
        }

        // tv/{id}/external_ids
        public async Task<SeriesExternalIdsJson> GetSeriesExternalIds(long seriesId)
        {
            var request = "/tv/" + seriesId + "/external_ids";
            request = AddApiVersion(request);
            request = AddApiKey(request);


            var response = await GetResponse(request);

            SeriesExternalIdsJson seriesExternalIdsJson = null;
            if (response != null)
            {
                seriesExternalIdsJson = JsonConvert.DeserializeObject<SeriesExternalIdsJson>(response.ToString());
            }

            return seriesExternalIdsJson;
        }

        // tv/popular
        public async Task<SeriesPopularJson> GetPopular(int page)
        {
            var request = "/tv/popular";
            request = AddApiVersion(request);
            request = AddApiKey(request);
            request = request + "&page=" + page;

            var response = await GetResponse(request);

            SeriesPopularJson seriesPopularJson = null;
            if (response != null)
            {
                seriesPopularJson = JsonConvert.DeserializeObject<SeriesPopularJson>(response.ToString());
            }

            return seriesPopularJson;
        }

        #endregion

        #region Persons

        // person/{id}/
        public async Task<PersonDetailsJson> GetPersonDetails(long personId)
        {
            var request = "/person/" + personId;
            request = AddApiVersion(request);
            request = AddApiKey(request);

            var response = await GetResponse(request);

            PersonDetailsJson seriesDetailsJson = null;
            if (response != null)
            {
                seriesDetailsJson = JsonConvert.DeserializeObject<PersonDetailsJson>(response.ToString());
            }

            return seriesDetailsJson;
        }

        // person/{id}/external_ids
        public async Task<PersonDetailsIdsJson> GetPersonExternalIds(long personId)
        {
            var request = "/person/" + personId + "/external_ids";
            request = AddApiVersion(request);
            request = AddApiKey(request);

            var response = await GetResponse(request);

            PersonDetailsIdsJson personDetailsIdsJson = null;
            if (response != null)
            {
                personDetailsIdsJson = JsonConvert.DeserializeObject<PersonDetailsIdsJson>(response.ToString());
            }

            return personDetailsIdsJson;
        }

        #endregion

        #region GenresListJson

        // genre/tv/list
        public async Task<GenresListJson> GetGenres()
        {
            var request = "/genre/tv/list";
            request = AddApiVersion(request);
            request = AddApiKey(request);

            var response = await GetResponse(request);

            GenresListJson genresJson = null;
            if (response != null)
            {
                genresJson = JsonConvert.DeserializeObject<GenresListJson>(response.ToString());
            }

            return genresJson;
        }

        #endregion

        #region Seasons

        // tv/{id}/season/{number}
        public async Task<SeasonDetailsJson> GetSeason(long seriesId, long seasonNumber)
        {
            var request = "/tv/" + seriesId + "/season/" + seasonNumber;
            request = AddApiVersion(request);
            request = AddApiKey(request);

            var response = await GetResponse(request);
            SeasonDetailsJson seasonJson = null;
            if (response != null)
            {
                seasonJson = JsonConvert.DeserializeObject<SeasonDetailsJson>(response.ToString());
            }

            return seasonJson;
        }

        // tv/{id}/credits
        public async Task<SeriesCreditsJson> GetCredits(long seriesId)
        {
            var request = "/tv/" + seriesId + "/credits";
            request = AddApiVersion(request);
            request = AddApiKey(request);

            var response = await GetResponse(request);
            SeriesCreditsJson seriesCreditsJson = null;
            if (response != null)
            {
                seriesCreditsJson = JsonConvert.DeserializeObject<SeriesCreditsJson>(response.ToString());
            }

            return seriesCreditsJson;
        }

        #endregion

        #region Private methods

        private async Task<object> GetResponse(string request)
        {
            var response = await _httpClient.GetAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            // try again after some time
            if (response.StatusCode == HttpStatusCode.TooManyRequests)
            {
                System.Threading.Thread.Sleep(5000);
                response = await _httpClient.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
            }
            return null;
        }

        private async Task<byte[]> GetImageResponse(string request)
        {
            var response = await _httpClient.GetAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsByteArrayAsync();
            }
            return null;
        }

        private string AddApiVersion(string request)
            => _apiVersion + request;

        private string AddApiKey(string request) 
            => request + _apiSuffix;

        #endregion
    }
}
