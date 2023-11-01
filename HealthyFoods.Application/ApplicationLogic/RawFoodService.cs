using HealthyFoods.Application.ApplicationLogic.Interfaces;
using HealthyFoods.Core.Entities.Ingredients;
using HealthyFoods.Persistence.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace HealthyFoods.Application.ApplicationLogic;
public class RawFoodService : IRawFoodService
{
    private readonly IRawFoodRepository _rawFoodRepository;
    private readonly ILogger _logger;

    public RawFoodService(IRawFoodRepository rawFoodRepository, ILogger<RawFoodService> logger)
    {
        _rawFoodRepository = rawFoodRepository;
        _logger = logger;
    }

    public async Task<RawFood> Create(string name)
    {
        try
        {
            var rawFoodEntity = RawFood.Create(name);

            var createdRawFood = await _rawFoodRepository.CreateAsync(rawFoodEntity);

            return createdRawFood;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Dictionary<Guid,string>> GetAll()
    {
        try
        {
            var rawFoods = await _rawFoodRepository.GetManyAsync(x => !x.IsDeleted, includes: null, sort: x => x.OrderBy(x => x.Name));

            if (rawFoods is null)
            {
                return new Dictionary<Guid, string>();
            }

            return rawFoods.ToDictionary(x => x.Id, x => x.Name);
        }
        catch(Exception)
        {
            throw;
        }
    }
}
