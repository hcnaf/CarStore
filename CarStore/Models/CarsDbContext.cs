using Microsoft.EntityFrameworkCore;

namespace CarStore.Models
{
    public class CarsDbContext : DbContext
    {
        public CarsDbContext(DbContextOptions<CarsDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;DataBase=CarStore;Trusted_Connection=True;MultipleActiveReusltSets=true");
            }
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
