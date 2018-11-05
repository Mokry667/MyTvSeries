using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ImportService.TheMovieDb.Api.Json.Entities
{
    public class SeriesPopularJson
    {
        [JsonProperty("page", NullValueHandling = NullValueHandling.Ignore)]
        public long? Page { get; set; }

        [JsonProperty("total_results", NullValueHandling = NullValueHandling.Ignore)]
        public long? TotalResults { get; set; }

        [JsonProperty("total_pages", NullValueHandling = NullValueHandling.Ignore)]
        public long? TotalPages { get; set; }

        [JsonProperty("results", NullValueHandling = NullValueHandling.Ignore)]
        public Result[] Results { get; set; }

        public partial class Result
        {
            [JsonProperty("original_name")]
            public string OriginalName { get; set; }

            [JsonProperty("genre_ids")]
            public long[] GenreIds { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("popularity")]
            public double? Popularity { get; set; }

            [JsonProperty("origin_country")]
            public string[] OriginCountry { get; set; }

            [JsonProperty("vote_count")]
            public long? VoteCount { get; set; }

            [JsonProperty("first_air_date")]
            public DateTimeOffset? FirstAirDate { get; set; }

            [JsonProperty("backdrop_path")]
            public string BackdropPath { get; set; }

            [JsonProperty("original_language")]
            public string OriginalLanguage { get; set; }

            [JsonProperty("id")]
            public long Id { get; set; }

            [JsonProperty("vote_average")]
            public double? VoteAverage { get; set; }

            [JsonProperty("overview")]
            public string Overview { get; set; }

            [JsonProperty("poster_path")]
            public string PosterPath { get; set; }
        }
    }
}
