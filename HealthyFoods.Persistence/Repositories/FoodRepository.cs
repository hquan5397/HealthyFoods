using HealthyFoods.Core.Entities.Ingredients;
using HealthyFoods.Persistence.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace HealthyFoods.Persistence.Repositories
{
    public class FoodRepository : BaseRepository<Food>, IFoodRepository
    {
        public FoodRepository(DatabaseContext context, ILogger<FoodRepository> logger) : base(context, logger)
        {

        }
    }
}
