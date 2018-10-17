using System;
using System.Linq;
using System.Threading.Tasks;
using ImportService.TheMovieDb.Api.Json.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyTvSeries.Domain.Ef;
using MyTvSeries.Domain.Entities;

namespace ImportService.TheMovieDb.Converter
{
    public class MovieDbDomainDbHelper : IMovieDbDomainDbHelper
    {
        #region Properties

        private readonly ITvSeriesContext _context;
        private readonly ILogger<IMovieDbDomainDbHelper> _logger;

        #endregion

        #region Ctors

        public MovieDbDomainDbHelper(ITvSeriesContext context, ILogger<IMovieDbDomainDbHelper> logger)
        {
            _context = context;
            _logger = logger;
        }

        #endregion

        #region Public methods

        public async Task<Genre> GetOrAddGenre(GenreJson genreJson)
        {
            var genreFromDb = await GetGenreFromDb(genreJson.Name);

            if (genreFromDb != null)
                return genreFromDb;

            return await AddGenreToDb(genreJson);
        }

        #endregion

        #region MyRegion

        private async Task<Genre> AddGenreToDb(GenreJson genreJson)
        {
            // TODO replace with system user id
            var genre = new Genre
            {
                MovieDbId = genreJson.Id,
                Name = genreJson.Name,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = 1
            };

            await _context.Genres.AddAsync(genre);
            await _context.SaveChangesAsync();
            _logger.LogInformation("GenreJson [{0}] added to db with id [{1}] and MovieDbId [{2}]", genre.Name, genre.Id, genre.MovieDbId);
            return genre;
        }

        private async Task<Genre> GetGenreFromDb(string name)
        {
            var genre = await _context
                .Genres
                .Where(x => x.Name == name)
                .FirstOrDefaultAsync();

            return genre;
        }

        #endregion
    }
}
