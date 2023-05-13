using ELibrary_BookService.Application.Command;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ELibrary_BookService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICommonProvider _commonProvider;

        public CategoryController(ICommonProvider commonProvider)
        {
            _commonProvider = commonProvider;
        }

        [HttpPost]
        [Authorize(Roles = "admin, employee")]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(401, Type = typeof(string))]
        [ProducesResponseType(204)]
        public async Task<ActionResult> CreateCategory([FromBody] string catName)
        {
            await _commonProvider.CreateCategory(catName);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin, employee")]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(401, Type = typeof(string))]
        [ProducesResponseType(204)]
        public async Task<ActionResult> DeleteCategory([FromRoute] int id)
        {
            await _commonProvider.DeleteCategory(id);
            return NoContent();
        }
    }
}
