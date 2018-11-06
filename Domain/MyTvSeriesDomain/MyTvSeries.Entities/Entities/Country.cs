using System;
using System.Collections.Generic;
using MyTvSeries.Domain.ManyToMany;

namespace MyTvSeries.Domain.Entities
{
    public class Country
    {
        public long Id { get; set; }

        public virtual ICollection<SeriesCountries> SeriesCountries { get; set; }

        public string Name { get; set; }
        public string ShortName { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string LastChangedBy { get; set; }
        public DateTime? LastChangedAt { get; set; }
    }
}
