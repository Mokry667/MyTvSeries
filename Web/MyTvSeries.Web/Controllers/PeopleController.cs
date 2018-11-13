using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyTvSeries.Domain.Ef;
using MyTvSeries.Domain.Entities;
using MyTvSeries.Web.Models.People;
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

            var viewModel = new PersonDetailViewModel
            {
                PersonId = person.Id,
                Name = person.Name,
                Biography = person.Biography,
                Birthday = person.Birthday,
                Gender = person.Gender.ToString(),
                PlaceOfBirth = person.PlaceOfBirth,
                PosterContent = person.PosterContent,
                Deathday = person.Deathday,
                Cast = new List<PersonDetailCastViewModel>(),
                Departments = new List<PersonDetailDepartmentCrewViewModel>()
            };

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var favourite = await _context
                .FavoritesPersons
                .Where(x => x.UserId == userId)
                .FirstOrDefaultAsync();

            viewModel.IsFavourite = favourite != null;

            var cast = await _context.Characters
                .Include(x => x.SeriesCharacters)
                .ThenInclude(x => x.Series)
                .Where(x => x.PersonId == id)
                .ToListAsync();

            foreach (var character in cast)
            {
                var seriesViewModel = new PersonDetailCastViewModel();

                var seriesCharacters = character.SeriesCharacters.FirstOrDefault(x => x.CharacterId == character.Id);

                if (seriesCharacters != null)
                {
                    seriesViewModel.SeriesName = seriesCharacters.Series.Name;
                    seriesViewModel.SeriesId = seriesCharacters.Series.Id;
                    seriesViewModel.CharacterName = character.Name;
                    seriesViewModel.SeriesRating = seriesCharacters.Series.UserRating;

                    if (seriesCharacters.Series.AiredFrom != null)
                        seriesViewModel.Year = seriesCharacters.Series.AiredFrom.Value.Year;

                    viewModel.Cast.Add(seriesViewModel);
                }
            }

            viewModel.Cast = viewModel
                .Cast
                .OrderByDescending(x => x.Year)
                .ToList();

            var crew = await _context.Crews
                .Where(x => x.PersonId == id)
                .Include(x => x.Series)
                .ToListAsync();

            var departments = crew.Select(x => x.Department).Distinct();

            foreach (var department in departments)
            {
                var crewInDepartment = crew.Where(x => x.Department == department).ToList();

                var departmentViewModel = new PersonDetailDepartmentCrewViewModel
                {
                    DepartmentName = department
                };


                var crews = new List<PersonDetailCrewViewModel>();

                foreach (var crewMember in crewInDepartment)
                {
                    var seriesViewModel = new PersonDetailCrewViewModel
                    {
                        SeriesName = crewMember.Series.Name,
                        SeriesId = crewMember.Series.Id,
                        Job = crewMember.Job,
                        SeriesRating = crewMember.Series.UserRating
                    };

                    if (crewMember.Series.AiredFrom != null)
                        seriesViewModel.Year = crewMember.Series.AiredFrom.Value.Year;

                    crews.Add(seriesViewModel);
                }

                crews = crews.OrderByDescending(x => x.Year).ToList();

                departmentViewModel.Crew = crews;

                viewModel.Departments.Add(departmentViewModel);
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrRemoveFromFavourites(long id)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var favourite = await _context
                .FavoritesPersons
                .Where(x => x.UserId == userId)
                .FirstOrDefaultAsync();

            // add to favourites
            if (favourite == null)
            {
                var newFavourite = new FavoritesPerson
                {
                    UserId = userId,
                    PersonId = id,
                    CreatedBy = userId,
                    CreatedAt = DateTime.UtcNow
                };

                await _context.FavoritesPersons.AddAsync(newFavourite);
            }
            else 
            {
                _context.FavoritesPersons.Remove(favourite);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id });
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
