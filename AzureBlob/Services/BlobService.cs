using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using AzureBlob.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace AzureBlob.Services
{
    public class BlobService : IBlobService
    {
        private readonly IConfiguration _configuration;
        private BlobServiceClient blobServiceClient;

        public BlobService(IConfiguration configuration)
        {
            _configuration = configuration;
            blobServiceClient = new BlobServiceClient(_configuration["ConnectionStrings:AzureStorageAccount"]);
        }

        public async Task<List<AzBlob>> ListContainerItems(string containerName)
        {
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            var blobList = new List<AzBlob>();

            await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
            {
                blobList.Add(new AzBlob() { Container=containerName,BlobName= blobItem.Name,Name = containerClient.GetBlobClient(blobItem.Name).Uri.ToString(), ContentType = blobItem.Properties.ContentType});
            }

            return blobList;
        }


        public async Task<bool> DeleteBlob(string containerName, string blobName)
        {
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(blobName);

            try
            {
                var response = await blobClient.DeleteIfExistsAsync();
                return response; 
            }
            catch (RequestFailedException ex)
            {
                
                Console.WriteLine($"Failed to delete blob: {ex.ErrorCode}");
                return false;
            }
        }



        public async Task<BlobDto> DownloadBlobInContainer(string containerName, string blobName)
        {
            try
            {
                var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
                var blobClient = containerClient.GetBlobClient(blobName);

                if (await blobClient.ExistsAsync())
                {
                    var data = await blobClient.OpenReadAsync();
                    Stream blobContent = data;

                    var content = await blobClient.DownloadContentAsync();

     
                    string name = blobName;
                    string contentType = content.Value.Details.ContentType;

                    return new BlobDto() { Content = blobContent, Name = name, ContentType = contentType };
                };
                

                
            }
            catch (RequestFailedException ex)
            {
                return null;
            }

            return null;

        }

        public async Task<bool> UploadBlobInContainer(IFormFile formFile,string name)
        {
            try
            {
                var containerClient = blobServiceClient.GetBlobContainerClient(name);
                var blobClient = containerClient.GetBlobClient(formFile.FileName);

                await using (Stream? data = formFile.OpenReadStream())
                {
                    BlobUploadOptions options = new BlobUploadOptions
                    {
                        HttpHeaders = new BlobHttpHeaders { ContentType = formFile.ContentType }
                    };
                    await blobClient.UploadAsync(data,options);
                }

                return true;
            }catch(RequestFailedException ex) { return false; }

        }
    }

}



