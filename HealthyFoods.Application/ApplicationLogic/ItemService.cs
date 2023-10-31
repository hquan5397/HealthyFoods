using AutoMapper;
using HealthyFoods.Application.ApplicationLogic.Interfaces;
using HealthyFoods.Application.Models;
using HealthyFoods.Application.Models.Item;
using HealthyFoods.Core.Entities.Ingredients;
using HealthyFoods.Core.Extensions;
using HealthyFoods.Core.Models;
using HealthyFoods.Persistence.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace HealthyFoods.Application.ApplicationLogic;

public class ItemService : IItemService
{
    private readonly IItemRepository _itemRepository;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;

    public ItemService(IItemRepository itemRepository, ILogger<ItemService> logger, IMapper mapper)
    {
        _itemRepository = itemRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<ItemReponseModel> Get(Guid id)
    {
        try
        {
            var food = await _itemRepository.GetAsync(x => x.Id == id);

            if (food == null)
            {
                throw new Exception("Wrong Id, cannot found");
            }

            var itemReponse = _mapper.Map<ItemReponseModel>(food);

            return itemReponse;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<PagingResponse<ItemReponseModel>> GetMany(GetItemsRequestModel model)
    {
        try
        {
            Expression<Func<Item, bool>> itemFilter = x => x.IsDeleted == false;

            if (model.ItemNames.Any())
            {
                itemFilter.And(x => model.ItemNames.Contains(x.Name));
            }

            if (model.ItemIds.Any())
            {
                itemFilter.And(x => model.ItemIds.Contains(x.Id));
            }

            if (model.From.HasValue)
            {
                itemFilter.And(x => x.CreatedDate >= model.From.Value);
            }

            if (model.To.HasValue)
            {
                itemFilter.And(x => x.CreatedDate <= model.To.Value);
            }

            var itemPagingResponse = await _itemRepository.GetManyAsync(
                itemFilter,
                includes: null,
                sort: x => x.OrderBy(od => od.CreatedDate),
                pageIndex: model.PageIndex,
                pageSize: model.PageSize);

            var itemReponseModels = _mapper.Map<IEnumerable<ItemReponseModel>>(itemPagingResponse.Result);

            var itemsPagingResponseModel = new PagingResponse<ItemReponseModel>()
            {
                PageIndex = model.PageIndex,
                PageSize = model.PageSize,
                TotalRecords = itemPagingResponse.TotalRecords,
                Result = itemReponseModels
            };

            return itemsPagingResponseModel;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<ItemReponseModel> Import(CreateItemModel model)
    {
        try
        {
            var item = Item.Create(
                name: model.ItemName,
                originalPrice: model.OriginalPrice,
                price: model.PricePerEach,
                quantity: model.Quantity,
                importedFrom: model.ImportedFrom);

            var createdItem = await _itemRepository.CreateAsync(item);

            var itemResponseModel = _mapper.Map<ItemReponseModel>(createdItem);

            return itemResponseModel;
        }
        catch (Exception ex)
        {
            _logger.LogError("Cannot create item", ex);
            throw;
        }
    }

    public async Task<ItemReponseModel> Update(UpdateItemModel model)
    {
        try
        {
            var item = await _itemRepository.GetAsync(x => x.Id == model.Id);

            if (item == null)
            {
                throw new Exception($"Not found item with id:{model.Id}");
            }

            item.Update(
                name: model.ItemName,
                originalPrice: model.OriginalPrice,
                price: model.PricePerEach,
                quantity: model.Quantity,
                importedFrom: model.ImportedFrom);

            var updatedItem = await _itemRepository.UpdateAsync(item);

            var itemResponse = _mapper.Map<ItemReponseModel>(updatedItem);

            return itemResponse;
        }
        catch (Exception ex)
        {
            _logger.LogError("Cannot update food", ex);
            throw;
        }
    }


    public async Task<Guid> Delete(Guid id)
    {
        try
        {
            var item = await _itemRepository.GetAsync(x => x.Id == id);

            if (item == null)
            {
                throw new Exception($"Not found item with id:{id}");
            }

            item.Delete();

            var updatedItem = await _itemRepository.UpdateAsync(item);

            return updatedItem.Id;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
