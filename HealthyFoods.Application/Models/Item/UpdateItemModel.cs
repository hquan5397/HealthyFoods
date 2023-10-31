namespace HealthyFoods.Application.Models.Item;

public class UpdateItemModel
{
    public Guid Id { get; set; }

    public string ItemName { get; set; } = string.Empty;

    public double OriginalPrice { get; set; }

    public int Quantity { get; set; }

    public double PricePerEach { get; set; }

    public string ImportedFrom { get; set; } = string.Empty;
}
