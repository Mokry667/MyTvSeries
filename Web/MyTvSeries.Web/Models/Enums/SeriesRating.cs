using System.ComponentModel.DataAnnotations;

namespace MyTvSeries.Web.Models.Enums
{
    public enum SeriesRating
    {
        [Display(Name = "Not rated")]
        NotRated = 0,

        [Display(Name = "(1) The Worst")]
        Worst = 1,

        [Display(Name = "(2) Terrible")]
        Terrible = 2,

        [Display(Name = "(3) Very bad")]
        VeryBad = 3,

        [Display(Name = "(4) Bad")]
        Bad = 4,

        [Display(Name = "(5) Average")]
        Average = 5,

        [Display(Name = "(6) Fine")]
        Fine = 6,

        [Display(Name = "(7) Good")]
        Good = 7,

        [Display(Name = "(8) Very good")]
        VeryGood = 8,

        [Display(Name = "(9) Great")]
        Great = 9,

        [Display(Name = "(10) Masterpiece")]
        Masterpiece = 10
    }
}
