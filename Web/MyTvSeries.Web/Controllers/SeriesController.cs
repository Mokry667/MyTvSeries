using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Pages.Internal.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyTvSeries.Domain.Ef;
using MyTvSeries.Domain.Entities;
using MyTvSeries.Domain.Enums;
using MyTvSeries.Domain.Identity;
using MyTvSeries.Web.Models.Enums;
using MyTvSeries.Web.Models.Series;
using X.PagedList;

namespace MyTvSeries.Web.Controllers
{
    public class SeriesController : Controller
    {
        private readonly TvSeriesContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SeriesController(TvSeriesContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Series
        public async Task<IActionResult> Index(int? page, string keyword, string sortBy, string ChosenGenre, string ChosenStatus)
        {
            IQueryable<Series> allSeries = null;
            if (!string.IsNullOrEmpty(keyword))
            {
                allSeries = _context.Series
                    .Include(x => x.SeriesGenres)
                    .Where(x => x.Name.Contains(keyword)).AsQueryable();
            }
            else
            {
                allSeries = _context.Series
                    .Include(x => x.SeriesGenres)
                    .AsQueryable();
            }

            if (!string.IsNullOrEmpty(ChosenGenre))
            {
                allSeries = allSeries.Where(x => x.SeriesGenres.Any(y => y.Genre.Name == ChosenGenre));
            }

            if (!string.IsNullOrEmpty(ChosenStatus))
            {
                Enum.TryParse(ChosenStatus, out SeriesStatus myStatus);
                allSeries = allSeries.Where(x => x.Status == myStatus);
            }

            switch (sortBy)
            {
                case "Rating":
                    allSeries = allSeries.OrderByDescending(x => x.UserRating);
                    break;
                case "Name":
                    allSeries = allSeries.OrderBy(x => x.Name);
                    break;
                default:
                    break;
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
                Keyword = keyword,
            };

            if (!string.IsNullOrEmpty(ChosenGenre))
            {
                viewModelWithKeyword.ChosenGenre = ChosenGenre;
            }

            if (!string.IsNullOrEmpty(ChosenStatus))
            {
                viewModelWithKeyword.ChosenStatus = ChosenStatus;
            }

            viewModelWithKeyword.Genres = await _context.Genres.Select(x => x.Name).ToListAsync();
            viewModelWithKeyword.Statuses = Enum.GetNames(typeof(SeriesStatus)).ToList();

            return View(viewModelWithKeyword);
        }

        // GET: Series/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var series = await _context.Series.Include(x => x.Seasons)
                .Include(x => x.SeriesCharacters)
                .Include(x => x.Crews)
                .Include(x => x.SeriesReviews)
                .ThenInclude(x => x.UserReviews)
                .Include(x => x.SeriesGenres)
                .ThenInclude(x => x.Genre)
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
                UserVotes = series.UserVotes,
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
                Genres = new List<Genre>(),
                SeasonSeries = new List<SeasonSeriesDetailViewModel>(),
                Cast = new List<CastSeriesDetailViewModel>(),
                Crew = new List<CrewSeriesDetailViewModel>(),
                Reviews = new List<SeriesReviewDetailViewModel>()
            };

            // GENRES
            foreach (var seriesGenre in series.SeriesGenres)
            {
                viewModel.Genres.Add(seriesGenre.Genre);
            }

            //SEASONS
            var seasonsOnView = 5;
            var seasons = series.Seasons.OrderBy(x => x.SeasonNumber).ToList();

            for (int i = 0; i < seasonsOnView; i++)
            {
                if (i < seasons.Count)
                {
                    var season = seasons.ElementAt(i);
                    var seasonViewModel = new SeasonSeriesDetailViewModel
                    {
                        SeasonId = season.Id,
                        NumberOfEpisodes = season.NumberOfEpisodes,
                        SeasonName = season.Name,
                        SeasonNumber = season.SeasonNumber,
                    };
                    if (season.AiredFrom != null)
                        seasonViewModel.AiredFrom = season.AiredFrom.Value.ToString("dd MMMM yyyy");

                    viewModel.SeasonSeries.Add(seasonViewModel);
                }
                else
                    break;
            }
            

            viewModel.IsMoreSeasons = seasons.Count > viewModel.SeasonSeries.Count;

            //CAST

            for (int i = 0; i < 4; i++)
            {
                if (i < series.SeriesCharacters.Count)
                {
                    var characterId = series.SeriesCharacters.ElementAt(i).CharacterId;
                    var castMember = await _context.Characters
                        .Include(x => x.Person)
                        .FirstOrDefaultAsync(x => x.Id == characterId);                   

                    var castViewModel = new CastSeriesDetailViewModel
                    {
                        PersonId = castMember.PersonId,
                        Character = castMember.Name,
                        Name = castMember.Person.Name,
                        Picture = castMember.Person.PosterContent
                    };

                    viewModel.Cast.Add(castViewModel);
                }
                else
                    break;
            }

            viewModel.IsMoreCast = series.SeriesCharacters.Count > viewModel.Cast.Count;

            // Crew

