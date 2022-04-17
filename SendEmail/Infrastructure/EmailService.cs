using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;

namespace SendEmail.Infrastructure
{
    public class EmailService
    {
        private readonly EmailSetting _emailSetting;

        public EmailService(IOptions<EmailSetting> emailSetting)
        {
            _emailSetting = emailSetting.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                // Credentials
                var credentials = new NetworkCredential(_emailSetting.Sender, _emailSetting.Password);

                // Mail message
                var mail = new MailMessage
                {
                    From = new MailAddress(_emailSetting.Sender, _emailSetting.SenderName),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true
                };

                mail.To.Add(new MailAddress(email));

                // Smtp client
                var client = new SmtpClient
                {
                    Port = _emailSetting.MailPort,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Host = _emailSetting.MailServer,
                    EnableSsl = _emailSetting.Sender.Contains("gmail"),
                    Credentials = credentials
                };

                // Send it...         
                await client.SendMailAsync(mail);
            }
            catch (Exception ex)
            {
                // TODO: handle exception

                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
