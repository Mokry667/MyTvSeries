using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyTvSeries.Web.Models.Series
{
    public class SeriesReviewDetailViewModel
    {
        public long Id { get; set; }

        public string Content { get; set; }

        [Display(Name = "Username")]
        public string Username { get; set; }

        public int Rating { get; set; }

        public DateTime WrittenOn { get; set; }

        public int Likes { get; set; }
    }
}
