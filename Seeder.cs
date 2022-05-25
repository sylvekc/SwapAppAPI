using Microsoft.EntityFrameworkCore;
using SwapApp.Entities;

namespace SwapApp
{
    public class Seeder
    {
        private readonly ItemDbContext _dbContext;
        public Seeder(ItemDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                var pendingMigrations = _dbContext.Database.GetPendingMigrations();
                if (pendingMigrations != null && pendingMigrations.Any())
                {
                    _dbContext.Database.Migrate();
                }
                if (!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }
                if (!_dbContext.Item.Any())
                {
                    var items = GetItems();
                    _dbContext.Item.AddRange(items);
                    _dbContext.SaveChanges();
                }
            }
        }
        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "User"
                },

                new Role()
                {
                    Name = "Admin"
                },
            };
            return roles;
        }

        private IEnumerable<Item> GetItems()
        {
            var items = new List<Item>()
            {
                new Item()
                {
                    Name = "Drukarka HP",
                    Description = "Stan dobry, sprawna.",
                    ForFree = false,
                    SwapFor = "5 puszek krojonych pomidorów.",
                    City = "Kraków",
                    District = "Kazimierz",
                    Street = "Miodowa",
                    ItemPhotos = new List<ItemPhoto>()
                    {
                        new ItemPhoto() 
                        {
                        FilePath = "drukarka1.jpg"
                        },
                        new ItemPhoto()
                        {
                            FilePath = "drukarka2.jpg"
                        }
                    }

                },

                new Item()
                {
                    Name = "Toster",
                    Description = "Oddam za darmo sprawny toster.",
                    ForFree = true,
                    City = "Kraków",
                    District = "Dębniki",
                    Street = "Kapelanka",
                    ItemPhotos = new List<ItemPhoto>()
                    {
                        new ItemPhoto()
                        {
                            FilePath = "toster1.jpg"
                        },
                        new ItemPhoto()
                        {
                            FilePath = "toster2.jpg"
                        }
                    }

                }
            };

            return items;
        }

    }
}
