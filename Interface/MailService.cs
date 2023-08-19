using AlBayanWebAPI.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace AlBayanWebAPI.Interface
{
    public class MailService:IMailService
    {
        private readonly MailSettings _mailSettings;
        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async  Task SendEmailAsync_Old()
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse("shajiikp@gmail.com"));
            email.Subject = "Al Bayan Registeration Confirmation";
            var builder = new BodyBuilder();
            //if (mailRequest.Attachments != null)
            //{
            //    byte[] fileBytes;
            //    foreach (var file in mailRequest.Attachments)
            //    {
            //        if (file.Length > 0)
            //        {
            //            using (var ms = new MemoryStream())
            //            {
            //                file.CopyTo(ms);
            //                fileBytes = ms.ToArray();
            //            }
            //            builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
            //        }
            //    }
            //}
            builder.HtmlBody ="Congragulations For Registering To Al Bayan AI DashBoard";
            email.Body = builder.ToMessageBody();
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public async Task SendEmailAsync(string Email, string Provider)
        {
            //Get settings from appsettings
            string emailbody = "<p> Dear  " +Provider+" </p>";
            emailbody = emailbody + " Congragulations For Registering To Al Bayan AI DashBoard </p>";
            emailbody = emailbody + "<p>Pls Login to http://albayan.ai-projects.in For Accessing the DashBoard</p>";
            emailbody = emailbody + "<p>With Regards</p>";
            emailbody = emailbody + "<p>Admin Al Bayan</p>";
            var SmtpHost = _mailSettings.Host;
            var SmtpPort = _mailSettings.Port;
            var SmtpUserFriendlyName = "Al Bayan Buisness Intelligent Tool";
            var SmtpUserEmailAddress = _mailSettings.Mail;
            var SmtpPass = _mailSettings.Password;
            // create message
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(SmtpUserFriendlyName, SmtpUserEmailAddress));
            email.To.Add(new MailboxAddress(Email, Email));           
            email.Subject = "Al Bayan Registeration Confirmation";
            email.Body = new TextPart(TextFormat.Html) { Text = emailbody };

            try
            {
                // send email
                using var smtp = new MailKit.Net.Smtp.SmtpClient();

                smtp.Connect(SmtpHost, SmtpPort, SecureSocketOptions.StartTls);
                smtp.Authenticate(SmtpUserEmailAddress, SmtpPass);
                await Task.Run(() => smtp.Send(email));
                smtp.Disconnect(true);

                
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
