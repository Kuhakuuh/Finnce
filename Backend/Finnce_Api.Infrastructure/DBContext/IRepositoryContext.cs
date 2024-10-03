
namespace Finnce_Api.Infrastructure.DBContext
{
    public interface IRepositoryContext
    {
        DbSet<Account>? Accounts { get; set; }
        DbSet<Category>? Categories { get; set; }
        DbSet<EntityCore>? Entities { get; set; }
        DbSet<Notification>? Notifications { get; set; }
        DbSet<TinkAcessApi>? ThinkAcessApis { get; set; }
        DbSet<Transaction>? Transactions { get; set; }
        DbSet<User>? Users { get; set; }
    }
}