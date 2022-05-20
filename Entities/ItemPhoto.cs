using System.ComponentModel.DataAnnotations;

namespace SwapApp.Entities
{
    public class ItemPhoto
    {
        public int Id { get; set; }
        [Required]
        public string FileName { get; set; }
        [Required]
        public int ItemId { get; set; }
    }
}
