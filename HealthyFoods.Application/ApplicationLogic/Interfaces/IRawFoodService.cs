using HealthyFoods.Application.Models.Food;
using HealthyFoods.Core.Entities.Ingredients;

namespace HealthyFoods.Application.ApplicationLogic.Interfaces;
public interface IRawFoodService
{
    Task<RawFood> Create(string name);

    Task<Dictionary<Guid,string>> GetAll();
}
