using ELibrary_BookService.Domain.Entity;
using ELibrary_BookService.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary_BookService.Domain.EF.Repository
{
    public abstract class EntityRepository<T> : IEntityRepository<T>
    {
        protected readonly BookDbContext _dbContext;

        public EntityRepository(BookDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(T entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public abstract Task<T?> GetAsync(int id);

        public async Task UpdateAsync(T book)
        {
            _dbContext.Update(book);
            await _dbContext.SaveChangesAsync();
        }
    }
}
