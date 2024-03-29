﻿using System.Collections.Generic;
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
        public virtual ICollection<SeriesReview> SeriesReviews { get; set; }
        public virtual ICollection<UserEpisode> UserEpisodes { get; set; }
        public virtual ICollection<UserReview> UserReviews { get; set; }
        public virtual ICollection<SeriesNotification> SeriesNotifications { get; set; }
        public virtual ICollection<PersonNotification> PersonNotifications { get; set; }
    }
}
