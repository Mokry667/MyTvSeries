using System.Collections.Generic;

namespace MyTvSeries.Web.Models.Profile
{
    public class SeriesListViewModel
    {
        public string Username { get; set; }

        public List<SeriesOnListWithWatchStatusViewModel> SeriesByWatchStatus { get; set; }

        public IEnumerable<SeriesOnListViewModel> WatchingSeries { get; set; }

        public IEnumerable<SeriesOnListViewModel> CompletedSeries { get; set; }

        public IEnumerable<SeriesOnListViewModel> OnHoldSeries { get; set; }

        public IEnumerable<SeriesOnListViewModel> PlanToWatchSeries { get; set; }

        public IEnumerable<SeriesOnListViewModel> DroppedSeries { get; set; }

    }
}
