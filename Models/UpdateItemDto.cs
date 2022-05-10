using System.ComponentModel.DataAnnotations;

namespace SwapApp.Models
{
    public class UpdateItemDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
        [Required]
        public bool ForFree { get; set; }
        [MaxLength(100)]
        public string SwapFor { get; set; }
        [Required]
        [MaxLength(50)]
        public string City { get; set; }
        [MaxLength(50)]
        public string District { get; set; }
        [MaxLength(50)]
        public string Street { get; set; }
    }
}
