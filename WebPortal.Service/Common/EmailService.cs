using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using WebPortal.Data.Entities;
using System.Threading.Tasks;
using WebPortal.ViewModels;

namespace WebPortal.Services.Common
{
    public class EmailService : IEmailService
    {
        public string Error=string.Empty;
        
        public bool Send(string fromName, string fromEmail, string toEmail, string subject, string html, string SmtpHost, int SmtpPort, string SmtpUser, string SmtpPass, bool SmtpSSl)
        {
            try
            {
                // create message
                var email = new MimeMessage();
                email.From.Add(new MailboxAddress(fromName, fromEmail));
                email.To.Add(MailboxAddress.Parse(toEmail));
                email.Subject = subject;
                email.Body = new TextPart(TextFormat.Html) { Text = html };

                // send email
                using var smtp = new SmtpClient();
                //smtp.Connect(SmtpHost, SmtpPort, SmtpSSl); //un-comment this for gmail
                smtp.ConnectAsync(SmtpHost, SmtpPort, SecureSocketOptions.None).Wait();
                smtp.Authenticate(SmtpUser, SmtpPass);
                smtp.Send(email);
                smtp.Disconnect(true);

                return true;
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return false;
            }
        }
    }
}
