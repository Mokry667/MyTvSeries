using System;
using MyTvSeries.Domain.Identity;

namespace MyTvSeries.Domain.Entities
{
    public class UserEpisode
    {
        public long Id { get; set; }

        public long EpisodeId { get; set; }
        public virtual Episode Episode { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public int Rating { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string LastChangedBy { get; set; }
        public DateTime? LastChangedAt { get; set; }
    }
}
