using BusinessLayer;
using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/Categories")]
    public class CategoriesController : ControllerBase
    {
        [HttpGet("All", Name = "GetAllCategories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<CategoryDTO>> GetAllCategories()
        {
            List<CategoryDTO> CategoriesList = BusinessLayer.Category.GetAllCategories();

            if (CategoriesList.Count == 0)
            {
                return NotFound("No Categories found.");
            }

            return Ok(CategoriesList);
        }
    }
}
