using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebPortal.Services.Common
{
    public interface IEmailService
    {
        bool Send(string fromName, string fromEmail, string toEmail, string subject, string html, string SmtpHost, int SmtpPort, string SmtpUser, string SmtpPass, bool SmtpSSl);
    }
}
