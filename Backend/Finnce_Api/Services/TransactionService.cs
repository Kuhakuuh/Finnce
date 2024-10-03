using Finnce_Api.core.Notifications;
using Finnce_Api.Models.NotificationDto;
using Microsoft.AspNetCore.SignalR;
using Transaction = Finnce_Api.core.Transactions.Transaction;

namespace Finnce_Api.Services
{
    public class TransactionService
    {
        private readonly RepositoryContext context;
        private readonly IHubContext<TransactionNotificationHub, INotification> transactionNotification;
        private readonly AccountService accountService;
        private readonly UserService userService;
        private readonly CategoryService categoryService;
        private readonly EntityService entityService;
        private readonly NotificationService notificationService;
        public TransactionService(RepositoryContext context, NotificationService notificationService, IHubContext<TransactionNotificationHub, INotification> transactionNotification, AccountService accountService, UserService userService,
            CategoryService categoryService, EntityService entityService)
        {
            this.context = context;
            this.transactionNotification = transactionNotification;
            this.accountService = accountService;
            this.userService = userService;
            this.categoryService = categoryService;
            this.entityService = entityService;
            this.notificationService = notificationService;
        }

        /// <summary>
        /// THis is for admin user
        /// </summary>
        /// <returns></returns>
        public async Task<List<Transaction>> GetAllTransactions()
        {
            var transactionList = context.Transactions.ToList();

            return transactionList;

        }


        /// <summary>
        /// Get All transactions for user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<Transaction>> GetAllTransactionsForUser(string userId)
        {
            var transactionList = context.Transactions
         .Where(t => t.IdUser == userId)
         .ToList();

            return transactionList;

        }
        /// <summary>
        /// Adiciona uma transação criada manualmente inserida pelo utilizador
        /// A transação pode ser do tipo Revenue ou Expense (Para conseguir saber se é uma despesa ou uma receita)
        /// </summary>
        /// <param name="transactionsModel"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<Transaction> CreateManualTransaction(TransactionsModel transactionsModel)
        {
            if (transactionsModel == null)
            {
                throw new ArgumentNullException(nameof(transactionsModel));
            }

            var newTransaction = MapCreateTransactionViewToTransaction(transactionsModel) ?? throw new InvalidOperationException("Erro ao mapear a subcategoria");
            context.Transactions.Add(newTransaction);
            context.SaveChanges();
            await transactionNotification.Clients.All.SendMessage(new Notification
            {

                Name = newTransaction.DescriptionDisplay,
                Message = "New Transaction Added"

            });

            

            NotificationModel notificationModel = new NotificationModel
            {
                IdUser = newTransaction.IdUser,
                Message = "New Transaction Added",
                Name = newTransaction.DescriptionDisplay,
            };
            notificationService.CreateNotification(notificationModel, newTransaction.IdUser);
            Console.WriteLine("Mensagem enviada");
            accountService.EditAmount(transactionsModel, transactionsModel.Amount);
            return newTransaction;

        }



        /// <summary>
        /// Cria uma view da transação na BD 
        /// </summary>
        /// <param name="transactionsModel"></param>
        /// <returns></returns>
        private Transaction MapCreateTransactionViewToTransaction(TransactionsModel transactionsModel)
        {

            return new Transaction
            {
                type = ConvertToEnum(transactionsModel.Type),
                Amount = transactionsModel.Amount,
                CurrencyCode = transactionsModel.CurrencyCode,
                providerTransactionId = "1000", // type manual
                IdUser = transactionsModel.IdUser,
                IdAccount = transactionsModel.IdAccount,
                DateBokeed = transactionsModel.DateBokeed,
                DescriptionDisplay = transactionsModel.DescriptionDisplay,
                IdCategory = transactionsModel.IdCategory
            };

        }
        /// <summary>
        /// Converte o valor da string para Revenue|Expense
        /// 
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        private EnumTypeTransiction ConvertToEnum(String Type)
        {
            EnumTypeTransiction result;
            Enum.TryParse(Type, out result);
            return result;
        }

        public Transaction CategorizeExpense(TransactionsModel transaction)
        {

            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }

            var editTransaction = MapCreateTransactionViewToTransaction(transaction) ?? throw new InvalidOperationException("Erro ao mapear a subcategoria");
            context.Transactions.Update(editTransaction);
            context.SaveChanges();
            return editTransaction;

        }



        public async Task<bool> EditTransactionAsync(TransactionsModelNull transaction, string userId)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }

            var transactionToUpdate = await context.Transactions
                .FirstOrDefaultAsync(t => t.Id == transaction.Id && t.IdUser == userId);

            if (transactionToUpdate != null)
            {
                //Corrigir este update
                transactionToUpdate.type = transactionToUpdate.type;
                if (transaction.Amount != null)
                {
                    transactionToUpdate.Amount = (decimal)transaction.Amount;
                }

                transactionToUpdate.CurrencyCode = transactionToUpdate.CurrencyCode;
                transactionToUpdate.providerTransactionId = transactionToUpdate.providerTransactionId;
                transactionToUpdate.IdUser = transactionToUpdate.IdUser;
                transactionToUpdate.IdAccount = transactionToUpdate.IdAccount;
                transactionToUpdate.DateBokeed = transactionToUpdate.DateBokeed;
                transactionToUpdate.DescriptionDisplay = transaction?.DescriptionDisplay;
                transactionToUpdate.IdCategory = transaction?.IdCategory;

                await context.SaveChangesAsync();

                return true;
            }

            return false;
        }

    }
}
