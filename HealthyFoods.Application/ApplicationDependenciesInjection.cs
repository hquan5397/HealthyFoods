using HealthyFoods.Application.ApplicationLogic;
using HealthyFoods.Application.ApplicationLogic.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace HealthyFoods.Application
{
    public static class ApplicationDependenciesInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {          
            services.AddScoped<IFoodService, FoodService>();
            services.AddScoped<IItemService, ItemService>();

            return services;
        }
    }
}
