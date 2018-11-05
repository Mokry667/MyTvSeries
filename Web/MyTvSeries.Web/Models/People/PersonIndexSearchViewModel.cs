using X.PagedList;

namespace MyTvSeries.Web.Models.People
{
    public class PersonIndexSearchViewModel
    {
        public StaticPagedList<PersonIndexViewModel> ViewModels { get; set; }

        public string Keyword { get; set; }
    }
}
