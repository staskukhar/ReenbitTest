using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlobStorage.Shared.Models
{
    public class BlobUploadRequestDto
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("name")]
        public string FileName { get; set; }
        [JsonPropertyName("binary")]
        public BinaryData BinaryData { get; set; }

        public BlobUploadRequestDto(string email, string fileName, BinaryData binaryData)
        {
            Email = email;  
            FileName = fileName;
            BinaryData = binaryData;
        }
        public BlobUploadRequestDto() { }   
    }
}
