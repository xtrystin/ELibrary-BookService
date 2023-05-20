using ELibrary_BookService.Application.Command;
using ELibrary_BookService.Application.Query;
using ELibrary_BookService.Application.Query.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ELibrary_BookService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ICommonProvider _commonProvider;
        private readonly ICommonReadProvider _commonReadProvider;

        public TagController(ICommonProvider commonProvider, ICommonReadProvider commonReadProvider)
        {
            _commonProvider = commonProvider;
            _commonReadProvider = commonReadProvider;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        [ProducesResponseType(200, Type = typeof(BookReadModel))]
        public async Task<ActionResult<TagReadModel>> Get(int id)
        {
            var result = await _commonReadProvider.GetTag(id);
            if (result is null)
                return NotFound("Tag does not exist");

            return result;
        }

        [HttpGet]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        [ProducesResponseType(200, Type = typeof(BookReadModel))]
        public async Task<ActionResult<List<TagReadModel>>> GetAll()
        {
            var result = await _commonReadProvider.GetTags();

            return result;
        }

        [HttpPost]
        [Authorize(Roles = "admin, employee")]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(401, Type = typeof(string))]
        [ProducesResponseType(403, Type = typeof(string))]
        [ProducesResponseType(204)]
        public async Task<ActionResult> CreateTag([FromBody] string tagName)
        {
            await _commonProvider.CreateTag(tagName);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin, employee")]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(401, Type = typeof(string))]
        [ProducesResponseType(403, Type = typeof(string))]
        [ProducesResponseType(204)]
        public async Task<ActionResult> DeleteTag([FromRoute] int id)
        {
            await _commonProvider.DeleteTag(id);
            return NoContent();
        }
    }
}
