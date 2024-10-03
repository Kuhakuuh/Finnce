

using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Finnce_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly AccountService accountService;
        public AccountController(AccountService accountService)
        {
            this.accountService = accountService;

        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("getAllAccounts")]
        public ActionResult<IEnumerable<AccountModel>> GetAllAccounts()
        {
            var accounts = this.accountService.GetAllAccount();
            return Ok(accounts);
        }

        [HttpGet("User/GetAllAccountsForUser")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Account>>> GetAllAccountsForUser()
        {
            var accounts = await accountService.GetAllAccountForUser(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            return Ok(accounts);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("GetAccountsById")]
        public async Task<IActionResult> GetAccountsByName(string IdUser)
        {
            try
            {
                var accounts = await accountService.GetAccountsById(IdUser);

                if (accounts == null || accounts.Count == 0)
                {
                    return NotFound("No accounts found with Id ");
                }

                return Ok(accounts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost("CreateAccount")]
        [Authorize]
        public ActionResult<Account> CreateAccount([FromBody] AccountModel accountModel)
        {
            try
            {
                var createdAccount = accountService.CreateAccount(accountModel, User.FindFirst(ClaimTypes.NameIdentifier)?.Value);



                return Ok(createdAccount);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Erro interno do servidor");
            }
        }



    }
}
