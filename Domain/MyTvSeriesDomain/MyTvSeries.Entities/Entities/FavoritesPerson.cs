using System;
using MyTvSeries.Domain.Identity;

namespace MyTvSeries.Domain.Entities
{
    public class FavoritesPerson
    {
        public long Id { get; set; }

        public long? PersonId { get; set; }
        public virtual Person Person { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string LastChangedBy { get; set; }
        public DateTime? LastChangedAt { get; set; }
    }
}
