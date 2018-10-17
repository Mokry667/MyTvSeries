﻿using Newtonsoft.Json;

namespace ImportService.TheMovieDb.Api.Json.Entities
{
    public class PersonDetailsIdsJson
    {
        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("twitter_id")]
        public string TwitterId { get; set; }

        [JsonProperty("facebook_id")]
        public string FacebookId { get; set; }

        [JsonProperty("tvrage_id")]
        public long? TvrageId { get; set; }

        [JsonProperty("instagram_id")]
        public string InstagramId { get; set; }

        [JsonProperty("freebase_mid")]
        public string FreebaseMid { get; set; }

        [JsonProperty("imdb_id")]
        public string ImdbId { get; set; }

        [JsonProperty("freebase_id")]
        public string FreebaseId { get; set; }
    }
}