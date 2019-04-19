using MyTvSeries.Domain.Entities.Base;
using MyTvSeries.Domain.Identity;

namespace MyTvSeries.Domain.Entities
{
    public class UserEpisode : AuditEntity
    {
        public long EpisodeId { get; set; }
        public virtual Episode Episode { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public int Rating { get; set; }
    }
}
