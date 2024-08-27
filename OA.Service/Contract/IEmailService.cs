using ECom.Domain.Settings;
using System.Threading.Tasks;

namespace ECom.Service.Contract
{
    public interface IEmailService
    {
        Task SendEmailAsync(MailRequest mailRequest);

    }
}
