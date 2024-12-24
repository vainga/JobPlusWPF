using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace JobPlusWPF.DBLogic
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
        }

        // Метод для добавления нового элемента
        public async Task AddAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        // Метод для удаления элемента по Id
        public async Task DeleteAsync(object id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) throw new InvalidOperationException("Entity not found");

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        // Метод для поиска элемента по Id
        public async Task<T> FindByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        // Метод для поиска всех элементов
        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        // Метод для поиска элементов по условию
        public async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        // Метод для обновления элемента
        public async Task UpdateAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T> FindByNameAsync(string name)
        {
            return await _dbSet.FirstOrDefaultAsync(entity => EF.Property<string>(entity, "Name") == name);
        }

    }
}
