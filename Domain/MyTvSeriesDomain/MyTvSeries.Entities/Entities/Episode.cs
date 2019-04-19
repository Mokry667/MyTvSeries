using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MyTvSeries.Domain.Entities.Base;

namespace MyTvSeries.Domain.Entities
{
    public class Episode : ImportEntity
    {
        public long? MovieDbId { get; set; }
        public long? TvDbId { get; set; }

        public long SeasonId { get; set; }
        public virtual Season Season { get; set; }

        public virtual ICollection<UserEpisode> UserEpisodes { get; set; }

        public string Name { get; set; }
        public string Overview { get; set; }
        public DateTime? Aired { get; set; }
        public long? SeasonEpisodeNumber { get; set; }
        public long? AbsoluteEpisodeNumber { get; set; }
        public long? SeasonNumber { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal UserRating { get; set; }

        public int UserVotes { get; set; }
    }
}
