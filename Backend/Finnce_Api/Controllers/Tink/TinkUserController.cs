using Finnce_Api.Models.UserModelDto;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Finnce_Api.Controllers.Tink
{
    [Route("api/[controller]")]
    [ApiController]
    public class TinkUserController : ControllerBase
    {
        private readonly TinkEndpoints tinkendpoints;
        private readonly UserService userService;
        private readonly TinkService tinkService;

        public TinkUserController(TinkEndpoints tinkEndpoints, UserService userService, TinkService tinkService)
        {
            this.tinkendpoints = tinkEndpoints;
            this.userService = userService;
            this.tinkService = tinkService;
        }



        /// <summary>
        /// Esta função cria o acesso á tink
        /// </summary>
        /// <returns></returns>
        [HttpPost("CreateUserTink")]
        [Authorize]
        public async Task<ActionResult<UserModel>> CreateAccessToTink()
        {
            try
            {
                var result = await tinkendpoints.ContinuosAcessFunction(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return Unauthorized("Error in obtain Acess to Tink");

            }

        }


        [HttpGet("callback")]
        public async Task<IActionResult> ReceiveCallback([FromQuery] string userId, [FromQuery] string code, [FromQuery] string credentialsId)
        {
            try
            {
                var result = await tinkendpoints.GetTokenFromAuthCode(code);
                bool teste = await tinkService.UpdateToken(userId, result);

                return Redirect("http://localhost:4200/dashboards");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor falha");
            }

        }



        /// <summary>
        /// Retorna as contas assiciados ao user
        /// </summary>
        /// <param name="tinkModel"></param>
        /// <returns></returns>
        [HttpPost("GetTinkUserAccounts")]
        [Authorize]
        public async Task<ActionResult<TinkAccountModel>> GetTinkUserAccount()
        {
            try
            {

                var token = await tinkService.GetUserToken(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                //var accessToken = JObject.Parse(token)["access_token"].ToString();
                var result = await tinkendpoints.GetTinkUserAccounts(token);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor falha");
            }
            {

            }


        }

        /// <summary>
        /// Retorna as transações assiciadas ao user
        /// </summary>
        /// <param name="tinkModel"></param>
        /// <returns></returns>
        [HttpPost("GetTinkUserTransactions")]
        [Authorize]
        public async Task<ActionResult<TinkAccountModel>> GetTinkUserTransactions([FromBody] UserIdModel userID)
        {
            try
            {
                var token = await tinkService.GetUserToken(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var accessToken = JObject.Parse(token)["access_token"].ToString();
                var result = await tinkendpoints.GetTinkUserTransactions(accessToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor falha");
            }
            {

            }


        }



    }



}
