using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyTvSeries.Domain.Ef;
using MyTvSeries.Domain.Enums;
using MyTvSeries.Web.Models.Profile;

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
                            Count = 0
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

            return View(viewModel);
        }
    }
}
