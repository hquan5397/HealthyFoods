using HealthyFoods.Core.Entities.Ingredients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthyFoods.Persistence.Configurations
{
    public class FoodConfigurations : IEntityTypeConfiguration<Food>, IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Food> builder)
        {
            builder.ToTable("Foods");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.Amount)
                .IsRequired();

            builder.Property(x => x.PricePerKg) 
                .IsRequired();
        }

        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable("Items");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.Quantity)
                .IsRequired();

            builder.Property(x => x.PricePerEach)
                .IsRequired();
        }
    }
}
