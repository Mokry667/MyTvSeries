using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTvSeries.Web.Models.Profile
{
    public class ReviewOnListViewModel
    {
        public long Id { get; set; }
        public string SeriesName { get; set; }
        public long Rating { get; set; }
        public long Likes { get; set; }
    }
}
