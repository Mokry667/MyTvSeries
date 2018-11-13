using System;
using System.Collections.Generic;
using System.Text;
using MyTvSeries.Domain.Identity;

namespace MyTvSeries.Domain.Entities
{
    public class SeriesReview
    {
        public long Id { get; set; }

        public long? SeriesId { get; set; }
        public virtual Series Series { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public string Content { get; set; }
        public int Likes { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string LastChangedBy { get; set; }
        public DateTime? LastChangedAt { get; set; }
    }
}
