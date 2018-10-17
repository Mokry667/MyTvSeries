using System;
using System.IO;
using System.Threading.Tasks;
using ImportService.TheMovieDb.Api;
using ImportService.TheMovieDb.Converter;
using ImportService.TheTvDb.Api;
using ImportService.TheTvDb.Converter;
using ImportService.Worker.MovieDb;
using ImportService.Worker.TvDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyTvSeries.Domain.Ef;
using NLog.Extensions.Logging;

namespace ImportService.ConsoleApp
{
    internal class Program
    {
        private static string _environmentName;

        private static ITvDbImportWorker _tvDbImportWorker;
        private static IMovieDbImportWorker _movieDbImportWorker;
        private static ILogger _logger;
        public static IConfigurationRoot Configuration { get; set; }

        static async Task Main()
        {
            RegisterDependencies();
            _logger.LogInformation("Loaded [{0}] environment config", _environmentName);

            _logger.LogInformation("Initialize MovieDb Import Worker");
            await _movieDbImportWorker.Initialize();
            _logger.LogInformation("Start MovieDb Import");
            await _movieDbImportWorker.Start();

            _logger.LogInformation("MovieDb Import finished");

            NLog.LogManager.Shutdown();
        }

        static void RegisterDependencies()
        {
            _environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{_environmentName}.json", true, true)
                .AddEnvironmentVariables()
                .AddUserSecrets<Program>();

            Configuration = builder.Build();

            var tvSeriesConnectionString = Configuration["ConnectionStrings:TvSeriesDatabase"];

            var serviceProvider = new ServiceCollection()

                .AddSingleton<ITvDbApi, TvDbApi>()
                .AddSingleton<ITvDbDomainConverter, TvDbDomainConverter>()
                .AddSingleton<ITvDbDomainDbHelper, TvDbDomainDbHelper>()
                .AddSingleton<ITvDbImportWorker, TvDbImportWorker>()

                .AddSingleton<IMovieDbApi, MovieDbApi>()
                .AddSingleton<IMovieDbDomainConverter, MovieDbDomainConverter>()
                .AddSingleton<IMovieDbDomainDbHelper, MovieDbDomainDbHelper>()
                .AddSingleton<IMovieDbImportWorker, MovieDbImportWorker>()
                .AddSingleton<IMovieDbMapper, MovieDbMapper>()
                .AddSingleton<IMovieDbImportServiceDbHelper, MovieDbImportServiceDbHelper>()

                .AddSingleton<ILoggerFactory, LoggerFactory>()
                .AddSingleton(typeof(ILogger<>), typeof(Logger<>))
                .AddSingleton<IConfiguration>(Configuration)
                .AddScoped<ITvSeriesContext, TvSeriesContext>()
                .AddDbContext<TvSeriesContext>(options => options.UseSqlServer(tvSeriesConnectionString))
                .AddLogging(b => b.SetMinimumLevel(LogLevel.Debug))
                .BuildServiceProvider();

            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            loggerFactory.AddNLog();
            NLog.LogManager.LoadConfiguration("nlog.config");

            _tvDbImportWorker = serviceProvider.GetService<ITvDbImportWorker>();
            _movieDbImportWorker = serviceProvider.GetService<IMovieDbImportWorker>();

            _logger = serviceProvider.GetService<ILogger<Program>>();
        }
    }
}
