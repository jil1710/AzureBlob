using AzureBlob.Models;

namespace AzureBlob.Services
{
    public interface IBlobService
    {
        Task<List<AzBlob>> ListContainerItems(string containerName);
        Task<bool> DeleteBlob(string containerName, string blobName);

        Task<BlobDto> DownloadBlobInContainer(string containerName, string blobName);

        Task<bool> UploadBlobInContainer(IFormFile formFile, string name);
    }
}