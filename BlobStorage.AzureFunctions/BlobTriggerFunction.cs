using Azure.Storage.Blobs;
using BlobStorage.AzureFunctions.Models.Configs;
using BlobStorage.AzureFunctions.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BlobStorage.AzureFunctions
{
    public class BlobTriggerFunction
    {
        private readonly ILogger<BlobTriggerFunction> _logger;
        private readonly EmailCredentials _emailCredentials;
        private readonly IEmailSender _emailSender;

        public BlobTriggerFunction(
            ILogger<BlobTriggerFunction> logger,
            IOptions<EmailCredentials> options)
        {
            _logger = logger;
            _emailCredentials = options.Value;
            _emailSender = new OutlookEmailSender(
                _emailCredentials,
                OutlookEmailSender.defaultSmtpHost,
                OutlookEmailSender.defaultSmptPort,
                enableSsl: true);
        }

        [Function(nameof(BlobTriggerFunction))]
        public void Run(
            [BlobTrigger("containerfordocs/{name}", Connection = "BlobClientConnection")] BlobClient blob,
            IDictionary<string, string> metadata,
            string name)
        {
            try
            {
                _emailSender.SendEmail(
                    _emailCredentials.Email,
                    metadata["OwnerEmail"],
                    "Blob file uploaded",
                    $"Hi, here is your file(access ends in a hour): {blob.GenerateSasUri(
                        Azure.Storage.Sas.BlobSasPermissions.Read,
                        DateTimeOffset.UtcNow.AddHours(1))}");
                _logger.LogInformation($"Email successfully sent to: {metadata["OwnerEmail"]}, at: {DateTime.UtcNow}");
            }
            catch (Exception ex) 
            {
                _logger.LogError($"Email sender error occured: {ex.Message}");
            }
        }
    }
}
