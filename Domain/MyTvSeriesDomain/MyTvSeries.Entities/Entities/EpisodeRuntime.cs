using System;

namespace MyTvSeries.Domain.Entities
{
    public class EpisodeRuntime
    {
        public long Id { get; set; }

        public long SeriesId { get; set; }
        public virtual Series Series { get; set; }

        public long RuntimeInMinutes { get; set; }

        public long CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public long? LastChangedBy { get; set; }
        public DateTime? LastChangedAt { get; set; }
    }
}
