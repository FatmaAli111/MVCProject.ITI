using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;

namespace MVCProject.ITI.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _config;
        private readonly ILogger<EmailSender> _logger;

        public EmailSender(IConfiguration config, ILogger<EmailSender> logger)
        {
            _config = config;
            _logger = logger;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var smtpHost = _config["Email:SmtpHost"];
            var smtpPort = int.Parse(_config["Email:SmtpPort"] ?? "587");
            var smtpUser = _config["Email:SmtpUser"];
            var smtpPass = _config["Email:SmtpPass"];
            var fromName = _config["Email:FromName"] ?? "Smart Trip";
            var fromAddress = _config["Email:FromAddress"] ?? smtpUser;

            // Guard: if credentials are not configured yet, log and skip silently
            if (string.IsNullOrWhiteSpace(smtpUser) ||
                string.IsNullOrWhiteSpace(smtpPass) ||
                smtpPass == "PASTE_YOUR_APP_PASSWORD_HERE")
            {
                _logger.LogWarning("Email not sent to {Email} — SMTP credentials are not configured in appsettings.json.", email);
                return;
            }

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(fromName, fromAddress));
            message.To.Add(MailboxAddress.Parse(email));
            message.Subject = subject;

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = BuildEmailTemplate(subject, htmlMessage)
            };
            message.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            await client.ConnectAsync(smtpHost, smtpPort, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(smtpUser, smtpPass);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);

            _logger.LogInformation("Email sent to {Email} with subject '{Subject}'", email, subject);
        }

        private static string BuildEmailTemplate(string subject, string body)
        {
            return $@"
<!DOCTYPE html>
<html lang=""en"">
<head>
<meta charset=""UTF-8""/>
<meta name=""viewport"" content=""width=device-width, initial-scale=1.0""/>
<title>{subject}</title>
</head>
<body style=""margin:0;padding:0;background:#f0fdf9;font-family:'Segoe UI',Arial,sans-serif;"">
  <table width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""background:#f0fdf9;padding:32px 16px;"">
    <tr><td align=""center"">
      <table width=""600"" cellpadding=""0"" cellspacing=""0"" style=""max-width:600px;width:100%;"">
        <tr>
          <td style=""background:linear-gradient(135deg,#0d9488,#14b8a6);border-radius:20px 20px 0 0;padding:32px;text-align:center;"">
            <div style=""display:inline-block;background:rgba(255,255,255,0.15);border-radius:16px;padding:14px 20px;margin-bottom:16px;"">
              <span style=""font-size:32px;"">🚗</span>
            </div>
            <h1 style=""margin:0;color:#fff;font-size:26px;font-weight:900;letter-spacing:-0.5px;"">SMART TRIP</h1>
            <p style=""margin:6px 0 0;color:rgba(255,255,255,0.85);font-size:14px;"">Your intelligent fuel cost companion</p>
          </td>
        </tr>
        <tr>
          <td style=""background:#fff;padding:36px 32px;border-left:1px solid #e5e7eb;border-right:1px solid #e5e7eb;"">
            <h2 style=""margin:0 0 16px;color:#111827;font-size:20px;font-weight:800;"">{subject}</h2>
            <div style=""font-size:15px;color:#374151;line-height:1.7;"">
              {body}
            </div>
          </td>
        </tr>
        <tr>
          <td style=""background:#f9fafb;border:1px solid #e5e7eb;border-top:none;border-radius:0 0 20px 20px;padding:20px 32px;text-align:center;"">
            <p style=""margin:0;font-size:12px;color:#9ca3af;"">
              &copy; {DateTime.Now.Year} Smart Trip &middot; This email was sent automatically &mdash; please do not reply.<br/>
              If you didn&apos;t request this, you can safely ignore this email.
            </p>
          </td>
        </tr>
      </table>
    </td></tr>
  </table>
</body>
</html>";
        }
    }
}
