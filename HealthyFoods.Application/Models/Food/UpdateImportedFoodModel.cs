namespace HealthyFoods.Application.Models.Food;

public class UpdateImportedFoodModel
{
    public Guid Id { get; set; }

    public Guid RawFoodId { get; set; }

    public string FoodName { get; set; } = string.Empty;

    public double OriginalPrice { get; set; }

    public double Amount { get; set; }

    public double PricePerKg { get; set; }

    public string ImportedFrom { get; set; } = string.Empty;
}
