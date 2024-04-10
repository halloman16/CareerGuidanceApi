using MailKit.Net.Smtp;
using MimeKit;
using webapi.src.App.IService;

namespace webapi.src.App.Service
{
    public class EmailService : IEmailService
    {
        private static MailboxAddress _senderMail = new MailboxAddress("Oren_hack", "nastxs04@mail.ru");

        public async Task SendEmail(string email, string subject, string message)
        {
            using var emailMessage = new MimeMessage();
            emailMessage.Subject = subject;
            emailMessage.From.Add(_senderMail);
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Body = new TextPart()
            {
                Text = message
            };

            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.mail.ru", 587, false);
            await client.AuthenticateAsync(_senderMail.Address, "ikM9FQ5T81xPriWcwkeZ");
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }
}