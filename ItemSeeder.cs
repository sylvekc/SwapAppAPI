﻿using SwapApp.Entities;

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
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }
                //if (!_dbContext.Item.Any())
                //{
                //    var items = GetItems();
                //    _dbContext.Item.AddRange(items);
                //    _dbContext.SaveChanges();
                //}
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
                Name = "długopis",
                Description = "czarny",
                ForFree = false,
                SwapFor = "obojętne",
                City = "Kraków",
                District = "Kazimierz",
                Street = "Miodowa",

            },
                new Item()
                {
                    Name = "długopis",
                    Description = "czarny",
                    ForFree = false,
                    SwapFor = "obojętne",
                    City = "Kraków",
                    District = "Kazimierz",
                    Street = "Miodowa",
                }
            };

            return items;
        }

    }
}
