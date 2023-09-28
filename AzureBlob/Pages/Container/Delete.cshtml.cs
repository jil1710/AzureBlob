using AzureBlob.Models;
using AzureBlob.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AzureBlob.Pages.Container
{
    public class DeleteModel : PageModel
    {
        private readonly IContainerService containerService;

        [BindProperty]
        public AzContainer Container { get; set; }

        public DeleteModel(IContainerService containerService)
        {
            this.containerService = containerService;
        }

        public async Task OnGet(string name)
        {
            var ContainerList = await containerService.GetAllContainer();
            var result = ContainerList.FirstOrDefault(e=> e.Equals(name))!;
            if(!string.IsNullOrEmpty(result))
            {
                Container = new AzContainer() { Name = result};    
            }
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var result = await containerService.DeleteContainerByName(Container.Name);

                if (result)
                {
                    return RedirectToPage("Index");
                }
            }

            return Page();
        }
    }
}
