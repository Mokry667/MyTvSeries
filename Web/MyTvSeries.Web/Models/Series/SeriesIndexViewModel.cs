using System.ComponentModel.DataAnnotations;

namespace MyTvSeries.Web.Models
{
    public class SeriesIndexViewModel
    {
        public long Id { get; set; }

        [Display(Name = "Title")]
        public string Name { get; set; }

        [Display (Name = "Site Rating")]
        public decimal UserRating { get; set; }

        public byte[] PosterContent { get; set; }

        public string SearchKeyword { get; set; }
    }
}
