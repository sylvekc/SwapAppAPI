namespace SwapApp.Entities
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description  { get; set; }
        public bool ForFree { get; set; }
        public string SwapFor { get; set; }
        public string WherePickupId { get; set; }
        public virtual WherePickup WherePickup { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
