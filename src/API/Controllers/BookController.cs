using ELibrary_BookService.Application.Command;
using ELibrary_BookService.Application.Command.Model;
using ELibrary_BookService.Application.Query;
using ELibrary_BookService.Application.Query.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
        [ProducesResponseType(200, Type = typeof(List<BookReadModel>))]
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
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        [ProducesResponseType(200, Type = typeof(BookReadModel))]
        public async Task<ActionResult<BookReadModel>> Get(int id)
        {
            var result = await _bookReadProvider.GetBook(id);
            if (result is null)
                return NotFound("Book does not exist");
            
            return result;
        }

        // GET api/<BookController>/5
        [HttpGet("ByFilter")]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        [ProducesResponseType(200, Type = typeof(BookReadModel))]
        [SwaggerOperation(Summary = "Get Books by one or more filters. Omitt filters which you do not want to use")]
        public async Task<ActionResult<List<BookReadModel>>> GetByFilter([FromQuery] int? categoryId, 
            [FromQuery] int? tagId, [FromQuery] int? authorId)
        {
            var result = await _bookReadProvider.GetBooksByFilter(categoryId, tagId, authorId);
            if (result is null)
                return NotFound("Book does not exist");

            return result;
        }

        // POST api/<BookController>
        [HttpPost]
        [Authorize(Roles = "admin, employee")]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(401, Type = typeof(string))]
        [ProducesResponseType(403, Type = typeof(string))]
        [ProducesResponseType(204)]
        public async Task<ActionResult> Post([FromBody] CreateBookModel bookData)
        {
            if (ModelState.IsValid == false)
            {
                var a = ModelState.Root.Errors.First().ErrorMessage;
                return BadRequest(a);
            }
            await _bookProvider.CreateBook(bookData);
            return NoContent();
        }

        // PUT api/<BookController>/5
        [HttpPatch("{id}")]
        [Authorize(Roles = "admin, employee")]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(401, Type = typeof(string))]
        [ProducesResponseType(403, Type = typeof(string))]
        [ProducesResponseType(204)]
        [SwaggerOperation(Summary = "Modify book data. You can send params which you want to change. Omitted params will remain the same. newPdfLink with value \"\" will set pdfLink to null")]
        public async Task<ActionResult> Patch(int id, [FromBody] ModifyBookModel bookData)
        {
            await _bookProvider.ModifyBook(id, bookData);
            return NoContent();
        }

        // DELETE api/<BookController>/5
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(401, Type = typeof(string))]
        [ProducesResponseType(403, Type = typeof(string))]
        [ProducesResponseType(204)]
        public async Task<ActionResult> Delete(int id)
        {
            await _bookProvider.DeleteBook(id);
            return NoContent();
        }

        [HttpPost("{id}/ChangeAmount")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(401, Type = typeof(string))]
        [ProducesResponseType(403, Type = typeof(string))]
        [ProducesResponseType(204)]
        public async Task<ActionResult> ChangeBookAmount([FromRoute] int id, [FromBody] int amount)
        {
            await _bookProvider.ChangeBookAmount(id, amount);
            return NoContent();
        }

    }
}
