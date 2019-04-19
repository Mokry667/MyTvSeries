using MyTvSeries.Domain.Entities.Base;
using MyTvSeries.Domain.Identity;

namespace MyTvSeries.Domain.Entities
{
    public class FavoritesSeries : AuditEntity
    {
        public long? SeriesId { get; set; }
        public virtual Series Series { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
