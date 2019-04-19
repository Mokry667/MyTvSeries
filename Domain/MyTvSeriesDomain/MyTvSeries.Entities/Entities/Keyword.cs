using System.Collections.Generic;
using MyTvSeries.Domain.Entities.Base;
using MyTvSeries.Domain.ManyToMany;

namespace MyTvSeries.Domain.Entities
{
    public class Keyword : ImportEntity
    {
        public virtual ICollection<SeriesKeywords> SeriesKeywords { get; set; }

        public string Name { get; set; }

        // TODO is it necessary?
        public string Description { get; set; }
    }
}
