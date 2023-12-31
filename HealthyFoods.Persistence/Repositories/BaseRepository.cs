﻿using HealthyFoods.Core.Models;
using HealthyFoods.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

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

                await _context.SaveChangesAsync();

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

                await _context.SaveChangesAsync();

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

        public async Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes, int pageIndex, int pageSize, bool isTracking = false)
        {
            try
            {
                var query = _dbSet.Where(predicate);

                if (!isTracking)
                {
                    query = query.AsNoTracking();
                }

                if (includes != null)
                {
                    query = includes(query);
                }

                var items = await query.Skip(pageIndex).Take(pageSize).ToListAsync();

                return items;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "error when retrieving data from database", ex.Message);
                throw;
            }
        }

        public async Task<PagingResponse<T>> GetManyAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes, Func<IQueryable<T>, IOrderedQueryable<T>> sort, int pageIndex, int pageSize, bool isTracking = false)
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

                var items = await query.Skip(pageIndex).Take(pageSize).ToListAsync();

                var total = await query.CountAsync();

                var pagingReponse = new PagingResponse<T>()
                {
                    TotalRecords = total,
                    Result = items,
                    PageSize = pageSize,
                    PageIndex = pageIndex
                };

                return pagingReponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error when retrieving data from database", ex.Message);
                throw;
            }
        }

        public async Task<T> UpdateAsync(T entity)
        {
            try
            {
                var updatedEntity = _dbSet.Update(entity);

                await _context.SaveChangesAsync();

                return updatedEntity.Entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error when updating  data from database", ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entities)
        {
            try
            {
                _dbSet.UpdateRange(entities);

                await _context.SaveChangesAsync();

                return entities;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error when updating data from database", ex.Message);
                throw;
            }
        }
    }
}
