using HealthyFoods.Application.Models.Food;
using HealthyFoods.Core.Models;

namespace HealthyFoods.Application.ApplicationLogic.Interfaces
{
    public interface IFoodService
    {
        Task<FoodResponseModel> Import(CreateFoodModel model);

        Task<Guid> Delete(Guid id);

        Task<FoodResponseModel> Update(UpdateFoodModel model);

        Task<FoodResponseModel> Get(Guid id);

        Task<PagingResponse<FoodResponseModel>> GetMany(GetFoodsRequestModel model);
    }
}
