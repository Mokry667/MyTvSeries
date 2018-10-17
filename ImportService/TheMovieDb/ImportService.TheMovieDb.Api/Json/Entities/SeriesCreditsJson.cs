using Newtonsoft.Json;

namespace ImportService.TheMovieDb.Api.Json.Entities
{
    public class SeriesCreditsJson
    {
        [JsonProperty("cast")]
        public CastJson[] CastJson { get; set; }

        [JsonProperty("crew")]
        public CrewJson[] CrewJson { get; set; }

        [JsonProperty("id")]
        public long? Id { get; set; }
    }

    public class CastJson
    {
        [JsonProperty("character")]
        public string Character { get; set; }

        [JsonProperty("credit_id")]
        public string CreditId { get; set; }

        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("gender")]
        public long? Gender { get; set; }

        [JsonProperty("profile_path")]
        public string ProfilePath { get; set; }

        [JsonProperty("order")]
        public long? Order { get; set; }
    }

    public class CrewJson
    {
        [JsonProperty("credit_id")]
        public string CreditId { get; set; }

        [JsonProperty("department")]
        public string Department { get; set; }

        [JsonProperty("id")]
        public long? Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("gender")]
        public long? Gender { get; set; }

        [JsonProperty("job")]
        public string Job { get; set; }

        [JsonProperty("profile_path")]
        public string ProfilePath { get; set; }
    }
}
