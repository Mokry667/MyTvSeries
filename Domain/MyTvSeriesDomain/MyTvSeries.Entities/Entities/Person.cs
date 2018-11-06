using System;
using System.Collections.Generic;
using MyTvSeries.Domain.Enums;

namespace MyTvSeries.Domain.Entities
{
    public class Person
    {
        public long Id { get; set; }
        public long? TvDbId { get; set; }
        public long? MovieDbId { get; set; }
        public string ImdbId { get; set; }

        public virtual ICollection<Character> Characters { get; set; }
        public virtual ICollection<Crew> Crews { get; set; }
        public virtual ICollection<FavoritesPerson> FavoritesPersons { get; set; }

        public string Name { get; set; } 
        public Gender Gender { get; set; }
        public string Biography { get; set; }
        public DateTime? Birthday { get; set; }
        public DateTime? Deathday { get; set; }
        public string PlaceOfBirth { get; set; }

        public bool IsImportEnabled { get; set; }

        public string PosterName { get; set; }
        public byte[] PosterContent { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string LastChangedBy { get; set; }
        public DateTime? LastChangedAt { get; set; }
    }
}
