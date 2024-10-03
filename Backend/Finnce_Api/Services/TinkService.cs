using Finnce_Api.core.Notifications;
using Finnce_Api.core.TinkAcess;
using Finnce_Api.Models.NotificationDto;
using Hangfire;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace Finnce_Api.Services
{
    public class TinkService
    {
        private readonly RepositoryContext context;
        private readonly IBackgroundJobClient backgroundJobClient;
        private readonly TinkEndpoints tinkEndpoints;
        private readonly AccountService accountService;
        private readonly UserService userService;
        private readonly CategoryService categoryService;
        private readonly EntityService entityService;
        private readonly NotificationService notificationService;
        private readonly IHubContext<TransactionNotificationHub, INotification> transactionNotification;



        public TinkService(RepositoryContext context, IBackgroundJobClient backgroundJobClient,
            TinkEndpoints tinkEndpoints, AccountService accountService, UserService userService,
            CategoryService categoryService, NotificationService notificationService, EntityService entityService, IHubContext<TransactionNotificationHub, INotification> transactionNotification)
        {
            this.context = context;
            this.backgroundJobClient = backgroundJobClient;
            this.tinkEndpoints = tinkEndpoints;
            this.accountService = accountService;
            this.userService = userService;
            this.categoryService = categoryService;
            this.entityService = entityService;
            this.transactionNotification = transactionNotification;
            this.notificationService = notificationService;
        }

        /// <summary>
        /// Atualiza o access token do user na base de dados
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<Boolean> UpdateToken(string userId, string token)
        {
            User user = await userService.AddIdTink(userId, token);


            if (user != null)
            {
                backgroundJobClient.Enqueue(() => SyncBankingAccounts(userId, token, null));
                backgroundJobClient.Enqueue(() => SyncBankingTransactions(userId, null));
                return true;
            }


            return false;
        }

        public async Task<String> GetUserToken(string id)
        {
            var user = context.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                return user.TinkToken;
            }

            return null;
        }


        /// <summary>
        /// This code ir for data conversion and insert in DataBase
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<Boolean> SyncBankingAccounts(string userId, string? token, string? jsonAccounts)
        {
            try
            {

                string objSon;
                if (jsonAccounts == null)
                {
                    var accessToken = JObject.Parse(token)["access_token"].ToString();
                    var result = await tinkEndpoints.GetTinkUserAccounts(accessToken);
                    objSon = JObject.Parse(result)["accounts"].ToString();


                }
                else
                {
                    objSon = JObject.Parse(jsonAccounts)["accounts"].ToString();

                }


                List<Accounts> accountsParsed = JsonConvert.DeserializeObject<List<Accounts>>(objSon);
                List<Account> accounts = new List<Account>();

                foreach (Accounts account in accountsParsed)
                {
                    //Accounts account = accountToken.ToObject<Accounts>();


                    decimal amount = 0;
                    int unscaledValue = account.Balances.available.amount.value.unscaledValue;
                    int scale = account.Balances.available.amount.value.scale;


                    if (scale != 0)
                    {
                        if (unscaledValue >= int.MaxValue / (decimal)Math.Pow(10, scale))
                        {
                            throw new OverflowException("Value is too large for a Decimal.");
                        }

                        amount = unscaledValue / (decimal)Math.Pow(10, scale);
                    }
                    else
                    {
                        amount = unscaledValue;
                    }


                    var newaccount = new Account
                    {
                        AccountId = account.Id,
                        Name = account.Name,
                        IdUser = userId,
                        Iban = account.Identifiers.iban.iban,
                        Description = account.Name,
                        TypeAccount = account.Identifiers.iban.iban,
                        CurrencyCode = account.Balances.available.amount.currencyCode,
                        lastRefreshed = account.Dates.lastRefreshed,
                        Amount = amount,
                        StatusBlockedTransation = false
                    };

                    accountService.CreateAccountWithCompleteData(newaccount);
                }

                await transactionNotification.Clients.All.SendMessage(new Notification
                {
                    //TransactionID = newTransaction.Id.ToString(),
                    Name = "Accounts",
                    Message = "New Accounts Added"

                });
                NotificationModel notificationModel = new NotificationModel
                {
                    IdUser = userId,
                    Message = "New Accounts Added",
                    Name = "Accounts",
                };
                notificationService.CreateNotification(notificationModel, userId);
                Console.WriteLine("Mensagem enviada");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error syncing banking accounts: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Get all the transactions from the @userID and Deserialize the json object recieved to Transaction Type,
        /// then save the data in Database.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<Boolean> SyncBankingTransactions(string userId, string? jsonTransactions)
        {

            string token;
            string JsonTransactions;
            string objSON;
            if (jsonTransactions == null)
            {
                token = await GetUserToken(userId);
                JsonTransactions = await tinkEndpoints.GetTinkUserTransactions(token);
                objSON = JObject.Parse(JsonTransactions)["transactions"].ToString();



            }
            else
            {
                objSON = JObject.Parse(jsonTransactions)["transactions"].ToString();

            }




            Dictionary<string, string> AccountDictionary = new();
            List<Account> accounts = await accountService.GetAllAccountForUser(userId);
            Guid idGeneric = await categoryService.SearchCategoryGeneric(userId);
            List<TransactionParse> transactionsParsed = JsonConvert.DeserializeObject<List<TransactionParse>>(objSON);
            //List<Transaction> transactions = new List<Transaction>();

            foreach (var account in accounts)
            {

                string guidString = account.Id.ToString();
                AccountDictionary.Add(account.AccountId, guidString);
            }

            try
            {

                foreach (TransactionParse transaction in transactionsParsed)
                {
                    var amount = transaction.Amount.Value.UnscaledValue / (decimal)Math.Pow(10, (double)transaction.Amount.Value.Scale);
                    EnumTypeTransiction typeOfTransaction;
                    if (amount >= 0)
                    {
                        typeOfTransaction = EnumTypeTransiction.Revenue;
                    }
                    else
                    {
                        typeOfTransaction = EnumTypeTransiction.Expense;
                    }
                    AccountDictionary.TryGetValue(transaction.AccountId, out string accountId);

                    Transaction tempTransaction = new Transaction
                    {
                        IdAccount = new Guid(accountId),
                        type = typeOfTransaction,
                        Amount = amount,
                        IdUser = userId,
                        CurrencyCode = transaction.Amount.CurrencyCode,
                        DescriptionDisplay = transaction.Descriptions.Display,
                        DateBokeed = transaction.Dates.Booked,
                        providerTransactionId = transaction.Identifiers.ProviderTransactionId,
                    };



                    if (transaction.categories != null)
                    {
                        Category category = new()
                        {
                            Name = transaction.categories.pfm.name,
                            IdInternal = transaction.categories.pfm.id,
                            IdUser = userId
                        };

                        tempTransaction.IdCategory = categoryService.CreateCategoryInTink(category).Id;

                    }
                    else
                    {
                        tempTransaction.IdCategory = idGeneric;
                    }
                    EntityCore entityCore = new()
                    {
                        Name = transaction.Descriptions.Display,
                        IdUser = userId

                    };

                    EntityCore returnentityCore = await entityService.CreateEntityInTink(entityCore);

                    tempTransaction.IdEntity = returnentityCore.Id;


                    context.Transactions.Add(tempTransaction);



                }
                context.SaveChanges();
                await transactionNotification.Clients.All.SendMessage(new Notification
                {
                    //TransactionID = newTransaction.Id.ToString(),
                    Name = "Transactions",
                    Message = "New Transactions Added"

                });
                NotificationModel notificationModel = new NotificationModel
                {
                    IdUser = userId,
                    Message = "New Transactions Added",
                    Name = "Transactions",
                };
                notificationService.CreateNotification(notificationModel, userId);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error syncing the transaction data : {ex.Message}");
                return false;
            }
        }
    }
}