using System;

namespace BlobStorage.AzureFunctions.Services
{
    interface IEmailSender
    {
        public void SendEmail(
            string senderEmail,
            string recipiantEmail,
            string subject,
            string messageBody,
            bool isBodyHtml = false);
    }
}
