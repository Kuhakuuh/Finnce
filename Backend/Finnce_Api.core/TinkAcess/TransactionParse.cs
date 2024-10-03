
namespace Finnce_Api.core.TinkAcess
{

    public class TransactionParse
    {
        public Guid Id { get; set; }

        public string AccountId { get; set; }
        public Amount Amount { get; set; }
        public Descriptions? Descriptions { get; set; }
        public Dates Dates { get; set; }
        public identifiers Identifiers { get; set; }

        public Categories categories { get; set; }

    }

    public class Categories
    {
        public Pfm? pfm { get; set; }

    }

    public class Pfm
    {
        public string? id { get; set; }
        public string? name { get; set; }
    }
    public class Amount
    {
        public Value Value { get; set; }
        public EnumCurrencyCode CurrencyCode { get; set; }
    }
    public class Value
    {
        public long UnscaledValue { get; set; }
        public int Scale { get; set; }

    }
    public class Descriptions
    {
        public string? Original { get; set; }
        public string? Display { get; set; }
    }

    public class Dates
    {
        public DateTime Booked { get; set; }
    }


    public class identifiers
    {
        public string? ProviderTransactionId { get; set; }

    }




}
