namespace Finnce_Api.Services
{
    public class ChartService
    {
        private readonly RepositoryContext context;
        private readonly TransactionService transactionService;
        private readonly CategoryService categoryService;

        public ChartService(RepositoryContext context, TransactionService transactionService, CategoryService categoryService)
        {
            this.context = context;
            this.transactionService = transactionService;
            this.categoryService = categoryService;

        }



        public async Task<Dictionary<string, decimal>> GetExpenseForMonth(string IdUser)
        {

            List<Transaction> ListTransaction = await this.transactionService.GetAllTransactionsForUser(IdUser);

            var expensesByMonth = new Dictionary<string, decimal>();

            for (int i = 0; i < 12; i++)
            {

                decimal totalExpense = 0;
                var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                firstDayOfMonth = firstDayOfMonth.AddMonths(-i);
                List<Transaction> transactionsToRemove = new List<Transaction>();


                foreach (var item in ListTransaction)
                {
                    if (item.type == EnumTypeTransiction.Revenue)
                    {
                        transactionsToRemove.Add(item);
                    }
                    else
                    {
                        var monthAndYearOfTransaction = new DateTime(item.DateBokeed.Year, item.DateBokeed.Month, 1);


                        if (monthAndYearOfTransaction == firstDayOfMonth)
                        {

                            totalExpense += Math.Abs(item.Amount);

                            transactionsToRemove.Add(item);
                        }
                    }



                }
                foreach (var itemToRemove in transactionsToRemove)
                {
                    ListTransaction.Remove(itemToRemove);
                }


                string key = $"{firstDayOfMonth.Year}-{firstDayOfMonth.Month}";
                expensesByMonth.Add(key, totalExpense);



            }



            return expensesByMonth;
        }


        /// <summary>
        /// This function provide data to dashboard, is data in expense and revenue for month in one year.
        /// </summary>
        /// <param name="IdUser"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, object>> GetDataForMonthDashboard(string IdUser)
        {

            List<Transaction> ListTransaction = await this.transactionService.GetAllTransactionsForUser(IdUser);

            var DashboardData = new Dictionary<string, object>();
            decimal totalExpenseYear = 0;
            decimal totalRevenueYear = 0;
            decimal expenseInActualyMonth = 0;
            decimal revenueInActualyMonth = 0;
            List<Transaction> transactionsExpenses = new();

            for (int i = 0, y = 12; i < 12; i++, y++)
            {

                decimal totalExpense = 0;
                decimal totalRevenues = 0;

                var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                firstDayOfMonth = firstDayOfMonth.AddMonths(-i);
                List<Transaction> transactionsToRemove = new();


                foreach (var item in ListTransaction)
                {

                    //get data in datatime
                    var monthAndYearOfTransaction = new DateTime(item.DateBokeed.Year, item.DateBokeed.Month, 1);


                    if (monthAndYearOfTransaction == firstDayOfMonth)
                    {

                        if (item.type == EnumTypeTransiction.Expense)
                        {
                            totalExpense += Math.Abs(item.Amount);
                            totalExpenseYear += Math.Abs(item.Amount);
                            transactionsToRemove.Add(item);
                            transactionsExpenses.Add(item);

                        }
                        else
                        {


                            totalRevenues += Math.Abs(item.Amount);
                            totalRevenueYear += Math.Abs(item.Amount);
                            transactionsToRemove.Add(item);
                        }


                    }
                    if (i == 1)
                    {

                        expenseInActualyMonth = totalExpense;
                        revenueInActualyMonth = totalRevenues;
                    }


                }


                //this foreach remove the transactions in list, the objetive is optimize the search in other transactions 
                foreach (var itemToRemove in transactionsToRemove)
                {
                    ListTransaction.Remove(itemToRemove);
                }



                //To insert Data in dictionary 
                string key = $"{firstDayOfMonth.Month}-{firstDayOfMonth.Year}";
                string[] arrayObject = new string[] { key, totalExpense.ToString() };
                DashboardData.Add(i.ToString(), arrayObject);

                string[] arrayObject1 = new string[] { key, totalRevenues.ToString() };
                DashboardData.Add(y.ToString(), arrayObject1);


            }

            //Insert data in final of foreach

            Dictionary<string, decimal> categoryData = await GetCategoryDashboard(IdUser, transactionsExpenses);

            DashboardData.Add("CategoryData", categoryData);
            DashboardData.Add("ExpenseInYear", totalExpenseYear);
            DashboardData.Add("totalRevenueYear", totalRevenueYear);
            DashboardData.Add("totalExpenseInActualyMonth", expenseInActualyMonth);
            DashboardData.Add("totalRevenueInActualyMonth", revenueInActualyMonth);
            return DashboardData;
        }


        /// <summary>
        /// This function get data in one year of category, example: "Food", "1000".
        /// </summary>
        /// <param name="IdUser"></param>
        /// <param name="ListTransaction"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, decimal>> GetCategoryDashboard(string IdUser, List<Transaction>? ListTransaction)
        {

            List<Category> categoryList = await this.categoryService.GetAllCategoryForUser(IdUser);
            Dictionary<string, decimal> dictionaryCategory = new();


            //Get category to dictionary
            foreach (var category in categoryList)
            {
                dictionaryCategory.Add(category.Name, 0);
            }



            if (ListTransaction != null)
            {


                foreach (var transaction in ListTransaction)
                {
                    Guid idCategory = transaction.Category.Id;
                    Category category = categoryList.Find(x => x.Id == idCategory);

                    dictionaryCategory[category.Name] = dictionaryCategory[category.Name] + Math.Abs(transaction.Amount);


                }
            }
            return dictionaryCategory;
        }
    }
}
