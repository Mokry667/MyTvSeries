using System;
using System.Collections.Generic;
using MyTvSeries.Domain.Entities.Base;
using MyTvSeries.Domain.Enums;

namespace MyTvSeries.Domain.Entities
{
    public class Person : ImportEntity
    {
        public long? TvDbId { get; set; }
        public long? MovieDbId { get; set; }
        public string ImdbId { get; set; }

        public virtual ICollection<Character> Characters { get; set; }
        public virtual ICollection<Crew> Crews { get; set; }
        public virtual ICollection<FavoritesPerson> FavoritesPersons { get; set; }
        public virtual ICollection<PersonNotification> PersonNotifications { get; set; }

        public string Name { get; set; } 
        public Gender Gender { get; set; }
        public string Biography { get; set; }
        public DateTime? Birthday { get; set; }
        public DateTime? Deathday { get; set; }
        public string PlaceOfBirth { get; set; }

        public string PosterName { get; set; }
        public byte[] PosterContent { get; set; }
    }
}
