using System;
using System.Collections.Generic;
using MyTvSeries.Domain.ManyToMany;

namespace MyTvSeries.Domain.Entities
{
    public class Character
    {
        public long Id { get; set; }

        public long PersonId { get; set; }
        public virtual Person Person { get; set; }

        public virtual ICollection<SeriesCharacters> SeriesCharacters { get; set; }

        public string Name { get; set; }

        public bool IsImportEnabled { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string LastChangedBy { get; set; }
        public DateTime? LastChangedAt { get; set; }
    }
}
