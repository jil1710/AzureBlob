using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace AzureBlob.Services
{
    public class ContainerService : IContainerService
    {
        private readonly IConfiguration _configuration;
        private BlobServiceClient blobServiceClient;

        public ContainerService(IConfiguration configuration)
        {
            _configuration = configuration;
            blobServiceClient = new BlobServiceClient(_configuration["ConnectionStrings:AzureStorageAccount"]);
        }


        public async Task<bool> CreateContainer(string containerName)
        {      

            var blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);
            if (!await blobContainerClient.ExistsAsync())
            {
                await blobContainerClient.CreateAsync(Azure.Storage.Blobs.Models.PublicAccessType.BlobContainer);
                return true;
            }
            else
            {
                return false;
            }

        }

        public async Task<IEnumerable<string>> GetAllContainer()
        {
            try
            {
                var publicContainers = new List<string>();

                await foreach (var containerItem in blobServiceClient.GetBlobContainersAsync())
                {

                    var containerClient = blobServiceClient.GetBlobContainerClient(containerItem.Name);
                    var containerProperties = await containerClient.GetPropertiesAsync();

                    if (containerProperties.Value.PublicAccess == PublicAccessType.Blob || containerProperties.Value.PublicAccess == PublicAccessType.BlobContainer)
                    {
                        publicContainers.Add(containerItem.Name);
                    }
                }

                return publicContainers;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error listing containers: {ex.Message}");
                return null;
            }

        }

       
        public async Task<bool> DeleteContainerByName(string name)
        {
            try
            {
                var containerClient = blobServiceClient.GetBlobContainerClient(name);

                if (await containerClient.ExistsAsync())
                {
                    await containerClient.DeleteAsync();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting container: {ex.Message}");
                return false;
            }
        }


        public async Task<bool> CopyContainerFromAnother(string sourceContainerName, string destinationContainerName)
        {
            try
            {
                var sourceContainerClient = blobServiceClient.GetBlobContainerClient(sourceContainerName);
                var destinationContainerClient = blobServiceClient.GetBlobContainerClient(destinationContainerName);

                if (await sourceContainerClient.ExistsAsync() && await destinationContainerClient.ExistsAsync())
                {
                    await foreach (BlobItem blobItem in sourceContainerClient.GetBlobsAsync())
                    {
                        BlobClient sourceBlobClient = sourceContainerClient.GetBlobClient(blobItem.Name);
                        BlobClient destinationBlobClient = destinationContainerClient.GetBlobClient(blobItem.Name);
                        await destinationBlobClient.StartCopyFromUriAsync(sourceBlobClient.Uri);
                    }

                        return true;
                }
                else
                {
                    Console.WriteLine("Source or destination container does not exist.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error copying container: {ex.Message}");
                return false;
            }
        }
    }
}
