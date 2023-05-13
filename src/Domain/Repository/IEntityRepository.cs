using ELibrary_BookService.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary_BookService.Domain.Repository;

public interface IEntityRepository<T>
{
    Task AddAsync(T entity);
    Task DeleteAsync(T entity);
    Task<T> GetAsync(int id);
    Task UpdateAsync(T entity);
}
