using HealthyFoods.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HealthyFoods.Middlewares
{
    public class TransactionMiddleware
    {
        private readonly RequestDelegate _next;

        public TransactionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, DatabaseContext databaseContext)
        {
            var strategy = databaseContext.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await databaseContext.Database.BeginTransactionAsync();

                try
                {
                    await _next(context);

                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();

                    throw;
                }
            });
        }
    }
}
