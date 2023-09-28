using AzureBlob.Models;
using AzureBlob.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AzureBlob.Pages.Blob
{
    public class IndexModel : PageModel
    {
        private readonly IBlobService blobService;
        public List<AzBlob> AZBlobs { get; set; }
        public IndexModel(IBlobService blobService)
        {
            this.blobService = blobService;
        }

        public async Task OnGet(string name)
        {
            var blobList = await blobService.ListContainerItems(name);
            AZBlobs = blobList;
        }

        public IActionResult OnGetDeleteBlob(string name, string containerName)
        {
            if(string.IsNullOrEmpty(containerName) && string.IsNullOrEmpty(name))
            {
                return Redirect($"/Blob/Index?name={containerName}");
            }
            var blobList = blobService.DeleteBlob(containerName,name);
            return Redirect($"/Blob/Index?name={containerName}");
        }


        public async Task<IActionResult> OnGetDownloadBlob(string blobname,string containerName)
        {

            BlobDto? file = await blobService.DownloadBlobInContainer(containerName,blobname);
            if (file == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"File {blobname} could not be downloaded.");
            }
            else
            {
                return File(file.Content, file.ContentType, file.Name);
            }
        }

        public async Task<IActionResult> OnPost(IFormFile fileblob,string name)
        {
            var result = await blobService.UploadBlobInContainer(fileblob,name);
            return Redirect($"/Blob/Index?name={name}");
        }
    }
}
