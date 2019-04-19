using System;
using System.Collections.Generic;
using MyTvSeries.Domain.Entities.Base;

namespace MyTvSeries.Domain.Entities
{
    public class Season : ImportEntity
    {
        public long? MovieDbId { get; set; }

        public long SeriesId { get; set; }
        public virtual Series Series { get; set; }

        public virtual ICollection<Episode> Episodes { get; set; }

        public string Name { get; set; }
        public string Overview { get; set; }
        public int? SeasonNumber { get; set; }
        public DateTime? AiredFrom { get; set; }
        public int? NumberOfEpisodes { get; set; }

    }
}
