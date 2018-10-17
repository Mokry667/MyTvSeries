using System;

namespace MyTvSeries.Domain.Entities
{
    public class Crew
    {
        public long Id { get; set; }

        public long PersonId { get; set; }
        public virtual Person Person { get; set; }

        public long SeriesId { get; set; }
        public virtual Series Series { get; set; }

        // TODO probably set to enum later on 
        public string Department { get; set; }
        public string Job { get; set; }

        public bool IsImportEnabled { get; set; }

        public long CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public long? LastChangedBy { get; set; }
        public DateTime? LastChangedAt { get; set; }
    }
}
