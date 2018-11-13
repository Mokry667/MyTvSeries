using System;
using System.ComponentModel.DataAnnotations;

namespace MyTvSeries.Web.Models.Profile
{
    public class FavouritePersonOnListViewModel
    {
        public long? PersonId { get; set; }

        public string PersonName { get; set; }

        public byte[] PosterContent { get; set; }

        [Display(Name = "Last update")]
        public DateTime LastChangedAt { get; set; }
    }
}
