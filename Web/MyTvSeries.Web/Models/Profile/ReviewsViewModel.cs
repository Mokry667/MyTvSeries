using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTvSeries.Web.Models.Profile
{
    public class ReviewsViewModel
    {
        public List<ReviewOnListViewModel> Reviews { get; set; }

        public string Username { get; set; }
    }
}
