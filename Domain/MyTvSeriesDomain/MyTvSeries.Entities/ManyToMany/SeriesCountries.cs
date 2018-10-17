using MyTvSeries.Domain.Entities;

namespace MyTvSeries.Domain.ManyToMany
{
    public class SeriesCountries
    {
        public long SeriesId { get; set; }
        public Series Series { get; set; }

        public long CountryId { get; set; }
        public Country Country { get; set; }
    }
}
