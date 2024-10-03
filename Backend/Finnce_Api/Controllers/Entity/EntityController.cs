
using Finnce_Api.Models.EntityModelDto;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Finnce_Api.Controllers.Entity
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntityController : ControllerBase
    {
        private readonly EntityService entityService;

        public EntityController(EntityService entityService)
        {
            this.entityService = entityService;
        }



        [HttpPost("CreateEntity")]
        [Authorize]
        public ActionResult<EntityCore> CreateEntity([FromBody] EntityModel entityModel)
        {
            try
            {
                var createEntity = entityService.CreateEntity(entityModel, User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                return Ok(createEntity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro Interno do servidor");
            }
        }

        [HttpDelete("DeleteEntity")]
        public IActionResult DeleteEntity(String entityId)
        {
            try
            {
                var deleteEntity = entityService.DeleteEntity(entityId);
                return Ok(deleteEntity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro Interno do servidor");
            }
        }


        [HttpPut("UpdateEntity")]
        public IActionResult UpdateEntity([FromBody] EntityModel entityModel)
        {
            try
            {
                var editCategory = entityService.UpdateEntity(entityModel);
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


        [HttpGet("User/GetAllEntityForUser")]
        [Authorize]
        public ActionResult<IEnumerable<EntityCore>> GetAllEntityForUser()
        {
            var entitys = this.entityService.GetAllEntityForUser(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            return Ok(entitys);
        }

    }
}
