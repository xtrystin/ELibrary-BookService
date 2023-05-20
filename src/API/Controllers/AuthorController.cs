using ELibrary_BookService.Application.Command;
using ELibrary_BookService.Application.Command.Model;
using ELibrary_BookService.Application.Query;
using ELibrary_BookService.Application.Query.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ELibrary_BookService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly ICommonProvider _commonProvider;
        private readonly ICommonReadProvider _commonReadProvider;

        public AuthorController(ICommonProvider commonProvider, ICommonReadProvider commonReadProvider)
        {
            _commonProvider = commonProvider;
            _commonReadProvider = commonReadProvider;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        [ProducesResponseType(200, Type = typeof(BookReadModel))]
        public async Task<ActionResult<AuthorReadModel>> Get(int id)
        {
            var result = await _commonReadProvider.GetAuthor(id);
            if (result is null)
                return NotFound("Author does not exist");

            return result;
        }

        [HttpGet]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        [ProducesResponseType(200, Type = typeof(BookReadModel))]
        public async Task<ActionResult<List<AuthorReadModel>>> GetAll()
        {
            var result = await _commonReadProvider.GetAuthors();

            return result;
        }

        [HttpPost]
        [Authorize(Roles = "admin, employee")]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(401, Type = typeof(string))]
        [ProducesResponseType(403, Type = typeof(string))]
        [ProducesResponseType(204)]
        public async Task<ActionResult> CreateAuthor([FromBody] CreateAuthorModel authorData)
        {
            
            await _commonProvider.CreateAuthor(authorData);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin, employee")]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(401, Type = typeof(string))]
        [ProducesResponseType(403, Type = typeof(string))]
        [ProducesResponseType(204)]
        public async Task<ActionResult> DeleteAuthor([FromRoute] int id)
        {
            await _commonProvider.DeleteAuthor(id);
            return NoContent();
        }
    }
}
