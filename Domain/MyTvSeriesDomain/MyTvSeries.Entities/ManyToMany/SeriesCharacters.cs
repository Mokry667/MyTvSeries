using MyTvSeries.Domain.Entities;

namespace MyTvSeries.Domain.ManyToMany
{
    public class SeriesCharacters
    {
        public long SeriesId { get; set; }
        public Series Series { get; set; }

        public long CharacterId { get; set; }
        public Character Character { get; set; }
    }
}
