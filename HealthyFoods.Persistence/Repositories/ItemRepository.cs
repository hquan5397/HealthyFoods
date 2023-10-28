using HealthyFoods.Core.Entities.Ingredients;
using HealthyFoods.Persistence.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace HealthyFoods.Persistence.Repositories
{
    public class ItemRepository : BaseRepository<Item>, IItemRepository
    {
        public ItemRepository(DatabaseContext context, ILogger<ItemRepository> logger) : base(context, logger) { }
    }
}
