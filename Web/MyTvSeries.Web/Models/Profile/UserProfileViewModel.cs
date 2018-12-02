using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTvSeries.Web.Models.Profile
{
    public class UserProfileViewModel
    {
        public IEnumerable<WatchStatusViewModel> WatchStatusSummary { get; set; }
        public IEnumerable<GenreViewModel> GenreSummary { get; set; }
        public int GenresCount { get; set; }
        public int SeriesCount { get; set; }
        public string UserName { get; set; }
        public double MeanScore { get; set; }
        public int WatchedEpisodes { get; set; }
        public int WatchedSeasons { get; set; }
        public string FavoriteGenre { get; set; }
        public string TotalWatchTime { get; set; }
        public int TotalEntries { get; set; }
    }
}
