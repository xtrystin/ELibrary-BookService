using ELibrary_BookService.Application;
using ELibrary_BookService.Application.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ELibrary_BookService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookProvider _bookProvider;
        private readonly IBookReadProvider _bookReadProvider;

        public BookController(IBookProvider bookProvider, IBookReadProvider bookReadProvider)
        {
            _bookProvider = bookProvider;
            _bookReadProvider = bookReadProvider;
        }

        // GET: api/<BookController>
        [HttpGet]
        public async Task <ActionResult<List<BookReadModel>>> Get()
        {
            var result = await _bookReadProvider.GetBooks();
            return result;
           /* BookReadModel book = new()
            {
                Id = 1,
                Title = "string",
                Description = "string",
                CreatedDate = DateTime.Now,
                ImageUrl = "string",
                PdfUrl = "string?",
                BookAmount = 100,
                Categories = new List<CategoryReadModel>()
                {
                    new CategoryReadModel() { Id = 1, Name = "cat1"},
                    new CategoryReadModel() { Id = 2, Name = "cat2"}
                },
                Tags = new List<TagReadModel>()
                {
                    new TagReadModel() { Id = 1, Name = "tag1"},
                    new TagReadModel() { Id = 2, Name = "tag2"}
                },
                Authors = new List<AuthorReadModel>()
                {
                    new AuthorReadModel() { Id = 1, Firstname = "John", Lastname = "Json"},
                    new AuthorReadModel() { Id = 2, Firstname = "Hans", Lastname = "Xml"}
                }

            };

            var json = JsonSerializer.Serialize(book);
            Console.WriteLine(json);

            return book;*/
        }

        // GET api/<BookController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookReadModel>> Get(int id)
        {
            var result = await _bookReadProvider.GetBook(id);
            if (result is null)
                return NotFound("Book does not exist");
            
            return result;
        }

        // POST api/<BookController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BookController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BookController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _bookProvider.DeleteBook(id);
        }
    }
}
