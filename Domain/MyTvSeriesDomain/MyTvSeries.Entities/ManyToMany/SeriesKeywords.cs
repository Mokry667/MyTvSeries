using MyTvSeries.Domain.Entities;

namespace MyTvSeries.Domain.ManyToMany
{
    public class SeriesKeywords
    {
        public long SeriesId { get; set; }
        public Series Series { get; set; }

        public long KeywordId { get; set; }
        public Keyword Keyword { get; set; }
    }
}
