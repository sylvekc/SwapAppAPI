using Microsoft.EntityFrameworkCore;

namespace SwapApp.Entities
{
    public class ItemDbContext : DbContext
    {
        private string _connectionString = "Server = localhost; Database=SwapAppAPI;Trusted_Connection=True;";
        public DbSet<Item> Item { get; set; }
        //public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                .Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Item>()
               .Property(e => e.Description)
               .IsRequired()
               .HasMaxLength(500);

            modelBuilder.Entity<Item>()
               .Property(e => e.ForFree)
               .IsRequired();

            modelBuilder.Entity<Item>()
               .Property(e => e.SwapFor)
               .IsRequired()
               .HasMaxLength(100);

            modelBuilder.Entity<Item>()
                .Property(e => e.City)
                .IsRequired()
                .HasMaxLength(25);

            modelBuilder.Entity<Item>()
                .Property(e => e.Street)
                .HasMaxLength(50);
    
            //modelBuilder.Entity<User>()
            //    .Property(e => e.Email)
            //    .IsRequired()
            //    .HasMaxLength(100);

            //modelBuilder.Entity<User>()
            //    .Property(e => e.Name)
            //    .IsRequired()
            //    .HasMaxLength(25);

            //modelBuilder.Entity<User>()
            //    .Property(e => e.Password)
            //    .IsRequired()
            //    .HasMaxLength(100);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

    }
}
