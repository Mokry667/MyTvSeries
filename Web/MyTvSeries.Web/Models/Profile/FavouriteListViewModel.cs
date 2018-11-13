using System.Collections.Generic;

namespace MyTvSeries.Web.Models.Profile
{
    public class FavouriteListViewModel
    {
        public string Username { get; set; }

        public List<FavouriteSeriesOnListViewModel> FavouriteSeries { get; set; }
        public List<FavouritePersonOnListViewModel> FavouritePersons { get; set; }
    }
}
