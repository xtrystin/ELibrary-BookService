using ELibrary_BookService.Application.Command;
using ELibrary_BookService.Application.Command.Dto;
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

        public AuthorController(ICommonProvider commonProvider)
        {
            _commonProvider = commonProvider;
        }

        [HttpPost]
        [Authorize(Roles = "admin, employee")]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(401, Type = typeof(string))]
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
        [ProducesResponseType(204)]
        public async Task<ActionResult> DeleteAuthor([FromRoute] int id)
        {
            await _commonProvider.DeleteAuthor(id);
            return NoContent();
        }
    }
}
