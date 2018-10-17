using System;
using Newtonsoft.Json;

namespace ImportService.TheMovieDb.Api.Json.Entities
{
    public class PersonDetailsJson
    {
        [JsonProperty("birthday")]
        public DateTimeOffset? Birthday { get; set; }

        [JsonProperty("known_for_department")]
        public string KnownForDepartment { get; set; }

        [JsonProperty("deathday")]
        public DateTimeOffset? Deathday { get; set; }

        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("also_known_as")]
        public string[] AlsoKnownAs { get; set; }

        [JsonProperty("gender")]
        public long? Gender { get; set; }

        [JsonProperty("biography")]
        public string Biography { get; set; }

        [JsonProperty("popularity")]
        public double? Popularity { get; set; }

        [JsonProperty("place_of_birth")]
        public string PlaceOfBirth { get; set; }

        [JsonProperty("profile_path")]
        public string ProfilePath { get; set; }

        [JsonProperty("adult")]
        public bool? Adult { get; set; }

        [JsonProperty("imdb_id")]
        public string ImdbId { get; set; }

        [JsonProperty("homepage")]
        public string Homepage { get; set; }
    }
}