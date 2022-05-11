using System.ComponentModel.DataAnnotations;

namespace SwapApp.Entities
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description  { get; set; }
        public bool ForFree { get; set; }
        public string SwapFor { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
