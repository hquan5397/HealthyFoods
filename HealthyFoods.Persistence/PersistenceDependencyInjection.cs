using HealthyFoods.Persistence.Repositories;
using HealthyFoods.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HealthyFoods.Persistence
{
    public static class PersistenceDependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext(configuration);

            services.AddTransient<IFoodRepository, FoodRepository>();
            services.AddTransient<IItemRepository, ItemRepository>();

            return services;
        }

        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>((provider, options) =>
            {
                var connectionString = configuration["Database:ConnectionString"];

                options.UseSqlServer(connectionString, opt =>
                {
                    opt.MigrationsAssembly(typeof(DatabaseContext).Assembly.FullName);
                });

                return;
            });
        }
    }
}
