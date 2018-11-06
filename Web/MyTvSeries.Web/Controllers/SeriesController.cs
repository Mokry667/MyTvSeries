using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyTvSeries.Domain.Ef;
using MyTvSeries.Domain.Entities;
using MyTvSeries.Web.Models.Enums;
using MyTvSeries.Web.Models.Series;
using X.PagedList;

namespace MyTvSeries.Web.Controllers
{
    public class SeriesController : Controller
    {
        private readonly TvSeriesContext _context;

        public SeriesController(TvSeriesContext context)
        {
            _context = context;
        }

        // GET: Series
        public async Task<IActionResult> Index(int? page, string keyword)
        {
            IQueryable<Series> allSeries = null;
            if (!string.IsNullOrEmpty(keyword))
            {
                allSeries = _context.Series.Where(x => x.Name.Contains(keyword)).AsQueryable();
            }
            else
            {
                allSeries = _context.Series.AsQueryable();
            }

            var pageNumber = page ?? 1;

            var pageSize = 25;

            var pagedSeries = await allSeries.ToPagedListAsync(pageNumber, pageSize);

            var viewModels = new List<SeriesIndexViewModel>();

            foreach (var series in pagedSeries)
            {
                var viewModel = new SeriesIndexViewModel
                {
                    Id = series.Id,
                    Name = series.Name,
                    UserRating = series.UserRating,
                    PosterContent = series.PosterContent
                };
                viewModels.Add(viewModel);
            }

            var pagedViewModels = new StaticPagedList<SeriesIndexViewModel>(viewModels, pageNumber, pageSize, pagedSeries.TotalItemCount);

            var viewModelWithKeyword = new SeriesIndexSearchViewModel
            {
                ViewModels = pagedViewModels,
                Keyword = keyword
            };


            return View(viewModelWithKeyword);
        }

        // GET: Series/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var series = await _context.Series.Include(x => x.SeriesGenres).ThenInclude(x => x.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (series == null)
            {
                return NotFound();
            }


            // TODO move to mapper
            var viewModel = new UserSeriesDetailViewModel
            {
                SeriesId = series.Id,
                Name = series.Name,
                OriginalName = series.OriginalName,
                SiteRating = series.UserRating,
                Overview = series.Overview,
                Status = series.Status,
                AiredFrom = series.AiredFrom,
                AiredTo = series.AiredTo,
                AirDayOfWeek = series.AirDayOfWeek,
                AirTime = series.AirTime,
                NumberOfSeasons = series.NumberOfSeasons,
                NumberOfEpisodes = series.NumberOfEpisodes,
                EpisodeRuntime = series.EpisodeRuntime,
                TotalRuntime = series.TotalRuntime,
                PosterContent = series.PosterContent,
                Genres = new List<Genre>()
            };

