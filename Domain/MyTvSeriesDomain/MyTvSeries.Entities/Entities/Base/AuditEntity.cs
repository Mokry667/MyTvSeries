using System;

namespace MyTvSeries.Domain.Entities.Base
{
    public class AuditEntity : BaseEntity
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string LastChangedBy { get; set; }
        public DateTime? LastChangedAt { get; set; }

        public void UpdateAuditValuesForNewEntity(string userGuid)
        {
            CreatedAt = DateTime.UtcNow;
            CreatedBy = userGuid;
        }

        public void UpdateAuditValuesForUpdatedEntity(string userGuid)
        {
            LastChangedAt = DateTime.UtcNow;
            LastChangedBy = userGuid;
        }
    }
}
