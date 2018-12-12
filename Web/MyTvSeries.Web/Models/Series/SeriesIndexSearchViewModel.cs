using MyTvSeries.Domain.Entities;
using System.Collections.Generic;
using X.PagedList;

namespace MyTvSeries.Web.Models.Series
{
    public class SeriesIndexSearchViewModel
    {
        public StaticPagedList<SeriesIndexViewModel> ViewModels { get; set; }

        public List<string> Genres { get; set; }
        public string ChosenGenre { get; set;}

        public List<string> Statuses { get; set; }
        public string ChosenStatus { get; set; }

        public string Keyword { get; set; }
    }
}
