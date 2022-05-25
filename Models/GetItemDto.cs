using System.ComponentModel.DataAnnotations;
using SwapApp.Entities;

namespace SwapApp.Models
{
    public class GetItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public string Description { get; set; }
        public bool ForFree { get; set; }
        public string SwapFor { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool IsPublic { get; set; } = true;
        public int ReservedBy { get; set; }
        public List<ItemPhotoDto> ItemPhotos { get; set; }
    }
}
