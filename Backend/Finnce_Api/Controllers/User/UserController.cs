using Microsoft.AspNetCore.Authorization;

namespace Finnce_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService userService;
        private readonly CategoryService categoryService;


        public UserController(UserService userService, CategoryService categoryService)
        {
            this.userService = userService;
            this.categoryService = categoryService;
        }


        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("getAllUsers")]
        public ActionResult<IEnumerable<UserModel>> GetUsers()
        {
            var users = userService.GetAllUsers();
            return Ok(users);
        }


        [HttpPost("getUser")]
        public ActionResult<UserModel> GetUser(UserModel userModel)
        {
            var user = userService.GetUser(userModel);
            return Ok(user);
        }


        /// <summary>
        /// Creation of normal User
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        [HttpPost("CreateUser")]
        public async Task<ActionResult<UserModelCreate>> CreateUser([FromBody] UserModelCreate newUser)
        {
            if (newUser == null)
            {
                return BadRequest("Invalid user data");
            }


            var createdUser = await userService.CreateUser(newUser);
            if (createdUser == null)
            {
                return BadRequest("User already exists or is internal error");
            }

            if (categoryService.CreateCategoryGeneric(createdUser.Id) == false)
            {
                return BadRequest("Internal error in create Category Generic");
            };



            var userEmail = createdUser.Email;


            return CreatedAtAction(nameof(GetUsers), new { Message = "The user was created successfully" });

        }
    }


}
