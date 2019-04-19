using MyTvSeries.Domain.Entities.Base;
using MyTvSeries.Domain.Identity;

namespace MyTvSeries.Domain.Entities
{
    public class FavoritesPerson : AuditEntity
    {
        public long? PersonId { get; set; }
        public virtual Person Person { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
