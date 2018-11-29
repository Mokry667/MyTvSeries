using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTvSeries.Web.Models.Seasons
{
    public class SeasonDetailViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Overview { get; set; }
        public int NumberOfEpisodes { get; set; }
        public int? SeasonNumber { get; set; }
        public string AiredFrom { get; set; }
        public long SeriesId { get; set; }
        public List<EpisodeOnSeasonViewModel> Episodes { get; set; }
    }
}
