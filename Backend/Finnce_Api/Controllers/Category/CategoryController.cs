
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Finnce_Api.Controllers.CategoryController_
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase, ICategoryController
    {
        private readonly CategoryService categoryService;

        public CategoryController(CategoryService categoryService)
        {
            this.categoryService = categoryService;
        }



        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("getAllCategory")]
        public ActionResult<IEnumerable<Category>> GetAllCategories()
        {
            var categories = this.categoryService.GetAllCategories();
            return Ok(categories);
        }

        [HttpGet("User/GetAllCategoryForUser")]
        [Authorize]
        public ActionResult<IEnumerable<Category>> GetAllCategoryForUser()
        {
            var categories = this.categoryService.GetAllCategoryForUser(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            return Ok(categories);
        }

        [HttpGet("GetCategoryById")]
        public async Task<IActionResult> GetCategoryById(string IdUser)
        {
            try
            {
                var categories = await categoryService.GetCategoriesById(IdUser);

                if (categories == null || categories.Count == 0)
                {
                    return NotFound("No accounts found with Id ");
                }

                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }


        [HttpPut("UpdateCategory")]
        public IActionResult UpdateCategoy([FromBody] CategoryModel categoryModel)
        {
            try
            {
                var editCategory = categoryService.UpdateCategory(categoryModel);
                return NoContent();
            }
            catch (DirectoryNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("CreateCategory")]
        public ActionResult<Category> CreateCategory([FromBody] CategoryModel categoryModel)
        {
            try
            {
                var createdCategory = categoryService.CreateCategory(categoryModel);


                return Ok(createdCategory);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Erro interno do servidor");
            }
        }





    }
}
