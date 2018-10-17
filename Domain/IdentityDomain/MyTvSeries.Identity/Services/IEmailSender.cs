using System.Threading.Tasks;

namespace MyTvSeries.Identity.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
