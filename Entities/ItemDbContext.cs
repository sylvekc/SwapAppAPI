using Microsoft.EntityFrameworkCore;

namespace SwapApp.Entities
{
    public class ItemDbContext : DbContext
    {
        public ItemDbContext(DbContextOptions<ItemDbContext> options) : base(options)
        {
            
        }
        public DbSet<Item> Item { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ItemPhoto> ItemPhotos { get; set; }
    }
}