            foreach (var seriesGenre in series.SeriesGenres)
            {
                viewModel.Genres.Add(seriesGenre.Genre);
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId != null)
            {
                var userSeries = await _context.UsersSeries.FirstOrDefaultAsync(x => x.UserId == userId && x.SeriesId == id);

                if (userSeries != null)
                {
                    viewModel.WatchStatus = userSeries.WatchStatus;
                    viewModel.SeriesRating = (SeriesRating) userSeries.Rating;
                    viewModel.SeasonsWatched = userSeries.SeasonsWatched;
                    viewModel.EpisodesWatched = userSeries.EpisodesWatched;
                }
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // GET: Series/Details/5
        public async Task<IActionResult> Details(UserSeriesDetailViewModel viewModel)
        {
            viewModel.Genres = new List<Genre>();

            Series series = null;

            if (ModelState.IsValid)
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var userSeries =
                    await _context.UsersSeries.FirstOrDefaultAsync(x =>
                        x.UserId == userId && x.SeriesId == viewModel.SeriesId);

                if (userSeries == null)
                {
                    userSeries = new UserSeries
                    {
                        SeriesId = viewModel.SeriesId,
                        UserId = userId,
                        Rating = (int) viewModel.SeriesRating,
                        WatchStatus = viewModel.WatchStatus,
                        CreatedBy = userId,
                        CreatedAt = DateTime.UtcNow,
                        EpisodesWatched = viewModel.EpisodesWatched,
                        SeasonsWatched = viewModel.SeasonsWatched,
                    };

                    _context.Add(userSeries);


                    if (viewModel.SeriesRating != SeriesRating.NotRated)
                    {
                        series = await _context.Series.FirstOrDefaultAsync(x => x.Id == viewModel.SeriesId);
                        series.UserVotes++;
                        var seriesRating = series.SeriesUsers.Sum(x => x.Rating);
                        series.UserRating = (decimal) seriesRating / series.UserVotes;
                        _context.Update(series);
                    }
                }
                else
                {
                    userSeries.WatchStatus = viewModel.WatchStatus;
                    userSeries.Rating = (int) viewModel.SeriesRating;
                    userSeries.EpisodesWatched = viewModel.EpisodesWatched;
                    userSeries.SeasonsWatched = viewModel.SeasonsWatched;

                    series = await _context.Series
                        .Include(x => x.SeriesGenres)
                        .ThenInclude(x => x.Genre)
                        .FirstOrDefaultAsync(x => x.Id == viewModel.SeriesId);

                    if (userSeries.Rating != 0)
                    {
                        var seriesRating = series.SeriesUsers.Sum(x => x.Rating);
                        series.UserRating = (decimal)seriesRating / series.UserVotes;
                    }

                    _context.Update(series);
                    _context.Update(userSeries);
                }

                series = await _context.Series
                    .Include(x => x.SeriesGenres)
                    .ThenInclude(x => x.Genre)
                    .FirstOrDefaultAsync(x => x.Id == viewModel.SeriesId);

                if (series != null)
                {
                    viewModel.SiteRating = series.UserRating;
                    viewModel.PosterContent = series.PosterContent;

                    foreach (var seriesGenre in series.SeriesGenres)
                    {
                        viewModel.Genres.Add(seriesGenre.Genre);
                    }
                }

                await _context.SaveChangesAsync();

                return View(viewModel);
            }
            else
            {
                series = await _context.Series
                    .Include(x => x.SeriesGenres)
                    .ThenInclude(x => x.Genre)
                    .FirstOrDefaultAsync(x => x.Id == viewModel.SeriesId);

                viewModel.SiteRating = series.UserRating;
                viewModel.PosterContent = series.PosterContent;

                foreach (var seriesGenre in series.SeriesGenres)
                {
                    viewModel.Genres.Add(seriesGenre.Genre);
                }

                return View(viewModel);
            }
        }

        // GET: Series/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Series/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TvDbId,MovieDbId,ImdbId,Name,OriginalName,Overview,Status,AiredFrom,AiredTo,AirDayOfWeek,AirTime,NumberOfSeasons,NumberOfEpisodes,TotalRuntime,UserRating,IsImportEnabled,CreatedBy,CreatedAt,LastChangedBy,LastChangedAt")] Series series)
        {
            if (ModelState.IsValid)
            {
                _context.Add(series);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(series);
        }

        // GET: Series/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var series = await _context.Series.FindAsync(id);
            if (series == null)
            {
                return NotFound();
            }
            return View(series);
        }

        // POST: Series/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,TvDbId,MovieDbId,ImdbId,Name,OriginalName,Overview,Status,AiredFrom,AiredTo,AirDayOfWeek,AirTime,NumberOfSeasons,NumberOfEpisodes,TotalRuntime,UserRating,IsImportEnabled,CreatedBy,CreatedAt,LastChangedBy,LastChangedAt")] Series series)
        {
            if (id != series.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(series);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeriesExists(series.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(series);
        }

        // GET: Series/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var series = await _context.Series
                .FirstOrDefaultAsync(m => m.Id == id);
            if (series == null)
            {
                return NotFound();
            }

            return View(series);
        }

        // POST: Series/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var series = await _context.Series.FindAsync(id);
            _context.Series.Remove(series);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SeriesExists(long id)
        {
            return _context.Series.Any(e => e.Id == id);
        }
    }
}
