namespace HealthyFoods.Application.Models;

public class ItemReponseModel
{
    public string ItemName { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public double OriginalPrice { get; set; }

    public double PricePerEach { get; set; }

    public string ImportedFrom { get; set; } = string.Empty;

    public double TotalPrice => PricePerEach * Quantity;

    public DateTime CreatedDate { get; set; }
}

