using HealthyFoods.Core.Entities.Ingredients;
using HealthyFoods.Persistence.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace HealthyFoods.Persistence.Repositories;
public class RawFoodRepository : BaseRepository<RawFood>, IRawFoodRepository
{
    public RawFoodRepository(DatabaseContext context, ILogger<RawFoodRepository> logger) : base(context, logger)
    {

    }
}
