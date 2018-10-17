using MyTvSeries.Domain.Entities;

namespace MyTvSeries.Domain.ManyToMany
{
    public class SeriesStudios
    {
        public long SeriesId { get; set; }
        public Series Series { get; set; }

        public long StudioId { get; set; }
        public Studio Studio { get; set; }
    }
}
