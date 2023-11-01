namespace HealthyFoods.Application.Models.Food;

public class ImportedFoodResponseModel
{
    public string FoodName { get; set; } = string.Empty;

    public double Amount { get; set; }

    public double OriginalPrice { get; set; }

    public double PricePerKg { get; set; }

    public string ImportedFrom { get; set; } = string.Empty;

    public double TotalPrice => Amount * PricePerKg;

    public DateTime CreatedDate { get; set; }
}
