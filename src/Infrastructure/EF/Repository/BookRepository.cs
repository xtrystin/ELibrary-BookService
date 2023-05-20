using ELibrary_BookService.Infrastructure.EF.Repository;
using ELibrary_BookService.Domain.Entity;
using ELibrary_BookService.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary_BookService.Infrastructure.EF
{
    public class BookRepository : EntityRepository<Book>, IBookRepository
    {
        public BookRepository(BookDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<Book?> GetAsync(int id) 
            => await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == id);
    }
}
