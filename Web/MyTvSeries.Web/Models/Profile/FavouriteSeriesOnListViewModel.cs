using System;
using System.ComponentModel.DataAnnotations;

namespace MyTvSeries.Web.Models.Profile
{
    public class FavouriteSeriesOnListViewModel
    {
        public long? SeriesId { get; set; }

        public string SeriesName { get; set; }

        public byte[] PosterContent { get; set; }

        [Display(Name = "Last update")]
        public DateTime LastChangedAt { get; set; }
    }
}
