using HealthyFoods.Core.Entities.Ingredients;
using Microsoft.EntityFrameworkCore;

namespace HealthyFoods.Persistence
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options) 
        { 
        
        }

        public DbSet<ImportedFood> Foods { get; set; }

        public DbSet<ImportedItem> Items { get; set; }

    }
}
