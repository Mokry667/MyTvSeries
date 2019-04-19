using System.Collections.Generic;
using MyTvSeries.Domain.Entities.Base;
using MyTvSeries.Domain.ManyToMany;

namespace MyTvSeries.Domain.Entities
{
    public class Genre : AuditEntity
    {
        public long? MovieDbId { get; set; }

        public virtual ICollection<SeriesGenres> SeriesGenres { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}
