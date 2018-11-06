using System;
using MyTvSeries.Domain.Enums;
using MyTvSeries.Domain.Identity;

namespace MyTvSeries.Domain.Entities
{
    public class UserSeries
    {
        public long Id { get; set; }

        public long SeriesId { get; set; }
        public virtual Series Series { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public WatchStatus WatchStatus { get; set; }
        public int Rating { get; set; }
        public int SeasonsWatched { get; set; }
        public int EpisodesWatched { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string LastChangedBy { get; set; }
        public DateTime? LastChangedAt { get; set; }
    }
}
