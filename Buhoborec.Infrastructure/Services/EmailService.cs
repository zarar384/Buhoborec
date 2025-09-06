using System.Threading.Tasks;
using System;

namespace Buhoborec.Infrastructure.Services;

public interface IEmailService
{
    Task SendAsync(string to, string subject, string html);
}

public class EmailService : IEmailService
{
    public Task SendAsync(string to, string subject, string html)
    {
        Console.WriteLine($"[Email] To={to}, Subject={subject}");
        return Task.CompletedTask;
    }
}
