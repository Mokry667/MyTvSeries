using System;
using System.ComponentModel.DataAnnotations;

namespace MyTvSeries.Web.Models.People
{
    public class PersonDetailCastViewModel
    {
        public long SeriesId { get; set; }

        public string SeriesName { get; set; }

        public string CharacterName { get; set; }

        public int Year { get; set; }
        public decimal SeriesRating { get; set; }
    }
}
