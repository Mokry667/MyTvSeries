using System;
using MyTvSeries.Domain.Entities.Base;
using MyTvSeries.Domain.Enums;
using MyTvSeries.Domain.Identity;

namespace MyTvSeries.Domain.Entities
{
    public class UserSeries : AuditEntity
    {
        public long SeriesId { get; set; }
        public virtual Series Series { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public WatchStatus WatchStatus { get; set; }
        public int Rating { get; set; }
        public int SeasonsWatched { get; set; }
        public int EpisodesWatched { get; set; }
    }
}
