using Newtonsoft.Json;

namespace ImportService.TheTvDb.Api.Json.Entities
{
    public class SeriesActorJson
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("seriesId")]
        public string SeriesId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("sortOrder")]
        public string SortOrder { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("imageAuthor")]
        public string ImageAuthor { get; set; }

        [JsonProperty("imageAdded")]
        public string ImageAdded { get; set; }

        [JsonProperty("lastUpdated")]
        public string LastUpdated { get; set; }
    }
}
