using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyTvSeries.Domain.Entities;

namespace MyTvSeries.Web.Models.Home
{
    public class NotificationsViewModel
    {
        public List<SeriesNotification> SeriesNotifications { get; set; }
        public List<PersonNotification> PersonNotifications { get; set; }
    }
}
