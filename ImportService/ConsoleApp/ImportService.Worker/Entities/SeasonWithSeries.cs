namespace ImportService.Worker.Entities
{
    public class SeasonWithSeries
    {
        public long MovieDbSeriesId { get; set; }
        public long SeriesId { get; set; }
        public long SeasonId { get; set; }
        public long SeasonNumber { get; set; }
    }
}
