using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BlobStorage.Shared;
using BlobStorage.Shared.Models;

namespace BlobStorage.Api.Services
{
    public class BlobManagerService : IBlobManager
    {
        private string _connectionString;
        private string _defaultContainer;

        public BlobManagerService(IConfiguration configuration)
        {
            _connectionString = configuration.GetValue<string>("AzureClient:BlobStorage:ConnectionString");
            _defaultContainer = configuration.GetValue<string>("AzureClient:BlobStorage:DefaultContainer");
        }
        public async Task UploadBlobAsync(BlobUploadRequestDto blob)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(_connectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(_defaultContainer);
            BlobClient blobClient = containerClient.GetBlobClient(blob.FileName);

            BlobUploadOptions options = new BlobUploadOptions
            {
                HttpHeaders = new BlobHttpHeaders
                {
                    ContentType = FileExtensions.DocxContentType,
                },
                Metadata = new Dictionary<string, string> { {"OwnerEmail", blob.Email} },
            };

            await blobClient.UploadAsync(new BinaryData(blob.BinaryData), options);
        }

        public async Task UploadBlobAsync(BlobUploadRequestDto blob, string containerName)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(_connectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            BlobClient blobClient = containerClient.GetBlobClient(blob.FileName);

            BlobUploadOptions options = new BlobUploadOptions
            {
                HttpHeaders = new BlobHttpHeaders
                {
                    ContentType = FileExtensions.DocxContentType,
                }
            };

            await blobClient.UploadAsync(new BinaryData(blob.BinaryData), options);
        }
    }
}
