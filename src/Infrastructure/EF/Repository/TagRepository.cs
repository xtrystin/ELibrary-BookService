using ELibrary_BookService.Domain.Entity;
using ELibrary_BookService.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace ELibrary_BookService.Domain.EF.Repository
{
    public class TagRepository : EntityRepository<Tag>, ITagRepository
    {
        public TagRepository(BookDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<Tag?> GetAsync(int id)
            => await _dbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);
    }
}
