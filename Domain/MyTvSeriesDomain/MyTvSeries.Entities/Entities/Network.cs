using System;
using System.Collections.Generic;
using MyTvSeries.Domain.Entities.Base;
using MyTvSeries.Domain.ManyToMany;

namespace MyTvSeries.Domain.Entities
{
    public class Network : ImportEntity
    {
        public long? TvDbId { get; set; }

        public Country Country { get; set; }
        public virtual ICollection<SeriesNetworks> SeriesNetworks { get; set; }

        public string Name { get; set; }
    }
}
