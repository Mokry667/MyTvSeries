using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ImportService.TheTvDb.Api.Json.Authentication;
using ImportService.TheTvDb.Api.Json.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ImportService.TheTvDb.Api
{
    public class TvDbApi : ITvDbApi
    {
        #region Fields

        private readonly ILogger<ITvDbApi> _logger;

        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _username;
        private readonly string _userkey;
        private JwtTokenJson _token;
        private bool _isTokenFresh;

        #endregion

        #region Ctors

        public TvDbApi(ILogger<ITvDbApi> logger, IConfiguration configuration)
        {
            _logger = logger;
            _apiKey = configuration["TvDbApi:ApiKey"];
            var apiPrefix = configuration.GetSection("TvDbApi").GetSection("ApiPrefix").Value;
            _username = configuration["TvDbApi:Username"];
            _userkey = configuration["TvDbApi:UserKey"];
            _httpClient = new HttpClient { BaseAddress = new Uri(apiPrefix) };
            _token = null;
        }

        #endregion

        #region Public methods

        public bool IsTokenFresh()
            => _isTokenFresh;

        public async Task RefreshJwtToken()
        {
            _token = await GetJwtToken();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token.Token);
            _isTokenFresh = true;
        }

        // /login
        public async Task<JwtTokenJson> GetJwtToken()
        {
            var authenticationJson = new AuthenticationJson(_apiKey, _username, _userkey);
            var serializedJson = JsonConvert.SerializeObject(authenticationJson);
            var response = await _httpClient.PostAsync("/login",
                new StringContent(serializedJson, Encoding.UTF8, "application/json"));
            var responseString = await response.Content.ReadAsStringAsync();
            var token = JsonConvert.DeserializeObject<JwtTokenJson>(responseString);
            return token;
        }

        // /series/{id}/
        public async Task<SeriesJson> GetSeries(long seriesId)
        {
            var request = "/series/" + seriesId;

            var response = await GetResponse(request);

            if (response == null) return null;

            var errorsJson = GetErrors(response);
            if (errorsJson == null)
            {
                return JObject.Parse(response.ToString()).SelectToken("data").ToObject<SeriesJson>();
            }
            _logger.LogInformation("Series with id [{0}] has following errors [{1}]", seriesId, errorsJson.ToString());
            return null;
        }


        // /series/{id}/actors
        public async Task <IEnumerable<SeriesActorJson>> GetSeriesActors(long seriesId)
        {
            var request = "/series/" + seriesId + "/actors";

            var response =  await GetResponse(request);
            var errorsJson = GetErrors(response);
            if (errorsJson == null)
            {
                return JObject.Parse(response.ToString()).SelectToken("data").ToObject<IEnumerable<SeriesActorJson>>();
            }
            _logger.LogInformation("Series with id [{0}] has following errors [{1}]", seriesId, errorsJson.ToString());
            return null;
        }

        // /series/{id}/episodes
        public async Task<object> GetSeriesEpisodes(long seriesId)
        {
            var request = "/series/" + seriesId + "/episodes";

            //var response = await GetResponse(request);

            return await GetResponse(request);
        }

        // /series/{id}/episodes/query?airedSeason=
        public async Task<object> GetSeriesEpisodes(long seriesId, int seasonNumber)
        {
            var request = "/series/" + seriesId + "/episodes/query?airedSeason=" + seasonNumber;

            return await GetResponse(request);
        }

        // /series/{id}/episodes/query?airedEpisode=
        public async Task<object> GetSeriesEpisode(long seriesId, int episodeNumber)
        {
            var request = "/series/" + seriesId + "/episodes/query?airedEpisode=" + episodeNumber;

            return await GetResponse(request);
        }

        // /episodes/{id}/
        public async Task<object> GetEpisode(long episodeId)
        {
            var request = "/episodes/" + episodeId;

            return await GetResponse(request);
        }

        // updated/query
        public async Task<object> GetUpdated(string fromTime, string toTime)
        {
            var request = "/updated/query/?fromTime=" + fromTime + "&toTime=" + toTime;

            return await GetResponse(request);
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
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                _isTokenFresh = false;
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

        private JToken GetErrors(object response)
            => JObject.Parse(response.ToString()).SelectToken("errors");

        #endregion
    }
}
