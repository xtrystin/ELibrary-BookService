using ELibrary_BookService.Domain.Entity;
using ELibrary_BookService.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace ELibrary_BookService.Infrastructure.EF.Repository
{
    public class AuthorRepository : EntityRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(BookDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<Author?> GetAsync(int id)
            => await _dbContext.Authors.FirstOrDefaultAsync(x => x.Id == id);

        public Task<bool> Exists(string firstname, string lastname)
           => _dbContext.Authors.AnyAsync(x => x.Firstname == firstname && x.Lastname == lastname);
    }
}
