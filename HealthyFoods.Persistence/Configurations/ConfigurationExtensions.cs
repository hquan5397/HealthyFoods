using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthyFoods.Persistence.Configurations
{
    public static class ConfigurationExtensions
    {
        public static PropertyBuilder<TProperty> HasEnumConversion<TProperty>(this PropertyBuilder<TProperty> builder) where TProperty : struct, Enum
        {
            return builder
                .HasConversion(p => p.ToString(), p => Enum.Parse<TProperty>(p))
                .IsRequired();
        }
    }
}
