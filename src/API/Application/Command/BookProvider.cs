using ELibrary_BookService.Application.Exception;
using ELibrary_BookService.Domain.Repository;

namespace ELibrary_BookService.Application.Command
{
    public class BookProvider : IBookProvider
    {
        private readonly IBookRepository _bookRepository;

        public BookProvider(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public void CreateBook()
        {

        }

        public async Task DeleteBook(int id)
        {
            var book = await _bookRepository.GetAsync(id);
            if (book is null)
                throw new BookNotFoundException();

            await _bookRepository.DeleteAsync(book);
        }
    }
}
