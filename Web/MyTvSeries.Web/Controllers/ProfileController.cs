using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyTvSeries.Domain.Ef;
using MyTvSeries.Domain.Entities;
using MyTvSeries.Domain.Enums;
using MyTvSeries.Web.Models.Profile;
using Newtonsoft.Json;
using X.PagedList;

namespace MyTvSeries.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly TvSeriesContext _context;

        public ProfileController(TvSeriesContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string username)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userSeries = await _context.UsersSeries.Where(x => x.UserId == userId)
                .Include(x => x.Series)
                .ThenInclude(x => x.SeriesGenres)
                .ThenInclude(x => x.Genre)
                .ToListAsync();

            var viewModel = new UserProfileViewModel { UserName = User.Identity.Name };

            // create every watch status

            var completedWatchStatusViewModel = new WatchStatusViewModel
            {
                WatchStatus = WatchStatus.Completed,
                Count = 0
            };

            var watchingWatchStatusViewModel = new WatchStatusViewModel
            {
                WatchStatus = WatchStatus.Watching,
                Count = 0
            };

            var droppedWatchStatusViewModel = new WatchStatusViewModel
            {
                WatchStatus = WatchStatus.Dropped,
                Count = 0
            };

            var planToWatchWatchStatusViewModel = new WatchStatusViewModel
            {
                WatchStatus = WatchStatus.PlanToWatch,
                Count = 0
            };

            var onHoldWatchStatusViewModel = new WatchStatusViewModel
            {
                WatchStatus = WatchStatus.OnHold,
                Count = 0
            };

            var watchStatusViewModels = new List<WatchStatusViewModel>
            {
                completedWatchStatusViewModel,
                watchingWatchStatusViewModel,
                droppedWatchStatusViewModel,
                planToWatchWatchStatusViewModel,
                onHoldWatchStatusViewModel
            };

            var genreViewModels = new List<GenreViewModel>();

            foreach (var series in userSeries)
            {
                var currentWatchStatus = watchStatusViewModels.FirstOrDefault(x => x.WatchStatus == series.WatchStatus);
                if (currentWatchStatus != null) currentWatchStatus.Count++;

                foreach (var seriesGenre in series.Series.SeriesGenres)
                {
                    var genreName = seriesGenre.Genre.Name;
                    var currentGenre = genreViewModels.FirstOrDefault(x => x.Genre == genreName);
                    if (currentGenre == null)
                    {
                        var newGenre = new GenreViewModel
                        {
                            Genre = genreName,
                            Count = 1
                        };
                        genreViewModels.Add(newGenre);
                    }
                    else
                        currentGenre.Count++;

                    viewModel.GenresCount++;
                }

                viewModel.SeriesCount++;
            }

            viewModel.WatchStatusSummary = watchStatusViewModels;
            viewModel.GenreSummary = genreViewModels;

            // CREATE charts

            List<DataPointPie> pieChartsDataPoints = new List<DataPointPie>();
            foreach (var genre in viewModel.GenreSummary)
            {
                var dataPoint = new DataPointPie(genre.Count, genre.Genre);
                pieChartsDataPoints.Add(dataPoint);
            }

            ViewBag.DataPointsPie = JsonConvert.SerializeObject(pieChartsDataPoints);

            foreach (var seriesStatus in viewModel.WatchStatusSummary)
            {
                DataPointStackedBar dataPoint = null;
                switch (seriesStatus.WatchStatus)
                {
                    case WatchStatus.Watching:
                        dataPoint = new DataPointStackedBar(seriesStatus.Count, seriesStatus.WatchStatus.ToString(), "#64b3fc");
                        ViewBag.WatchingSeries = JsonConvert.SerializeObject(dataPoint);
                        break;
                    case WatchStatus.Completed:
                        dataPoint = new DataPointStackedBar(seriesStatus.Count, seriesStatus.WatchStatus.ToString(), "#72ffc6");
                        ViewBag.CompletedSeries = JsonConvert.SerializeObject(dataPoint);
                        break;
                    case WatchStatus.Dropped:
                        dataPoint = new DataPointStackedBar(seriesStatus.Count, seriesStatus.WatchStatus.ToString(), "#ff6b6b");
                        ViewBag.DroppedSeries = JsonConvert.SerializeObject(dataPoint); ;
                        break;
                    case WatchStatus.OnHold:
                        dataPoint = new DataPointStackedBar(seriesStatus.Count, seriesStatus.WatchStatus.ToString(), "#ffb96a");
                        ViewBag.OnHoldSeries = JsonConvert.SerializeObject(dataPoint); ;
                        break;
                    case WatchStatus.PlanToWatch:
                        dataPoint = new DataPointStackedBar(seriesStatus.Count, seriesStatus.WatchStatus.ToString(), "#dddddd");
                        ViewBag.PlanToWatchSeries = JsonConvert.SerializeObject(dataPoint); ;
                        break;
                }
            }

            var userSeriesForUser = _context.UsersSeries
                .Include(x => x.Series)
                .Where(x => x.UserId == userId);

            var episodesWatched = userSeriesForUser.Sum(x => x.EpisodesWatched);
            var seasonsWatched = userSeriesForUser.Sum(x => x.SeasonsWatched);

            var meanScore = Math.Round(Convert.ToDouble(userSeriesForUser.Sum(x => x.Rating)) / userSeriesForUser.Where(x => x.Rating != 0).ToList().Count, 2);

            var userSeriesWithEpisodes = userSeriesForUser.Where(x => x.EpisodesWatched != 0);

            int totalWatchTime = 0;

            foreach(var userSeriesEpisodes in userSeriesWithEpisodes)
            {
                totalWatchTime += userSeriesEpisodes.EpisodesWatched * userSeriesEpisodes.Series.EpisodeRuntime;
            }

            TimeSpan watchTimeSpan = TimeSpan.FromMinutes(totalWatchTime);

            string watchTime = string.Format("{0} days {1} hours {2} minutes",
                watchTimeSpan.Days,
                watchTimeSpan.Hours,
                watchTimeSpan.Minutes);

            viewModel.FavoriteGenre = viewModel.GenreSummary
                .OrderByDescending(x => x.Count)
                .FirstOrDefault()
                .Genre.ToString();

            viewModel.WatchedEpisodes = episodesWatched;
            viewModel.WatchedSeasons = seasonsWatched;
            viewModel.MeanScore = meanScore;
            viewModel.TotalWatchTime = watchTime;
            viewModel.TotalEntries = userSeriesForUser.Count();

            return View(viewModel);
        }

        public async Task<IActionResult> Calendar(string username, int? week)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var viewModel = new CalendarViewModel
            {
                Username = username,
                Monday = new List<SeriesOnCalendarViewModel>(),
                Tuesday = new List<SeriesOnCalendarViewModel>(),
                Wednesday = new List<SeriesOnCalendarViewModel>(),
                Thursday = new List<SeriesOnCalendarViewModel>(),
                Friday = new List<SeriesOnCalendarViewModel>(),
                Saturday = new List<SeriesOnCalendarViewModel>(),
                Sunday = new List<SeriesOnCalendarViewModel>()
            };

            if (week == null) week = 0;

            viewModel.CurrentWeek = week.Value;


            var today = DateTime.UtcNow.AddDays(week.Value * 7);

            if (week == 0)
            {
                viewModel.Today = today.DayOfWeek;
            }

            var nearestMonday = today;
            while ( nearestMonday.DayOfWeek != DayOfWeek.Monday)
            {
                nearestMonday = nearestMonday.AddDays(-1);
            }

            viewModel.StartOfWeekDate = nearestMonday;
            viewModel.EndOfWeekDate = nearestMonday.AddDays(6);

            var airingSeries = await _context.UsersSeries
                .Include(x => x.Series)
                .Where(x => x.UserId == userId && x.Series.Status == SeriesStatus.Airing)
                .ToListAsync();

            foreach (var userSeries in airingSeries)
            {
                // TODO should only one season be taken into consideration ?
                var latestSeason = await _context.Seasons
                    .Where(x => x.SeriesId == userSeries.SeriesId)
                    .Include(x => x.Episodes)
                    .OrderByDescending(x => x.AiredFrom)
                    .FirstOrDefaultAsync();

                // should be one
                var episodesInThisWeek = await latestSeason.Episodes
                    .Where(x => DatesAreInTheSameWeek(x.Aired, nearestMonday)).ToListAsync();

                foreach (var episodeInThisWeek in episodesInThisWeek)
                {
                    if (episodeInThisWeek != null && episodeInThisWeek.Aired != null)
                    {
                        var seriesViewModel = new SeriesOnCalendarViewModel
                        {
                            SeriesId = userSeries.SeriesId,
                            SeriesName = userSeries.Series.Name,
                            AirTime = userSeries.Series.AirTime,
                            SeasonNumber = episodeInThisWeek.SeasonNumber,
                            EpisodeNumber = episodeInThisWeek.SeasonEpisodeNumber
                        };

                        // userSeries.Series.AirDayOfWeek
                        switch (episodeInThisWeek.Aired.Value.DayOfWeek)
                        {
                            case DayOfWeek.Monday:
                                viewModel.Monday.Add(seriesViewModel);
                                break;
                            case DayOfWeek.Tuesday:
                                viewModel.Tuesday.Add(seriesViewModel);
                                break;
                            case DayOfWeek.Wednesday:
                                viewModel.Wednesday.Add(seriesViewModel);
                                break;
                            case DayOfWeek.Thursday:
                                viewModel.Thursday.Add(seriesViewModel);
                                break;
                            case DayOfWeek.Friday:
                                viewModel.Friday.Add(seriesViewModel);
                                break;
                            case DayOfWeek.Saturday:
                                viewModel.Saturday.Add(seriesViewModel);
                                break;
                            case DayOfWeek.Sunday:
                                viewModel.Sunday.Add(seriesViewModel);
                                break;
                        }

                    }
                }
            }


            return View(viewModel);
        }

        private bool DatesAreInTheSameWeek(DateTime? date1, DateTime date2)
        {
            if (date1 != null)
            {
                DateTime date = date1.Value;
                var cal = System.Globalization.DateTimeFormatInfo.CurrentInfo.Calendar;
                var d1 = date.Date.AddDays(-1 * (int)cal.GetDayOfWeek(date) - 1);
                var d2 = date2.Date.AddDays(-1 * (int)cal.GetDayOfWeek(date2) - 1);

                return d1 == d2;
            }
            return false;
        }

        public async Task<IActionResult> SeriesList(string username)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var viewModel = new SeriesListViewModel { Username = username };

            var userSeries = await _context.UsersSeries.Include(x => x.Series)
                .Where(x => x.UserId == userId)
                .ToListAsync();

            viewModel.SeriesByWatchStatus = new List<SeriesOnListWithWatchStatusViewModel>();

            var watchingSeries = userSeries.Where(x => x.WatchStatus == WatchStatus.Watching);
            viewModel.SeriesByWatchStatus.Add(ToSeriesOnList(watchingSeries, "Watching"));

            var completedSeries = userSeries.Where(x => x.WatchStatus == WatchStatus.Completed);
            viewModel.SeriesByWatchStatus.Add(ToSeriesOnList(completedSeries, "Completed"));

            var onHoldSeries = userSeries.Where(x => x.WatchStatus == WatchStatus.OnHold);
            viewModel.SeriesByWatchStatus.Add(ToSeriesOnList(onHoldSeries, "On hold"));

            var planToWatchSeries = userSeries.Where(x => x.WatchStatus == WatchStatus.PlanToWatch);
            viewModel.SeriesByWatchStatus.Add(ToSeriesOnList(planToWatchSeries, "Plan to watch"));

            var droppedSeries = userSeries.Where(x => x.WatchStatus == WatchStatus.Dropped);
            viewModel.SeriesByWatchStatus.Add(ToSeriesOnList(droppedSeries, "Dropped"));

            return View(viewModel);
        }

        public async Task<IActionResult> Favourites(string username)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var viewModel = new FavouriteListViewModel
            {
                Username = username,
                FavouriteSeries = new List<FavouriteSeriesOnListViewModel>(),
                FavouritePersons = new List<FavouritePersonOnListViewModel>()
            };

            var favouriteSeries = await _context.FavoritesSeries
                .Include(x => x.Series)
                .Where(x => x.UserId == userId)
                .ToListAsync();

            foreach (var favourite in favouriteSeries)
            {
                var favouriteSeriesViewModel = new FavouriteSeriesOnListViewModel
                {
                    SeriesId = favourite.SeriesId,
                    SeriesName = favourite.Series.Name,
                    PosterContent = favourite.Series.PosterContent,
                    LastChangedAt = favourite.Series.CreatedAt
                };

                if (favourite.Series.LastChangedAt != null)
                {
                    favouriteSeriesViewModel.LastChangedAt = favourite.Series.LastChangedAt.Value;
                }

                viewModel.FavouriteSeries.Add(favouriteSeriesViewModel);
            }

            var favouritePersons = await _context.FavoritesPersons
                .Include(x => x.Person)
                .Where(x => x.UserId == userId)
                .ToListAsync();

            foreach (var favourite in favouritePersons)
            {
                var favoruitePersonViewModel = new FavouritePersonOnListViewModel
                {
                    PersonId = favourite.PersonId,
                    PersonName = favourite.Person.Name,
                    PosterContent = favourite.Person.PosterContent,
                    LastChangedAt = favourite.Person.CreatedAt
                };

                if (favourite.Person.LastChangedAt != null)
                {
                    favoruitePersonViewModel.LastChangedAt = favourite.Person.LastChangedAt.Value;
                }

                viewModel.FavouritePersons.Add(favoruitePersonViewModel);
            }

            return View(viewModel);
        }

        public async Task<IActionResult> ReviewList(string username)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var reviews = await _context.SeriesReviews
                .Include(x => x.Series)
                .Where(x => x.UserId == userId)
                .ToListAsync();

            var viewModel = new ReviewsViewModel();

            viewModel.Username = username;
            viewModel.Reviews = new List<ReviewOnListViewModel>();

            foreach(var review in reviews)
            {
                var reviewViewModel = new ReviewOnListViewModel
                {
                    Id = review.Id,
                    SeriesName = review.Series.Name,
                    Likes = review.Likes
                };

                viewModel.Reviews.Add(reviewViewModel);
            }

            return View(viewModel);
        }

        private SeriesOnListWithWatchStatusViewModel ToSeriesOnList(IEnumerable<UserSeries> userSeries, string watchStatus)
        {

            var seriesOnListViewModels = new List<SeriesOnListViewModel>();

            foreach (var series in userSeries)
            {
                var seriesOnList = new SeriesOnListViewModel
                {
                    Name = series.Series.Name,
                    Id = series.SeriesId,
                    NumberOfEpisodes = series.Series.NumberOfEpisodes,
                    EpisodesWatched = series.EpisodesWatched,
                    NumberOfSeasons = series.Series.NumberOfSeasons,
                    SeasonsWatched = series.SeasonsWatched,
                    PosterContent = series.Series.PosterContent,
                    UserRating = series.Rating
                };

                seriesOnListViewModels.Add(seriesOnList);
            }

            var seriesWithWatchStatus = new SeriesOnListWithWatchStatusViewModel
            {
                WatchStatus = watchStatus,
                Series = seriesOnListViewModels
            };

            return seriesWithWatchStatus;
        }
    }
}
