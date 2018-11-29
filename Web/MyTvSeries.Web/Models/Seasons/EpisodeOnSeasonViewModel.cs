using MyTvSeries.Web.Models.Enums;

namespace MyTvSeries.Web.Models.Seasons
{
    public class EpisodeOnSeasonViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Overview { get; set; }
        public string Aired { get; set; }
        public long? EpisodeNumber { get; set; }
        public decimal EpisodeSiteRating { get; set; }
        public EpisodeRating Rating { get; set; }

    }
}
