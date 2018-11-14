using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyTvSeries.Web.Models.Profile
{
    public class SeriesOnCalendarViewModel
    {
        public long SeriesId { get; set; }

        public string SeriesName { get; set; }

        public long? SeasonNumber { get; set; }

        public long? EpisodeNumber { get; set; }

        [DisplayFormat(DataFormatString = "{0:hh\\:mm}")]
        public TimeSpan AirTime { get; set; }
    }
}
