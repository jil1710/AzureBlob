using AzureBlob.Models;
using AzureBlob.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AzureBlob.Pages.Container
{
    public class CreateModel : PageModel
    {
        private readonly IContainerService containerService;

        [BindProperty]
        public AzContainer Container { get; set; }

        public CreateModel(IContainerService containerService)
        {
            this.containerService = containerService;
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPost()
        {
            if(ModelState.IsValid)
            {
                var result = await containerService.CreateContainer(Container.Name);

                if (result)
                {
                    return RedirectToPage("Index");
                }
            }

            return Page();
        }
    }
}
