using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PizzaSalesChallenge.Infrastructure.DataAccess
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);

        Task AddRageAsync(T[] entities);
        T Update(T entity);
        T Delete(T entity);
        void DeleteRange(T[] entities);
        DbSet<T> GetDBSet();
        Task<(IEnumerable<T>, int TotalCount, int pageCount)> GetAllAsync(Expression<Func<T, bool>> filter = null,
             Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
             int pageNumber = 1,
             int pageSize = 10,
             params Func<IQueryable<T>, IIncludableQueryable<T, object>>[] includes);

        Task<T?> GetAsync(Expression<Func<T, bool>> filter, bool track, params Func<IQueryable<T>, IIncludableQueryable<T, object>>[] includes);
    }
}
