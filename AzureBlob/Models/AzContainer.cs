using System.ComponentModel.DataAnnotations;

namespace AzureBlob.Models
{
    public class AzContainer
    {
        [Required]
        public string Name { get; set; }
    }
}
