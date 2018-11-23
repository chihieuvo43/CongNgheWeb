using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Mail;
using System.Net;

namespace Common
{
    public class MailHelper
    {
        public void SendMail(string toEmailAdress, string subject, string content)
        {
            var fromEmailAddress = ConfigurationManager.AppSettings["FromEmailAddress"].ToString();
            var formEmailDisplayName = ConfigurationManager.AppSettings["FromEmaiDisplayName"].ToString();
            var formEmailPassword = ConfigurationManager.AppSettings["FromEmailPassword"].ToString();
            var smtpHost = ConfigurationManager.AppSettings["SMTPHost"].ToString();
            var smtpPost = ConfigurationManager.AppSettings["SMTPPost"].ToString();

            bool EnabledSsl = bool.Parse(ConfigurationManager.AppSettings["EnabledSSL"].ToString());

            string body = content;
            MailMessage message = new MailMessage(new MailAddress(fromEmailAddress, formEmailDisplayName), new MailAddress(toEmailAdress));
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;

            var client = new SmtpClient();
            client.Credentials = new NetworkCredential(fromEmailAddress,formEmailPassword);
            client.Host = smtpHost;
            client.EnableSsl = EnabledSsl;
            client.Port = !string.IsNullOrEmpty(smtpPost) ? Convert.ToInt32(smtpPost) : 0;
            client.Send(message);

        }
    }
}
