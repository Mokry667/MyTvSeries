using System.Collections.Generic;
using MyTvSeries.Domain.Entities.Base;
using MyTvSeries.Domain.Identity;

namespace MyTvSeries.Domain.Entities
{
    public class SeriesReview : AuditEntity
    {
        public long? SeriesId { get; set; }
        public virtual Series Series { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<UserReview> UserReviews { get; set; }

        public string Content { get; set; }
        public int Likes { get; set; }
    }
}
