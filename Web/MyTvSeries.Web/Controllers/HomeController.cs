using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyTvSeries.Domain.Ef;
using MyTvSeries.Domain.Entities;
using MyTvSeries.Web.Models;
using MyTvSeries.Web.Models.Home;
using MyTvSeries.Web.Models.Series;

namespace MyTvSeries.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly TvSeriesContext _context;

        public HomeController(TvSeriesContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var viewModel = new HomeViewModel();

            var topSeries = _context.Series.OrderBy(x => x.UserRating).Take(4).ToList();

            var viewModels = new List<SeriesIndexViewModel>();

            foreach (var series in topSeries)
            {
                var seriesViewModel = new SeriesIndexViewModel
                {
                    Id = series.Id,
                    Name = series.Name,
                    UserRating = series.UserRating,
                    PosterContent = series.PosterContent
                };
                viewModels.Add(seriesViewModel);
            }

            viewModel.Series = viewModels;

            return View(viewModel);
        }

        public IActionResult GetNotifications()
        {
            var viewModel = new NotificationsViewModel();

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            viewModel.SeriesNotifications = new List<SeriesNotification>();
            viewModel.PersonNotifications = new List<PersonNotification>();

            if (!string.IsNullOrEmpty(userId))
            {
                var seriesNotifications = _context.SeriesNotifications
                    .Where(x => x.UserId == userId && x.IsRead == false)
                    .ToList();

                foreach(var seriesNotification in seriesNotifications)
                {
                    var seriesName = _context.Series
                            .Where(x => x.Id == seriesNotification.SeriesId)
                            .Select(x => x.Name)
                            .FirstOrDefault();

                    seriesNotification.Content = Regex.Replace(seriesNotification.Content, @"\[\d*\]", seriesName);
                }

                viewModel.SeriesNotifications = seriesNotifications;

                var personNotifications = _context.PersonNotifications
                    .Where(x => x.UserId == userId && x.IsRead == false)
                    .ToList();

                foreach(var personNotificaiton in personNotifications)
                {
                    var personName = _context.Persons
                        .Where(x => x.Id == personNotificaiton.PersonId)
                        .Select(x => x.Name)
                        .FirstOrDefault();

                    Regex rgx = new Regex(@"\[\d*\]");

                    personNotificaiton.Content = rgx.Replace(personNotificaiton.Content,  personName, 1);

                    var characterId = getBetween(personNotificaiton.Content, "[", "]");

                    var characterName = _context.Characters
                        .Where(x => x.PersonId == personNotificaiton.PersonId && x.Id == Convert.ToInt64(characterId))
                        .Select(x => x.Name)
                        .FirstOrDefault();

                    personNotificaiton.Content = rgx.Replace(personNotificaiton.Content, characterName, 1);

                    var seriesID = getBetween(personNotificaiton.Content, "[", "]");

                    var seriesName = _context.Series
                        .Where(x => x.Id == Convert.ToInt64(seriesID))
                        .Select(x => x.Name)
                        .FirstOrDefault();

                    personNotificaiton.Content = rgx.Replace(personNotificaiton.Content, seriesName, 1);
                }

                viewModel.PersonNotifications = personNotifications;
            }

            return PartialView(viewModel);
        }

        [HttpPost]
        public void MakeSeriesNotificationRead(string notificationIdString)
        {
            var notificationId = Convert.ToInt64(notificationIdString);

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var notification = _context.SeriesNotifications.Where(x => x.Id == notificationId).FirstOrDefault();

            notification.IsRead = true;

            _context.Update(notification);
            _context.SaveChanges();
        }

        [HttpPost]
        public void MakePersonNotificationRead(string notificationIdString)
        {
            var notificationId = Convert.ToInt64(notificationIdString);

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var notification = _context.PersonNotifications.Where(x => x.Id == notificationId).FirstOrDefault();

            notification.IsRead = true;

            _context.Update(notification);
            _context.SaveChanges();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public string getBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }
    }
}
