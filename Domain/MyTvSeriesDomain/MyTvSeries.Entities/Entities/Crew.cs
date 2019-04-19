using MyTvSeries.Domain.Entities.Base;

namespace MyTvSeries.Domain.Entities
{
    public class Crew : ImportEntity
    {
        public long PersonId { get; set; }
        public virtual Person Person { get; set; }

        public long SeriesId { get; set; }
        public virtual Series Series { get; set; }

        // TODO probably set to enum later on 
        public string Department { get; set; }
        public string Job { get; set; }
    }
}
