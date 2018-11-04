using System.ComponentModel.DataAnnotations;

namespace MyTvSeries.Web.Models.Profile
{
    public class SeriesOnListViewModel
    {
        public long Id { get; set; }

        [Display(Name = "Title")]
        public string Name { get; set; }

        [Display(Name = "Rating")]
        public int UserRating { get; set; }

        public byte[] PosterContent { get; set; }

        public int NumberOfSeasons { get; set; }

        public int NumberOfEpisodes { get; set; }

        public int SeasonsWatched { get; set; }

        public int EpisodesWatched { get; set; }
    }
}
