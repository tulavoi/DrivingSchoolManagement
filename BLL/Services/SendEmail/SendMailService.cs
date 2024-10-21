using BLL.Services.SendEmail;
using MailKit.Security;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace BLL.Services.SendEmail
{
    public class SendMailService
    {
        private static MailSettings _mailSettings { get; set; }

        public SendMailService(MailSettings mailSettings)
        {
            _mailSettings = mailSettings;
        }

        public async Task<bool> SendMail(MailContent mailContent)
        {
            var email = new MimeMessage();
            var mailBoxAddress = new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail);

            email.Sender = mailBoxAddress;
            email.From.Add(mailBoxAddress);
            email.To.Add(new MailboxAddress(mailContent.To, mailContent.To));
            email.Subject = mailContent.Subject;

            var builder = new BodyBuilder();

            builder.HtmlBody = mailContent.Body; // Gửi nội dung html, có thể gửi file,...

            email.Body = builder.ToMessageBody();

            using (var smtp = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                    await smtp.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);
                    await smtp.SendAsync(email);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }

                smtp.Disconnect(true);
                return true;
            }
        }
    }
}
