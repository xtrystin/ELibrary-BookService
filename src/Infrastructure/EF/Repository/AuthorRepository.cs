using ELibrary_BookService.Domain.Entity;
using ELibrary_BookService.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary_BookService.Domain.EF.Repository
{
    public class AuthorRepository : EntityRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(BookDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<Author?> GetAsync(int id)
            => await _dbContext.Authors.FirstOrDefaultAsync(x => x.Id == id);
    }
}
