using System.ComponentModel.DataAnnotations;

namespace MyTvSeries.Web.Models.Seasons
{
    public class SeasonIndexViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }

        [Display(Name = "Season number")]
        public int? SeasonNumber { get; set; }

        [Display(Name = "Number of episodes")]
        public int? NumberOfEpisodes { get; set; }

        [Display(Name = "Aired from")]
        public string AiredFrom { get; set; }

        [Display(Name = "Rating")]
        public string Rating { get; set; }
    }
}
