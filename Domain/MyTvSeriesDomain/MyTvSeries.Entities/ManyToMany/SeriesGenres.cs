using MyTvSeries.Domain.Entities;

namespace MyTvSeries.Domain.ManyToMany
{
    public class SeriesGenres
    {
        public long SeriesId { get; set; }
        public Series Series { get; set; }
        
        public long GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
