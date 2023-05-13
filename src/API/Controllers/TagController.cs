using ELibrary_BookService.Application.Command;
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

        public TagController(ICommonProvider commonProvider)
        {
            _commonProvider = commonProvider;
        }

        [HttpPost]
        [Authorize(Roles = "admin, employee")]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(401, Type = typeof(string))]
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
        [ProducesResponseType(204)]
        public async Task<ActionResult> DeleteTag([FromRoute] int id)
        {
            await _commonProvider.DeleteTag(id);
            return NoContent();
        }
    }
}
