using Microsoft.AspNetCore.Identity.UI.Services;

namespace MVCProject.ITI.Services
{
    public class NoOpEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
            => Task.CompletedTask;
    }
}