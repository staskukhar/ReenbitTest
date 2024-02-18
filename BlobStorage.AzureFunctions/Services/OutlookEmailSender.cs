using BlobStorage.AzureFunctions.Models.Configs;
using System.Net;
using System.Net.Mail;

namespace BlobStorage.AzureFunctions.Services
{
    public class OutlookEmailSender : IEmailSender
    {
        private readonly SmtpClient _smtpClient;
        public static string defaultSmtpHost = "smtp-mail.outlook.com";
        public static int defaultSmptPort = 587;

        public OutlookEmailSender(
            EmailCredentials emailCredentials,
            string smtpClientHost,
            int smtpPort,
            bool enableSsl) 
        {
            _smtpClient = new SmtpClient(smtpClientHost)
            {
                Credentials = new NetworkCredential(emailCredentials.Email, emailCredentials.Password),
                Port = smtpPort,
                EnableSsl = enableSsl
            };
        }
        public void SendEmail(
            string senderEmail,
            string recipiantEmail,
            string subject,
            string messageBody,
            bool isBodyHtml = false
            )
        {
            var emailMessage = new MailMessage()
            {
                From = new MailAddress(senderEmail),
                Subject = subject,
                Body = messageBody,
                IsBodyHtml = isBodyHtml
            };
            emailMessage.To.Add(recipiantEmail);
            _smtpClient.Send(emailMessage);
        }
    }
}
