using ELibrary_BookService.Application.Command;
using ELibrary_BookService.Application.Dto;
using ELibrary_BookService.Application.Query;
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
        private readonly ICommonReadProvider _commonReadProvider;

        public CategoryController(ICommonProvider commonProvider, ICommonReadProvider commonReadProvider)
        {
            _commonProvider = commonProvider;
            _commonReadProvider = commonReadProvider;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        [ProducesResponseType(200, Type = typeof(BookReadModel))]
        public async Task<ActionResult<CategoryReadModel>> Get(int id)
        {
            var result = await _commonReadProvider.GetCategory(id);
            if (result is null)
                return NotFound("Category does not exist");

            return result;
        }

        [HttpGet]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        [ProducesResponseType(200, Type = typeof(BookReadModel))]
        public async Task<ActionResult<List<CategoryReadModel>>> GetAll()
        {
            var result = await _commonReadProvider.GetCategories();

            return result;
        }

        [HttpPost]
        [Authorize(Roles = "admin, employee")]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(401, Type = typeof(string))]
        [ProducesResponseType(403, Type = typeof(string))]
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
        [ProducesResponseType(403, Type = typeof(string))]
        [ProducesResponseType(204)]
        public async Task<ActionResult> DeleteCategory([FromRoute] int id)
        {
            await _commonProvider.DeleteCategory(id);
            return NoContent();
        }
    }
}
