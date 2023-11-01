using AutoMapper;
using HealthyFoods.Application.ApplicationLogic.Interfaces;
using HealthyFoods.Application.Models.Food;
using HealthyFoods.Core.Entities.Ingredients;
using HealthyFoods.Core.Extensions;
using HealthyFoods.Core.Models;
using HealthyFoods.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace HealthyFoods.Application.ApplicationLogic;

public class ImportedFoodService : IImportedFoodService
{
    private readonly IImportedFoodRepository _importedFoodRepository;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;

    public ImportedFoodService(IImportedFoodRepository foodRepository, ILogger<ImportedFoodService> logger, IMapper mapper)
    {
        _importedFoodRepository = foodRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<ImportedFoodResponseModel> Get(Guid id)
    {
        try
        {
            var food = await _importedFoodRepository.GetAsync(x => x.Id == id);

            if (food == null)
            {
                throw new Exception("Wrong Id, cannot found");
            }

            var foodReponse = _mapper.Map<ImportedFoodResponseModel>(food);

            return foodReponse;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<PagingResponse<ImportedFoodResponseModel>> GetMany(GetImportedFoodsRequestModel model)
    {
        try
        {
            Expression<Func<ImportedFood, bool>> foodFilter = x => x.IsDeleted == false;

            if (model.FoodNames.Any())
            {
                foodFilter.And(x => model.FoodNames.Contains(x.RawFood.Name));
            }

            if(model.FoodIds.Any())
            {
                foodFilter.And(x => model.FoodIds.Contains(x.Id));
            }

            if (model.From.HasValue)
            {
                foodFilter.And(x => x.CreatedDate >= model.From.Value);
            }

            if(model.To.HasValue)
            {
                foodFilter.And(x => x.CreatedDate <= model.To.Value);
            }
              
            var foodPagingResponse = await _importedFoodRepository.GetManyAsync(
                foodFilter,
                includes: x => x.Include(x => x.RawFood),
                sort: x => x.OrderBy(od => od.CreatedDate),
                pageIndex: model.PageIndex,
                pageSize: model.PageSize);

            var foodReponseModels = _mapper.Map<IEnumerable<ImportedFoodResponseModel>>(foodPagingResponse.Result);

            var foodsPagingResponseModel = new PagingResponse<ImportedFoodResponseModel>()
            {
                PageIndex = model.PageIndex,
                PageSize = model.PageSize,
                TotalRecords = foodPagingResponse.TotalRecords,
                Result = foodReponseModels
            };

            return foodsPagingResponseModel;
        }
        catch(Exception)
        {
            throw;
        }
    }

    public async Task<ImportedFoodResponseModel> Import(CreateImportedFoodModel model)
    {
        try
        {
            var food = ImportedFood.Create(
                rawFoodId: model.RawFoodId,
                orgiginalPrice: model.OriginalPrice,
                price: model.PricePerKg,
                amount: model.Amount,
                importedFrom: model.ImportedFrom);

            var createdFood = await _importedFoodRepository.CreateAsync(food);

            var foodReponse = _mapper.Map<ImportedFoodResponseModel>(createdFood);

            return foodReponse;
        }
        catch (Exception ex)
        {
            _logger.LogError("Cannot create food", ex);
            throw;
        }
    }

    public async Task<ImportedFoodResponseModel> Update(UpdateImportedFoodModel model)
    {
        try
        {
            var food = await _importedFoodRepository.GetAsync(x => x.Id == model.Id);

            if (food == null)
            {
                throw new Exception($"Not found food with id:{model.Id}");
            }

            food.Update(
                rawFoodId: model.RawFoodId,
                originalPrice: model.OriginalPrice,
                price: model.PricePerKg,
                amount: model.Amount,
                importedFrom: model.ImportedFrom);

            var updatedFood = await _importedFoodRepository.UpdateAsync(food);

            var foodReponse = _mapper.Map<ImportedFoodResponseModel>(updatedFood);

            return foodReponse;
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
            var food = await _importedFoodRepository.GetAsync(x => x.Id == id);

            if (food == null)
            {
                throw new Exception($"Not found food with id:{id}");
            }

            food.Delete();

            var updatedFood = await _importedFoodRepository.UpdateAsync(food);

            return updatedFood.Id;
        }
        catch (Exception)
        {
            throw;
        }
    }

}
