using MyTvSeries.Domain.Identity;
using System;
using MyTvSeries.Domain.Entities.Base;

namespace MyTvSeries.Domain.Entities
{
    public class PersonNotification : AuditEntity
    {
        public long PersonId { get; set; }
        public virtual Person Person { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public string Content { get; set; }
        public bool IsRead { get; set; }
    }
}
