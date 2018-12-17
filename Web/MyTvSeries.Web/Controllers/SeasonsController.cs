using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyTvSeries.Domain.Ef;
using MyTvSeries.Domain.Entities;
using MyTvSeries.Web.Models.Charts;
using MyTvSeries.Web.Models.Enums;
using MyTvSeries.Web.Models.Seasons;
using Newtonsoft.Json;

namespace MyTvSeries.Web.Controllers
{
    public class SeasonsController : Controller
    {
        private readonly TvSeriesContext _context;

        public SeasonsController(TvSeriesContext context)
        {
            _context = context;
        }

        // GET: Seasons
        public async Task<IActionResult> Index(long? seriesId)
        {
            var seasons = await _context.Seasons.Include(s => s.Series)
                .Include(s => s.Episodes)
                .Where(x => x.SeriesId == seriesId)
                .ToListAsync();

            var viewModel = new SeasonIndexListViewModel
            {
                SeriesId = seriesId,
                ViewModels = new List<SeasonIndexViewModel>()
            };

            List<DataPointLine> LineChartDataPoints = new List<DataPointLine>();

            foreach (var season in seasons)
            {
                var seasonViewModel = new SeasonIndexViewModel
                {
                    Id = season.Id,
                    Name = season.Name,
                    SeasonNumber = season.SeasonNumber,
                    NumberOfEpisodes = season.NumberOfEpisodes,
                };

                if (season.AiredFrom != null)
                    seasonViewModel.AiredFrom = season.AiredFrom.Value.ToString("dd MMMM yyyy");

                viewModel.ViewModels.Add(seasonViewModel);


                var meanRating = season.Episodes.Sum(x => x.UserRating) / season.Episodes.Count();

                var dataPoint = new DataPointLine(Convert.ToDouble(meanRating), "S" + season.SeasonNumber.ToString());
                LineChartDataPoints.Add(dataPoint);

            }

            ViewBag.DataPointsLine = JsonConvert.SerializeObject(LineChartDataPoints);

            return View(viewModel);
        }

        // GET: Seasons/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var season = await _context.Seasons
                .Include(s => s.Series)
                .Include(s => s.Episodes)
                .FirstOrDefaultAsync(m => m.Id == id);

            var viewModel = new SeasonDetailViewModel
            {
                Id = season.Id,
                SeriesId = season.SeriesId,
                Name = season.Name,
                Overview = season.Overview,
                SeasonNumber = season.SeasonNumber,
            };

            if (season.AiredFrom != null)
                viewModel.AiredFrom = season.AiredFrom.Value.ToString("dd MMMM yyyy");

            viewModel.Episodes = new List<EpisodeOnSeasonViewModel>();

            List<DataPointLine> LineChartDataPoints = new List<DataPointLine>();

            foreach (var episode in season.Episodes)
            {
                var episodeViewModel = new EpisodeOnSeasonViewModel
                {
                    Id = episode.Id,
                    Name = episode.Name,
                    Overview = episode.Overview,
                    EpisodeNumber = episode.SeasonEpisodeNumber,
                    EpisodeSiteRating = episode.UserRating
                };

                if (episode.Aired != null)
                    episodeViewModel.Aired = episode.Aired.Value.ToString("dd MMMM yyyy");

                var userEpisode = await _context.UserEpisodes
                    .Where(x => x.EpisodeId == episode.Id && x.UserId == userId)
                    .FirstOrDefaultAsync();

                if (userEpisode != null)
                {
                    episodeViewModel.Rating = (EpisodeRating) userEpisode.Rating;
                }

                viewModel.Episodes.Add(episodeViewModel);

                var dataPoint = new DataPointLine(Convert.ToDouble(episode.UserRating), "E" + episode.SeasonEpisodeNumber.ToString());
                LineChartDataPoints.Add(dataPoint);
            }

            ViewBag.DataPointsLine = JsonConvert.SerializeObject(LineChartDataPoints);

            viewModel.Episodes = viewModel.Episodes.OrderBy(x => x.EpisodeNumber).ToList();


            if (season == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        [HttpPost]
        public void RateEpisode(string episodeIdString, string ratingString)
        {
            var episodeId = Convert.ToInt64(episodeIdString);
            var rating = Convert.ToInt32(ratingString);

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userEpisode = _context.UserEpisodes
                .Where(x => x.EpisodeId == episodeId && x.UserId == userId)
                .FirstOrDefault();

            if (userEpisode == null)
            {
                var episodeRating = new UserEpisode
                {
                    EpisodeId = episodeId,
                    UserId = userId,
                    Rating = rating,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = userId
                };
                _context.UserEpisodes.Add(episodeRating);
                _context.SaveChanges();
            }
            else
            {
                userEpisode.Rating = rating;
                userEpisode.LastChangedAt = DateTime.UtcNow;
                userEpisode.LastChangedBy = userId;
                _context.Update(userEpisode);
                _context.SaveChanges();
            }

            var episode = _context.Episodes.Where(x => x.Id == episodeId).FirstOrDefault();

            // TODO handle when episode vote is updated not added

            var userEpisodes = _context.UserEpisodes.Where(x => x.EpisodeId == episodeId).ToList();
            var numberOfVotes = userEpisodes.Count();
            var ratingSum = userEpisodes.Sum(x => x.Rating);

            var newRating = (decimal)ratingSum / numberOfVotes;
            episode.UserRating = newRating;
            _context.Update(episode);
            _context.SaveChanges();
        }


        // GET: Seasons/Create
        public IActionResult Create()
        {
            ViewData["SeriesId"] = new SelectList(_context.Series, "Id", "Id");
            return View();
        }

        // POST: Seasons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MovieDbId,SeriesId,Name,Overview,SeasonNumber,AiredFrom,NumberOfEpisodes,IsImportEnabled,CreatedBy,CreatedAt,LastChangedBy,LastChangedAt")] Season season)
        {
            if (ModelState.IsValid)
            {
                _context.Add(season);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SeriesId"] = new SelectList(_context.Series, "Id", "Id", season.SeriesId);
            return View(season);
        }

        // GET: Seasons/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var season = await _context.Seasons.FindAsync(id);
            if (season == null)
            {
                return NotFound();
            }
            ViewData["SeriesId"] = new SelectList(_context.Series, "Id", "Id", season.SeriesId);
            return View(season);
        }

        // POST: Seasons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,MovieDbId,SeriesId,Name,Overview,SeasonNumber,AiredFrom,NumberOfEpisodes,IsImportEnabled,CreatedBy,CreatedAt,LastChangedBy,LastChangedAt")] Season season)
        {
            if (id != season.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(season);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeasonExists(season.Id))
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
            ViewData["SeriesId"] = new SelectList(_context.Series, "Id", "Id", season.SeriesId);
            return View(season);
        }

        // GET: Seasons/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var season = await _context.Seasons
                .Include(s => s.Series)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (season == null)
            {
                return NotFound();
            }

            return View(season);
        }

        // POST: Seasons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var season = await _context.Seasons.FindAsync(id);
            _context.Seasons.Remove(season);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SeasonExists(long id)
        {
            return _context.Seasons.Any(e => e.Id == id);
        }
    }
}