            for (int i = 0; i < 4; i++)
            {
                if (i < series.Crews.Count)
                {
                    var crew = series.Crews.ElementAt(i);
                    var personId = crew.PersonId;
                    var person = await _context.Persons
                        .FirstOrDefaultAsync(x => x.Id == personId);

                    var crewViewModel = new CrewSeriesDetailViewModel
                    {
                        PersonId = personId,
                        Name = person.Name,
                        Job = crew.Job,
                        Department = crew.Department,                  
                        Picture = person.PosterContent
                    };

                    viewModel.Crew.Add(crewViewModel);
                }
                else
                    break;
            }

            viewModel.IsMoreCrew = series.Crews.Count > viewModel.Crew.Count;

            // REVIEWS

            for (int i = 0; i < 3; i++)
            {
                if (i < series.SeriesReviews.Count)
                {
                    var review = series.SeriesReviews.ElementAt(i);

                    var reviewViewModel = new SeriesReviewDetailViewModel()
                    {
                        Id = review.Id,
                        Content = review.Content,
                        Likes = review.Likes,
                        WrittenOn = review.CreatedAt
                    };

                    if (review.LastChangedAt != null)
                    {
                        reviewViewModel.WrittenOn = review.LastChangedAt.Value;
                    }

                    var user = await _userManager.FindByIdAsync(review.UserId);
                    reviewViewModel.Username = user.UserName;

                    var userRating = await _context.UsersSeries
                        .Where(x => x.SeriesId == id && x.UserId == user.Id)
                        .FirstOrDefaultAsync();

                    if (userRating != null)
                    {
                        reviewViewModel.Rating = userRating.Rating;
                    }

                    reviewViewModel.IsRated = review.UserReviews.Where(x => x.UserId == userId).Any();

                    viewModel.Reviews.Add(reviewViewModel);
                }
                else
                    break;
            }
            

            if (userId != null)
            {
                var review =
                    await _context.SeriesReviews.FirstOrDefaultAsync(x => x.UserId == userId && x.SeriesId == id);

                if (review != null)
                {
                    viewModel.IsReviewWritten = true;
                    viewModel.ReviewId = review.Id;
                }

                var userSeries = await _context.UsersSeries.FirstOrDefaultAsync(x => x.UserId == userId && x.SeriesId == id);

                if (userSeries != null)
                {
                    viewModel.WatchStatus = userSeries.WatchStatus;
                    viewModel.SeriesRating = (SeriesRating) userSeries.Rating;
                    viewModel.SeasonsWatched = userSeries.SeasonsWatched;
                    viewModel.EpisodesWatched = userSeries.EpisodesWatched;
                }

                var favouriteSeries =
                    await _context.FavoritesSeries.FirstOrDefaultAsync(x => x.UserId == userId && x.SeriesId == id);

                viewModel.IsFavourite = favouriteSeries != null;
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // GET: Series/Details/5
        public async Task<IActionResult> Details(UserSeriesDetailViewModel viewModel)
        {
            Series series = null;

            if (ModelState.IsValid)
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var userSeries =
                    await _context.UsersSeries.FirstOrDefaultAsync(x =>
                        x.UserId == userId && x.SeriesId == viewModel.SeriesId);

                var favouriteSeries =
                    await _context.FavoritesSeries.FirstOrDefaultAsync(x => x.UserId == userId && x.SeriesId == viewModel.SeriesId);

                if (favouriteSeries == null && viewModel.IsFavourite)
                {
                    var newFavoritesSeries = new FavoritesSeries
                    {
                        SeriesId = viewModel.SeriesId,
                        UserId = userId,
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = userId
                    };

                    _context.FavoritesSeries.Add(newFavoritesSeries);
                    await _context.SaveChangesAsync();

                }else if (favouriteSeries != null && !viewModel.IsFavourite)
                {
                    _context.FavoritesSeries.Remove(favouriteSeries);
                    await _context.SaveChangesAsync();
                }

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

                await _context.SaveChangesAsync();

                return RedirectToAction("Details", new { id = viewModel.SeriesId } );
            }
            else
            {
                series = await _context.Series
                    .Include(x => x.SeriesGenres)
                    .ThenInclude(x => x.Genre)
                    .FirstOrDefaultAsync(x => x.Id == viewModel.SeriesId);

                viewModel.SiteRating = series.UserRating;
                viewModel.PosterContent = series.PosterContent;

                viewModel.Genres = new List<Genre>();

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

        [HttpPost]
        public void RateReview(string reviewIdString)
        {
            var reviewId = Convert.ToInt64(reviewIdString);

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userReview = _context.UserReviews
                .Where(x => x.ReviewId == reviewId && x.UserId == userId)
                .FirstOrDefault();

            var review = _context.SeriesReviews.Where(x => x.Id == reviewId).FirstOrDefault();

            if(userReview == null)
            {
                userReview = new UserReview()
                {
                    UserId = userId,
                    ReviewId = reviewId,
                    IsUpvoted = true,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = userId,
                };

                _context.UserReviews.Add(userReview);

                review.Likes++;
                _context.SeriesReviews.Update(review);

                _context.SaveChanges();
            }
            else
            {
                _context.UserReviews.Remove(userReview);

                review.Likes--;
                _context.SeriesReviews.Update(review);

                _context.SaveChanges();
            }
        }

        private bool SeriesExists(long id)
        {
            return _context.Series.Any(e => e.Id == id);
        }
    }
}
