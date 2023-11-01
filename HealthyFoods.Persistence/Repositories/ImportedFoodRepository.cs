using HealthyFoods.Core.Entities.Ingredients;
using HealthyFoods.Persistence.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace HealthyFoods.Persistence.Repositories;

public class ImportedFoodRepository : BaseRepository<ImportedFood>, IImportedFoodRepository
{
    public ImportedFoodRepository(DatabaseContext context, ILogger<ImportedFoodRepository> logger) : base(context, logger)
    {

    }
}
