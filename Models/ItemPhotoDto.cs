using System.ComponentModel.DataAnnotations;

namespace SwapApp.Models
{
    public class ItemPhotoDto
    {
        [Required]
        public string FilePath { get; set; }
    }
}
