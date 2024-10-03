namespace Finnce_Api.Services
{
    public class AccountService
    {

        private readonly RepositoryContext context;

        public AccountService(RepositoryContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Retorna todas as contas de todos os utilizadores. Função Admin
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Account> GetAllAccount()
        {
            if (this.context.Accounts != null)
            {
                var account = this.context.Accounts.Select(account => new Account
                {
                    AccountId = account.AccountId,
                    Name = account.Name,
                    IdUser = account.IdUser,
                    Iban = account.Iban,
                    Description = account.Name,
                    TypeAccount = account.Iban,
                    CurrencyCode = account.CurrencyCode,
                    lastRefreshed = account.lastRefreshed,
                    Amount = account.Amount,
                    StatusBlockedTransation = false

                }).ToList();
                return account;
            }
            return null;
        }

        /// <summary>
        /// Retorna todas as contas por user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<Account>> GetAllAccountForUser(string userId)
        {
            var transactionList = context.Accounts
        .Where(t => t.IdUser == userId)
        .ToList();
            if (transactionList != null)
            {
                return transactionList;
            }

            return null;
        }

        /// <summary>
        /// Função para retornar conta por id
        /// </summary>
        /// <param name="IdUser"></param>
        /// <returns></returns>
        public async Task<List<Account>> GetAccountsById(string IdUser)
        {
            return await context.Accounts
                .Where(account => account.IdUser == IdUser)
                .ToListAsync();

        }


        /// <summary>
        /// Função para criar conta
        /// </summary>
        /// <param name="accountModel"></param>
        /// <param name="IdUser"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public Account CreateAccount(AccountModel accountModel, string IdUser)
        {
            if (accountModel == null)
            {
                throw new ArgumentNullException(nameof(accountModel));
            }

            var newAccount = MapCreateAccountViewToAccount(accountModel, IdUser) ?? throw new InvalidOperationException("Erro ao mapear dados da conta");

            context.Accounts.Add(newAccount);
            context.SaveChanges();
            return newAccount;
        }


        /// <summary>
        /// Função para editar valor da conta
        /// </summary>
        /// <param name="transactionsModel"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public async Task<bool> EditAmount(TransactionsModel transactionsModel, decimal amount)
        {
            var account = await context.Accounts.FirstOrDefaultAsync(x => x.Id == transactionsModel.IdAccount);

            if (account != null)
            {
                try
                {


                    if (transactionsModel.Type != null)
                    {
                      

                            if (transactionsModel.Type == "EXPENSE" || transactionsModel.Type == "Expense")
                            {
                                account.Amount += amount;
                                await context.SaveChangesAsync();
                                return true;
                            }
                            else if (transactionsModel.Type == "REVENUE" || transactionsModel.Type == "Revenue")
                            {
                                account.Amount += amount;
                                await context.SaveChangesAsync();
                                return true;
                            }


                        
                    }
                    else
                    {

                        return false;
                    }
                }
                catch (Exception ex)
                {

                    return false;
                }
            }

            return false;
        }

        /// <summary>
        /// Função para mapear conta 
        /// </summary>
        /// <param name="accountModel"></param>
        /// <param name="IdUser"></param>
        /// <returns></returns>
        private Account MapCreateAccountViewToAccount(AccountModel accountModel, string IdUser)
        {
            //var userById = context.Users.FirstOrDefault(u => u.Id == IdUser);
            return new Account
            {
                Name = accountModel.Name,
                TypeAccount = accountModel.TypeAccount,
                Amount = accountModel.Amount,
                CurrencyCode = "",
                Description = accountModel.Description,
                StatusBlockedTransation = accountModel.StatusBlockedTransation,
                IdUser = IdUser

            };
        }



        /// <summary>
        /// Função para salvar conta
        /// </summary>
        /// <param name="accountModel"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public Account CreateAccountWithCompleteData(Account accountModel)
        {
            if (accountModel == null)
            {
                throw new ArgumentNullException(nameof(accountModel));
            }



            context.Accounts.Add(accountModel);
            context.SaveChanges();
            return accountModel;
        }

    }


}
