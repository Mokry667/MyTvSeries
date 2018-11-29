using System.Collections.Generic;

namespace MyTvSeries.Web.Models.Seasons
{
    public class SeasonIndexListViewModel
    {
        public long? SeriesId { get; set; }
        public List<SeasonIndexViewModel> ViewModels { get; set; }
    }
}
