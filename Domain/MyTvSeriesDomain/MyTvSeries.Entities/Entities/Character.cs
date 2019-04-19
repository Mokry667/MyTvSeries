using System.Collections.Generic;
using MyTvSeries.Domain.Entities.Base;
using MyTvSeries.Domain.ManyToMany;

namespace MyTvSeries.Domain.Entities
{
    public class Character : ImportEntity
    {
        public long PersonId { get; set; }
        public virtual Person Person { get; set; }

        public virtual ICollection<SeriesCharacters> SeriesCharacters { get; set; }

        public string Name { get; set; }
    }
}
