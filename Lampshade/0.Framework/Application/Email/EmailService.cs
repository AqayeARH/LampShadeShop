using MailKit.Net.Smtp;
using MimeKit;

namespace _0.Framework.Application.Email
{
    public class EmailService : IEmailService
    {
        public void SendEmail(string title, string messageBody, string destination)
        {
            var message = new MimeMessage();

            var from = new MailboxAddress("علیرضا حیدری", "alireza80heydri@gmail.com");
            message.From.Add(from);

            var to = new MailboxAddress("User", destination);
            message.To.Add(to);

            message.Subject = title;
            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = $"<h2>{messageBody}</h2>",
            };

            message.Body = bodyBuilder.ToMessageBody();

            var client = new SmtpClient();
            client.Connect("185.88.152.251", 25, false);
            client.Authenticate("alireza80heydri@gmail.com", "lffdsgwdheikdcfk");
            client.Send(message);
            client.Disconnect(true);
            client.Dispose();
        }
    }
}