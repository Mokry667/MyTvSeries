using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using MyTvSeries.Domain.Enums;
using MyTvSeries.Domain.ManyToMany;

namespace MyTvSeries.Domain.Entities
{
    public class Series
    {
        public long Id { get; set; }
        public long? TvDbId { get; set; }
        public long? MovieDbId { get; set; }
        public string ImdbId { get; set; }

        public virtual ICollection<SeriesNetworks> SeriesNetworks { get; set; }
        public virtual ICollection<SeriesGenres> SeriesGenres { get; set; }
        public virtual ICollection<SeriesCharacters> SeriesCharacters { get; set; }
        public virtual ICollection<SeriesCountries> SeriesCountries { get; set; }
        public virtual ICollection<SeriesKeywords> SeriesKeywords { get; set; }
        public virtual ICollection<SeriesStudios> SeriesStudios { get; set; }
        public virtual ICollection<Crew> Crews { get; set; }

        public virtual ICollection<UserSeries> SeriesUsers { get; set; }
        public virtual ICollection<FavoritesSeries> FavoritesSeries { get; set; }

        public string Name { get; set; }
        public string OriginalName { get; set; }
        public string Overview { get; set; }
        public SeriesStatus Status { get; set; }
        public DateTime? AiredFrom { get; set; }
        public DateTime? AiredTo { get; set; }
        public DayOfWeek AirDayOfWeek { get; set; }
        public TimeSpan AirTime { get; set; }
        public int NumberOfSeasons { get; set; }
        public int NumberOfEpisodes { get; set; }


        public int EpisodeRuntime { get; set; }
        // TODO calculate this somewhere (maybe after episodes import (?))
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalRuntime { get; set; }


        [Column(TypeName = "decimal(18, 2)")]
        public decimal UserRating { get; set; }

        public int UserVotes { get; set; }

        public string PosterName { get; set; }
        public byte[] PosterContent { get; set; }

        public bool IsImportEnabled { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string LastChangedBy { get; set; }
        public DateTime? LastChangedAt { get; set; }
    }
}
