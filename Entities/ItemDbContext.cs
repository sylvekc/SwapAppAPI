using Microsoft.EntityFrameworkCore;

namespace SwapApp.Entities
{
    public class ItemDbContext : DbContext
    {
        private string _connectionString = "Server = localhost; Database=SwapAppAPI;Trusted_Connection=True;";
        public DbSet<Item> Item { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ItemPhoto> ItemPhotos { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

    }
}
