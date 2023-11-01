using HealthyFoods.Core.Models;

namespace HealthyFoods.Application.Models.Food;

public class GetImportedFoodsRequestModel : PagingRequest
{
    public List<string> FoodNames { get; set; } = new List<string>();

    public List<Guid> FoodIds { get; set; } = new();

    public DateTime? From { get; set; }

    public DateTime? To { get; set; }
}
