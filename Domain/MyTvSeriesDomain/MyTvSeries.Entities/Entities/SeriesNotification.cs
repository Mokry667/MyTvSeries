using MyTvSeries.Domain.Identity;
using MyTvSeries.Domain.Entities.Base;

namespace MyTvSeries.Domain.Entities
{
    public class SeriesNotification : AuditEntity
    {
        public long SeriesId { get; set; }
        public virtual Series Series { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public string Content { get; set; }
        public bool IsRead { get; set; }
    }
}
