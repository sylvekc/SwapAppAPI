using SwapApp.Entities;

namespace SwapApp
{
    public class ItemSeeder
    {
        private readonly ItemDbContext _dbContext;
        public ItemSeeder(ItemDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Seed()
        {
            if(_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Item.Any())
                {
                    var items = GetItems();
                    _dbContext.Item.AddRange(items);
                    _dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Item> GetItems()
        {
            var items = new List<Item>();
            new Item()
            {
                Name = "długopis",
                Description = "czarny",
                ForFree = false,
                SwapFor = "obojętne",
                City = "Kraków",
                District = "Kazimierz",
                Street = "Miodowa",
                User = new User()
                {
                    Name = "Sylwek",
                    Email = "sylvekz@vp.pl",
                    Password = "123",
                }
                //WherePickup = new WherePickup()
                //{
                //    City = "Kraków",
                //    District = "Kazimierz",
                //    Street = "Miodowa",
                //},
                //User = new User()
                //{
                //    Email = "sylvekz@vp.pl",
                //    Name = "Sylwek",
                //    Password = "123",
                //},

            };
            return items;
        }

    }
}
