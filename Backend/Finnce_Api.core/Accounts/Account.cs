namespace Finnce_Api.core.Accounts;
public class Account
{
    [Column("idAccount")]
    public Guid Id { get; set; }
    [StringLength(25)]
    public string Name { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal? Amount { get; set; }

    public DateTime? lastRefreshed { get; set; }
    public string? CurrencyCode { get; set; }
    [Required]
    [StringLength(35)]
    public string TypeAccount { get; set; }
    [StringLength(40)]
    public string? Description { get; set; }
    public bool StatusBlockedTransation { get; set; }


    public string? AccountId { get; set; }

    [StringLength(40)]
    public string? Iban { get; set; }

    [Required]
    [ForeignKey(nameof(User))]
    public string IdUser { get; set; }
    public User User { get; set; }



}

