using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using PizzaSalesChallenge.Infrastructure.DataAccess.Interface;
using PizzaSalesChallenge.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PizzaSalesChallenge.Infrastructure.DataAccess
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly PizzaDatabaseContext _dbContext;
        public GenericRepository(PizzaDatabaseContext context)
        {
            _dbContext = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            var res = await _dbContext.Set<T>().AddAsync(entity);
            return res.Entity;
        }

        public async Task AddRageAsync(T[] entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
        }

        public T Delete(T entity)
        {
            var res = _dbContext.Set<T>().Remove(entity);
            return res.Entity;
        }

        public void DeleteRange(T[] entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
        }

        public async Task<(IEnumerable<T>, int TotalCount, int pageCount)> GetAllAsync(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            int pageNumber = 1,
            int pageSize = 10,
            params Func<IQueryable<T>, IIncludableQueryable<T, object>>[] includes)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            int totalCount = query.Count();
            int pageCount = 0;

            if (filter != null)
            {
                query = query.Where(filter);
                pageCount = query.Count();
            }
            else
                pageCount = pageSize;



            if (includes != null)
            {
                foreach (var include in includes)
                    query = include(query);
            }



            if (orderBy != null)
                query = orderBy(query);
            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return (await query.ToListAsync(), totalCount, pageCount);
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> filter, bool tracked = true, params Func<IQueryable<T>, IIncludableQueryable<T, object>>[] includes)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            if (filter != null)
                query = query.Where(filter);

            if (includes != null)
            {
                foreach (var include in includes)
                    query = include(query);
            }

            if (!tracked)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync();
        }

        public DbSet<T> GetDBSet()
        {
            return _dbContext.Set<T>();
        }

        public T Update(T entity)
        {
            var res = _dbContext.Set<T>().Update(entity);
            return res.Entity;
        }
    }
}
