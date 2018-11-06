using System;
using System.Collections.Generic;
using MyTvSeries.Domain.ManyToMany;

namespace MyTvSeries.Domain.Entities
{
    public class Studio
    {
        public long Id { get; set; }
        public long? TvDbId { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<SeriesStudios> SeriesStudios { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public bool IsImportEnabled { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string LastChangedBy { get; set; }
        public DateTime? LastChangedAt { get; set; }
    }
}
