using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyTvSeries.Domain.Ef;
using MyTvSeries.Domain.Entities;
using MyTvSeries.Web.Models;
using MyTvSeries.Web.Models.People;
using MyTvSeries.Web.Models.Series;
using X.PagedList;

namespace MyTvSeries.Web.Controllers
{
    public class PeopleController : Controller
    {
        private readonly TvSeriesContext _context;

        public PeopleController(TvSeriesContext context)
        {
            _context = context;
        }

        // GET: People
        public async Task<IActionResult> Index(int? page, string keyword)
        {
            IQueryable<Person> allPersons = null;
            if (!string.IsNullOrEmpty(keyword))
            {
                allPersons = _context.Persons.Where(x => x.Name.Contains(keyword)).AsQueryable();
            }
            else
            {
                allPersons = _context.Persons.AsQueryable();
            }

            var pageNumber = page ?? 1;

            var pageSize = 25;

            var pagedSeries = await allPersons.ToPagedListAsync(pageNumber, pageSize);

            var viewModels = new List<PersonIndexViewModel>();

            foreach (var person in pagedSeries)
            {
                var viewModel = new PersonIndexViewModel
                {
                    Id = person.Id,
                    Name = person.Name,
                    PosterContent = person.PosterContent
                };
                viewModels.Add(viewModel);
            }

            var pagedViewModels = new StaticPagedList<PersonIndexViewModel>(viewModels, pageNumber, pageSize, pagedSeries.TotalItemCount);

            var viewModelWithKeyword = new PersonIndexSearchViewModel
            {
                ViewModels = pagedViewModels,
                Keyword = keyword
            };

            return View(viewModelWithKeyword);
        }

        // GET: People/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Persons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: People/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TvDbId,MovieDbId,ImdbId,Name,Gender,Biography,Birthday,Deathday,PlaceOfBirth,IsImportEnabled,PosterName,PosterContent,CreatedBy,CreatedAt,LastChangedBy,LastChangedAt")] Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Persons.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,TvDbId,MovieDbId,ImdbId,Name,Gender,Biography,Birthday,Deathday,PlaceOfBirth,IsImportEnabled,PosterName,PosterContent,CreatedBy,CreatedAt,LastChangedBy,LastChangedAt")] Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.Id))
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
            return View(person);
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Persons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var person = await _context.Persons.FindAsync(id);
            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(long id)
        {
            return _context.Persons.Any(e => e.Id == id);
        }
    }
}
