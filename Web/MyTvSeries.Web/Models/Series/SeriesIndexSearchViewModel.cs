using X.PagedList;

namespace MyTvSeries.Web.Models.Series
{
    public class SeriesIndexSearchViewModel
    {
        public StaticPagedList<SeriesIndexViewModel> ViewModels { get; set; }

        public string Keyword { get; set; }
    }
}
