using System;
using Newtonsoft.Json;

namespace ImportService.TheMovieDb.Api.Json.Entities
{
    public class SeriesDetailsJson
    {
        [JsonProperty("backdrop_path")]
        public string BackdropPath { get; set; }

        [JsonProperty("created_by")]
        public CreatedBy[] CreatedBy { get; set; }

        [JsonProperty("episode_run_time")]
        public long[] EpisodeRunTime { get; set; }

        [JsonProperty("first_air_date")]
        public DateTimeOffset? FirstAirDate { get; set; }

        [JsonProperty("genres")]
        public GenreJson[] GenresJson { get; set; }

        [JsonProperty("homepage")]
        public string Homepage { get; set; }

        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("in_production")]
        public bool? InProduction { get; set; }

        [JsonProperty("languages")]
        public string[] Languages { get; set; }

        [JsonProperty("last_air_date")]
        public DateTimeOffset? LastAirDate { get; set; }

        [JsonProperty("last_episode_to_air")]
        public LastEpisodeToAir LastEpisodeToAir { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("next_episode_to_air")]
        public object NextEpisodeToAir { get; set; }

        [JsonProperty("networks")]
        public Network[] Networks { get; set; }

        [JsonProperty("number_of_episodes")]
        public long? NumberOfEpisodes { get; set; }

        [JsonProperty("number_of_seasons")]
        public long? NumberOfSeasons { get; set; }

        [JsonProperty("origin_country")]
        public string[] OriginCountry { get; set; }

        [JsonProperty("original_language")]
        public string OriginalLanguage { get; set; }

        [JsonProperty("original_name")]
        public string OriginalName { get; set; }

        [JsonProperty("overview")]
        public string Overview { get; set; }

        [JsonProperty("popularity")]
        public double? Popularity { get; set; }

        [JsonProperty("poster_path")]
        public string PosterPath { get; set; }

        [JsonProperty("production_companies")]
        public Network[] ProductionCompanies { get; set; }

        [JsonProperty("seasons")]
        public SeasonJson[] SeasonsJson { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("vote_average")]
        public double? VoteAverage { get; set; }

        [JsonProperty("vote_count")]
        public long? VoteCount { get; set; }
    }

    public class CreatedBy
    {
        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("credit_id")]
        public string CreditId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("gender")]
        public long? Gender { get; set; }

        [JsonProperty("profile_path")]
        public string ProfilePath { get; set; }
    }

    public class GenreJson
    {
        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class LastEpisodeToAir
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
        public long? VoteAverage { get; set; }

        [JsonProperty("vote_count")]
        public long? VoteCount { get; set; }
    }

    public class Network
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("logo_path")]
        public string LogoPath { get; set; }

        [JsonProperty("origin_country")]
        public string OriginCountry { get; set; }
    }

    public class SeasonJson
    {
        [JsonProperty("air_date")]
        public DateTimeOffset? AirDate { get; set; }

        [JsonProperty("episode_count")]
        public long? EpisodeCount { get; set; }

        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("overview")]
        public string Overview { get; set; }

        [JsonProperty("poster_path")]
        public string PosterPath { get; set; }

        [JsonProperty("season_number")]
        public long? SeasonNumber { get; set; }
    }
}