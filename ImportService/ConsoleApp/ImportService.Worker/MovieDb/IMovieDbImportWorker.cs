using System.Threading.Tasks;

namespace ImportService.Worker.MovieDb
{
    public interface IMovieDbImportWorker
    {
        Task Initialize();
        Task Start();
    }
}
