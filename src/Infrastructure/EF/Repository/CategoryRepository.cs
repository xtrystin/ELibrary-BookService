using ELibrary_BookService.Domain.Entity;
using ELibrary_BookService.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace ELibrary_BookService.Infrastructure.EF.Repository
{
    public class CategoryRepository : EntityRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(BookDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<Category?> GetAsync(int id)
            => await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);

        public Task<bool> Exists(string name)
            => _dbContext.Categories.AnyAsync(x => x.Name == name);
    }
}
