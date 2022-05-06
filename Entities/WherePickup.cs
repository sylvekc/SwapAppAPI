namespace SwapApp.Entities
{
    public class WherePickup
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string City { get; set; }
        public string District  { get; set; }
        public string Street { get; set; }
        public int ItemId { get; set; }
        public virtual Item Item { get; set; }

    }
}
