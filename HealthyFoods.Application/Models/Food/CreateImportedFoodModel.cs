namespace HealthyFoods.Application.Models.Food;

public class CreateImportedFoodModel
{
    public Guid RawFoodId { get; set; }

    public double Amount { get; set; }

    public double OriginalPrice { get; set; }

    public double PricePerKg { get; set; }

    public string ImportedFrom { get; set; } = string.Empty;
}
