using System.Threading.Tasks.Dataflow;

namespace BlobStorage.Api.Services
{
    public class FileFormatter
    {
        public static string GetUniqueName(string fullName,Guid uniqueString, char separator = '_') 
        {
            return String.Concat(
                Path.GetFileNameWithoutExtension(fullName),
                separator,
                uniqueString.ToString(),
                Path.GetExtension(fullName));
        }
    }
}
