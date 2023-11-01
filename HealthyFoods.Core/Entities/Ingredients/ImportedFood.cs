namespace HealthyFoods.Core.Entities.Ingredients;

public class ImportedFood : BaseEntity
{
    public double OriginalPrice { get; set; }

    public double PricePerKg { get; set; }

    public double Amount { get; set; }

    public string ImportedFrom { get; set; } = string.Empty;

    public Guid RawFoodId { get; set; }

    public RawFood RawFood { get; set; }

    public static ImportedFood Create(Guid rawFoodId, double orgiginalPrice, double price, double amount, string importedFrom)
    {
        var food = new ImportedFood
        {
            Id = Guid.NewGuid(),
            RawFoodId = rawFoodId,
            OriginalPrice = orgiginalPrice,
            PricePerKg = price,
            Amount = amount,
            ImportedFrom = importedFrom,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow
        };

        return food;
    }

    public void Update(Guid rawFoodId,double originalPrice, double price, double amount, string importedFrom)
    {
        RawFoodId= rawFoodId;
        OriginalPrice = originalPrice;
        PricePerKg = price;
        Amount = amount;
        ImportedFrom = importedFrom;
        UpdatedDate = DateTime.UtcNow;
    }

    public void Delete()
    {
        IsDeleted = true;
        UpdatedDate = DateTime.UtcNow;
    }
}
