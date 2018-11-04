using System.Collections.Generic;

namespace MyTvSeries.Web.Models.Profile
{
    public class SeriesOnListWithWatchStatusViewModel
    {
        public string WatchStatus { get; set; }

        public IEnumerable<SeriesOnListViewModel> Series { get; set; }
    }
}
