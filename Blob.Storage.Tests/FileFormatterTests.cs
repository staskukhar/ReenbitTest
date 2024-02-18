using BlobStorage.Api.Services;

namespace Blob.Storage.Tests
{
    public class FileFormatterTests
    {
        [Fact]
        public void Get_Unique_Name_Test_Def_Separator()
        {
            //Arrange
            string fileName = "somefile.docx";
            Guid guid = Guid.NewGuid();

            //Act & Assert
            Assert.Equal(
                expected: String.Concat("somefile", '_', guid.ToString(), ".docx"),
                actual: FileFormatter.GetUniqueName(fileName, guid));
        }
        [Fact]
        public void Get_Unique_Name_Custom_Separator()
        {
            //Arrange
            string fileName = "somefile.docx";
            Guid guid = Guid.NewGuid();
            char separator = ':';

            //Act & Assert
            Assert.Equal(
                expected: String.Concat("somefile", separator, guid.ToString(), ".docx"),
                actual: FileFormatter.GetUniqueName(fileName, guid, separator));
        }
    }
}