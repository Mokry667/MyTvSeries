using System.Collections.Generic;
using System.Drawing;
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

            return View(viewModel);
        }

        public async Task<IActionResult> Calendar(string username)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var viewModel = new CalendarViewModel { Username = username };

            // take series that are still airing
            var airingSeries = await _context.UsersSeries.Include(x => x.Series)
                .Where(x => x.UserId == userId && x.Series.Status == SeriesStatus.Airing)
                .ToListAsync();


            return View(viewModel);
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
