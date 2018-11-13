using System;
using System.ComponentModel.DataAnnotations;

namespace MyTvSeries.Web.Models.People
{
    public class PersonDetailCrewViewModel
    {
        public long SeriesId { get; set; }

        public string SeriesName { get; set; }

        public string Job { get; set; }

        public int Year { get; set; }
        public decimal SeriesRating { get; set; }
    }
}
