using HealthyFoods.Core.Entities.Ingredients;
using Microsoft.EntityFrameworkCore;

namespace HealthyFoods.Persistence
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options) 
        { 
        
        }

        public DbSet<Food> Foods { get; set; }

        public DbSet<Item> Items { get; set; }

    }
}
