using MyTvSeries.Domain.Identity;
using MyTvSeries.Domain.Entities.Base;

namespace MyTvSeries.Domain.Entities
{
    public class UserReview : AuditEntity
    {
        public long ReviewId { get; set; }
        public virtual SeriesReview Review { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public bool IsUpvoted { get; set; }
    }
}
