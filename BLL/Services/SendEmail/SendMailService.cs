using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL.Services.SendEmail;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace GUI.Services.SendEmail
{
    public class SendMailService
    {
        private static MailSettings _mailSettings { get; set; }

        public SendMailService(MailSettings mailSettings)
        {
            _mailSettings = mailSettings;
        }

        public static bool SendMail(MailContent mailContent)
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
                    smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                    smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                    smtp.Send(email);
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
