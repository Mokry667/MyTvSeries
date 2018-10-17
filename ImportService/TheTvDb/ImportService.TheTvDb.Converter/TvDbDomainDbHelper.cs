using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyTvSeries.Domain.Ef;
using MyTvSeries.Domain.Entities;

namespace ImportService.TheTvDb.Converter
{
    public class TvDbDomainDbHelper : ITvDbDomainDbHelper
    {
        private readonly ITvSeriesContext _context;
        private readonly ILogger<ITvDbDomainDbHelper> _logger;

        #region Ctors

        public TvDbDomainDbHelper(ITvSeriesContext context, ILogger<ITvDbDomainDbHelper> logger)
        {
            _context = context;
            _logger = logger;
        }

        #endregion

        #region Public methods

        public async Task<Genre> GetGenre(string name)
        {
            var genreFromDb = await GetGenreFromDb(name);

            if (genreFromDb != null)
                return genreFromDb;

            return await AddGenreToDb(name);
        }

        #endregion

        #region Private methods

        // Genre

        private async Task<Genre> AddGenreToDb(string name)
        {
            // TODO replace with system user id
            var genre = new Genre
            {
                Name = name,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = 1
            };

            await _context.Genres.AddAsync(genre);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Genre [{0}] added to db with id [{1}]", name, genre.Id);
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

        // 

        #endregion

    }
}
