using MyTvSeries.Domain.Identity;
using System;

namespace MyTvSeries.Domain.Entities
{
    public class UserReview
    {
        public long Id { get; set; }

        public long ReviewId { get; set; }
        public virtual SeriesReview Review { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public bool IsUpvoted { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string LastChangedBy { get; set; }
        public DateTime? LastChangedAt { get; set; }
    }
}
