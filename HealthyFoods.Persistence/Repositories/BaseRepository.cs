using HealthyFoods.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HealthyFoods.Persistence.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<T> _dbSet;
        private readonly ILogger _logger;

        public BaseRepository(DatabaseContext context, ILogger<BaseRepository<T>> logger)
        {
            _context = context;
            _dbSet = _context.Set<T>();
            _logger = logger;
        }

        public async Task<T> CreateAsync(T entity)
        {
            try
            {
                var createdEntity = await _dbSet.AddAsync(entity);

                return createdEntity.Entity;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "cannot create record in database",ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<T>> CreateRangeAsync(IEnumerable<T> entities)
        {
            try
            {
                await _dbSet.AddRangeAsync(entities);

                return entities;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "cannot create records in database", ex.Message);
                throw;
            }
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, bool isTracking = false)
        {
            try
            {
                var query = _dbSet.Where(predicate);

                if (isTracking) {
                    query = query.AsNoTracking();
                }

                var item = await query.FirstOrDefaultAsync();

                return item;
            }
            catch(Exception ex)
            { 
                _logger.LogError(ex, "error when retrieving data from database", ex.Message);
                throw;
            }
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes, bool isTracking = false)
        {
            try
            {
                var query = _dbSet.Where(predicate);

                if (!isTracking)
                {
                    query = query.AsNoTracking();
                }

                if(includes != null)
                {
                    query = includes(query);
                }

                var item = await query.FirstOrDefaultAsync();

                return item;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "error when retrieving data from database", ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes, Func<IQueryable<T>, IOrderedQueryable<T>> sort, bool isTracking = false)
        {
            try 
            {
                var query = _dbSet.Where(predicate);

                if (!isTracking)
                {
                    query = query.AsNoTracking();
                }

                if(includes != null)
                {
                    query = includes(query);
                }

                if(sort != null)
                {
                    query = sort(query);
                }

                return await query.ToListAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "error when retrieving data from database", ex.Message);
                throw;
            }
        }

        public Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes, int pageIndex, int pageSize, bool isTracking = false)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes, Func<IQueryable<T>, IOrderedQueryable<T>> sort, int pageIndex, int pageSize, bool isTracking = false)
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entities)
        {
            throw new NotImplementedException();
        }
    }
}
