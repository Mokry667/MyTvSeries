using System.Collections.Generic;
using MyTvSeries.Domain.Entities.Base;
using MyTvSeries.Domain.ManyToMany;

namespace MyTvSeries.Domain.Entities
{
    public class Country : AuditEntity
    {
        public virtual ICollection<SeriesCountries> SeriesCountries { get; set; }

        public string Name { get; set; }
        public string ShortName { get; set; }
    }
}
