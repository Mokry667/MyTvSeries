using System;
using System.Collections.Generic;

namespace MyTvSeries.Domain.Entities
{
    public class Season
    {
        public long Id { get; set; }
        public long? MovieDbId { get; set; }

        public long SeriesId { get; set; }
        public virtual Series Series { get; set; }

        public virtual ICollection<Episode> Episodes { get; set; }

        public string Name { get; set; }
        public string Overview { get; set; }
        public long? SeasonNumber { get; set; }
        public DateTime? AiredFrom { get; set; }
        public DateTime? AiredTo { get; set; }
        public long? NumberOfEpisodes { get; set; }

        public bool IsImportEnabled { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string LastChangedBy { get; set; }
        public DateTime? LastChangedAt { get; set; }

    }
}
