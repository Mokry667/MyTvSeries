using System;
using System.Collections.Generic;
using MyTvSeries.Domain.ManyToMany;

namespace MyTvSeries.Domain.Entities
{
    public class Keyword
    {
        public long Id { get; set; }

        public virtual ICollection<SeriesKeywords> SeriesKeywords { get; set; }

        public string Name { get; set; }

        // TODO is it necessary?
        public string Description { get; set; }

        public bool IsImportEnabled { get; set; }

        public long CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public long? LastChangedBy { get; set; }
        public DateTime? LastChangedAt { get; set; }
    }
}
