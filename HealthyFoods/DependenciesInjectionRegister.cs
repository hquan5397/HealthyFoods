using Serilog;
using Serilog.Events;

namespace HealthyFoods
{
    public static class DependenciesInjectionRegister
    {
        public static IServiceCollection RegisDenpendencies(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    }
}
