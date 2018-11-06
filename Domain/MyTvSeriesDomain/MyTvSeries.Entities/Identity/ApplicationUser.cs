using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using MyTvSeries.Domain.Entities;

namespace MyTvSeries.Domain.Identity
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<UserSeries> SeriesUsers { get; set; }
        public virtual ICollection<FavoritesSeries> FavoritesSeries { get; set; }
        public virtual ICollection<FavoritesPerson> FavoritesPersons { get; set; }
    }
}
