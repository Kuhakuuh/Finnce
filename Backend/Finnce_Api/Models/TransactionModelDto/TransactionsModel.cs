
namespace Finnce_Api.Models.TransactionModelDto
{
    public class TransactionsModel
    {

        public Guid? Id { get; set; }
        public String Type { get; set; }
        public string DescriptionDisplay { get; set; }
        public decimal Amount { get; set; }
        public EnumCurrencyCode CurrencyCode { get; set; }
        public DateTime DateBokeed { get; set; }
        public Guid? IdCategory { get; set; }
        public Guid? IdAccount { get; set; }
        public string IdUser { get; set; }


    }




    public class TransactionsModelNull
    {

        public Guid Id { get; set; }
        public EnumTypeTransiction? Type { get; set; }
        public string? DescriptionDisplay { get; set; }
        public decimal? Amount { get; set; }
        public EnumCurrencyCode? CurrencyCode { get; set; }
        public DateTime? DateBokeed { get; set; }
        public Guid? IdCategory { get; set; }
        public Guid? IdAccount { get; set; }



    }
}
