using Newtonsoft.Json;

namespace ImportService.TheMovieDb.Api.Json.Entities
{
    public class GenresListJson
    {
        [JsonProperty("genres")]
        public GenreJson[] GenresJson { get; set; }
    }
}