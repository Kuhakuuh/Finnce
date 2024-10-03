

using Finnce_Api.core.Notifications;
using Microsoft.AspNetCore.Identity;

namespace Finnce_Api.core.Users;

//[NotMapped]
public class User : IdentityUser
{
    //[Column("IdUser")]
    //public Guid Id { get; set; }

    [Required]
    [StringLength(25)]
    public string Name { get; set; }
    public DateTime? BirthDate { get; set; }
    [Required]
    [StringLength(100)]
    public string Email { get; set; }

    [StringLength(5000)]

    public string? TinkToken { get; set; }
    [StringLength(100)]
    [NotMapped]
    public string? Discriminator { get; set; }
    public ICollection<Account>? ListAccounts { get; set; }
    public ICollection<Category>? ListCategories { get; set; }
    public ICollection<EntityCore>? ListEntities { get; set; }
    public ICollection<Transaction>? ListTransactions { get; set; }
    public ICollection<Notification>? ListNotifications { get; set; }

}

