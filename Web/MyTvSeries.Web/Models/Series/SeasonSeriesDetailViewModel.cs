﻿using System;

namespace MyTvSeries.Web.Models.Series
{
    public class SeasonSeriesDetailViewModel
    {
        public long? SeasonId { get; set; }
        public string SeasonName { get; set; }
        public int? SeasonNumber { get; set; }
        public int? NumberOfEpisodes { get; set; }
        public string AiredFrom { get; set; }
    }
}
