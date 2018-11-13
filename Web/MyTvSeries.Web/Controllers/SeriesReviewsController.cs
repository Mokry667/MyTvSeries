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
using MyTvSeries.Web.Models.Review;

namespace MyTvSeries.Web.Controllers
{
    public class SeriesReviewsController : Controller
    {
        private readonly TvSeriesContext _context;

        public SeriesReviewsController(TvSeriesContext context)
        {
            _context = context;
        }

        // GET: SeriesReviews
        public async Task<IActionResult> Index()
        {
            var tvSeriesContext = _context.SeriesReviews.Include(s => s.Series).Include(s => s.User);
            return View(await tvSeriesContext.ToListAsync());
        }

        // GET: SeriesReviews/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seriesReview = await _context.SeriesReviews
                .Include(s => s.Series)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (seriesReview == null)
            {
                return NotFound();
            }

            return View(seriesReview);
        }

        // GET: SeriesReviews/Create
        public IActionResult Create(long seriesId)
        {
            var viewModel = new ReviewCreateViewModel
            {
                SeriesId = seriesId
            };
            return View(viewModel);
        }

        // POST: SeriesReviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReviewCreateViewModel viewModel)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (ModelState.IsValid)
            {
                var seriesReview = new SeriesReview()
                {
                    SeriesId = viewModel.SeriesId,
                    UserId = userId,
                    Content = viewModel.Content,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = userId,
                    Likes = 0
                };
                await _context.SeriesReviews.AddAsync(seriesReview);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Series", new { id = viewModel.SeriesId });
            }
            return View(viewModel);
        }

        // GET: SeriesReviews/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seriesReview = await _context.SeriesReviews.FindAsync(id);
            if (seriesReview == null)
            {
                return NotFound();
            }
            ViewData["SeriesId"] = new SelectList(_context.Series, "Id", "Id", seriesReview.SeriesId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", seriesReview.UserId);
            return View(seriesReview);
        }

        // POST: SeriesReviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,SeriesId,UserId,Content,Likes,CreatedBy,CreatedAt,LastChangedBy,LastChangedAt")] SeriesReview seriesReview)
        {
            if (id != seriesReview.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(seriesReview);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeriesReviewExists(seriesReview.Id))
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
            ViewData["SeriesId"] = new SelectList(_context.Series, "Id", "Id", seriesReview.SeriesId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", seriesReview.UserId);
            return View(seriesReview);
        }

        // GET: SeriesReviews/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seriesReview = await _context.SeriesReviews
                .Include(s => s.Series)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (seriesReview == null)
            {
                return NotFound();
            }

            return View(seriesReview);
        }

        // POST: SeriesReviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var seriesReview = await _context.SeriesReviews.FindAsync(id);
            _context.SeriesReviews.Remove(seriesReview);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SeriesReviewExists(long id)
        {
            return _context.SeriesReviews.Any(e => e.Id == id);
        }
    }
}
