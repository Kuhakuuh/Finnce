

namespace Finnce_Api.core.Transactions

{
    public class Transaction
    {
        [Column("IdTransactions")]
        public Guid Id { get; set; }
        public EnumTypeTransiction type { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        public EnumCurrencyCode CurrencyCode { get; set; }
        [StringLength(40)]
        public string DescriptionDisplay { get; set; }
        public DateTime DateBokeed { get; set; }

        [StringLength(40)]
        public string providerTransactionId { get; set; }





        [ForeignKey(nameof(EntityCore))]
        public Guid? IdEntity { get; set; }
        public EntityCore? EntityCore { get; set; }

        [Required]
        [ForeignKey(nameof(Category))]
        public Guid? IdCategory { get; set; }
        public Category? Category { get; set; }


        [Required]
        [ForeignKey(nameof(Account))]
        public Guid? IdAccount { get; set; }
        public Account? Account { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public string IdUser { get; set; }
        public User User { get; set; }



    }
}
