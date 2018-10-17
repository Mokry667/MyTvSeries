using System;
using System.Collections.Generic;
using MyTvSeries.Domain.ManyToMany;

namespace MyTvSeries.Domain.Entities
{
    public class Genre
    {
        public long Id { get; set; }
        public long? MovieDbId { get; set; }

        public virtual ICollection<SeriesGenres> SeriesGenres { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public long CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public long? LastChangedBy { get; set; }
        public DateTime? LastChangedAt { get; set; }
    }
}
