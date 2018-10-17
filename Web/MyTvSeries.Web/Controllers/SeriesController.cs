﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyTvSeries.Domain.Ef;
using MyTvSeries.Domain.Entities;

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
        public async Task<IActionResult> Index()
        {
            // TODO pagination
            return View(await _context.Series.ToListAsync());
        }

        // GET: Series/Details/5
        public async Task<IActionResult> Details(long? id)
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
