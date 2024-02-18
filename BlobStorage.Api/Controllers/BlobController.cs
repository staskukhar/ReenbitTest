using BlobStorage.Api.Services;
using BlobStorage.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlobStorage.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlobController : ControllerBase
    {
        private IBlobManager _blobManager;
        public BlobController(IBlobManager blobManager)
        {
            _blobManager = blobManager;
        }
        [HttpPost]
        public async Task<IActionResult> UploadBlobAsync([FromBody] BlobUploadRequestDto blob)
        {
            try
            {
                blob.FileName = FileFormatter.GetUniqueName(blob.FileName, Guid.NewGuid());
                await _blobManager.UploadBlobAsync(blob);
                return Ok("File uploaded successfully");
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
