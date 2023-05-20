using ELibrary_BookService.Application.Command.Exception;
using ELibrary_BookService.Application.Command.Model;
using ELibrary_BookService.Domain.Entity;
using ELibrary_BookService.Domain.Repository;

namespace ELibrary_BookService.Application.Command
{
    public class CommonProvider : ICommonProvider
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IAuthorRepository _authorRepository;

        public CommonProvider(ICategoryRepository categoryRepository, ITagRepository tagRepository,
            IAuthorRepository authorRepository)
        {
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
            _authorRepository = authorRepository;
        }

        public async Task CreateTag(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new EmptyException("Tag name cannot be empty");

            if (await _tagRepository.Exists(name))
                throw new AlreadyExistsException("Tag with this name already exists");

            var tag = new Tag(name);
            await _tagRepository.AddAsync(tag);
        }

        public async Task DeleteTag(int tagId)
        {
            var tag = await _tagRepository.GetAsync(tagId);
            if (tag == null)
                throw new EntityNotFoundException("Tag does not exist");
            
            await _tagRepository.DeleteAsync(tag);
        }

        public async Task CreateCategory(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new EmptyException("Category name cannot be empty");

            if (await _categoryRepository.Exists(name))
                throw new AlreadyExistsException("Category with this name already exists");

            var category = new Category(name);
            await _categoryRepository.AddAsync(category);
        }

        public async Task DeleteCategory(int catId)
        {
            var category = await _categoryRepository.GetAsync(catId);
            if (category == null)
                throw new EntityNotFoundException("Category does not exist");

            await _categoryRepository.DeleteAsync(category);
        }

        public async Task CreateAuthor(CreateAuthorModel authorData)
        {
            if (string.IsNullOrEmpty(authorData.Firstname) || string.IsNullOrEmpty(authorData.Lastname))
                throw new EmptyException("Firstname/Lastname name cannot be empty");

            if (await _authorRepository.Exists(authorData.Firstname, authorData.Lastname))
                throw new AlreadyExistsException("Author with this firstname and lastname already exists");

            var author = new Author(authorData.Firstname, authorData.Lastname);
            await _authorRepository.AddAsync(author);
        }

        public async Task DeleteAuthor(int catId)
        {
            var author = await _authorRepository.GetAsync(catId);
            if (author == null)
                throw new EntityNotFoundException("Author does not exist");

            await _authorRepository.DeleteAsync(author);
        }
    }
}
