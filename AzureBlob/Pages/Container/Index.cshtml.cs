using AzureBlob.Models;
using AzureBlob.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AzureBlob.Pages.Container
{
    public class IndexModel : PageModel
    {
        private readonly IContainerService containerService;

        public IEnumerable<string> Containers { get; set; }

        public IndexModel(IContainerService containerService)
        {
            this.containerService = containerService;
        }

        public async Task OnGet()
        {
            Containers = await containerService.GetAllContainer();
        }

     
        public IActionResult OnPostCopyContainer(string source, string destination)
        {
            if(!string.IsNullOrEmpty(source) && !string.IsNullOrEmpty(destination)) 
            {
                var result = containerService.CopyContainerFromAnother(source, destination);
                if (result.Result)
                {
                    return new JsonResult($"Successfully transfer from {source} to {destination}");
                }
                else
                {
                    return new JsonResult("Something went wrong! please try later.");
                }
            }
            return new JsonResult("Please Select Both Source and Destination");

        }
    }
}
