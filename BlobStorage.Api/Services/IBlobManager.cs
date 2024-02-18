using BlobStorage.Shared.Models;

namespace BlobStorage.Api.Services
{
    public interface IBlobManager
    {
        public Task UploadBlobAsync(BlobUploadRequestDto blob);
        public Task UploadBlobAsync(BlobUploadRequestDto blob, string containerName);
    }
}
