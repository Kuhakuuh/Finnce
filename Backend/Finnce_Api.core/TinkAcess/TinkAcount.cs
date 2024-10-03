namespace Finnce_Api.core.TinkAcess
{
    using System;





    public class Accounts
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? Type { get; set; }
        public balances? Balances { get; set; }
        public identifiers? Identifiers { get; set; }

        public string? financialInstitutionId { get; set; }
        public string? customerSegment { get; set; }

        public dates? Dates { get; set; }

        public class
            balances
        {
            public booked booked { get; set; }
            public available available { get; set; }
        }

        public class available
        {
            public amount amount { get; set; }
        }
        public class booked
        {
            public amount amount { get; set; }

        }
        public class amount
        {
            public value value { get; set; }
            public string currencyCode { get; set; }
        }

        public class value
        {
            public int unscaledValue { get; set; }
            public int scale { get; set; }
        }

        public class identifiers
        {
            public Iban iban { get; set; }
            public financialInstitution financialInstitution { get; set; }
        }
        public class dates
        {
            public DateTime lastRefreshed { get; set; }
        }
        public class Iban
        {
            public string iban { get; set; }
            public string bban { get; set; }
        }

        public class financialInstitution
        {
            public string accountNumber { get; set; }
            //public Dictionary<string, string> ReferenceNumbers { get; set; }
        }


    }
}