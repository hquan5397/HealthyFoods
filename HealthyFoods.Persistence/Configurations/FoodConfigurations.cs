using HealthyFoods.Core.Entities.Ingredients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthyFoods.Persistence.Configurations;

public class FoodConfigurations :
    IEntityTypeConfiguration<RawFood>,
    IEntityTypeConfiguration<ImportedFood>, IEntityTypeConfiguration<ImportedItem>
{
    public void Configure(EntityTypeBuilder<ImportedFood> builder)
    {
        builder.ToTable("Foods");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.RawFoodId)
            .IsRequired();

        builder.Property(x => x.Amount)
            .IsRequired();

        builder.Property(x => x.PricePerKg) 
            .IsRequired();
    }

    public void Configure(EntityTypeBuilder<ImportedItem> builder)
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

    public void Configure(EntityTypeBuilder<RawFood> builder)
    {
        builder.ToTable("RawFoods");

        builder.Property(x => x.Name)
            .IsRequired(true)
            .HasMaxLength(255);

        builder.HasMany(rawFood => rawFood.ImportedFoods)
            .WithOne(impFood => impFood.RawFood)
            .HasForeignKey(impFood => impFood.RawFoodId);
    }
}
