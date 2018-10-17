using System;
using Newtonsoft.Json;

namespace ImportService.TheMovieDb.Api.Json.Entities
{
    public class SeasonDetailsJson
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("air_date")]
        public DateTimeOffset? AirDate { get; set; }

        [JsonProperty("episodes")]
        public EpisodeJson[] EpisodesJson { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("overview")]
        public string Overview { get; set; }

        [JsonProperty("id")]
        public long? WelcomeId { get; set; }

        [JsonProperty("poster_path")]
        public string PosterPath { get; set; }

        [JsonProperty("season_number")]
        public long? SeasonNumber { get; set; }
    }

    public class EpisodeJson
    {
        [JsonProperty("air_date")]
        public DateTimeOffset? AirDate { get; set; }

        [JsonProperty("episode_number")]
        public long? EpisodeNumber { get; set; }

        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("overview")]
        public string Overview { get; set; }

        [JsonProperty("production_code")]
        public string ProductionCode { get; set; }

        [JsonProperty("season_number")]
        public long? SeasonNumber { get; set; }

        [JsonProperty("show_id")]
        public long? ShowId { get; set; }

        [JsonProperty("still_path")]
        public string StillPath { get; set; }

        [JsonProperty("vote_average")]
        public double? VoteAverage { get; set; }

        [JsonProperty("vote_count")]
        public long? VoteCount { get; set; }

        [JsonProperty("crew")]
        public SeasonCrewJson[] SeasonCrewJson { get; set; }

        [JsonProperty("guest_stars")]
        public GuestStar[] GuestStars { get; set; }
    }

    public class SeasonCrewJson
    {
        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("credit_id")]
        public string CreditId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("department")]
        public string Department { get; set; }

        [JsonProperty("job")]
        public string Job { get; set; }

        [JsonProperty("gender")]
        public long? Gender { get; set; }

        [JsonProperty("profile_path")]
        public string ProfilePath { get; set; }
    }

    public class GuestStar
    {
        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("credit_id")]
        public string CreditId { get; set; }

        [JsonProperty("character")]
        public string Character { get; set; }

        [JsonProperty("order")]
        public long? Order { get; set; }

        [JsonProperty("gender")]
        public long? Gender { get; set; }

        [JsonProperty("profile_path")]
        public string ProfilePath { get; set; }
    }
}
