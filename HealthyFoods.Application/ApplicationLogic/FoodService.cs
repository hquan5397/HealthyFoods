using AutoMapper;
using HealthyFoods.Application.ApplicationLogic.Interfaces;
using HealthyFoods.Application.Models.Food;
using HealthyFoods.Core.Entities.Ingredients;
using HealthyFoods.Core.Extensions;
using HealthyFoods.Core.Models;
using HealthyFoods.Persistence.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace HealthyFoods.Application.ApplicationLogic
{
    public class FoodService : IFoodService
    {
        private readonly IFoodRepository _foodRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public FoodService(IFoodRepository foodRepository, ILogger<FoodService> logger, IMapper mapper)
        {
            _foodRepository = foodRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<FoodResponseModel> Get(Guid id)
        {
            try
            {
                var food = await _foodRepository.GetAsync(x => x.Id == id);

                if (food == null)
                {
                    throw new Exception("Wrong Id, cannot found");
                }

                var foodReponse = _mapper.Map<FoodResponseModel>(food);

                return foodReponse;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<PagingResponse<FoodResponseModel>> GetMany(GetFoodsRequestModel model)
        {
            try
            {
                Expression<Func<Food, bool>> foodFilter = x => x.IsDeleted == false;

                if (model.FoodNames.Any())
                {
                    foodFilter.And(x => model.FoodNames.Contains(x.Name));
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
                  
                var foodPagingResponse = await _foodRepository.GetManyAsync(
                    foodFilter,
                    includes: null,
                    sort: x => x.OrderBy(od => od.CreatedDate),
                    pageIndex: model.PageIndex,
                    pageSize: model.PageSize);

                var foodReponseModels = _mapper.Map<IEnumerable<FoodResponseModel>>(foodPagingResponse.Result);

                var foodsPagingResponseModel = new PagingResponse<FoodResponseModel>()
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

        public async Task<FoodResponseModel> Import(CreateFoodModel model)
        {
            try
            {
                var food = Food.Create(
                    name: model.FoodName,
                    price: model.Price,
                    amount: model.Amount,
                    importedFrom: model.ImportedFrom);

                var createdFood = await _foodRepository.CreateAsync(food);

                var foodReponse = _mapper.Map<FoodResponseModel>(createdFood);

                return foodReponse;
            }
            catch (Exception ex)
            {
                _logger.LogError("Cannot create food", ex);
                throw;
            }
        }

        public async Task<FoodResponseModel> Update(UpdateFoodModel model)
        {
            try
            {
                var food = await _foodRepository.GetAsync(x => x.Id == model.Id);

                if (food == null)
                {
                    throw new Exception($"Not found food with id:{model.Id}");
                }

                food.Update(
                    name: model.FoodName,
                    price: model.Price,
                    amount: model.Amount,
                    importedFrom: model.ImportedFrom);

                var updatedFood = await _foodRepository.UpdateAsync(food);

                var foodReponse = _mapper.Map<FoodResponseModel>(updatedFood);

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
                var food = await _foodRepository.GetAsync(x => x.Id == id);

                if (food == null)
                {
                    throw new Exception($"Not found food with id:{id}");
                }

                food.Delete();

                var updatedFood = await _foodRepository.UpdateAsync(food);

                return updatedFood.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
