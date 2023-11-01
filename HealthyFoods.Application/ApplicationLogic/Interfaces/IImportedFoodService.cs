using HealthyFoods.Application.Models.Food;
using HealthyFoods.Core.Models;

namespace HealthyFoods.Application.ApplicationLogic.Interfaces
{
    public interface IImportedFoodService
    {
        Task<ImportedFoodResponseModel> Import(CreateImportedFoodModel model);

        Task<Guid> Delete(Guid id);

        Task<ImportedFoodResponseModel> Update(UpdateImportedFoodModel model);

        Task<ImportedFoodResponseModel> Get(Guid id);

        Task<PagingResponse<ImportedFoodResponseModel>> GetMany(GetImportedFoodsRequestModel model);
    }
}
