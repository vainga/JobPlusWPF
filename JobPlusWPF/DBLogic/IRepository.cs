using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace JobPlusWPF.DBLogic
{
    public interface IRepository<T> where T : class
    {
        Task AddAsync(T entity);

        Task DeleteAsync(object id);

        Task<T> FindByIdAsync(object id);

        Task<List<T>> GetAllAsync();

        Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate);

        Task UpdateAsync(T entity);
    }
}
