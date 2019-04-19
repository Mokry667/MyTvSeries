using System.Collections.Generic;
using MyTvSeries.Domain.Entities.Base;
using MyTvSeries.Domain.ManyToMany;

namespace MyTvSeries.Domain.Entities
{
    public class Studio : ImportEntity
    {
        public long? TvDbId { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<SeriesStudios> SeriesStudios { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}
