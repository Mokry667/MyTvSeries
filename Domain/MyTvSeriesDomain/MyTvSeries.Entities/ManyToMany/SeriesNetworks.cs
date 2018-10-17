using MyTvSeries.Domain.Entities;

namespace MyTvSeries.Domain.ManyToMany
{
    public class SeriesNetworks
    {
        public long SeriesId { get; set; }
        public Series Series { get; set; }

        public long NetworkId { get; set; }
        public Network Network { get; set; }
    }
}
