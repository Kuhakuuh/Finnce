using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Finnce_Api.Controllers.Charts
{

    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : Controller
    {
        private readonly ChartService chartService;
        public ChartController(ChartService chartService)

        {
            this.chartService = chartService;
        }



        /// <summary>
        /// This controler get Data for month In One Year
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetDataForMonthInYear")]
        [Authorize]
        public IActionResult GetDataForMonthInYear()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {

                return BadRequest("User ID not found");
            }

            var dictionaryReturn = chartService.GetDataForMonthDashboard(userId);

            if (dictionaryReturn == null)
            {

                return NotFound("No expense data found");
            }

            return Ok(dictionaryReturn);
        }


    }
}
