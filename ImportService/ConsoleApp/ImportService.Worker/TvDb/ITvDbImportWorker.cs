using System.Threading.Tasks;

namespace ImportService.Worker.TvDb
{
    public interface ITvDbImportWorker
    {
        Task Initialize();
        Task Start();
    }
}
