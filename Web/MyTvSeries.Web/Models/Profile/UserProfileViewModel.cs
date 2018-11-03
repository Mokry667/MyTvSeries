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
    }
}
