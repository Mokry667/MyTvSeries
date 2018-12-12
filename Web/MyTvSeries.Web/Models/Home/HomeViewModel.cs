using MyTvSeries.Domain.Entities;
using MyTvSeries.Web.Models.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTvSeries.Web.Models.Home
{
    public class HomeViewModel
    {
        public List<SeriesIndexViewModel> Series { get; set; }
    }
}
