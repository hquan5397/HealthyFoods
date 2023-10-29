using HealthyFoods.Application.Models.Item;
using HealthyFoods.Core.Models;

namespace HealthyFoods.Application.ApplicationLogic.Interfaces
{
    public interface IItemService
    {
        Task<ItemReponseModel> Import(CreateItemModel model);

        Task<Guid> Delete(Guid id);

        Task<ItemReponseModel> Update(UpdateItemModel model);

        Task<ItemReponseModel> Get(Guid id);

        Task<PagingResponse<ItemReponseModel>> GetMany(GetItemsRequestModel model);
    }
}
