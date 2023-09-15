using LoginPractice.Helper;

namespace LoginPractice.Service
{
    public interface IEmailService
    {
        Task SendEmailAsync(Mailrequest mailrequest);
    }
}
