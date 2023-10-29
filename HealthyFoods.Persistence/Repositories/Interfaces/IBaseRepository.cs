using HealthyFoods.Core.Models;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace HealthyFoods.Persistence.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> CreateAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task<IEnumerable<T>> CreateRangeAsync(IEnumerable<T> entities);

        Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entities);

        Task<T> GetAsync(Expression<Func<T, bool>> predicate, bool isTracking = false);

        Task<T> GetAsync(Expression<Func<T,bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes, bool isTracking = false);

        Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes, Func<IQueryable<T>, IOrderedQueryable<T>> sort, bool isTracking = false);

        Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes, int pageIndex, int pageSize, bool isTracking = false);

        Task<PagingResponse<T>> GetManyAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes, Func<IQueryable<T>, IOrderedQueryable<T>> sort, int pageIndex, int pageSize, bool isTracking = false);

    }
}
