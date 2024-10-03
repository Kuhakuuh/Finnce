using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Text;

namespace Finnce_Api.Controllers.Transations
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransationsController : ControllerBase
    {
        private readonly TransactionService transactionService;
        private readonly TinkService tinkService;

        public TransationsController(TransactionService transactionService, TinkService tinkService)
        {
            this.transactionService = transactionService;
            this.tinkService = tinkService;
        }


        /// <summary>
        /// Função exclusiva para buscar users, Só administrador.
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet("getAllTransactions")]
        public ActionResult<IEnumerable<TransactionsModel>> GetAllTransactions()
        {
            var transactions = transactionService.GetAllTransactions();
            return Ok(transactions);
        }


        /// <summary>
        /// Esta função retorna transações do user com token
        /// </summary>
        /// <returns></returns>
        [HttpGet("User/GetAllTransactionsOfUser")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TransactionsModel>>> GetAllTransactionsForUserAsync()
        {
            try
            {
                var transactions =
                    await transactionService.GetAllTransactionsForUser(User.FindFirst(ClaimTypes.NameIdentifier)
                        ?.Value);

                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        /// <summary>
        /// Adiciona uma transação criada manualmente inserida pelo utilizador
        /// A transação pode ser do tipo Revenue ou Expense (Para conseguir saber se é uma despesa ou uma receita)
        /// </summary>
        /// <param name="transactionsModel"></param>
        /// <returns></returns>
        [HttpPost("CreateManualTransaction")]
        public ActionResult<Transaction> RegisterManualTransaction([FromBody] TransactionsModel transactionsModel)
        {
            try
            {
                var createTransaction = transactionService.CreateManualTransaction(transactionsModel);
                return Ok(createTransaction);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor");
            }
        }


        /// <summary>
        /// Esta função serve para categorizar transações
        /// </summary>
        /// <param name="transactionsModel"></param>
        /// <returns></returns>
        [HttpPut("RegisterExpense")]
        public IActionResult CategorizeExpense([FromBody] TransactionsModel transactionsModel)
        {
            try
            {
                var editTransaction = transactionService.CategorizeExpense(transactionsModel);

                return Ok(editTransaction);
            }
            catch (Exception ex)
            {
                return StatusCode(204, "Erro interno do servidor");
            }
        }




        /// <summary>
        /// Esta função serve a introdução manual de contas via json
        /// </summary>
        /// <param name="accounts"></param>
        /// <returns></returns>
        [HttpPost("RegisterJsonManualAccounts")]
        [Authorize]
        public async Task<IActionResult> CreateJsonManualAccount([FromBody] string accounts)
        {
            try
            {

                byte[] dataBytes = Convert.FromBase64String(accounts);
                string jsonData = Encoding.UTF8.GetString(dataBytes);
                var accountsreturn = await this.tinkService.SyncBankingAccounts(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, null, jsonData);

                if (accountsreturn != null)
                {
                    return Ok(jsonData);
                }
                else
                {
                    return BadRequest("error In data Async");
                }
            }
            catch (Exception ex)
            {

                return BadRequest($"Error processing JSON: {ex.Message}");
            }



        }



        /// <summary>
        /// Esta função serve a introdução manual de transações via json
        /// </summary>
        /// <param name="transactions"></param>
        /// <returns></returns>
        [HttpPost("RegisterJsonManualTransactions")]
        [Authorize]
        public async Task<IActionResult> CreateJsonManualTransactions([FromBody] string transactions)
        {
            try
            {

                byte[] dataBytes = Convert.FromBase64String(transactions);
                string jsonData = Encoding.UTF8.GetString(dataBytes);
                var transactionsreturn = await this.tinkService.SyncBankingTransactions(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, jsonData);

                if (transactionsreturn != null)
                {
                    return Ok(jsonData);
                }
                else
                {
                    return BadRequest("error In data Async");
                }
            }
            catch (Exception ex)
            {

                return BadRequest($"Error processing JSON: {ex.Message}");
            }



        }



        [HttpPost("EditTransaction")]
        [Authorize]
        public async Task<IActionResult> EditTransaction([FromBody] TransactionsModelNull transactionModel)
        {
            Boolean result = await transactionService.EditTransactionAsync(transactionModel, User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            if (result)
            {

               return Ok(result);
            }
            
            return BadRequest(result);
        }

    }
}

